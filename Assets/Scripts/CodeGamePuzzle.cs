using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CodeGamePuzzle : MonoBehaviour
{
    private static Transform codeStart;
    private static Transform codeMid;
    private static Transform codeEnd;

    private Transform codeStartPos;
    private Transform codeMidPos;
    private Transform codeEndPos;

    private bool start = false;
    private bool mid = false;
    private bool end = false;

    private static bool add;
    private static bool add1;
    private static bool add2;

    private static bool completeCode = false;

    public int value = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        codeStart = GameObject.Find("Code").transform;
        codeMid = GameObject.Find("Code (1)").transform;
        codeEnd = GameObject.Find("Code (2)").transform;

        codeStartPos = GameObject.Find("CodePos").transform;
        codeMidPos = GameObject.Find("CodePos (1)").transform;
        codeEndPos = GameObject.Find("CodePos (2)").transform;

        add = false;
        add1 = false;
        add2 = false;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = codeStart.position - codeStartPos.position;
        float magnitude = distance.magnitude;
        if(magnitude < 1)
        {
            start = true;
            codeStart.position = codeStartPos.position;
            codeStart.GetComponent<BoxCollider2D>().enabled = false;
            if (add == false)
            {
                AngerAndCalm.setState(value);
                add = true;
            }
          
        }

        Vector3 distance1 = codeMid.position - codeMidPos.position;
        float magnitude1 = distance1.magnitude;
        if (magnitude1 < 1)
        {
            mid = true;
            codeMid.position = codeMidPos.position;
            codeMid.GetComponent<BoxCollider2D>().enabled = false;
            if (add1 == false)
            {
                AngerAndCalm.setState(value);
                add1 = true;
            }

        }

        Vector3 distance2 = codeEnd.position - codeEndPos.position;
        float magnitude2 = distance2.magnitude;
        if (magnitude2 < 1)
        {
            end = true;
            codeEnd.position = codeEndPos.position;
            codeEnd.GetComponent<BoxCollider2D>().enabled = false;
            if (add2 == false)
            {
                AngerAndCalm.setState(value);
                add2 = true;
            }

        }

        if((start && end) && mid)
        {
            completeCode = true;
            Camera.main.transform.position = new Vector3(40, -20, -10);
            reset();
        }
    }

    public static void reset()
    {
        codeEnd.position = new Vector3(45,2,1);
        codeMid.position = new Vector3(45,0,1);
        codeEnd.position = new Vector3(39,3,1);
        completeCode = false;

        add = false;
        add1 = false;
        add2 = false;
    }
}
