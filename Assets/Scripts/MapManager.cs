using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance = null;

    private Dictionary<Coordinate, Worker> workerMap = new Dictionary<Coordinate, Worker>();
    private Dictionary<Coordinate, Building?> buildingMap = new Dictionary<Coordinate, Building?>();

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance.
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                workerMap.Add(new Coordinate(x, y), null);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Boolean isEmpty(Coordinate coordinate)
    {
        return workerMap.ContainsKey(coordinate) && workerMap[coordinate] == null;
    }

    public void MoveWorker(Worker worker, Coordinate coordinate)
    {
        workerMap[worker.currentCoordinate] = null;

        workerMap[coordinate] = worker;
    }

    public void PlaceBuilding(Coordinate coordinate)
    {
        if (buildingMap[coordinate] == null)
        {
            buildingMap[coordinate] = Building.ONE;
        }
        else
        {
            buildingMap[coordinate] = BuildingUtils.NextBuilding(buildingMap[coordinate]);
        }
    }

    public Boolean CanBuild(Coordinate coordinate)
    {
        return buildingMap[coordinate] != Building.ROOF;
    }

}
