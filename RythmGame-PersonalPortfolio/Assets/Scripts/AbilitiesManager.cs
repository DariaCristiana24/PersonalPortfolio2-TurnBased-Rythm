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
    AttackingPhaseManager attackingPhaseManager;
    void Start()
    {
        harmonizingManager = FindObjectOfType<HarmonizingManager>();
        attackingPhaseManager = FindObjectOfType<AttackingPhaseManager>();
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

        if (!chosenAbilities.Contains(0))
        {
            if (checkChosenAbiliteis())
            {
                harmonizedAbilities = harmonizingManager.HarmonizedAbilities(harmonizedAbilities);
                GameManager.Instance.UpdateGameState(GameManager.GameState.Rhythm);
                UIManager.Instance.DisableHarmonies();
                for(int i=0; i<harmonizedAbilities.Count; i++)
                {
                    attackingPhaseManager.characters[i].SetHarmonized(true);
                   

                }
                for(int i = harmonizedAbilities.Count; i<chosenAbilities.Count;i++)
                {
                    attackingPhaseManager.characters[i].SetHarmonized(false);

                }
            }
            else
            {
                UIManager.Instance.DoubleColorWarning();
            }
        }
    }

    bool checkChosenAbiliteis()
    {
        List<int> orderedAbilities = new List<int>(chosenAbilities);
        orderedAbilities.Sort();

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
