using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    int ID = 0;
    [SerializeField]
    int life = 100;

    //ability
    [SerializeField]
    int abilityID = 0;

    [SerializeField]
    int damage = 0;


    [SerializeField]
    int aoeDamage = 0;

    //[SerializeField]
    float bonusMultiplier = 1;

    [SerializeField]
    bool isEnemy;

    [SerializeField]
    bool isHarmonized = false;

    AttackingPhaseManager attackingPhaseManager;

    // Start is called before the first frame update
    void Start()
    {
        attackingPhaseManager = FindObjectOfType<AttackingPhaseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        //add debuff s here
        life -=damage;
        if(life<= 0)
        {
            Debug.Log(name + " was killed");

            attackingPhaseManager.CharacterKilled(this, isEnemy);
            
        }
    }

    public void Heal(int heal)
    {
        if (life + heal <= 100)
        {
            life += heal;
        }
        else
        {
            life = 100;
        }
    }

    public void SetAbility(int ID)
    {
        abilityID = ID;
    }
    public int GetAbility()
    {
        if(isEnemy) abilityID = Random.Range(0, 2);
        return abilityID;
    }

    public void SetMultiplier(float multiplier)
    {
        if (multiplier > 0)
        {
            bonusMultiplier = multiplier + 1; // + 1 because we dont the multiplier to decrease our number // 

        }
        else
        {
            Debug.Log("Multiplier is not > 0 : " + multiplier);
        }
        if (isHarmonized)
        {
            bonusMultiplier += 0.5f;
        }
        UIManager.Instance.UpdateMultiplier(bonusMultiplier,ID);


    }
    public float GetMultiplier()
    {
        return bonusMultiplier;
    }

    public int GetDamage()
    {
        return damage;
    }

    public int GetAOEDamage()
    {
        return aoeDamage;
    }

    public void SetHarmonized(bool harmony)
    {
        isHarmonized = harmony;
        UIManager.Instance.ShowHarmony(harmony, ID);

    }

}
