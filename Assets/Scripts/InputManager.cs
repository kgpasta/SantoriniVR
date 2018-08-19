using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Player player1;
    public Player player2;

    private Player currentPlayer;

    // Use this for initialization
    void Start()
    {
        TurnManager.instance.OnTurnStart += UpdateCurrentPlayer;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleInputChange(string input)
    {
        string[] commands = input.Split(' ');
        Debug.Log(currentPlayer.playerId + ": " + commands[0]);

        int workerId = Int32.Parse(commands[1]);

        Coordinate coordinate = new Coordinate(Int32.Parse(commands[2]), Int32.Parse(commands[3]));

        if (commands[0].ToLower() == "move")
        {
            if (currentPlayer.CanMove(workerId, coordinate))
            {
                currentPlayer.MoveBuilder(workerId, coordinate);

            }
            else
            {
                Debug.LogError("cannot move builder to: " + coordinate);
            }
        }
        else if (commands[0].ToLower() == "build")
        {
            if (currentPlayer.CanBuild(workerId, coordinate))
            {
                currentPlayer.PlaceBuilding(workerId, coordinate);
            }
            else
            {
                Debug.LogError("cannot build at: " + coordinate);
            }
        }
        else if (commands[0].ToLower() == "place")
        {
            if (currentPlayer.CanPlace(workerId, coordinate))
            {
                currentPlayer.PlaceBuilder(workerId, coordinate);
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
            currentPlayer = player1;
        }
        else if (args.player == 2)
        {
            currentPlayer = player2;
        }
        else
        {
            Debug.LogError("player doesn't exist! " + args.player);
        }
    }
}