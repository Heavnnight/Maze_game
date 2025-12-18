using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletHomingSimple : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 4f;

    [Header("Damage (1 = نصف قلب إذا كل قلب = 2 HP)")]
    public int damage = 1;

    [Header("Homing Settings")]
    public bool homing = true;
    public float turnSpeed = 8f;

    [Header("Auto Target (اذا البوس ما نادى SetTarget)")]
    public bool autoFindPlayer = true;

    [Header("Wall Tag")]
    public string wallTag = "wall";

    private Transform target;
    private Vector2 moveDir;
    private float timer;
    private bool started = false;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void OnEnable()
    {
        timer = 0f;
        started = false;
    }

    void Start()
    {
        // ✅ لو البوس نسي ينادي SetTarget، نلقط اللاعب تلقائيًا
        if (autoFindPlayer && target == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) SetTarget(p.transform);
        }
    }

    public void SetTarget(Transform t)
    {
        target = t;

        if (target != null)
            moveDir = ((Vector2)target.position - rb.position).normalized;
        else
            moveDir = Vector2.right;

        started = true;

        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        rb.MoveRotation(angle);
    }

    public void SetFixedDirection(Vector2 dir)
    {
        target = null;
        homing = false;

        moveDir = dir.normalized;
        started = true;

        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        rb.MoveRotation(angle);
    }

    void FixedUpdate()
    {
        // ✅ لو ما بدأ، نحاول مرة ثانية نلقط اللاعب
        if (!started)
        {
            if (autoFindPlayer && target == null)
            {
                GameObject p = GameObject.FindGameObjectWithTag("Player");
                if (p != null) SetTarget(p.transform);
            }
            return;
        }

        if (homing && target != null)
        {
            Vector2 desiredDir = ((Vector2)target.position - rb.position).normalized;
            moveDir = Vector2.Lerp(moveDir, desiredDir, turnSpeed * Time.fixedDeltaTime).normalized;

            float ang = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            rb.MoveRotation(ang);
        }

        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);

        timer += Time.fixedDeltaTime;
        if (timer >= lifeTime) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null) ph.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        if (!string.IsNullOrEmpty(wallTag) && other.CompareTag(wallTag))
        {
            Destroy(gameObject);
        }
    }
}

