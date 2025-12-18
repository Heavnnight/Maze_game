using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource audioSource;
    public float stepInterval = 0.4f; // كل كم ثانية صوت خطوة

    float stepTimer;

    void Update()
    {
        // نقرأ حركة اللاعب
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        bool isMoving = Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0;

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                audioSource.PlayOneShot(audioSource.clip);
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }
}

