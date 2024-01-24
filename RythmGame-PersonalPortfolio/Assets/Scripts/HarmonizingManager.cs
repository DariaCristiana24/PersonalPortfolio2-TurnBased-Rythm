using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmonizingManager : MonoBehaviour
{

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
    {

    }
    public List<int> badNodes = new List<int>();
    public List<int> HarmonizedAbilities(List<int> _abilities)
    {
        badNodes = new List<int>();

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
           // Debug.Log("checking " + _abilities[i] + " with " + _abilities[i + 1]);
           // Debug.Log("badNodes " + amountOfBadNodes);
            if (amountOfBadNodes > 0)
            {
                for (int j = 0; j < amountOfBadNodes; j++)
                {
                     badNodes.Add(_abilities[i] + 1 + j);
                    // Debug.Log("badNode " + (_abilities[i] + 1 + j));
                }
            }
            if (amountOfBadNodes < 0) 
            {
                for (int j = 0; j < amountOfBadNodes+amountOfColors; j++)
                {
                        if (_abilities[i] + 1 + j > amountOfColors)
                        {
                            badNodes.Add(_abilities[i] + 1 + j - amountOfColors);
                           // Debug.Log("badNode " + (_abilities[i] + 1 + j - amountOfColors));
                        }
                        else
                        {
                            badNodes.Add(_abilities[i] + 1 + j);
                            //Debug.Log("badNode " + (_abilities[i] + 1 + j));
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
              //  Debug.Log("checking " + _abilities[i] + " with " + _abilities[i + 1]);
              //  Debug.Log("badNodes " + amountOfBadNodes);
                if (amountOfBadNodes > 0)
                {
                    for (int j = 0; j < amountOfBadNodes; j++)
                    {
                        badNodes.Add(_abilities[i] - 1 - j);
                        //Debug.Log("badNode " + (_abilities[i] - 1 - j));
                    }
                }
                if (amountOfBadNodes < 0)
                {
                    for (int j = 0; j < amountOfBadNodes + amountOfColors; j++)
                    {
                        if (_abilities[i] - 1 - j <= 0)
                        {
                            badNodes.Add(_abilities[i] - 1 - j + amountOfColors);
                            //Debug.Log("badNode " + (_abilities[i] - 1 - j + amountOfColors));
                        }
                        else
                        {
                            badNodes.Add(_abilities[i] - 1 - j );
                            //Debug.Log("badNode " + (_abilities[i] - 1 - j ));
                        }
                    }
                }




            }

        }

            return _abilities;

    }

}
