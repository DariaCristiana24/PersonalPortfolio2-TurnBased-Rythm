using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{

    [SerializeField]
    List<int> abilities = new List<int>();
    [SerializeField]
    List<bool> abilitiesUsed = new List<bool>();



    bool chronological;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            checkAbbility(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            checkAbbility(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            checkAbbility(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            checkAbbility(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            checkAbbility(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            checkAbbility(6);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            checkHarmony();
        }
    }

    void checkHarmony()
    {
        if (abilities.Count > 2) 
        {

            if (abilities[1] > abilities[0])
            {
                for(int i =1; i< abilities.Count-1; i++)
                {
                    if (abilities[i] > abilities[i+1])
                    {
                        for (int j = abilities.Count; j > i+1 ; j--)
                        {
                            abilities.Remove(abilities[j-1]);

                        }
                        break;
                    }
                    
                }
            }
            else
            {
                for (int i = 1; i < abilities.Count - 1; i++)
                {
                    if (abilities[i] < abilities[i + 1])
                    {
                        abilities.Remove(i + 1);
                    }

                }
                for (int i = 1; i < abilities.Count - 1; i++)
                {
                    if (abilities[i] < abilities[i + 1])
                    {
                        for (int j = abilities.Count; j > i + 1; j--)
                        {
                            abilities.Remove(abilities[j - 1]);

                        }
                        break;
                    }

                }
            }

            
        }
    }

    void checkAbbility(int i)
    {
        if (!abilitiesUsed[i - 1])
        {
            abilities.Add(i);
            abilitiesUsed[i - 1] = true;
        }
    }
}
