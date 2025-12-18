using UnityEngine;

public class SpikeSideMove : MonoBehaviour
{
    public float moveRightDistance = 2f; // كم يروح يمين
    public float moveLeftDistance = 2f;  // كم يروح يسار
    public float speed = 2f;

    float startX;
    int direction = 1; // 1 = يمين, -1 = يسار

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        // حد اليمين
        if (transform.position.x >= startX + moveRightDistance)
        {
            direction = -1;
        }
        // حد اليسار
        else if (transform.position.x <= startX - moveLeftDistance)
        {
            direction = 1;
        }

        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
    }
}
