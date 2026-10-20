using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RPGGameManager : MonoBehaviour
{
    private Transform player;
    
    private Transform attack;
    private Animator animator;

    private static bool playerTurn;
    private static int playerHealth;
    
    private static bool playerAttack;
    private Transform enemy;

    private static bool won;

    private Transform heart;
    private Transform heart1;
    private Transform heart2;
    private static int loseCount;

    private DialogueManager dialogueManager;
    private bool dialogueFinished = true;
    private string[] message = new string[1];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").transform;
        attack = GameObject.Find("Attack").transform;

        heart = GameObject.Find("Heart").transform;
        heart1 = GameObject.Find("Heart (1)").transform;
        heart2 = GameObject.Find("Heart (2)").transform;

        playerTurn = true;
        playerHealth = 100;
        
        playerAttack = false;
        won = false;
        loseCount = 0;

        dialogueManager = FindFirstObjectByType<DialogueManager>();
        
        animator = GetComponent<Animator>();

        enemy = GameObject.Find("Enemy").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Player health: " + playerHealth + "; Enemy health: " + RPGManagerEnemy.eGetH());

        if (playerHealth <= 0 && loseCount < 4)
        {
            loseCount++;
            playerHealth = 100;
            if (loseCount >= 1)
            {
                heart.GetComponent<SpriteRenderer>().enabled = false;
                AngerAndCalm.setState(-2);
                if (loseCount >= 2)
                {
                    heart1.GetComponent<SpriteRenderer>().enabled = false;
                    AngerAndCalm.setState(-2);

                    if (loseCount >= 3)
                    {
                        heart2.GetComponent<SpriteRenderer>().enabled = false;
                        AngerAndCalm.setState(-2);
                        dialogueFinished = true;
                    }
                }
                
            }

        }
        else if(playerHealth <= 0 && loseCount == 4)
        {
            RPGManagerEnemy.setWinE(true);
            won = false;
            
        }
        if (!RPGManagerEnemy.eTurn())
        {
            playerTurn = true;
        }
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (Input.GetMouseButtonDown(0))
        {

            if (hit.collider != null)
            {
                if(hit.collider.gameObject.tag == "Attack")
                {
                    playerAttack = true;
                    RPGManagerEnemy.eSetH(-15);
                    message[0] = "The player attacked the enemy! The enemy has " + RPGManagerEnemy.eGetH() + " health left!";
                    dialogueFinished = false;

                }
            }
        }

        if (playerAttack)
        {
            
            animator.Play("PlayerAttack");
            
            DoDelayAction(1.0f);
            
            
        }
        else
        {
            animator.Play("PlayerDefault");
        }
    }

    void DoDelayAction(float delaytime)
    {
        StartCoroutine(DelayAction(delaytime));
    }

    IEnumerator DelayAction(float delayTime)
    {
        playerTurn = false;
        RPGManagerEnemy.setTurnE();
        yield return new WaitForSeconds(delayTime);
        playerAttack = false;
        RPGManagerEnemy.enemyAttack = true;
        
    }

    public static void pSetH(int health)
    {
        playerHealth += health;
    }

    public static int pGetH()
    {
        return playerHealth;
    }
    public static bool getPTurn()
    {
        return playerTurn;
    }

    public static void setWinP(bool didWin)
    {
        won = didWin;
    }

    public static void resetGame()
    {
        //Camera.main.transform.position = new Vector3(20, 0, -10);
        AngerAndCalm.reset();
        playerHealth = 100;
        playerTurn = true;
        loseCount = 0;
        playerAttack = false;
        RPGManagerEnemy.reset();
    }
}
