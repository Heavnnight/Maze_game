using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;   // قدّري السرعة من الانسبكتور

    void Update()
    {
        // قراءة الحركة من الكيبورد (WASD + الأسهم)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // اتجاه الحركة
        Vector3 direction = new Vector3(moveX, moveY, 0f).normalized;

        // تحريك اللاعب
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
