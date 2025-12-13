using UnityEngine;

public class BulletHomingSimple : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 3f;

    private Transform target;
    private float timer;

    public void SetTarget(Transform t)
    {
        target = t;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            Destroy(gameObject);
            return;
        }

        if (target == null) return;

        // اتجاه من الرصاصة → اللاعب
        Vector2 dir = (target.position - transform.position).normalized;

        // حركتها مباشرة باتجاه اللاعب
        transform.position += (Vector3)(dir * speed * Time.deltaTime);

        // نخلي الرصاصة تلف شوي لتواجه اللاعب (اختياري)
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
