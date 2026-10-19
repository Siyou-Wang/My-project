using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RPGGameManager : MonoBehaviour
{
    private Transform player;
    
    private Transform attack;
    private Animator animator;

    private static bool playerTurn;
    private static int playerHealth;
    
    private bool playerAttack;
    private Transform enemy;

    private static bool won;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").transform;
        attack = GameObject.Find("Attack").transform;
        
        playerTurn = true;
        playerHealth = 100;
        
        playerAttack = false;
        won = false;
        
        animator = GetComponent<Animator>();

        enemy = GameObject.Find("Enemy").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth <= 0)
        {
            RPGManagerEnemy.setWinE(true);
            won = false;
            AngerAndCalm.setState(-6);
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
        yield return new WaitForSeconds(delayTime);
        playerAttack = false;
        playerTurn = false;
        RPGManagerEnemy.setTurnE();
    }

    public void pSetH(int health)
    {
        playerHealth += health;
    }

    public int pGetH()
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
}
