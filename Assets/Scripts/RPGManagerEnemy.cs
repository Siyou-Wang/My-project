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
    }

    // Update is called once per frame
    void Update()
    {
        
        if (e1)
        {
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
}
