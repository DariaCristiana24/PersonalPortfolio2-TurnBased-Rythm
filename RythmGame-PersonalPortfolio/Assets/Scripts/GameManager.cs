using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;

    public enum GameState { 
        Harmonizing,
        Rhythm,
        PlayerAttack,
        EnemyAttack
    }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        UpdateGameState(GameState.Harmonizing);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
    }


}
