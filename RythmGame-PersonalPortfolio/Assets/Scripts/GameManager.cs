using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    NodeManager nodeManager;
    HarmonizingManager harmonizingManager;
    AttackingPhaseManager attackingPhaseManager;

    public enum GameState { 
        Harmonizing,
        Rhythm,
        Battle,
        GameFinished
    }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
       // UpdateGameState(GameState.Harmonizing);

        nodeManager = FindObjectOfType<NodeManager>();
        harmonizingManager = FindObjectOfType<HarmonizingManager>();
        attackingPhaseManager = FindObjectOfType<AttackingPhaseManager>();

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateGameState(GameState newState)
    {
        if (State != newState)
        {
            State = newState;
            Debug.Log(State);
            switch (State)
            {
                case GameState.Harmonizing:
                    nodeManager.instanceBig.setParameterByName("Mode", 0);
                    UIManager.Instance.EnableHarmonies();
                    harmonizingManager.ResetHarmonizing();
                    break;
                case GameState.Rhythm:
                    nodeManager.instanceBig.setParameterByName("Mode", 1);
                    nodeManager.StartSpawning();
                    break;
                case GameState.Battle:
                    nodeManager.instanceBig.setParameterByName("Mode", 2);
                    StartCoroutine(attackingPhaseManager.StartAttacking());
                    break;
                case GameState.GameFinished:
                    UIManager.Instance.EnableFinishScreen(attackingPhaseManager.GetcharactersWon());
                    break;
            }
        }
    }


}
