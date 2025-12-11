using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger with: " + other.name); // نعرف مين دخل على التريقر

        if (!other.CompareTag("Player"))
        {
            Debug.Log("Not the player, ignoring.");
            return;
        }

        // نخلي اللاعب يعرف إنه أخذ المفتاح
        PlayerInventory inv = other.GetComponent<PlayerInventory>();

        if (inv == null)
        {
            Debug.LogWarning("No PlayerInventory found on: " + other.name);
            return;
        }

        inv.hasKey = true;
        Debug.Log("Player picked up the key! hasKey = true");

        // نخفي المفتاح أو ندمره
        Destroy(gameObject);
    }
}
