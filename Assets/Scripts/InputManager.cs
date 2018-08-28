using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public Player Player1;
    public Player Player2;

    private Player m_CurrentPlayer;
    private int m_SelectedWorker = 0;
    private int m_WorkerCounter = 1;

    // Use this for initialization
    void Start()
    {
        TurnManager.instance.OnTurnStart += UpdateCurrentPlayer;
        MapManager.instance.MapGrid.OnMapTileButtonPressed += MapGrid_OnMapTileButtonPressed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleInputChange(string input)
    {
        string[] commands = input.Split(' ');
        Debug.Log(m_CurrentPlayer.PlayerId + ": " + commands[0]);

        if (commands[0].ToLower() == "select")
        {
            int workerId = Int32.Parse(commands[1]);

            m_SelectedWorker = workerId;
            m_CurrentPlayer.SelectWorker(workerId);
        }
        else if (commands[0].ToLower() == "deselect")
        {
            m_SelectedWorker = 0;
            m_CurrentPlayer.DeselectWorker();
        }
        else if (commands[0].ToLower() == "place")
        {
            int workerId = Int32.Parse(commands[1]);
            Coordinate coordinate = new Coordinate(Int32.Parse(commands[2]), Int32.Parse(commands[3]));

            if (m_CurrentPlayer.CanPlace(workerId, coordinate))
            {
                m_CurrentPlayer.PlaceBuilder(workerId, coordinate);
            }
            else
            {
                Debug.LogError("cannot place worker at: " + coordinate);
            }
        }
        else
        {
            Coordinate coordinate = new Coordinate(Int32.Parse(commands[1]), Int32.Parse(commands[2]));

            if (commands[0].ToLower() == "move")
            {
                if (m_CurrentPlayer.CanMove(m_SelectedWorker, coordinate))
                {
                    m_CurrentPlayer.MoveBuilder(coordinate);

                }
                else
                {
                    Debug.LogError("cannot move builder to: " + coordinate);
                }
            }
            else if (commands[0].ToLower() == "build")
            {
                if (m_CurrentPlayer.CanBuild(m_SelectedWorker, coordinate))
                {
                    m_CurrentPlayer.PlaceBuilding(coordinate);
                }
                else
                {
                    Debug.LogError("cannot build at: " + coordinate);
                }
            }
        }
    }

    public void UpdateCurrentPlayer(object sender, TurnManager.TurnEventArgs args)
    {
        if (args.player == 1)
        {
            m_CurrentPlayer = Player1;
            Debug.Log("Current Player: 1");
        }
        else if (args.player == 2)
        {
            m_CurrentPlayer = Player2;
            Debug.Log("Current Player: 2");
        }
        else
        {
            Debug.LogError("player doesn't exist! " + args.player);
        }

        m_SelectedWorker = 0;
    }

    private void MapGrid_OnMapTileButtonPressed(object sender, MapGrid.MapGridEventArgs e)
    {
        switch (m_CurrentPlayer.CurrentPhase)
        {
            case Phase.WAITING:

                // Place worker
                if (m_CurrentPlayer.CanPlace(m_CurrentPlayer.WorkersPlaced + 1, e.coordinate))
                {
                    m_CurrentPlayer.PlaceBuilder(m_CurrentPlayer.WorkersPlaced + 1, e.coordinate);
                }
                else
                {
                    Debug.LogError("cannot place worker at: " + e.coordinate);
                }

                break;

            case Phase.SELECT:

                // Have to figure out how to select the worker using pointer collision/trigger

                break;

            case Phase.BUILD:

                if (m_CurrentPlayer.CanBuild(m_SelectedWorker, e.coordinate))
                {
                    m_CurrentPlayer.PlaceBuilding(e.coordinate);
                }
                else
                {
                    Debug.LogError("cannot build at: " + e.coordinate);
                }

                break;

            case Phase.MOVE:

                if (m_CurrentPlayer.CanMove(m_SelectedWorker, e.coordinate))
                {
                    m_CurrentPlayer.MoveBuilder(e.coordinate);
                }
                else
                {
                    Debug.LogError("cannot move builder to: " + e.coordinate);
                }

                break;

            default:

                break;
        }
    }
}