using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<int> chosenAbilities = new List<int>();
    [SerializeField]
    List<int> harmonizedAbilities = new List<int>();

    HarmonizingManager harmonizingManager;
    void Start()
    {
        harmonizingManager = FindObjectOfType<HarmonizingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddAbility(int characterID, int chosenColor)
    {
        chosenAbilities[characterID] = chosenColor;
    }

    public void SetAbilities()
    {
        harmonizedAbilities = new List<int>( chosenAbilities);

        if (checkChosenAbiliteis())
        {
            harmonizedAbilities= harmonizingManager.HarmonizedAbilities(harmonizedAbilities);
            GameManager.Instance.UpdateGameState(GameManager.GameState.Rhythm);
        }
        else
        {
            Debug.Log("double ability error");
        }
    }

    bool checkChosenAbiliteis()
    {
        List<int> orderedAbilities = new List<int>(chosenAbilities);
        orderedAbilities.Sort();
        if (orderedAbilities.Contains(0))
        {
            return false;
        }
        for (int i = 0; i < orderedAbilities.Count-1; i++)
        {
            if (orderedAbilities[i] == orderedAbilities[i + 1])
            {
                return false;
            }
        }
        return true;
    }

}
