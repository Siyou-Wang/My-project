using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RPGManagerEnemy : MonoBehaviour
{
    private Transform enemy;
    private bool enemyAttack;
    private Animator animator;
    private int enemyHealth;
    private static bool enemyTurn;

    private bool e1;
    private bool e2;
    private bool e3;

    private Transform player;

    private static bool eWon;

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
    }

    // Update is called once per frame
    void Update()
    {
        
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
            else
            {
                animator.Play("Enemy1Attack");
                DoDelayAction(2.0f);
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
            else
            {
                animator.Play("Enemy2Attack");
                DoDelayAction(2.0f);
            }
        }
        if (e3)
        {
            if (enemyHealth <= 0)
            {
                RPGGameManager.setWinP(true);
                eWon = false;
                AngerAndCalm.setState(3);
            }
            setTurnE();
            if (!enemyTurn)
            {
                animator.Play("Enemy3Default");
            }
            else
            {
                animator.Play("Enemy3Attack");
                DoDelayAction(2.0f);
            }
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
        enemyTurn = false;
        
        
    }

    public void eSetH(int health)
    {
        enemyHealth += health;
    }

    public int eGetH()
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
}
