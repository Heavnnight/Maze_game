using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip deathSound;

    public SpriteRenderer sprite;
    public float blinkInterval = 0.1f;
    public int blinkCount = 6;

    bool isDead = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.collider.CompareTag("wall"))
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        isDead = true;

        // 🔊 صوت الموت
        if (audioSource && deathSound)
            audioSource.PlayOneShot(deathSound);

        // ✨ وميض
        for (int i = 0; i < blinkCount; i++)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(blinkInterval);
            sprite.enabled = true;
            yield return new WaitForSeconds(blinkInterval);
        }

        // 🔄 ريستارت
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
