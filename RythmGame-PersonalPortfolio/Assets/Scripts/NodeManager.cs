using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class NodeManager : MonoBehaviour
{
    [SerializeField]
    float timeBetween = 1;
    float timer = 0;

    [SerializeField]
    bool spawningWithInterval = true;
    [SerializeField]
    bool spawningFMOD = true;

    [SerializeField]
    List<GameObject> nodePrefabs = new List<GameObject>();

    [SerializeField]
    List<Transform> spawnPositions = new List<Transform>();


    public List<GameObject> activeNodes = new List<GameObject>();

    [SerializeField]
    private FMODUnity.EventReference onBeatEvent;
    private FMOD.Studio.EventInstance instance;


    [SerializeField]
    private FMODUnity.EventReference onBeatEventBig;
    private FMOD.Studio.EventInstance instanceBig;

    bool nodeCanSpawn = true;
    Renderer ren;

    // public FMOD.Studio.PARAMETER_ID musicPar;
    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(onBeatEvent);
        instance.start();

        instanceBig = FMODUnity.RuntimeManager.CreateInstance(onBeatEventBig);


        timer = timeBetween;
    }

    // Update is called once per frame
    void Update()
    {
        // FMODUnity.RuntimeManager.StudioSystem.getParameterByName("NodeOnBeat", out float nodeOnBeat);
        // instance = FMODUnity.RuntimeManager.CreateInstance(onBeatEvent);
        //  instance.start()
       // instance.setParameterByName("NodeOnBeat", 1f);
       // instance.getParameterByName("NodeOnBeat", out float nodeOnBeat);
        instance.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
        string currentState = state.ToString();
        if (spawningFMOD)
        {
            if (currentState == "PLAYING" && nodeCanSpawn)
            {
                spawnNode();
                nodeCanSpawn = false;
            }

            if (currentState == "SUSTAINING")
            {
                nodeCanSpawn = true;
            }
        }
     //   UnityEngine.Debug.Log(state);
        if (spawningWithInterval)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                spawnNode();
            }
        }

        /* if(paramater == "true"){
            spawnNode();
         paramter.set(false)
        }
        */

        instanceBig.getPlaybackState(out  FMOD.Studio.PLAYBACK_STATE stateTrack);
         if (stateTrack.ToString() == "STOPPED")
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Battle);
        }

    }

    void spawnNode()
    {
        int nodePos = Random.Range(0, spawnPositions.Count);
        Transform spawnPos = spawnPositions[nodePos];

        GameObject node = Instantiate(nodePrefabs[nodePos], spawnPos.position, Quaternion.identity);
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

    public IEnumerator RemoveFirstNode()
    {
       // GameObject objectToDestroy = activeNodes[0].gameObject;
     //   objectToDestroy.SetActive(false);
        Destroy(activeNodes[0].gameObject);
        activeNodes.RemoveAt(0);
        yield return new WaitForSecondsRealtime(0.05f);
       // ren = activeNodes[0].GetComponent<Renderer>();
       // ren.material.color = Color.white;

    }

    public int GetNodePos()
    {
        return activeNodes[0].GetComponent<NodeBehaviour>().key;
    }

    public void StartSpawning()
    {
        spawningFMOD = true;
        instanceBig.start();

    }


}
