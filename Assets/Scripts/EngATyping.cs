using TMPro;
using UnityEngine;

public class EngATyping : MonoBehaviour
{
    [SerializeField] private TMP_Text label;              // the TextMeshPro text that scrolls
    [SerializeField] private float anchorX = 0f;          // where the current character sits (in the parent's space)
    [SerializeField] private float scrollSharpness = 15f; // higher = snappier slide
    [SerializeField] private string typedColor = "#7CFC7C";
    [SerializeField] private string currentColor = "#FFD84A";
    [SerializeField] private string upcomingColor = "#FFFFFF";

    private string sentence = "";
    private int progress = 0;
    private float targetX;
    private bool snapNext = true;
    private RectTransform rect;

    void Awake()
    {
        CacheRect();
    }

    void OnEnable()
    {
        Refresh(); // redraw if the object was inactive when the sentence was set
    }

    bool CacheRect()
    {
        if (label == null)
        {
            Debug.LogError("EngATyping: the Label slot is empty. Drag your TextMeshPro text object into it.", this);
            return false;
        }

        if (rect == null) rect = label.rectTransform;
        return true;
    }

    public void SetSentence(string newSentence)
    {
        sentence = newSentence;
        progress = 0;
        snapNext = true; // jump to the start of the new sentence instead of sliding
        Refresh();
    }

    public void SetProgress(int typedCount)
    {
        progress = typedCount;
        Refresh();
    }

    void Refresh()
    {
        if (!CacheRect()) return;

        if (string.IsNullOrEmpty(sentence)) { label.text = ""; return; }

        progress = Mathf.Clamp(progress, 0, sentence.Length);

        string typed = sentence.Substring(0, progress);
        string current = progress < sentence.Length ? sentence.Substring(progress, 1) : "";
        string upcoming = progress < sentence.Length ? sentence.Substring(progress + 1) : "";

        label.text = $"<color={typedColor}>{typed}</color><color={currentColor}>{current}</color><color={upcomingColor}>{upcoming}</color>";
        label.ForceMeshUpdate();
        Debug.Log($"Prompt text='{label.text}' chars={label.textInfo.characterCount} pos={rect.anchoredPosition} target={targetX} activeInHierarchy={label.gameObject.activeInHierarchy}");

        int count = label.textInfo.characterCount;
        if (count == 0) return;

        int i = Mathf.Min(progress, count - 1);
        targetX = anchorX - label.textInfo.characterInfo[i].origin;

        if (snapNext)
        {
            Vector2 p = rect.anchoredPosition;
            p.x = targetX;
            rect.anchoredPosition = p;
            snapNext = false;
        }
    }

    void Update()
    {
        if (rect == null) return;

        Vector2 p = rect.anchoredPosition;
        p.x = Mathf.Lerp(p.x, targetX, 1f - Mathf.Exp(-scrollSharpness * Time.deltaTime));
        rect.anchoredPosition = p;
    }
}