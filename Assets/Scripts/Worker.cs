using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker
{
    public int WorkerId { get; set; }
    public int PlayerId { get; set; }

    private Coordinate m_CurrentCoordinate;
    public Coordinate CurrentCoordinate
    {
        get { return m_CurrentCoordinate; }
        set
        {
            m_CurrentCoordinate = value;
            m_WorkerModel.transform.position = new Vector3(value.x - 2, 0.25f, value.y - 2);
            if (!m_WorkerModel.activeSelf)
            {
                m_WorkerModel.SetActive(true);
            }
        }
    }

    private bool m_IsSelected;
    public bool IsSelected
    {
        get { return m_IsSelected; }
        set
        {
            m_IsSelected = value;
            ToggleOutlineShader(m_IsSelected);
        }
    }

    private GameObject m_WorkerModel;
    public GameObject WorkerModel
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

    public Worker(GameObject modelPrefab, int playerId, Transform parentTransform, int workerId)
    {
        this.PlayerId = playerId;
        m_WorkerModel = GameObject.Instantiate(modelPrefab);
        m_WorkerModel.transform.SetParent(parentTransform);
        m_WorkerModel.name = "Worker" + workerId.ToString();

        m_WorkerModel.GetComponent<Renderer>().material.color = playerIdToColor[playerId];
    }

    private void ToggleOutlineShader(bool toggle)
    {
        // show outline/border if selected
        if (toggle)
        {
            m_WorkerModel.GetComponent<Renderer>().material.shader = Shader.Find("Custom/Outline");
        }
        else
        {
            m_WorkerModel.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
        }
    }
}