using UnityEngine;

public class SpikeSideMove : MonoBehaviour
{
    public float moveDistance = 2f;
    public float speed = 2f;

    private int direction = 1;
    private float startX;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        if (transform.position.x >= startX + moveDistance)
            direction = -1;
        else if (transform.position.x <= startX - moveDistance)
            direction = 1;

        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
    }
}
