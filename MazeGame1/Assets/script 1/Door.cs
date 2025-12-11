using UnityEngine;
using UnityEngine.SceneManagement; // لو تبين ينقل لمشهد جديد

public class Door : MonoBehaviour
{
    public GameObject doorObject;   // جسم الباب (sprite/collider)
    public string nextSceneName;    // اسم المشهد اللي تبين تروحين له (اختياري)
    private bool isOpen = false;    // عشان ما يتكرر الفتح

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // نحاول نجيب PlayerInventory من اللاعب
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        if (inventory == null)
        {
            Debug.LogWarning("No PlayerInventory found on Player!");
            return;
        }

        if (inventory.hasKey)
        {
            // معه مفتاح → نفتح الباب لو مو مفتوح من قبل
            if (!isOpen)
            {
                isOpen = true;

                if (doorObject != null)
                {
                    doorObject.SetActive(false); // يشيل الباب (sprite + collider لو موجودين عليه)
                }

                Debug.Log("Door opened!");
            }

            // لو حاطة اسم مشهد → ننقلك له
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
        else
        {
            Debug.Log("You need a key!");
        }
    }
}
