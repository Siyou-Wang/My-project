using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class AngerAndCalm : MonoBehaviour
{
    private Transform fire;
    private Transform fire1;
    private Transform fire2;
    private Transform fire3;
    private Transform fire4;
    private Transform fire5;
    private Transform water;

    private static int mentalState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fire = GameObject.Find("Fire").transform;
        fire1 = GameObject.Find("Fire (1)").transform;
        fire2 = GameObject.Find("Fire (2)").transform;
        fire3 = GameObject.Find("Fire (3)").transform;
        fire4 = GameObject.Find("Fire (4)").transform;
        fire5 = GameObject.Find("Fire (5)").transform;
        water = GameObject.Find("Water").transform;

        fire.GetComponent<Image>().enabled = false;
        fire1.GetComponent<Image>().enabled = false;
        fire2.GetComponent<Image>().enabled = false;
        fire3.GetComponent<Image>().enabled = false;
        fire4.GetComponent<Image>().enabled = false;
        fire5.GetComponent<Image>().enabled = false;
        water.GetComponent<Image>().enabled = false;
        mentalState = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(mentalState < 0)
        {
            if (mentalState > -5)
            {
                fire.GetComponent<Image>().enabled = true;
                fire1.GetComponent<Image>().enabled = false;
                fire2.GetComponent<Image>().enabled = false;
                fire3.GetComponent<Image>().enabled = false;
                fire4.GetComponent<Image>().enabled = false;
                fire5.GetComponent<Image>().enabled = false;

                if (mentalState > -7 && mentalState < -5)
                {
                    fire1.GetComponent<Image>().enabled = true;
                    fire2.GetComponent<Image>().enabled = true;
                    fire3.GetComponent<Image>().enabled = false;
                    fire4.GetComponent<Image>().enabled = false;
                    fire5.GetComponent<Image>().enabled = false;

                    if (mentalState > -10 && mentalState < -7)
                    {
                        fire3.GetComponent<Image>().enabled = true;
                        fire4.GetComponent<Image>().enabled = true;
                        fire5.GetComponent<Image>().enabled = true;
                        RPGGameManager.resetGame();
                    }
                }

            }
            
          
        }
        
        else if(mentalState > 0)
        {
            water.transform.position = new Vector3(0, -1636, 0);
            if(mentalState <5)
            {
                water.transform.position = new Vector3(0, -1000, 0);
            }
            if(mentalState <10 && mentalState >= 5)
            {
                water.transform.position = new Vector3(0, -500, 0);
            }
        }
        else
        {
            fire.GetComponent<Image>().enabled = false;
            fire1.GetComponent<Image>().enabled = false;
            fire2.GetComponent<Image>().enabled = false;
            fire3.GetComponent<Image>().enabled = false;
            fire4.GetComponent<Image>().enabled = false;
            fire5.GetComponent<Image>().enabled = false;
            water.GetComponent<Image>().enabled = false;
        }
    }

    public static int getState()
    {
        return mentalState;
    }

   
    public static void setState(int val)
    {
        mentalState += val;
        //Debug.Log(mentalState);
    }

    public static void reset()
    {
        mentalState = 0;
    }
    
}
