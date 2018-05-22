using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int playerId = 1;
    public List<Phase> phases = new List<Phase>();
    private Phase currentPhase = new WaitingPhase();
    private int phaseIndex = 0;
    private Dictionary<int, Worker> workers = new Dictionary<int, Worker>();

    // Use this for initialization
    void Start()
    {
        TurnManager.instance.OnTurnStart += InitializeTurn;

        phases.Add(new MovePhase());
        phases.Add(new BuildPhase());

        workers.Add(1, new Worker());
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
        if (MapManager.instance.isEmpty(coordinate))
        {
            Worker worker = workers[workerId];
            MapManager.instance.MoveWorker(worker, coordinate);
            worker.currentCoordinate = coordinate;

            phaseIndex++;
            currentPhase = phases[phaseIndex];
        }
    }

    public void PlaceBuilding(int workerId, Coordinate coordinate)
    {
        if (MapManager.instance.CanBuild(coordinate))
        {
            MapManager.instance.PlaceBuilding(coordinate);

            phaseIndex = 0;
            currentPhase = new WaitingPhase();

            TurnManager.instance.EndTurn();
        }
    }


}
