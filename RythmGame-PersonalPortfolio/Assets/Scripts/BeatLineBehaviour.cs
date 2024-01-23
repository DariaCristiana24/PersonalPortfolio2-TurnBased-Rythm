using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatLineBehaviour : MonoBehaviour
{
    bool nodeOnBeat = false;



    NodeManager nodeManager;

    public enum NodeState {  Nothing, Missed, Perfect, Late, Early };

    NodeState nodeState;

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
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        checkNode();
                    }
                    break;
                case 1:
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        checkNode();
                    }
                    break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        checkNode();
                    }
                    break;
                case 3:
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        checkNode();
                    }
                    break;

            }

        }
    }

    void checkNode()
    {
        nodeState = nodeManager.GetFirstNode().GetComponent<NodeBehaviour>().nodeState;

        Debug.Log(nodeState.ToString());

        switch (nodeState)
        {
            case NodeState.Missed:

                break;
            case NodeState.Late:

                break;
            case NodeState.Early:

                break;
            case NodeState.Perfect:

                break;
        }

        /*
        if (nodeOnBeat)
        {
            Debug.Log("good node");
            nodeOnBeat = false;
        }
        else
        {
            Debug.Log("early node");
        }
        */
        StartCoroutine(nodeManager.RemoveFirstNode());
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Node")
        {
           // nodeOnBeat = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
       // nodeOnBeat = false;
        //Debug.Log("no node");
       //StartCoroutine(nodeManager.RemoveFirstNode());
    }

}
