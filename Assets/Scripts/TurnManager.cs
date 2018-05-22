using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance = null;
    private Queue<int> playerQueue;
    public int currentPlayer;
    public int turnNumber;
    public event EventHandler<TurnEventArgs> OnTurnStart;
    public event EventHandler<TurnEventArgs> OnTurnEnd;

    public class TurnEventArgs : EventArgs
    {
        public TurnEventArgs(int turnNumber, int player)
        {
            this.turnNumber = turnNumber;
            this.player = player;
        }
        public int player { get; set; }
        public int turnNumber { get; set; }
    }
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
        playerQueue = new Queue<int>();

        playerQueue.Enqueue(1);
        playerQueue.Enqueue(2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartTurns()
    {
        turnNumber = 0;
        currentPlayer = playerQueue.Dequeue();
        OnTurnStart(this, new TurnEventArgs(0, currentPlayer));
    }

    public void EndTurn()
    {
        Debug.Log(currentPlayer + ": Ending turn");

        // OnTurnEnd(this, new TurnEventArgs(turnNumber, currentPlayer));

        MoveToNextPlayer(currentPlayer);

        if (playerQueue.Count == 0)
        {
            turnNumber++;
            playerQueue.Enqueue(1);
            playerQueue.Enqueue(2);
        }

        OnTurnStart(this, new TurnEventArgs(turnNumber, currentPlayer));
    }

    public void MoveToNextPlayer(int lastPlayer)
    {
        if (playerQueue.Count > 0)
        {
            currentPlayer = playerQueue.Dequeue();

        }
    }
}
