using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RPGGameManager : MonoBehaviour
{
    private Transform player;
    private Transform enemy;
    private Transform attack;
    private Animator animator;

    private bool playerTurn;
    private int playerHealth;
    private int enemyHealth;
    private bool playerAttack;
    private bool enemyAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").transform;
        attack = GameObject.Find("Attack").transform;
        enemy = GameObject.Find("Enemy").transform;
        playerTurn = true;
        playerHealth = 100;
        enemyHealth = 90;
        playerAttack = false;
        enemyAttack = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
            DoDelayAction(1.5f);
            
            
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
    }

}
