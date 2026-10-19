using UnityEngine;

public class EngAGroundReveal : MonoBehaviour
{
    [SerializeField] private BackgroundScroll backgroundScroll; // drag the object with BackgroundScroll here
    [SerializeField] private bool hideSpriteAfterClick = true;

    void OnMouseDown()
    {
        backgroundScroll.SetGroundVisible(true);

        if (hideSpriteAfterClick)
            gameObject.SetActive(false);
    }
}
