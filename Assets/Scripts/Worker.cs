using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker
{
    public int workerId { get; set; }

    private Coordinate m_CurrentCoordinate;
    public Coordinate currentCoordinate
    {
        get { return m_CurrentCoordinate; }
        set {
            m_CurrentCoordinate = value;
            m_WorkerModel.transform.position = new Vector3(value.x - 3, 0.25f, -value.y + 3);
            if (!m_WorkerModel.activeSelf)
            {
                m_WorkerModel.SetActive(true);
            }
        }
    }

    private GameObject m_WorkerModel;
    public GameObject workerModel
    {
        get { return m_WorkerModel; }
    }

    // Get worker color based on player Id
    private Dictionary<int, Color> playerIdToColor = new Dictionary<int, Color>()
    {
        {1, Color.red },
        {2, Color.blue },
        {3, Color.green }
    };

    public Worker() { }

    public Worker(GameObject modelPrefab, int playerId, Transform parentTransform)
    {
        m_WorkerModel = GameObject.Instantiate(modelPrefab);
        m_WorkerModel.transform.SetParent(parentTransform);
        m_WorkerModel.name = "Worker";

        m_WorkerModel.GetComponent<Renderer>().material.color = playerIdToColor[playerId];
    }
}