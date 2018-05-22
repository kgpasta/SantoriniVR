using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject player2Object;
    private Player player;
    private Player player2;
    private Player current;

    // Use this for initialization
    void Start()
    {
        player = playerObject.GetComponent<Player>();
        player2 = player2Object.GetComponent<Player>();

        TurnManager.instance.OnTurnStart += UpdateCurrentPlayer;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleInputChange(string input)
    {
        string[] commands = input.Split(' ');
        Debug.Log(current.playerId + ": " + commands[0]);

        int workerId = Int32.Parse(commands[1]);

        Coordinate coordinate = new Coordinate(Int32.Parse(commands[2]), Int32.Parse(commands[3]));

        if (commands[0].ToLower() == "move")
        {
            if (current.CanMove(workerId, coordinate))
            {
                current.MoveBuilder(workerId, coordinate);

            }
            else
            {
                Debug.LogError("cannot move builder to: " + coordinate);
            }
        }
        else if (commands[0].ToLower() == "build")
        {
            if (current.CanBuild(workerId, coordinate))
            {
                current.PlaceBuilding(workerId, coordinate);
            }
            else
            {
                Debug.LogError("cannot build at: " + coordinate);
            }
        }
        else if (commands[0].ToLower() == "place")
        {
            if (current.CanPlace(workerId, coordinate))
            {
                current.PlaceBuilder(workerId, coordinate);
            }
            else
            {
                Debug.LogError("cannot place worker at: " + coordinate);
            }
        }
    }

    public void UpdateCurrentPlayer(object sender, TurnManager.TurnEventArgs args)
    {
        if (args.player == 1)
        {
            current = player;
        }
        else if (args.player == 2)
        {
            current = player2;
        }
        else
        {
            Debug.LogError("player doesn't exist! " + args.player);
        }
    }
}