using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    NodeManager nodeManager;

    public enum GameState { 
        Harmonizing,
        Rhythm,
        Battle
    }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        UpdateGameState(GameState.Harmonizing);

        nodeManager = FindObjectOfType<NodeManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        Debug.Log(State);
        switch (State)
        {
            case GameState.Harmonizing:
                break;
            case GameState.Rhythm:
                nodeManager.StartSpawning();
                break;
            case GameState.Battle: 
                break;
        }
    }


}
