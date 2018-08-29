using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public MapGrid MapGrid = null;

    public static MapManager instance = null;

    private Dictionary<Coordinate, Worker> m_WorkerMap = new Dictionary<Coordinate, Worker>();
    private Dictionary<Coordinate, Building?> m_BuildingMap = new Dictionary<Coordinate, Building?>();

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
                m_BuildingMap.Add(new Coordinate(i, j), Building.NONE);
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
        return !m_WorkerMap.ContainsKey(coordinate);
    }

    public int GetWorker(Coordinate coordinate)
    {
        if (m_WorkerMap.ContainsKey(coordinate))
        {
            return m_WorkerMap[coordinate].WorkerId;
        }

        return 0;
    }

    public void MoveWorker(Worker worker, Coordinate coordinate)
    {
        m_WorkerMap.Remove(worker.CurrentCoordinate);

        m_WorkerMap[coordinate] = worker;
    }

    public void PlaceBuilding(Coordinate coordinate)
    {
        if (m_BuildingMap[coordinate] == Building.NONE)
        {
            m_BuildingMap[coordinate] = Building.ONE;
            MapGrid.MapTiles[coordinate].CurrentBuilding = Building.ONE;
        }
        else
        {
            Building nextBuilding = BuildingUtils.NextBuilding(m_BuildingMap[coordinate]);
            m_BuildingMap[coordinate] = nextBuilding;
            MapGrid.MapTiles[coordinate].CurrentBuilding = nextBuilding;
        }
    }

    public Boolean CanBuild(Coordinate coordinate)
    {
        return m_BuildingMap[coordinate] != Building.ROOF;
    }

    public Boolean CanSelect(int playerId, Coordinate coordinate)
    {
        return !isEmpty(coordinate) && m_WorkerMap[coordinate].PlayerId == playerId;
    }
}
