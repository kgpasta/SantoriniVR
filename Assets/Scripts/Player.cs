using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int playerId = 1;
    public List<Phase> phases = new List<Phase>();
    public Phase currentPhase = Phase.WAITING;
    private int phaseIndex = 0;
    private Dictionary<int, Worker> workers = new Dictionary<int, Worker>();

    // Use this for initialization
    void Start()
    {
        TurnManager.instance.OnTurnStart += InitializeTurn;

        phases.Add(Phase.MOVE);
        phases.Add(Phase.BUILD);

        workers.Add(1, new Worker());
        workers.Add(2, new Worker());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitializeTurn(object sender, TurnManager.TurnEventArgs args)
    {
        if (args.player == playerId)
        {
            currentPhase = phases[phaseIndex];
        }
    }

    public void MoveBuilder(int workerId, Coordinate coordinate)
    {
        if (CanMove(coordinate))
        {
            Worker worker = workers[workerId];
            MapManager.instance.MoveWorker(worker, coordinate);
            worker.currentCoordinate = coordinate;

            IncrementPhase();
        }
    }

    public void PlaceBuilding(int workerId, Coordinate coordinate)
    {
        if (CanBuild(coordinate))
        {
            MapManager.instance.PlaceBuilding(coordinate);

            IncrementPhase();

            TurnManager.instance.EndTurn();
        }
    }

    public bool CanMove(Coordinate coordinate)
    {
        Debug.Log(MapManager.instance.isEmpty(coordinate));
        Debug.Log(currentPhase.Equals(Phase.MOVE));
        return MapManager.instance.isEmpty(coordinate) && currentPhase.Equals(Phase.MOVE);
    }

    public bool CanBuild(Coordinate coordinate)
    {
        return MapManager.instance.CanBuild(coordinate) && currentPhase.Equals(Phase.BUILD);
    }

    private void IncrementPhase()
    {
        phaseIndex++;
        if (phases.Count <= phaseIndex)
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
