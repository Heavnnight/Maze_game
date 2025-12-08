using UnityEngine;
using TMPro;

public class NPCDialogueSpace : MonoBehaviour
{
    public GameObject dialogueBox;   // البوكس (Panel)
    public TMP_Text dialogueText;    // النص داخل البوكس

    [TextArea(2, 5)]
    public string[] lines;           // الجمل

    private int index = 0;
    private bool started = false;    // لأول مرة فقط
    private bool isShowing = false;  // هل الحوار مفتوح الآن؟

    void Start()
    {
        if (dialogueBox != null)
            dialogueBox.SetActive(false);
    }

    void Update()
    {
        // إذا الحوار مفتوح واللاعب ضغط Space → نروح للجملة اللي بعدها
        if (isShowing && Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!started && other.CompareTag("Player"))
        {
            started = true;
            StartDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideDialogue();
        }
    }

    void StartDialogue()
    {
        if (lines == null || lines.Length == 0 || dialogueBox == null || dialogueText == null)
            return;

        index = 0;
        dialogueBox.SetActive(true);
        dialogueText.text = lines[index];
        isShowing = true;
    }

    void NextLine()
    {
        index++;

        if (index >= lines.Length)
        {
            HideDialogue();
            return;
        }

        dialogueText.text = lines[index];
    }

    void HideDialogue()
    {
        if (dialogueBox != null)
            dialogueBox.SetActive(false);

        isShowing = false;
    }
}
