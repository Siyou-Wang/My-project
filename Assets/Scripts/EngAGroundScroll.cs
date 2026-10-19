using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private Transform ground1;   // LEFT tile
    private Transform ground2;   // RIGHT tile, directly touching ground1

    private float groundWidth;
    private Camera cam;

    [SerializeField] private float baseSpeed = 0.5f;            // distance per correct char at streak 0
    [SerializeField] private float speedPerCorrectChar = 0.05f; // extra distance per char for each streak point
    [SerializeField] private float glideSpeed = 10f;            // how fast the ground slides to cover pending distance
    [SerializeField] private bool startHidden = true;           // ground is invisible until revealed

    private float speed;
    private int streak = 0;
    private float pendingDistance = 0f; // distance still to scroll

    private readonly string typeAnsw = "The quick brown fox jumped over the lazy dogs.";
    private int typeDex = 0;

    void Start()
    {
        GameObject g1 = GameObject.Find("EngAGround1");
        GameObject g2 = GameObject.Find("EngAGround2");

        if (g1 == null || g2 == null)
        {
            Debug.LogError("BackgroundScroll: couldn't find EngAGround1 or EngAGround2. Check the object names.");
            enabled = false;
            return;
        }

        ground1 = g1.transform;
        ground2 = g2.transform;
        cam = Camera.main;

        // Measure BEFORE hiding, since a disabled renderer can report unreliable bounds
        Renderer r = g1.GetComponent<Renderer>();
        groundWidth = (r != null) ? r.bounds.size.x : Mathf.Abs(ground2.position.x - ground1.position.x);

        speed = baseSpeed;

        SetGroundVisible(!startHidden);
    }

    void Update()
    {
        HandleTyping();
        ScrollGround();
    }

    void HandleTyping()
    {
        foreach (char pressedChar in Input.inputString)
        {
            if (char.IsControl(pressedChar)) continue; // ignore backspace/enter

            if (pressedChar == typeAnsw[typeDex])
            {
                pendingDistance += speed; // each correct char queues up movement
                streak++;
                typeDex = (typeDex + 1) % typeAnsw.Length;
                speed = baseSpeed + streak * speedPerCorrectChar;
                Debug.Log("you (correctly) typed: " + pressedChar);
            }
            else
            {
                typeDex = 0;
                streak = 0;
                speed = baseSpeed;
                Debug.Log("wrong key: " + pressedChar);
            }
        }
    }

    void ScrollGround()
    {
        if (pendingDistance <= 0f) return; // nothing typed correctly, so no movement

        float step = Mathf.Min(pendingDistance, glideSpeed * Time.deltaTime);
        pendingDistance -= step;

        Vector3 move = Vector3.left * step;
        ground1.position += move;
        ground2.position += move;

        float leftEdge = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;

        // When the left tile is fully off-screen, put it behind the other tile
        while (ground1.position.x + groundWidth / 2f < leftEdge)
        {
            ground1.position = new Vector3(ground2.position.x + groundWidth, ground1.position.y, ground1.position.z);

            Transform temp = ground1;
            ground1 = ground2;
            ground2 = temp;
        }
    }

    // Called by the clickable sprite
    public void SetGroundVisible(bool visible)
    {
        SetRendererEnabled(ground1, visible);
        SetRendererEnabled(ground2, visible);
    }

    private void SetRendererEnabled(Transform t, bool visible)
    {
        Renderer r = t.GetComponent<Renderer>();
        if (r != null) r.enabled = visible;
    }
}