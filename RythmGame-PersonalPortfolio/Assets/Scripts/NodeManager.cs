using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

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
    public FMOD.Studio.EventInstance instanceBig;

    bool nodeCanSpawn = true;

    int characterTrackIndex = 0;
    AttackingPhaseManager attackingPhaseManager;
    BeatLineBehaviour beatLineBehaviour;


    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(onBeatEvent);
        instance.start();

        instanceBig = FMODUnity.RuntimeManager.CreateInstance(onBeatEventBig);


        timer = timeBetween;
        attackingPhaseManager = FindObjectOfType<AttackingPhaseManager>();
        beatLineBehaviour = FindObjectOfType<BeatLineBehaviour>();
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
        instanceBig.getTimelinePosition(out int pos);
        //Debug.Log(pos);

        instanceBig.getPlaybackState(out  FMOD.Studio.PLAYBACK_STATE stateTrack);
        if (Input.GetKeyDown(KeyCode.B) /*stateTrack.ToString() == "STOPPED"*/)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Battle);
            spawningWithInterval = false;
            spawningFMOD = false;
            characterTrackIndex = 0;
        }

        if (Input.GetKeyDown(KeyCode.N) && GameManager.Instance.State == GameManager.GameState.Rhythm) //stateTrack.ToString() == "SUSTAINING") // in between tracks per player 
        {
            nextCharacter();

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
        Destroy(activeNodes[0].gameObject);
        activeNodes.RemoveAt(0);
        yield return new WaitForSecondsRealtime(0.05f);

    }

    public int GetNodePos()
    {
        return activeNodes[0].GetComponent<NodeBehaviour>().key;
    }

    public void StartSpawning()
    {
        spawningFMOD = true;
        instanceBig.start();
        StartCoroutine(timeChecking());

    }

    IEnumerator timeChecking()
    {
        for (int i = 0; i < 4; i++)
        {
            nextCharacter();
            yield return new WaitForSecondsRealtime(16);
        }
        yield return new WaitForSecondsRealtime(1); // one second of transitioning 
        nextCharacter();

    }

    void nextCharacter()
    {
        if (characterTrackIndex < attackingPhaseManager.characters.Count - 1)
        {
            attackingPhaseManager.characters[characterTrackIndex].SetMultiplier(beatLineBehaviour.GetRhythmScore() / 1000);
            Debug.Log("multiplier ch " + characterTrackIndex + " : " + beatLineBehaviour.GetRhythmScore() / 1000);
            characterTrackIndex++;
            beatLineBehaviour.SetRhythmScore(0);
        }
        else
        {
            attackingPhaseManager.characters[characterTrackIndex].SetMultiplier(beatLineBehaviour.GetRhythmScore() / 1000);
            Debug.Log("multiplier ch " + characterTrackIndex + " : " + beatLineBehaviour.GetRhythmScore() / 1000);
            characterTrackIndex++;
            beatLineBehaviour.SetRhythmScore(0);
            characterTrackIndex = 0;
            //instanceBig.stop();
            instanceBig.stop(new FMOD.Studio.STOP_MODE());
            GameManager.Instance.UpdateGameState(GameManager.GameState.Battle);
            spawningWithInterval = false;
            spawningFMOD = false;
        }
    }


}
