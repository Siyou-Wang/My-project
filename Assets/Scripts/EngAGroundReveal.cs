using UnityEngine;

public class EngAGroundReveal : MonoBehaviour
{
    [SerializeField] private BackgroundScroll backgroundScroll; // drag the object with BackgroundScroll here
    [SerializeField] private bool hideSpriteAfterClick = true;

    void OnMouseDown()
    {
        if (EngATP.isCalled) {
            backgroundScroll.SetGroundVisible(true);

            if (hideSpriteAfterClick)
                gameObject.SetActive(false);
        }
    }
}
