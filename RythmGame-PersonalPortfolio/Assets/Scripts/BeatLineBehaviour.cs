using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatLineBehaviour : MonoBehaviour
{

    NodeManager nodeManager;

    public enum NodeState { Missed, Perfect, Late, Early };

    NodeState nodeState;
    float scoreRhythm = 0;

    [SerializeField]
    float scorePerfect = 100;
    [SerializeField]
    float scoreEarly = 10;
    [SerializeField]
    float scoreLate = 10;
    [SerializeField]
    float scoreMissed = -10;
    //divide 1000 for multiplier
    void Start()
    {
        nodeManager = FindObjectOfType<NodeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nodeManager.GetFirstNode()) {
            int nodePos = nodeManager.GetNodePos();
            switch (nodePos)
            {
                case 0:
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        CheckFirstNode();
                    }
                    break;
                case 1:
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        CheckFirstNode();
                    }
                    break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        CheckFirstNode();
                    }
                    break;
                case 3:
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        CheckFirstNode();
                    }
                    break;

            }

        }
    }

    public void CheckFirstNode()
    {
        nodeState = nodeManager.GetFirstNode().GetComponent<NodeBehaviour>().nodeState;
        UIManager.Instance.setRhythmFeedBack(nodeState.ToString());
        calculateScore(nodeState);
        StartCoroutine(nodeManager.RemoveFirstNode());
    }

    void calculateScore(NodeState state)
    {
        float valueToAdd = 0;
        switch (state)
        {
            case NodeState.Missed:
                if (scoreRhythm >= 10)
                {
                    valueToAdd = scoreMissed;
                }
                break;
            case NodeState.Late:
                valueToAdd = scoreLate;
                break;
            case NodeState.Early:
                valueToAdd = scoreEarly;
                break;
            case NodeState.Perfect:
                valueToAdd = scorePerfect;
                break;
        }

        scoreRhythm += valueToAdd;

        UIManager.Instance.setRhythmScore(scoreRhythm);
    }

    public float GetRhythmScore()
    {
        return scoreRhythm;
    }

    public void SetRhythmScore(float score)
    {
         scoreRhythm = score;
        UIManager.Instance.setRhythmScore(scoreRhythm);
    }


}
