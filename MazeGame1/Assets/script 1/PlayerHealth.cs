using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("HP (قلوب)")]
    [Tooltip("إذا عندك 4 قلوب ونص قلب: خليها 8. (عدد القلوب * 2)")]
    public int maxHP = 8;
    public int currentHP;

    public System.Action<int, int> OnHPChanged; // current, max

    [Header("Invincibility / Blink")]
    public float invincibleTime = 0.6f;   // مدة الحماية بعد الضربة
    public float blinkInterval = 0.08f;   // سرعة الوميض
    public SpriteRenderer[] renderers;    // إذا اللاعب فيه أكثر من Sprite

    [Header("Game Over")]
    public GameOverUI gameOverUI;         // اسحبي سكربت الـUI هنا من الـCanvas

    bool invincible = false;
    bool dead = false;
    Coroutine blinkRoutine;

    void Awake()
    {
        if (renderers == null || renderers.Length == 0)
            renderers = GetComponentsInChildren<SpriteRenderer>(true);

        currentHP = maxHP;
        OnHPChanged?.Invoke(currentHP, maxHP);
    }

    public void TakeDamage(int dmg)
    {
        if (dmg <= 0) return;
        if (invincible) return;
        if (dead) return;

        currentHP -= dmg;
        if (currentHP < 0) currentHP = 0;

        OnHPChanged?.Invoke(currentHP, maxHP);

        if (blinkRoutine != null) StopCoroutine(blinkRoutine);
        blinkRoutine = StartCoroutine(InvincibleBlink());

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        dead = true;
        Debug.Log("Player Dead");

        // (اختياري) طفي الكولايدر عشان ما يجي دامج زيادة
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // (اختياري) طفي Rigidbody حركة اللاعب
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        // طلّع شاشة الخسارة
        if (gameOverUI != null)
            gameOverUI.ShowGameOver();
        else
            Debug.LogError("GameOverUI not assigned on PlayerHealth!");
    }

    IEnumerator InvincibleBlink()
    {
        invincible = true;

        float t = 0f;
        bool visible = true;

        while (t < invincibleTime)
        {
            visible = !visible;

            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i] != null)
                    renderers[i].enabled = visible;
            }

            yield return new WaitForSeconds(blinkInterval);
            t += blinkInterval;
        }

        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] != null)
                renderers[i].enabled = true;
        }

        invincible = false;
        blinkRoutine = null;
    }
}
