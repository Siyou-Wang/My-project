using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;


public class RPGManagerEnemy : MonoBehaviour
{
    private Transform enemy;
    private Animator animator;
    private static int enemyHealth;
    private static bool enemyTurn;
    public static bool enemyAttack;
  

    private static bool e1;
    private static bool e2;
    private static bool e3;

    private Transform player;

    private static bool eWon;

    private DialogueManager dialogueManager;
    private bool dialogueFinished = true;
    private string[] message = new string[1];


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GameObject.Find("Enemy").transform;
        enemyHealth = 90;
        enemyAttack = false;
        enemyTurn = false;

        player = GameObject.Find("Player").transform;

        animator = GetComponent<Animator>();
        e1 = true;
        e2 = false;
        e3 = false;

        eWon = false;

        dialogueManager = FindFirstObjectByType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RPGGameManager.playerAttack&&!RPGGameManager.playerTurn)
        {
            enemyTurn = true;
            enemyAttack = true;
        }
        
        if (e1)
        {
            if(enemyHealth <= 0)
            {
                e1 = false;
                e2 = true;
                enemyHealth = 110;
                AngerAndCalm.setState(3);
            }
            setTurnE();
            if (!enemyTurn) {
                animator.Play("Enemy1Default");
            }

            if (enemyAttack)
            {
                animator.Play("Enemy1Attack");
                DoDelayAction(3.0f);

            }

            if (enemyTurn && !RPGGameManager.playerAttack)
            {
                
                RPGGameManager.pSetH(-1 * Random.Range(10,15));
                    enemyTurn = false;
            }
        }
        if (e2)
        {
            if(enemyHealth <= 0)
            {
                e2 = false;
                e3 = true;
                enemyHealth = 130;
                AngerAndCalm.setState(3);
            }
            setTurnE();
            if (!enemyTurn)
            {
                animator.Play("Enemy2Default");
            }

            if (enemyAttack)
            {
                animator.Play("Enemy2Attack");
                DoDelayAction(3.0f);
            }

            if (enemyTurn && !RPGGameManager.playerAttack)
            {
                
                RPGGameManager.pSetH(-1 * Random.Range(15,20));
                    enemyTurn = false;
                    //message[0] = "The enemy attacked the player! The player has " + RPGGameManager.pGetH() + " health left!";
                    //dialogueFinished = false;
            }
        }
        if (e3)
        {
            if (enemyHealth <= 0)
            {
                RPGGameManager.setWinP(true);
                eWon = false;
                AngerAndCalm.setState(3);
                dialogueFinished = true;
            }
            setTurnE();
            if (!enemyTurn)
            {
                animator.Play("Enemy3Default");
            }

            if (enemyAttack)
            {
                animator.Play("Enemy3Attack");
                DoDelayAction(3.0f);
            }


            if (enemyTurn && !RPGGameManager.playerAttack)
                {
                
                RPGGameManager.pSetH(-1 * Random.Range(20,25));
                    enemyTurn = false;
                    //message[0] = "The enemy attacked the player! The player has " + RPGGameManager.pGetH() + " health left!";
                    //dialogueFinished = false;
                    
                    
                }
                
            
            
        }

        if (!dialogueFinished)
        {
            dialogueManager.ShowBox(message);
        }
        if(!dialogueFinished && Input.GetKeyDown(KeyCode.Mouse1))
        {
            dialogueFinished = true;
        }
    }

    void DoDelayAction(float delaytime)
    {
        StartCoroutine(DelayAction(delaytime));
    }

    IEnumerator DelayAction(float delayTime)
    {
        
        yield return new WaitForSeconds(delayTime);
        enemyAttack = false;
        message[0] = "The enemy attacked the player! The player has " + RPGGameManager.pGetH() + " health left!";
        dialogueFinished = false;



    }


    public static void eSetH(int health)
    {
        enemyHealth += health;
    }

    public static int eGetH()
    {
        return enemyHealth;
    }

    public static void setTurnE()
    {
        if (RPGGameManager.getPTurn())
        {
            enemyTurn = false;
        }
        else
        {
            enemyTurn = true;
        }
    }

    public static bool eTurn()
    {
        return enemyTurn;
    }

    public static void setWinE(bool didWIn)
    {
        eWon = didWIn;
    }

    public static void reset()
    {
        e1 = true;
        enemyHealth = 90;
        enemyAttack = false;
        enemyTurn = false;
        eWon = false;
    }
}
