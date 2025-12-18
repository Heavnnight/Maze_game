using UnityEngine;
using TMPro;

public class NPCDialogueSpace : MonoBehaviour
{
    public GameObject dialogueBox;        // Panel
    public TMP_Text dialogueText;         // Text TMP
    public DialogueTypewriter typewriter; // اسحبي سكربت الـTypewriter هنا

    [TextArea(2, 5)]
    public string[] lines;

    private int index = 0;
    private bool started = false;
    private bool isShowing = false;

    void Start()
    {
        if (dialogueBox != null)
            dialogueBox.SetActive(false);
    }

    void Update()
    {
        if (isShowing && Input.GetKeyDown(KeyCode.Space))
        {
            // إذا لسه يكتب → كمّلي السطر فورًا
            if (typewriter != null && typewriter.IsTyping)
            {
                typewriter.FinishLine(lines[index]);
                return;
            }

            // إذا السطر خلص → روحي للي بعده
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
        isShowing = true;

        ShowLine();
    }

    void ShowLine()
    {
        if (typewriter != null)
            typewriter.TypeLine(lines[index]);
        else
            dialogueText.text = lines[index]; // احتياط لو ما ربطتي typewriter
    }

    void NextLine()
    {
        index++;

        if (index >= lines.Length)
        {
            HideDialogue();
            return;
        }

        ShowLine();
    }

    void HideDialogue()
    {
        if (dialogueBox != null)
            dialogueBox.SetActive(false);

        isShowing = false;
    }
}
