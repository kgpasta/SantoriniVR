using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public MapGrid MapGrid = null;

    public static MapManager instance = null;

    private Dictionary<Coordinate, Worker> workerMap = new Dictionary<Coordinate, Worker>();
    private Dictionary<Coordinate, Building?> buildingMap = new Dictionary<Coordinate, Building?>();

    private const int MAP_DIMENSION = 5;

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
        for (int i = 0; i < MAP_DIMENSION; i++)
        {
            for (int j = 0; j < MAP_DIMENSION; j++)
            {
                buildingMap.Add(new Coordinate(i, j), Building.NONE);
                MapGrid.AddMapTile(new Coordinate(i, j));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Boolean isEmpty(Coordinate coordinate)
    {
        return !workerMap.ContainsKey(coordinate);
    }

    public void MoveWorker(Worker worker, Coordinate coordinate)
    {
        workerMap.Remove(coordinate);

        workerMap[coordinate] = worker;
    }

    public void PlaceBuilding(Coordinate coordinate)
    {
        if (buildingMap[coordinate] == Building.NONE)
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
