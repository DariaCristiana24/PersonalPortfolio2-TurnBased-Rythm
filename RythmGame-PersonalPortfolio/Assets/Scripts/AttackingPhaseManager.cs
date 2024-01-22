using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackingPhaseManager : MonoBehaviour
{   
    [SerializeField]
    List <Character> characters = new List<Character>();
    [SerializeField]
    List<Character> enemies = new List<Character>();

    [SerializeField]
    int damage = 50;
    [SerializeField]
    int heal = 10;

    [SerializeField]
    int timeInBetweenAttacks;

    public enum Attackers
    {
        Idle,
        Characters,
        Enemies
    }

    public Attackers attackers = Attackers.Idle;
    // Start is called before the first frame update
    void Start()
    {
       // StartCoroutine(StartAttacking());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && attackers == Attackers.Idle)
        {
            StartCoroutine(StartAttacking());
        }
    }


    IEnumerator StartAttacking()
    {
        attackers = Attackers.Characters;
        foreach (Character character in characters )
        {
            StartCoroutine(attackSequence(character));
            yield return new WaitForSecondsRealtime(timeInBetweenAttacks);
        }
        attackers = Attackers.Enemies;
        foreach (Character enemy in enemies)
        {
            StartCoroutine(attackSequence(enemy));
            yield return new WaitForSecondsRealtime(timeInBetweenAttacks);

        }
        attackers = Attackers.Idle;

    }

    IEnumerator attackSequence(Character character)
    {
        doAction(character, character.GetAbility(), character.GetMultiplier());
        character.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(1);

        character.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    }

    void doAction(Character character,int actionID, int multiplier)
    {
        switch (actionID)
        {
            case 0://attack
                Debug.Log(character + "attacked");
                if (attackers == Attackers.Characters)
                {
                    enemies[0].TakeDamage(damage * multiplier);
                }
                else if (attackers == Attackers.Enemies)
                {
                    characters[0].TakeDamage(damage * multiplier);
                }
                break; 
            case 1: // heal
                character.Heal(heal * multiplier);
                Debug.Log(character + "healed themselves");
                break;
        }
    }

    public void CharacterKilled(Character character, bool isEnemy)
    {
        if (isEnemy)
        {

            enemies.Remove(character);
        }
        else
        {
            characters.Remove(character);
        }

       // Destroy(character.transform.GetChild(0).gameObject);
         character.transform.GetChild(0).gameObject.SetActive(false);
    }

 
    
}
