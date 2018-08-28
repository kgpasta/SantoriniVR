using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileButton : MonoBehaviour
{
    public Coordinate Coordinate { get; set; }

    private Building m_CurrentBuilding;
    public Building CurrentBuilding
    {
        get { return m_CurrentBuilding; }
        set
        {
            m_CurrentBuilding = value;
            UpdateMapTileBuilding();
        }
    }

    private Dictionary<Building, float> buildingToYPosition = new Dictionary<Building, float>()
    {
        {Building.ONE, 0.15f },
        {Building.TWO, 0.45f },
        {Building.THREE, 0.75f },
        {Building.ROOF, 0.9f }
    };

    public MapTileButton() { }

    private void UpdateMapTileBuilding()
    {
        switch (m_CurrentBuilding)
        {
            case Building.NONE:

                break;

            case Building.ONE:
            case Building.TWO:
            case Building.THREE:

                CreateBuildingCube();
                break;

            case Building.ROOF:

                CreateBuildingDome();
                break;
        }
    }

    private void CreateBuildingCube()
    {
        GameObject buildingCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        buildingCube.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        buildingCube.transform.SetParent(transform);
        buildingCube.transform.localPosition = new Vector3(0f, 0f, buildingToYPosition[m_CurrentBuilding]);
    }

    private void CreateBuildingDome()
    {
        GameObject buildingDome = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        buildingDome.transform.SetParent(transform);
        buildingDome.transform.localScale = new Vector3(2.8f, 0.3f, 2.8f);
        buildingDome.transform.localPosition = new Vector3(0f, 0f, buildingToYPosition[m_CurrentBuilding]);
        buildingDome.GetComponent<Renderer>().material.color = Color.blue;
    }

}
