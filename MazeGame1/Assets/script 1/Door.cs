using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject doorObject;  // الشيء اللي يمثل الباب (Sprite/Collider)

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory.hasKey)
            {
                // اللاعب معه مفتاح → نفتح الباب
                doorObject.SetActive(false);
            }
            else
            {
                Debug.Log("You need a key!");
            }
        }
    }
}

