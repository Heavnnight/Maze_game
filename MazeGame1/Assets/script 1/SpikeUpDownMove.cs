using UnityEngine;

public class SpikeUpDownMove : MonoBehaviour
{
    public float moveUpDistance = 2f;    // كم يطلع فوق
    public float moveDownDistance = 2f;  // كم ينزل تحت
    public float speed = 2f;

    float startY;
    int direction = 1; // 1 = فوق, -1 = تحت

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        // حد فوق
        if (transform.position.y >= startY + moveUpDistance)
        {
            direction = -1;
        }
        // حد تحت
        else if (transform.position.y <= startY - moveDownDistance)
        {
            direction = 1;
        }

        transform.Translate(Vector3.up * direction * speed * Time.deltaTime);
    }
}
