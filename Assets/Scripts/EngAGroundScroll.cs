using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private Transform ground1;
    private Transform ground2;

    private float speed = 0.5f;
    private int speedInc;

    private string typeAnsw = "The quick brown fox jumped over the lazy dogs";
    private int typeDex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ground1 = GameObject.Find("EngAGround1").transform;
        ground1 = GameObject.Find("EngAGround2").transform;
    }

    // Update is called once per frame
    void Update()
    {
        speed += speedInc / 1000;
        if (Input.inputString.Length > 0)
        {
            char pressedChar = Input.inputString[0];
            char targetChar = typeAnsw[typeDex];

            if (pressedChar == targetChar)
            {
                ground1.position += Vector3.left * speed * Time.deltaTime;
                ground2.position += Vector3.left * speed * Time.deltaTime;
                
                typeDex++;
                Debug.Log("probably worked. you typed: " + pressedChar);
            }
        }
        
    }
}
