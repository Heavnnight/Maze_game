using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public AudioClip pickupSound;   // 👈 اسحبي ملف الصوت هنا (مو AudioSource)
    public float volume = 1f;

    bool picked = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (picked) return;
        if (!other.CompareTag("Player")) return;

        picked = true;

        // ✅ يشغل الصوت حتى لو اختفى السلاح
        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position, volume);

        // اخفاء السلاح
        var sr = GetComponent<SpriteRenderer>();
        if (sr) sr.enabled = false;

        var col = GetComponent<Collider2D>();
        if (col) col.enabled = false;

        Destroy(gameObject);
    }
}

