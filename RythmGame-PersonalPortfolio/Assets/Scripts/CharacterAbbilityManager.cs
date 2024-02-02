using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CharacterAbbilityManager : MonoBehaviour
{
    [SerializeField]
    int characterID;

    [SerializeField]
    UnityEngine.UI.Image characterImg;

    [SerializeField]
    List<int> colorAbilityID = new List<int>();


    [SerializeField]
    List<Color> colors = new List <Color>();
    int chosenColor;

    AbilitiesManager abilitiesManager;
    NodeManager nodeManager;

    // Start is called before the first frame update
    void Start()
    {
        abilitiesManager = FindObjectOfType<AbilitiesManager>();
        nodeManager = FindObjectOfType<NodeManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked(int i) 
    {   

        chosenColor = colorAbilityID[i];
        abilitiesManager.AddAbility(characterID, chosenColor);
        characterImg.color = colors[i];
        Debug.Log("Clicked ability with color code: " + chosenColor);
        if(characterID==0)

        switch (characterID)
        {
            case 0:
                nodeManager.instanceBig.setParameterByName("Ability1", i);
                break; 
            case 1:
                nodeManager.instanceBig.setParameterByName("Ability2", i);
                break;
            case 2:
                nodeManager.instanceBig.setParameterByName("Ability3", i);
                break;
            case 3:
                nodeManager.instanceBig.setParameterByName("Ability4", i);
                break;
        }
    }

    void OnEnable()
    {
        characterImg.color = Color.white;
    }
}
