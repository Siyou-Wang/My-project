using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using PhysicsGame;

public class StartGameTp : MonoBehaviour
{
    // private Transform startButton;
    public static bool StartRpg;
    public static bool StartEng;
    public static bool StartPhys;

    public static readonly Vector3 MainScreen = new (20, 0, -10);
    
    private static readonly Vector3 CodeScreen = new (40, 0, -10);
    private static readonly Vector3 TerminalScreen = new (20, -20, -10);
    private static readonly Vector3 EnglishScreen = new (60, 0, -10);
    private static readonly Vector3 PhysicsScreen = new (80, -20, -10);
    private static readonly Vector3 JokeScreen = new (60, 60, -10);

    private Transform _rpgFile;
    private Transform _physFile;
    private Transform _engFile;

    private BoxCollider2D _rpgBox;
    private BoxCollider2D _physBox;
    private BoxCollider2D _engBox;

    private SpriteRenderer _rpgSprite;
    private SpriteRenderer _physSprite;
    private SpriteRenderer _engSprite;

    private VideoPlayer player;
    private bool videoFinished = false;

    private DialogueManager dialogueManager;
    private bool dialogueFinished = true;
    private string[] message = new string[1];

    private static int count;

    private bool _gameWon;

    private Camera _camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // startButton = GameObject.Find("startButton").transform;
        // Debug.Log("start");
        _camera = Camera.main;
        
        _rpgFile = GameObject.Find("Programing File").transform;
        _physFile = GameObject.Find("PhysicsFile").transform;
        _engFile = GameObject.Find("EngAButton").transform;
        
        _rpgBox = _rpgFile.GetComponent<BoxCollider2D>();
        _physBox = _physFile.GetComponent<BoxCollider2D>();
        _engBox = _engFile.GetComponent<BoxCollider2D>();
        
        _rpgSprite = _rpgFile.GetComponent<SpriteRenderer>();
        _physSprite = _physFile.GetComponent<SpriteRenderer>();
        _engSprite = _engFile.GetComponent<SpriteRenderer>();

        player = GameObject.Find("Tutorial").GetComponent<VideoPlayer>();
        player.isLooping = false;
        
        StartRpg = false;
        StartEng = false;
        StartPhys = false;

        dialogueManager = FindFirstObjectByType<DialogueManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!dialogueFinished)
        {
            dialogueManager.ShowBox(message);
        }
        
        if (!dialogueFinished && Input.GetKeyDown(KeyCode.Mouse1))
        {
            dialogueFinished = true;
        }

        if (!_gameWon && RPGGameManager.won && EngAGroundScroll.won && PhysicsMoodManager.Won)
        {
            _gameWon = true;
            _camera.transform.position = new Vector3(80, 80, -10);
            message[0] = "Finally...";
            dialogueFinished = false;
        }

        if (RPGGameManager.won) _rpgBox.enabled = false;
        if (PhysicsMoodManager.Won) _physBox.enabled = false;
        if (EngAGroundScroll.won) _engBox.enabled = false;

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
            message[0] = "I should start on the assignments...man I played games for way too long. Oh shoot, its all due tonight!?!?";
            dialogueFinished = false;
            player.Play();
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
            message[0] = "Oops...was I supposed to touch this?";
            dialogueFinished = false;
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

        else if (hit.collider.CompareTag("Trash"))
        {
            count++;
            if(count == 1)
            {
                _rpgBox.enabled = false;
                _rpgSprite.enabled = false;

            }
            if(count == 2)
            {
                _physBox.enabled = false;
                _physSprite.enabled = false;
            }
            if(count >= 3)
            {
                _engBox.enabled = false;
                _engSprite.enabled = false;
                
                _camera.transform.position = JokeScreen;
                
            }
        }
        player.loopPointReached += EndReached;
    }
    void EndReached(VideoPlayer vp)
    {
        vp.gameObject.SetActive(false);
        videoFinished = true;
    }
}
