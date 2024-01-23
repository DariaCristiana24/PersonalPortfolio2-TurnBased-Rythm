using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public int key;

    NodeManager nodeManager;
    public BeatLineBehaviour.NodeState nodeState = BeatLineBehaviour.NodeState.Nothing;
    void Start()
    {
        nodeManager = FindObjectOfType<NodeManager>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime *5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EarlyBeatLine")
        {
            nodeState = BeatLineBehaviour.NodeState.Early;
        }
        if (other.tag == "PerfectBeatLine")
        {
            nodeState = BeatLineBehaviour.NodeState.Perfect;
        }
        if (other.tag == "LateBeatLine")
        {
            nodeState = BeatLineBehaviour.NodeState.Late;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "LateBeatLine")
        {
            nodeState = BeatLineBehaviour.NodeState.Missed;
            Debug.Log(nodeState.ToString());
            StartCoroutine(nodeManager.RemoveFirstNode());
        }
        // nodeOnBeat = false;
        //Debug.Log("no node");

  
    }

}
