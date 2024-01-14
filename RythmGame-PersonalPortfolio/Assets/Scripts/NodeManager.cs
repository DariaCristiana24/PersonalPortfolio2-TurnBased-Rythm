using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField]
    float timeBetween = 1;
    float timer = 0;

    [SerializeField]
    bool spawning = true;

    [SerializeField]
    GameObject nodePrefab;

    [SerializeField]
    List<Transform> spawnPositions = new List<Transform>();


    public List<GameObject> activeNodes = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        timer = timeBetween;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawning)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                spawnNode();
            }
        }
    }

    void spawnNode()
    {
        int nodePos = Random.Range(0, spawnPositions.Count-1);
        Transform spawnPos = spawnPositions[nodePos];

        GameObject node = Instantiate(nodePrefab, spawnPos.position, Quaternion.identity);
        node.GetComponent<NodeBehaviour>().key = nodePos;
        activeNodes.Add(node);
        timer = timeBetween;
    }
    public GameObject GetFirstNode()
    {
        if (activeNodes.Count>0)
        {
            return activeNodes[0];
        }
        else
        {
            return null;
        }
    }

    public void RemoveFirstNode()
    {
        Destroy(activeNodes[0].gameObject);
        activeNodes.RemoveAt(0);
    }

    public int GetNodePos()
    {
        return activeNodes[0].GetComponent<NodeBehaviour>().key;
    }
}
