using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackingPhaseManager : MonoBehaviour
{   
    [SerializeField]
   public List <Character> characters = new List<Character>();
    [SerializeField]
    List<Character> enemies = new List<Character>();

    [SerializeField]
    int heal = 10;

    [SerializeField]
    float timeInBetweenAttacks;

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
    bool attackBuffEnemyAdded = false;

    int deadCharacters = 0;
    int deadEnemies = 0;

    bool charactersWon =false;
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
      /*  if (Input.GetKeyDown(KeyCode.Z) && attackers == Attackers.Idle)
        {
            StartCoroutine(StartAttacking());
        }*/
    }


    public IEnumerator StartAttacking()
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

        /*

        foreach (Character enemy in enemies)
        {
            if (enemy.GetLife() <= 0)
            {
                CharacterKilled(enemy, true);
            }
        }*/




        addEnemyBuffs();

        foreach (Character enemy in enemies)
        {
            StartCoroutine(attackSequence(enemy));
            yield return new WaitForSecondsRealtime(timeInBetweenAttacks);

        }

        EnemyBuff.attackBuff = 1;
        EnemyBuff.defenceBuff = 1;
        addEnemyBuffs();

        /*
        foreach (Character character in characters)
        {
            if (character.GetLife() <= 0)
            {
                CharacterKilled(character, false);
            }
        }   
        */


        GameManager.Instance.UpdateGameState(GameManager.GameState.Harmonizing);

        foreach (Character character in characters)
        {
            character.SetHarmonized(false);
            character.SetMultiplier(0);
        }
        foreach (Character enemy in enemies)
        {
            enemy.SetHarmonized(false);
            enemy.SetMultiplier(0);
        }
        //resterr harmony and multipliers
        for (int i=0; i < deadCharacters;i++)
        { 
           UIManager.Instance.ShowHarmony(false, i);
           UIManager.Instance.UpdateMultiplier(0, i);
        }

        attackers = Attackers.Idle;

    }

    IEnumerator attackSequence(Character character)
    {
        doAction(character, character.GetAbility(), character.GetMultiplier());
        character.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(0.5f);

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
                    //foreach (Character enemy in enemies)
                    for(int j = 0; j<enemies.Count; j++)
                    {
                        enemies[j].TakeDamage(aoeDamage);

                    }
                }
                else if (attackers == Attackers.Enemies)
                {
                    aoeDamage = (int)(aoeDamage * EnemyBuff.attackBuff);
                    for (int j = 0; j < characters.Count; j++)
                    {
                        characters[j].TakeDamage(aoeDamage );
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
                    attackBuffEnemyAdded = true;
                }

                break;

        }

    }

    public void CharacterKilled(Character character, bool isEnemy)
    {
        if (isEnemy)
        {
            enemies.Remove(character);
            deadEnemies++;
        }
        else
        {
            characters.Remove(character);
            deadCharacters++;
        }

         character.transform.GetChild(0).gameObject.SetActive(false);
        if (deadCharacters == 4 || deadEnemies == 4)
        {
            if (deadCharacters == 4)
            {
                charactersWon = false;
            }
            else
            {
                charactersWon = true;
            }
            GameManager.Instance.UpdateGameState(GameManager.GameState.GameFinished);
        }
    }

    void addPlayerBuffs()
    {
        if (attackBuffPlayerAdded && attackDebuffPlayerAdded) 
        {
            PlayerBuff.attackBuff = 1;
            UIManager.Instance.NoShowAttackBuff();
        }
        else
        {
            if (attackBuffPlayerAdded)
            {
                PlayerBuff.attackBuff = attackBuff;
                UIManager.Instance.ShowAttackBuff(true);
            }
            if (attackDebuffPlayerAdded)
            {
                PlayerBuff.attackBuff = attackDebuff;
                UIManager.Instance.ShowAttackBuff(false);
            }
        }

        attackBuffPlayerAdded = false;
        attackDebuffPlayerAdded = false;

    }

    void addEnemyBuffs()
    {
        if (attackDebuffEnemyAdded && attackBuffEnemyAdded)
        {
            EnemyBuff.attackBuff = 1;
        }
        else
        {
            if (attackBuffEnemyAdded)
            {
                EnemyBuff.attackBuff = attackBuff;
            }
            if (attackDebuffEnemyAdded)
            {
                EnemyBuff.attackBuff = attackDebuff;
            }
        }

        attackBuffEnemyAdded = false;
        attackDebuffEnemyAdded = false;
    }

    void initializeBuffs()
    {
        PlayerBuff.attackBuff = 1;
        PlayerBuff.defenceBuff = 1;
        EnemyBuff.attackBuff = 1;
        EnemyBuff.defenceBuff = 1; ;

    }

    public int GetDeadCharacters()
    {
        return deadCharacters;
    }

    public bool GetcharactersWon()
    {
        return charactersWon;
    }


}
