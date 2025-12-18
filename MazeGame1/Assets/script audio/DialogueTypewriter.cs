using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueTypewriter : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI dialogueText;
    public float letterSpeed = 0.04f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip blipSound;

    public int playSoundEvery = 3;   // كل كم حرف
    [Range(0f, 1f)]
    public float volume = 0.25f;

    public bool ignoreSpaces = true;

    public bool IsTyping { get; private set; }

    Coroutine typingCo;

    void Awake()
    {
        if (dialogueText == null)
            dialogueText = GetComponent<TextMeshProUGUI>();

        if (audioSource != null)
        {
            audioSource.clip = blipSound;
            audioSource.loop = false;
            audioSource.playOnAwake = false;
            audioSource.volume = volume;
        }
    }

    public void TypeLine(string line)
    {
        StopTypingSound();

        if (typingCo != null)
            StopCoroutine(typingCo);

        typingCo = StartCoroutine(TypeRoutine(line));
    }

    public void FinishLine(string line)
    {
        if (typingCo != null)
            StopCoroutine(typingCo);

        StopTypingSound();
        IsTyping = false;

        if (dialogueText != null)
            dialogueText.text = line;
    }

    IEnumerator TypeRoutine(string fullText)
    {
        IsTyping = true;
        dialogueText.text = "";
        int count = 0;

        foreach (char c in fullText)
        {
            dialogueText.text += c;
            count++;

            bool isSpace = (c == ' ' || c == '\n' || c == '\r' || c == '\t');

            if (audioSource != null && blipSound != null && playSoundEvery > 0)
            {
                if ((!ignoreSpaces || !isSpace) && count % playSoundEvery == 0)
                {
                    // نشغل الصوت للحرف الحالي
                    audioSource.Stop();
                    audioSource.Play();
                }
            }

            yield return new WaitForSeconds(letterSpeed);
        }

        // ✅ انتهى اللاين → أوقف الصوت
        StopTypingSound();
        IsTyping = false;
    }

    void StopTypingSound()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();
    }
}
