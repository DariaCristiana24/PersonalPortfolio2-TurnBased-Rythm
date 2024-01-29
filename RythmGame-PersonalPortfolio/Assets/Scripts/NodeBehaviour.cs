using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public int key;
    BeatLineBehaviour beatLineBehaviour;
    public BeatLineBehaviour.NodeState nodeState = BeatLineBehaviour.NodeState.Missed;
    void Start()
    {
        beatLineBehaviour = FindObjectOfType<BeatLineBehaviour>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * 4.5f);
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
            beatLineBehaviour.CheckFirstNode();
        }


  
    }

}
