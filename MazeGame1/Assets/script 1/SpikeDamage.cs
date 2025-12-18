
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player died");
            // هنا حطي موت اللاعب أو ريسباون
        }
    }
}

