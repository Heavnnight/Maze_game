using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject gameOverPanel;

    [Header("Music")]
    public AudioSource bossMusic;      // ميوزك الفايت
    public AudioSource gameOverMusic;  // ميوزك الموت

    [Header("Menu Scene")]
    public string menuSceneName = "MainMenu";

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void ShowGameOver()
    {
        // ✅ أظهر البانل
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            gameOverPanel.transform.SetAsLastSibling();
        }

        // ✅ وقف ميوزك الفايت
        if (bossMusic != null && bossMusic.isPlaying)
            bossMusic.Stop();

        // ✅ شغل ميوزك الموت
        if (gameOverMusic != null && !gameOverMusic.isPlaying)
            gameOverMusic.Play();

        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }
}
