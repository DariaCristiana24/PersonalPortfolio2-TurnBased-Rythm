using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField]
    TextMeshProUGUI rhythmFeedback;
    [SerializeField]
    TextMeshProUGUI rhythmScore;
    [SerializeField]
    TextMeshProUGUI doubleColorWarning;
    [SerializeField]
    List<TextMeshProUGUI> multipliers = new List<TextMeshProUGUI>();
    [SerializeField]
    GameObject harmonizingButtons;

    [SerializeField]
    GameObject arrowRight; 
    [SerializeField]
    GameObject arrowLeft;

    [SerializeField]
    GameObject buffAttackPlayer;
    [SerializeField]
    GameObject debuffAttackPlayer;

    [SerializeField]
    GameObject buffAttackEnemy;
    [SerializeField]
    GameObject debuffAttackEnemy;

    [SerializeField]
    List<GameObject> harmonies = new List<GameObject>();

    [SerializeField]
    GameObject finishScreen;
    [SerializeField]
    TextMeshProUGUI finishScreenText;

    [SerializeField]
    TextMeshProUGUI battleFeedBack;

    [SerializeField]
    List<TextMeshProUGUI> livesPlayers = new List<TextMeshProUGUI>();
    [SerializeField]
    List<TextMeshProUGUI> livesEnemies = new List<TextMeshProUGUI>();



    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setRhythmFeedBack(string feedback)
    {
        rhythmFeedback.SetText(feedback);
        StartCoroutine(FadeOut( rhythmFeedback,1));
    }

    private IEnumerator FadeOut(TextMeshProUGUI text, float time)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime/ time));
            yield return null;
        }
    }
    public void setRhythmScore(float score)
    {
        rhythmScore.SetText(score.ToString());

    }

    public void DoubleColorWarning()
    {
        StartCoroutine(FadeOut(doubleColorWarning, 4));
    }

    public void DisableHarmonies()
    {
        harmonizingButtons.SetActive(false);
    }
    public void EnableHarmonies()
    {
        harmonizingButtons.SetActive(true);
    }

    public void SetClockwise(bool clockwise)
    {
        if (clockwise)
        {
            arrowRight.SetActive(false);
            arrowLeft.SetActive(true);
        }
        else
        {
            arrowLeft.SetActive(false);
            arrowRight.SetActive(true);
        }
    }

    public void UpdateMultiplier( float multiplier, int i)
    {

        if (multiplier == 0)
        {
            multipliers[i].SetText("DEAD");
        }
        else
        {
            multipliers[i].SetText(multiplier.ToString() + "x");
        }

    }

    public void ShowHarmony(bool show, int i)
    {
        harmonies[i].SetActive(show);

    }

    public void ShowAttackBuffPlayer(bool buffed)
    {
        if (buffed)
        {
            debuffAttackPlayer.SetActive(false);
            buffAttackPlayer.SetActive(true);
        }
        else
        {
            buffAttackPlayer.SetActive(false);
            debuffAttackPlayer.SetActive(true);
        }
    }
    public void NoShowAttackBuffPlayer()
    {
        buffAttackPlayer.SetActive(false);
        debuffAttackPlayer.SetActive(false);
    }

    public void ShowAttackBuffEnemy(bool buffed)
    {
        if (buffed)
        {
            debuffAttackEnemy.SetActive(false);
            buffAttackEnemy.SetActive(true);
        }
        else
        {
            buffAttackEnemy.SetActive(false);
            debuffAttackEnemy.SetActive(true);
        }
    }
    public void NoShowAttackBuffEnemy()
    {
        buffAttackEnemy.SetActive(false);
        debuffAttackEnemy.SetActive(false);
    }

    public void EnableFinishScreen(bool charactersWon)
    {
        finishScreen.SetActive(true);
        if (charactersWon)
        {
            finishScreenText.SetText("You Win! :D");
        }
        else
        {
            finishScreenText.SetText("You Lose! >:|");
        }
    }

    public void DoBattleFeedback(string text)
    {
        battleFeedBack.SetText(text);
        StartCoroutine(FadeOut(battleFeedBack, 1));
    }

    public void SetLifePlayers(int life, int i)
    {
        if (life > 0)
        {
            livesPlayers[i].SetText(life.ToString());
        }
        else
        {
            livesPlayers[i].SetText("");
        }
    }
    public void SetLifeEnemies(int life, int i)
    {
        if (life > 0)
        {
            livesEnemies[i].SetText(life.ToString());
        }
        else
        {
            livesEnemies[i].SetText("");
        }
    }

}
