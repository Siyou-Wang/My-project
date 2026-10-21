using UnityEngine;
using PhysicsGame;

public class StartGameTp : MonoBehaviour
{
    //private Transform startButton;
    public static bool startPG;
    public static bool startPhy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //startButton = GameObject.Find("startButton").transform;
        //Debug.Log("start");
        startPG = false;
        startPhy = false;
    
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (Input.GetMouseButtonDown(0))
        {
            
            if (hit.collider != null)
            {
                //Debug.Log("clicked1");
                if (hit.collider.gameObject.tag == "StartButton")
                {
                    Camera.main.transform.position = new Vector3(20, 0, -10);
                    startPG = false;
                    startPhy = false;
                    
                }
                else if (hit.collider.gameObject.tag == "ProgramingFile")
                {
                    Camera.main.transform.position = new Vector3(40, 0, -10);
                    AngerAndCalm.reset();
                    startPG = true;
                    CodeGamePuzzle.reset();
                }
                else if (hit.collider.gameObject.tag == "BackToScreen")
                {
                    Camera.main.transform.position = new Vector3(20, 0, -10);
                    startPhy = false;

                }
                else if (hit.collider.gameObject.tag == "Terminal")
                {
                    Camera.main.transform.position = new Vector3(20, -20, -10);
                    startPhy = false;
                }
                else if (hit.collider.gameObject.tag == "PhysicsFile")
                {
                    Camera.main.transform.position = new Vector3(80, -20, -10);
                    startPhy = true;
                }
            }
        }
    }
}
