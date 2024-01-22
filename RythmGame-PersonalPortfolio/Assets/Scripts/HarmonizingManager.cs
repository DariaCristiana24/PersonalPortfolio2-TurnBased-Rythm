using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HarmonizingManager : MonoBehaviour
{

    // [SerializeField]
    // List<int> abilities = new List<int>();
    //  [SerializeField]
    //  List<bool> abilitiesUsed = new List<bool>();

    [SerializeField]
    int amountOfColors = 5;

    [SerializeField]
    bool clockwise;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {/*
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
          //  checkAbbility(6);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            checkHarmony();
        }*/
    }
    public List<int> badNodes = new List<int>();
    public List<int> HarmonizedAbilities(List<int> _abilities)
    {
        badNodes = new List<int>();


        //if (_abilities[1] > _abilities[0] && amountOfColors / 2 >= _abilities[1] - _abilities[0]-1) //chronological
        if (!clockwise)
        {
            for(int i =0; i< _abilities.Count-1; i++)
            {
            if (badNodes.Contains(_abilities[i + 1]))
            {
                 for (int j = _abilities.Count; j > i + 1; j--)
                 {
                    _abilities.Remove(_abilities[j - 1]);

                 }
                 break;
            }
            int amountOfBadNodes = _abilities[i + 1] - _abilities[i]-1;
            Debug.Log("checking " + _abilities[i] + " with " + _abilities[i + 1]);
            Debug.Log("badNodes " + amountOfBadNodes);
            if (amountOfBadNodes > 0)
            {
                for (int j = 0; j < amountOfBadNodes; j++)
                {
                     badNodes.Add(_abilities[i] + 1 + j);
                     Debug.Log("badNode " + (_abilities[i] + 1 + j));
                }
            }
            if (amountOfBadNodes < 0) 
            {
                for (int j = 0; j < amountOfBadNodes+amountOfColors; j++)
                {
                        if (_abilities[i] + 1 + j > amountOfColors)
                        {
                            badNodes.Add(_abilities[i] + 1 + j - amountOfColors);
                            Debug.Log("badNode " + (_abilities[i] + 1 + j - amountOfColors));
                        }
                        else
                        {
                            badNodes.Add(_abilities[i] + 1 + j);
                            Debug.Log("badNode " + (_abilities[i] + 1 + j));
                        }
                }
            }
                    
                    


            }
        }
        else 
        {
            for (int i = 0; i < _abilities.Count - 1; i++)
            {
                if (badNodes.Contains(_abilities[i + 1]))
                {
                    for (int j = _abilities.Count; j > i + 1; j--)
                    {
                        _abilities.Remove(_abilities[j - 1]);

                    }
                    break;
                }
                int amountOfBadNodes = _abilities[i ] - _abilities[i+1] - 1;
                Debug.Log("checking " + _abilities[i] + " with " + _abilities[i + 1]);
                Debug.Log("badNodes " + amountOfBadNodes);
                if (amountOfBadNodes > 0)
                {
                    for (int j = 0; j < amountOfBadNodes; j++)
                    {
                        badNodes.Add(_abilities[i] - 1 - j);
                        Debug.Log("badNode " + (_abilities[i] - 1 - j));
                    }
                }
                if (amountOfBadNodes < 0)
                {
                    for (int j = 0; j < amountOfBadNodes + amountOfColors; j++)
                    {
                        if (_abilities[i] - 1 - j <= 0)
                        {
                            badNodes.Add(_abilities[i] - 1 - j + amountOfColors);
                            Debug.Log("badNode " + (_abilities[i] - 1 - j + amountOfColors));
                        }
                        else
                        {
                            badNodes.Add(_abilities[i] - 1 - j );
                            Debug.Log("badNode " + (_abilities[i] - 1 - j ));
                        }
                    }
                }




            }

        }

            return _abilities;
        //}
    }
    /*
    void checkAbbility(int i)
    {
        if (!abilitiesUsed[i - 1])
        {
            abilities.Add(i);
            abilitiesUsed[i - 1] = true;
        }
    }*/
}
