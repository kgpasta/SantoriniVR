using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Properties")]
    public int playerId = 1;
    public List<Phase> phases = new List<Phase>();
    public Phase currentPhase = Phase.WAITING;
    public int phaseIndex = 0;

    [Header("Model Prefab References")]
    public GameObject WorkerModelPrefabReference = null;

    private Dictionary<int, Worker> workers = new Dictionary<int, Worker>();
    private int workersPlaced = 0;

    // Use this for initialization
    void Start()
    {
        TurnManager.instance.OnTurnStart += InitializeTurn;

        phases.Add(Phase.MOVE);
        phases.Add(Phase.BUILD);

        workers.Add(1, new Worker(WorkerModelPrefabReference, playerId, transform));
        workers.Add(2, new Worker(WorkerModelPrefabReference, playerId, transform));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitializeTurn(object sender, TurnManager.TurnEventArgs args)
    {
        if (args.player == playerId && args.turnNumber != 0)
        {
            currentPhase = phases[phaseIndex];
        }
    }

    public void PlaceBuilder(int workerId, Coordinate coordinate)
    {

        if (CanPlace(workerId, coordinate))
        {
            Worker worker = workers[workerId];

            MapManager.instance.MoveWorker(worker, coordinate);
            worker.currentCoordinate = coordinate;

            workersPlaced++;

            if (workersPlaced == workers.Count)
            {
                TurnManager.instance.EndTurn();
            }

        }
    }

    public void MoveBuilder(int workerId, Coordinate coordinate)
    {
        if (CanMove(workerId, coordinate))
        {
            Worker worker = workers[workerId];

            MapManager.instance.MoveWorker(worker, coordinate);
            worker.currentCoordinate = coordinate;

            IncrementPhase();
        }
    }

    public void PlaceBuilding(int workerId, Coordinate coordinate)
    {
        if (CanBuild(workerId, coordinate))
        {
            MapManager.instance.PlaceBuilding(coordinate);

            IncrementPhase();

            TurnManager.instance.EndTurn();
        }
    }

    public bool CanPlace(int workerId, Coordinate coordinate)
    {
        Worker worker = workers[workerId];
        return MapManager.instance.isEmpty(coordinate) && worker.currentCoordinate == null;
    }

    public bool CanMove(int workerId, Coordinate coordinate)
    {
        Worker worker = workers[workerId];
        return worker.currentCoordinate.IsAdjacent(coordinate) && MapManager.instance.isEmpty(coordinate) && currentPhase.Equals(Phase.MOVE);
    }

    public bool CanBuild(int workerId, Coordinate coordinate)
    {
        Worker worker = workers[workerId];
        return worker.currentCoordinate.IsAdjacent(coordinate) && MapManager.instance.CanBuild(coordinate) && currentPhase.Equals(Phase.BUILD);
    }

    private void IncrementPhase()
    {
        phaseIndex++;
        if (phases.Count > phaseIndex)
        {
            currentPhase = phases[phaseIndex];
        }
        else
        {
            phaseIndex = 0;
            currentPhase = Phase.WAITING;
        }

    }


}
