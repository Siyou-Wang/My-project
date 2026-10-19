using UnityEngine;

public class EngAGroundScroll : MonoBehaviour
{
    private Transform ground1;   // LEFT tile
    private Transform ground2;   // RIGHT tile, directly touching ground1

    private float groundWidth;
    private Camera cam;

    [SerializeField] private float baseSpeed = 0.5f;
    [SerializeField] private float speedPerCorrectChar = 0.05f;
    [SerializeField] private float glideSpeed = 10f;

    [SerializeField] private EngATyping prompt; // drag the object with TypingPrompt here
    [SerializeField] private string[] sentences =
    {
        "Quoth the Raven, \"Nevermore.\"",
        "All that we see or seem is but a dream within a dream."
    };

    private float speed;
    private int streak = 0;
    private float pendingDistance = 0f;

    private string typeAnsw = "";
    private int typeDex = 0;
    private int sentenceIndex = 0;

    void Start()
    {
        Debug.Log("BackgroundScroll Start running");
        GameObject g1 = GameObject.Find("EngAGround1");
        GameObject g2 = GameObject.Find("EngAGround2");

        if (g1 == null || g2 == null)
        {
            Debug.LogError("BackgroundScroll: couldn't find EngAGround1 or EngAGround2. Check the object names.");
            enabled = false;
            return;
        }

        if (sentences == null || sentences.Length == 0)
        {
            Debug.LogError("BackgroundScroll: add at least one sentence in the Inspector.");
            enabled = false;
            return;
        }

        ground1 = g1.transform;
        ground2 = g2.transform;
        cam = Camera.main;

        Renderer r = g1.GetComponent<Renderer>();
        groundWidth = (r != null) ? r.bounds.size.x : Mathf.Abs(ground2.position.x - ground1.position.x);

        speed = baseSpeed;

        LoadSentence(0);
    }

    void Update()
    {
        HandleTyping();
        ScrollGround();
    }

    // Load a sentence from the list (wraps around at the end)
    void LoadSentence(int index)
    {
        sentenceIndex = index % sentences.Length;
        SetSentence(sentences[sentenceIndex]);
    }

    // Call this from other scripts to set any custom sentence
    public void SetSentence(string newSentence)
    {
        Debug.Log("SetSentence called. prompt is null? " + (prompt == null));
        typeAnsw = newSentence;
        typeDex = 0;
        if (prompt != null) prompt.SetSentence(typeAnsw);
    }

    void HandleTyping()
    {
        if (string.IsNullOrEmpty(typeAnsw)) return;

        foreach (char pressedChar in Input.inputString)
        {
            if (char.IsControl(pressedChar)) continue;

            if (pressedChar == typeAnsw[typeDex])
            {
                pendingDistance += speed;
                streak++;
                speed = baseSpeed + streak * speedPerCorrectChar;
                typeDex++;

                if (typeDex >= typeAnsw.Length)
                    LoadSentence(sentenceIndex + 1); // finished: next sentence, streak keeps going
                else if (prompt != null)
                    prompt.SetProgress(typeDex);
            }
            else
            {
                typeDex = 0;
                streak = 0;
                speed = baseSpeed;
                pendingDistance = 0f;
                if (prompt != null) prompt.SetProgress(0);
            }
        }
    }

    void ScrollGround()
    {
        if (pendingDistance <= 0f) return;

        float step = Mathf.Min(pendingDistance, glideSpeed * Time.deltaTime);
        pendingDistance -= step;

        Vector3 move = Vector3.left * step;
        ground1.position += move;
        ground2.position += move;

        float leftEdge = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;

        while (ground1.position.x + groundWidth / 2f < leftEdge)
        {
            ground1.position = new Vector3(ground2.position.x + groundWidth, ground1.position.y, ground1.position.z);

            Transform temp = ground1;
            ground1 = ground2;
            ground2 = temp;
        }
    }
}