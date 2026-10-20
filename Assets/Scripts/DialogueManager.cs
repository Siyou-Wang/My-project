using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject background;
    public Text dText;
    private string[] strings;
    private int count = -1;

    bool dialogueActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            if(count == strings.Length)
            {
                background.SetActive(false);
                dialogueActive = false;
                count = 0;
            }
            dText.text = strings[count];
            count++;
        }
    }

    public bool ShowBox(string[] dialogue)
    {
        dialogueActive = true;
        background.SetActive(true);
        dText.text = dialogue[0];
        strings = dialogue;
        return true;
    }
}
