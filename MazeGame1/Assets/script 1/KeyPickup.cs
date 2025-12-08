using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // نخلي اللاعب يعرف إنه أخذ المفتاح
            other.GetComponent<PlayerInventory>().hasKey = true;

            // نخفي المفتاح أو ندمره
            Destroy(gameObject);
        }
    }
}
