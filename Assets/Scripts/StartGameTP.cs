using UnityEngine;

public class StartGameTp : MonoBehaviour
{
    // private Transform startButton;
    public static bool StartRpg;
    public static bool StartEng;
    public static bool StartPhys;

    private static readonly Vector3 MainScreen = new (20, 0, -10);
    private static readonly Vector3 CodeScreen = new (40, 0, -10);
    private static readonly Vector3 TerminalScreen = new (20, -20, -10);
    private static readonly Vector3 EnglishScreen = new (60, 0, -10);
    private static readonly Vector3 PhysicsScreen = new (80, -20, -10);

    private DialogueManager dialogueManager;
    private bool dialogueFinished = true;
    private string[] message = new string[1];

    private Camera _camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // startButton = GameObject.Find("startButton").transform;
        // Debug.Log("start");
        _camera = Camera.main;
        
        StartRpg = false;
        StartEng = false;
        StartPhys = false;

        dialogueManager = FindFirstObjectByType<DialogueManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        var mousePos2D = new Vector2(mousePos.x, mousePos.y);
        var hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider == null) return;
        
        // Debug.Log("clicked1");
        if (hit.collider.CompareTag("StartButton"))
        {
            _camera.transform.position = MainScreen;
            
            StartRpg = false;
            StartEng = false;
            StartPhys = false;
        }
        
        else if (hit.collider.CompareTag("BackToScreen"))
        {
            _camera.transform.position = MainScreen;
            
            StartRpg = false;
            StartPhys = false;
            StartEng = false;
        }
        
        else if (hit.collider.CompareTag("Terminal"))
        {
            _camera.transform.position = TerminalScreen;
            
            StartRpg = false;
            StartEng = false;
            StartPhys = false;
        }
        
        else if (hit.collider.CompareTag("EngATP"))
        {
            _camera.transform.position = EnglishScreen;

            AngerAndCalm.reset();
            
            StartEng = true;
            StartRpg = false;
            StartPhys = false;
        }
        
        else if (hit.collider.CompareTag("ProgramingFile"))
        {
            _camera.transform.position = CodeScreen;
            
            AngerAndCalm.reset();
            CodeGamePuzzle.reset();
            
            StartRpg = true;
            StartEng = false;
            StartPhys = false;
        }
        
        else if (hit.collider.CompareTag("PhysicsFile"))
        {
            _camera.transform.position = PhysicsScreen;

            AngerAndCalm.reset();
            
            StartPhys = true;
            StartRpg = false;
            StartEng = false;
        }
    }
}
