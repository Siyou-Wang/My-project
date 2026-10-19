using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private readonly List<Transform> tiles = new List<Transform>(); // ordered left -> right
    private float groundWidth;
    private Camera cam;

    [SerializeField] private float baseSpeed = 0.5f;            // distance per correct char at streak 0
    [SerializeField] private float speedPerCorrectChar = 0.05f; // extra distance per char for each streak point
    [SerializeField] private float glideSpeed = 10f;            // how fast the ground slides to cover pending distance
    [SerializeField] private bool startHidden = true;

    private float speed;
    private int streak = 0;
    private float pendingDistance = 0f;

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

        cam = Camera.main;

        Renderer r = g1.GetComponent<Renderer>();
        groundWidth = (r != null) ? r.bounds.size.x : Mathf.Abs(g2.transform.position.x - g1.transform.position.x);

        float leftEdge = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        float rightEdge = cam.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
        float screenWidth = rightEdge - leftEdge;

        // Enough tiles to cover the screen, plus extras so the right side is never bare
        int tileCount = Mathf.CeilToInt(screenWidth / groundWidth) + 2;

        // Start the row so the first tile's left edge is at or past the left screen edge
        float startX = g1.transform.position.x;
        while (startX - groundWidth / 2f > leftEdge)
            startX -= groundWidth;

        float y = g1.transform.position.y;
        float z = g1.transform.position.z;

        tiles.Add(g1.transform);
        tiles.Add(g2.transform);
        for (int i = 2; i < tileCount; i++)
        {
            GameObject clone = Instantiate(g1, g1.transform.parent);
            clone.name = "EngAGround" + (i + 1);
            tiles.Add(clone.transform);
        }

        // Lay them out edge to edge
        for (int i = 0; i < tiles.Count; i++)
            tiles[i].position = new Vector3(startX + i * groundWidth, y, z);

        speed = baseSpeed;

        // Hide only after measuring and cloning
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
                pendingDistance += speed;
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
        if (pendingDistance <= 0f) return;

        float step = Mathf.Min(pendingDistance, glideSpeed * Time.deltaTime);
        pendingDistance -= step;

        Vector3 move = Vector3.left * step;
        foreach (Transform t in tiles)
            t.position += move;

        float leftEdge = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;

        // Recycle the leftmost tile to the right end once it's fully off-screen
        while (tiles[0].position.x + groundWidth / 2f < leftEdge)
        {
            Transform first = tiles[0];
            Transform last = tiles[tiles.Count - 1];

            first.position = new Vector3(last.position.x + groundWidth, first.position.y, first.position.z);

            tiles.RemoveAt(0);
            tiles.Add(first);
        }
    }

    public void SetGroundVisible(bool visible)
    {
        foreach (Transform t in tiles)
        {
            Renderer r = t.GetComponent<Renderer>();
            if (r != null) r.enabled = visible;
        }
    }
}