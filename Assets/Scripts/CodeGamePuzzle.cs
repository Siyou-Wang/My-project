using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CodeGamePuzzle : MonoBehaviour
{
    private Transform codeStart;
    private Transform codeMid;
    private Transform codeEnd;

    private Transform codeStartPos;
    private Transform codeMidPos;
    private Transform codeEndPos;

    private bool start = false;
    private bool mid = false;
    private bool end = false;

    private bool completeCode = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        codeStart = GameObject.Find("Code").transform;
        codeMid = GameObject.Find("Code (1)").transform;
        codeEnd = GameObject.Find("Code (2)").transform;

        codeStartPos = GameObject.Find("CodePos").transform;
        codeMidPos = GameObject.Find("CodePos (1)").transform;
        codeEndPos = GameObject.Find("CodePos (2)").transform;

       

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
          
        }

        Vector3 distance1 = codeMid.position - codeMidPos.position;
        float magnitude1 = distance1.magnitude;
        if (magnitude1 < 1)
        {
            mid = true;
            codeMid.position = codeMidPos.position;
            codeMid.GetComponent<BoxCollider2D>().enabled = false;
           
        }

        Vector3 distance2 = codeEnd.position - codeEndPos.position;
        float magnitude2 = distance2.magnitude;
        if (magnitude2 < 1)
        {
            end = true;
            codeEnd.position = codeEndPos.position;
            codeEnd.GetComponent<BoxCollider2D>().enabled = false;
          
        }

        if((start && end) && mid)
        {
            completeCode = true;

        }
    }
}
