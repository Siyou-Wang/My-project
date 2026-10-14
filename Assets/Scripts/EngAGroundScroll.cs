using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private Transform ground1;
    private Transform ground2;
    private Transform tempGround;
    private Transform player;

    private float speed = 0.5f;

    private string typeAnsw = "The quick brown fox jumped over the lazy dogs.";
    private int typeDex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ground1 = GameObject.Find("EngAGround1").transform;
        ground2 = GameObject.Find("EngAGround2").transform;
        player = GameObject.Find("EngAPlayer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Speeds up depending on how many correct characters are typed 
        speed += typeDex / 200.0f;

        // If something is typed 
        if (Input.inputString.Length > 0)
        {
            char pressedChar = Input.inputString[0];
            char targetChar = typeAnsw[typeDex];
            Debug.Log("you typed: " + pressedChar);

            if (pressedChar == targetChar)
            {
                ground1.position += Vector3.left * speed * Time.deltaTime;
                ground2.position += Vector3.left * speed * Time.deltaTime;
                
                // Reset ground to infinite loop 
                if (ground1.position.x < 56)
                {
                    ground1.position = new Vector3(ground2.position.x + 10f, ground1.position.y, ground1.position.z);
                    tempGround = ground1;
                    ground1 = ground2;
                    ground2 = tempGround;
                }
                
                typeDex++;
                Debug.Log("you (correctly) typed: " + pressedChar);
            }
            // Reset speed/char looked for when something is incorrect 
            else
            {
                typeDex = 0;
                speed = 0.5f;
            }
        }
        
    }
}
