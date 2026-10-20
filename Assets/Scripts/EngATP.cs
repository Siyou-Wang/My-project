using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EngATP : MonoBehaviour
{
    //private Transform startButton;
    public static bool isCalled = false;
    private Transform backButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backButton = GameObject.Find("BackToScreen ENG").transform;
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
                if (hit.collider.gameObject.tag == "EngATP")
                {
                    Camera.main.transform.position = new Vector3(60, 0, -10);
                    isCalled = true;
                    AngerAndCalm.reset();
                }
                if(hit.collider.gameObject == backButton)
                {
                    isCalled = false;
                }
            }
        }
    }
}
