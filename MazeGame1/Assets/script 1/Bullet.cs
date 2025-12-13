using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 3f;

    private float timer;

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= lifeTime)
            Destroy(gameObject);
    }
}

