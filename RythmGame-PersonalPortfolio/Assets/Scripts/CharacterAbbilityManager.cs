using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbbilityManager : MonoBehaviour
{
    [SerializeField]
    int characterID;

    [SerializeField]
    List<int> colorAbility = new List<int>();

     int chosenColor;

    AbilitiesManager abilitiesManager;
    HarmonizingManager hairmonizingManager;
    // Start is called before the first frame update
    void Start()
    {
        abilitiesManager = FindObjectOfType<AbilitiesManager>();
        hairmonizingManager = FindObjectOfType<HarmonizingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked(int i) 
    {   
        chosenColor = colorAbility[i];
        abilitiesManager.AddAbility(characterID, chosenColor);
        Debug.Log("Clicked ability with color code: " + chosenColor);
    }
}
