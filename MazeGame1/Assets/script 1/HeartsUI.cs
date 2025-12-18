using UnityEngine;
using UnityEngine.UI;

public class HeartsUI : MonoBehaviour
{
    public PlayerHealth playerHealth;

    [Header("Heart Images (عددها = عدد القلوب)")]
    public Image[] hearts;

    [Header("Heart Sprites")]
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    void Start()
    {
        if (playerHealth == null)
            playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.OnHPChanged += UpdateHearts;
            UpdateHearts(playerHealth.currentHP, playerHealth.maxHP);
        }
    }

    void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnHPChanged -= UpdateHearts;
    }

    void UpdateHearts(int currentHP, int maxHP)
    {
        // كل قلب = 2 HP
        for (int i = 0; i < hearts.Length; i++)
        {
            int heartHP = currentHP - (i * 2);

            if (heartHP >= 2)
                hearts[i].sprite = fullHeart;
            else if (heartHP == 1)
                hearts[i].sprite = halfHeart;
            else
                hearts[i].sprite = emptyHeart;

            hearts[i].enabled = true;
        }
    }
}
