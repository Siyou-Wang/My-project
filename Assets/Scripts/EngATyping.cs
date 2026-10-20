using TMPro;
using UnityEngine;

public class EngATyping : MonoBehaviour
{
    [SerializeField] private TMP_Text label;              // the TextMeshPro text that scrolls
    [SerializeField] private float scrollSharpness = 15f; // higher = snappier slide
    [SerializeField] private string typedColor = "#7CFC7C";
    [SerializeField] private string currentColor = "#FFD84A";
    [SerializeField] private string upcomingColor = "#FFFFFF";
    [SerializeField] private bool debugLog = true;

    private string sentence = "";
    private int progress = 0;
    private float baseX;      // where you placed the text in the editor
    private float targetX;
    private bool snapNext = true;
    private bool needsRefresh = false;
    private RectTransform rect;

    void Awake()
    {
        CacheRect();
    }

    void OnEnable()
    {
        Refresh();
    }

    bool CacheRect()
    {
        if (label == null)
        {
            Debug.LogError("EngATyping: the Label slot is empty. Drag your TextMeshPro text object into it.", this);
            return false;
        }

        if (rect == null)
        {
            rect = label.rectTransform;
            baseX = rect.anchoredPosition.x; // remember the editor position
            targetX = baseX;
        }
        return true;
    }

    public void SetSentence(string newSentence)
    {
        sentence = newSentence;
        progress = 0;
        snapNext = true;
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

        if (!label.gameObject.activeInHierarchy)
        {
            if (debugLog) Debug.Log("EngATyping: text object is INACTIVE, waiting until it is enabled");
            needsRefresh = true;
            return;
        }

        needsRefresh = false;
        label.ForceMeshUpdate();

        int count = label.textInfo.characterCount;
        if (count == 0) return;

        int i = Mathf.Min(progress, count - 1);
        float shift = label.textInfo.characterInfo[i].origin - label.textInfo.characterInfo[0].origin;
        targetX = baseX - shift; // keeps the current character where the first one started

        if (snapNext)
        {
            Vector2 p = rect.anchoredPosition;
            p.x = targetX;
            rect.anchoredPosition = p;
            snapNext = false;
        }

        if (debugLog)
            Debug.Log($"EngATyping: chars={count} baseX={baseX} targetX={targetX} pos={rect.anchoredPosition} " +
                      $"canvas={(label.canvas != null ? label.canvas.renderMode.ToString() : "NONE")}");
    }

    void Update()
    {
        if (rect == null) return;

        if (needsRefresh) Refresh();

        Vector2 p = rect.anchoredPosition;
        p.x = Mathf.Lerp(p.x, targetX, 1f - Mathf.Exp(-scrollSharpness * Time.deltaTime));
        rect.anchoredPosition = p;
    }
}