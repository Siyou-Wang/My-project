using UnityEngine;

public class EngPlayerAni : MonoBehaviour
{
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EngAGroundScroll.isTyping)
        {
            animator.Play("EngDefault");
        }
        else if (!EngAGroundScroll.isTyping)
        {
            animator.Play("EngStop");
        }
    }
}
