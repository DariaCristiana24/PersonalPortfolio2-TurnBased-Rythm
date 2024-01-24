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
    int heal = 10;

    [SerializeField]
    int timeInBetweenAttacks;

    [SerializeField]
    float attackBuff = 1.1f;
    [SerializeField]
    float attackDebuff = 0.9f;

    struct buff
    {
        public float attackBuff;
        public float defenceBuff;
    }

    buff PlayerBuff = new buff();
    buff EnemyBuff = new buff();

    bool attackBuffPlayerAdded = false;
    bool attackDebuffPlayerAdded = false;
    bool attackDebuffEnemyAdded = false;
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

        initializeBuffs();
        // StartCoroutine(StartAttacking());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && attackers == Attackers.Idle)
        {
            StartCoroutine(StartAttacking());
        }
    }


    IEnumerator StartAttacking()
    {
        attackers = Attackers.Characters;

        addPlayerBuffs(); //debuffs cause by enemies (attack and defence)
        foreach (Character character in characters )
        {
            StartCoroutine(attackSequence(character));
            yield return new WaitForSecondsRealtime(timeInBetweenAttacks);
        }
        attackers = Attackers.Enemies;

        PlayerBuff.attackBuff = 1;
        PlayerBuff.defenceBuff = 1;
        addPlayerBuffs(); //buffs caasted 




        addEnemyBuffs();

        foreach (Character enemy in enemies)
        {
            StartCoroutine(attackSequence(enemy));
            yield return new WaitForSecondsRealtime(timeInBetweenAttacks);

        }

        EnemyBuff.attackBuff = 1;
        EnemyBuff.defenceBuff = 1;
        addEnemyBuffs();



        attackers = Attackers.Idle;

    }

    IEnumerator attackSequence(Character character)
    {
        doAction(character, character.GetAbility(), character.GetMultiplier());
        character.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(1);

        character.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    }

    void doAction(Character character,int actionID, float multiplier)
    {
        switch (actionID)
        {
            case 0://attack
                Debug.Log(character + "attacked");
                int damage = (int)(character.GetDamage() * multiplier);
                if (attackers == Attackers.Characters)
                {
                    damage = (int)(damage * PlayerBuff.attackBuff);
                    enemies[0].TakeDamage(damage);
                }
                else if (attackers == Attackers.Enemies)
                {
                    
                    damage = (int)(damage * EnemyBuff.attackBuff);
                    characters[0].TakeDamage(damage );
                }
                break; 
            case 1: // heal
                character.Heal((int)(heal * multiplier));
                Debug.Log(character + "healed themselves");
                break;
            case 2: // aoe attack
                int aoeDamage = (int)(character.GetAOEDamage() * multiplier);
                if (attackers == Attackers.Characters)
                {
                    aoeDamage = (int)(aoeDamage * PlayerBuff.attackBuff);
                    foreach (Character enemy in enemies)
                    {
                        enemy.TakeDamage(aoeDamage );
                    }
                }
                else if (attackers == Attackers.Enemies)
                {
                    aoeDamage = (int)(aoeDamage * EnemyBuff.attackBuff);
                    foreach (Character player in characters)
                    {
                        player.TakeDamage(aoeDamage );
                    }
                }
                Debug.Log(character + "AOE attacked ");
                break;
            case 3: // aoe heal 
                if (attackers == Attackers.Enemies)
                {
                    foreach (Character enemy in enemies)
                    {
                        enemy.Heal((int)(heal*multiplier)); ;
                    }
                }
                else if (attackers == Attackers.Characters)
                {
                    foreach (Character player in characters)
                    {
                        player.Heal((int)(heal* multiplier)); ;
                    }
                }
                Debug.Log(character + "AOE healed ");
                break;

            case 4: //attack debuff
                if (attackers == Attackers.Characters)
                {
                    attackDebuffEnemyAdded = true;
                }
                else if (attackers == Attackers.Enemies)
                {
                    attackDebuffPlayerAdded = true;
                }

                break;
            case 5: //attackbuff
                if (attackers == Attackers.Characters)
                {
                    attackBuffPlayerAdded = true;
                }
                else if (attackers == Attackers.Enemies)
                {
                    //attackBuffEnemyAdded
                }

                break;
            case 6: //defence buff
                //
                break;
            case 7://defence debuff
                //
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

    void addPlayerBuffs()
    {
        if (attackBuffPlayerAdded && attackDebuffPlayerAdded) 
        {
            PlayerBuff.attackBuff = 1;
        }
        else
        {
            if (attackBuffPlayerAdded)
            {
                PlayerBuff.attackBuff = attackBuff;
            }
            if (attackDebuffPlayerAdded)
            {
                PlayerBuff.attackBuff = attackDebuff;
            }
        }

        attackBuffPlayerAdded = false;
        attackDebuffPlayerAdded = false;

    }

    void addEnemyBuffs()
    {
        if (attackDebuffEnemyAdded)
        {
            EnemyBuff.attackBuff = attackDebuff;
        }
        //attackBuffEnemyAdded = false;
        attackDebuffEnemyAdded = false;
    }

    void initializeBuffs()
    {
        PlayerBuff.attackBuff = 1;
        PlayerBuff.defenceBuff = 1;
        EnemyBuff.attackBuff = 1;
        EnemyBuff.defenceBuff = 1; ;

    }


}
