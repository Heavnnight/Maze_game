using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clickSound;

    public void PlayClick()
    {
        if (source != null && clickSound != null)
            source.PlayOneShot(clickSound);
    }
}
