using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject playerObject;
    private Player player;

    // Use this for initialization
    void Start()
    {
        player = playerObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleInputChange(string input)
    {
        string[] commands = input.Split(' ');
        Debug.Log("executing: " + commands[0]);

        int workerId = Int32.Parse(commands[1]);
        Debug.Log("workerId: " + workerId);

        Coordinate coordinate = new Coordinate(Int32.Parse(commands[2]), Int32.Parse(commands[3]));
        Debug.Log("coordinate: " + coordinate.x + ", " + coordinate.y);

        if (commands[0].ToLower() == "move")
        {
            player.MoveBuilder(workerId, coordinate);
        }
        else if (commands[1].ToLower() == "build")
        {
            player.PlaceBuilding(workerId, coordinate);
        }
    }
}