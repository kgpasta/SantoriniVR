using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Properties")]
    public int PlayerId = 1;
    public List<Phase> Phases = new List<Phase>();
    public Phase CurrentPhase = Phase.WAITING;
    public int PhaseIndex = 0;

    [Header("Model Prefab References")]
    public GameObject WorkerModelPrefabReference = null;

    public int WorkersPlaced { get { return m_WorkersPlaced; } }
    public int CurrentlySelectedWorkerId { get { return m_CurrentSelectedWorkerId; } }

    private Dictionary<int, Worker> m_Workers = new Dictionary<int, Worker>();
    private int m_CurrentSelectedWorkerId = 0;
    private int m_WorkersPlaced = 0;

    // Use this for initialization
    void Start()
    {
        TurnManager.instance.OnTurnStart += InitializeTurn;

        Phases.Add(Phase.SELECT);
        Phases.Add(Phase.MOVE);
        Phases.Add(Phase.BUILD);

        m_Workers.Add(1, new Worker(WorkerModelPrefabReference, PlayerId, transform, 1));
        m_Workers.Add(2, new Worker(WorkerModelPrefabReference, PlayerId, transform, 2));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitializeTurn(object sender, TurnManager.TurnEventArgs args)
    {
        if (args.player == PlayerId && args.turnNumber != 0)
        {
            CurrentPhase = Phases[PhaseIndex];
        }
    }

    public void PlaceBuilder(int workerId, Coordinate coordinate)
    {
        if (CanPlace(workerId, coordinate))
        {
            Worker worker = m_Workers[workerId];

            MapManager.instance.MoveWorker(worker, coordinate, true);
            worker.CurrentCoordinate = coordinate;

            m_WorkersPlaced++;

            if (m_WorkersPlaced == m_Workers.Count)
            {
                TurnManager.instance.EndTurn();
            }
        }
    }

    public void SelectWorker(int workerId)
    {
        if (CanSelect())
        {
            m_CurrentSelectedWorkerId = workerId;
            m_Workers[m_CurrentSelectedWorkerId].IsSelected = true;
            IncrementPhase();
        }
    }

    public void DeselectWorker()
    {
        if (CanDeselect())
        {
            m_CurrentSelectedWorkerId = 0;
            m_Workers[m_CurrentSelectedWorkerId].IsSelected = false;
            DecrementPhase();
        }
    }

    public void MoveBuilder(Coordinate coordinate)
    {
        if (CanMove(m_CurrentSelectedWorkerId, coordinate))
        {
            Worker worker = m_Workers[m_CurrentSelectedWorkerId];

            MapManager.instance.MoveWorker(worker, coordinate);
            worker.CurrentCoordinate = coordinate;

            IncrementPhase();
        }
    }

    public void PlaceBuilding(Coordinate coordinate)
    {
        if (CanBuild(m_CurrentSelectedWorkerId, coordinate))
        {
            MapManager.instance.PlaceBuilding(coordinate);
            m_Workers[m_CurrentSelectedWorkerId].IsSelected = false;
            IncrementPhase();

            TurnManager.instance.EndTurn();
        }
    }

    public bool CanPlace(int workerId, Coordinate coordinate)
    {
        Worker worker = m_Workers[workerId];
        return MapManager.instance.isEmpty(coordinate) && worker.CurrentCoordinate == null;
    }

    public bool CanSelect()
    {
        return CurrentPhase.Equals(Phase.SELECT);
    }

    public bool CanDeselect()
    {
        return CurrentPhase.Equals(Phase.MOVE);
    }

    public bool CanMove(int workerId, Coordinate coordinate)
    {
        if (!m_Workers.ContainsKey(workerId))
        {
            return false;
        }
        Worker worker = m_Workers[workerId];
        return worker.CurrentCoordinate.IsAdjacent(coordinate) && MapManager.instance.isEmpty(coordinate) && CurrentPhase.Equals(Phase.MOVE);
    }

    public bool CanBuild(int workerId, Coordinate coordinate)
    {
        if (!m_Workers.ContainsKey(workerId))
        {
            return false;
        }
        Worker worker = m_Workers[workerId];
        return worker.CurrentCoordinate.IsAdjacent(coordinate) && MapManager.instance.CanBuild(coordinate) && CurrentPhase.Equals(Phase.BUILD);
    }

    private void IncrementPhase()
    {
        PhaseIndex++;
        if (Phases.Count > PhaseIndex)
        {
            CurrentPhase = Phases[PhaseIndex];
        }
        else
        {
            PhaseIndex = 0;
            CurrentPhase = Phase.WAITING;
        }

    }

    private void DecrementPhase()
    {
        PhaseIndex--;
        if (PhaseIndex >= 0)
        {
            CurrentPhase = Phases[PhaseIndex];
        }
        else
        {
            PhaseIndex = 1;
            CurrentPhase = Phase.SELECT;
        }

    }

}
