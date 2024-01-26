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

    // Start is called before the first frame update
    void Start()
    {
        abilitiesManager = FindObjectOfType<AbilitiesManager>();

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
    }

    void OnEnable()
    {
        characterImg.color = Color.white;
    }
}
