using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnDoor : MonoBehaviour
{
    public string nextScene = "Level 3"; // نحط اسم السين مباشرة

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
