using UnityEngine;

public class BossBulletWave : MonoBehaviour
{
    [Header("Bullet Prefab (Homing)")]
    public GameObject bulletPrefab;        // Prefab الطلقة

    [Header("Spawn Points")]
    public Transform[] spawnPoints;        // SpawnPoint1, 2, 3...

    [Header("Fight Control")]
    public bool canShoot = false;          // يبدأ الفايت
    public float fightDuration = 180f;     // 3 دقائق

    [Header("EASY Phase (أول دقيقة)")]
    public float easyDuration = 60f;
    public float easyFireRate = 1.0f;
    public float easyBulletSpeed = 3f;

    [Header("MEDIUM Phase (نص صعب)")]
    public float mediumDuration = 60f;
    public float mediumFireRate = 0.6f;
    public float mediumBulletSpeed = 5f;

    [Header("HARD Phase (آخر دقيقة)")]
    public float hardFireRate = 0.3f;
    public float hardBulletSpeed = 7f;

    float timer = 0f;
    float fightTimer = 0f;

    Transform player;

    void Start()
    {
        // نجيب اللاعب عن طريق التاق Player
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Update()
    {
        if (!canShoot) return;
        if (player == null) return;

        // وقت الفايت
        fightTimer += Time.deltaTime;

        // يوقف بعد انتهاء الوقت
        if (fightTimer >= fightDuration)
        {
            canShoot = false;
            return;
        }

        // نحدد الصعوبة الحالية
        float currentFireRate;
        float currentBulletSpeed;

        if (fightTimer < easyDuration)
        {
            currentFireRate = easyFireRate;
            currentBulletSpeed = easyBulletSpeed;
        }
        else if (fightTimer < easyDuration + mediumDuration)
        {
            currentFireRate = mediumFireRate;
            currentBulletSpeed = mediumBulletSpeed;
        }
        else
        {
            currentFireRate = hardFireRate;
            currentBulletSpeed = hardBulletSpeed;
        }

        // إطلاق الطلقات
        timer += Time.deltaTime;

        if (timer >= currentFireRate)
        {
            ShootWave(currentBulletSpeed);
            timer = 0f;
        }
    }

    void ShootWave(float currentBulletSpeed)
    {
        if (bulletPrefab == null) return;
        if (spawnPoints == null || spawnPoints.Length == 0) return;

        foreach (Transform sp in spawnPoints)
        {
            GameObject obj = Instantiate(bulletPrefab, sp.position, sp.rotation);

            BulletHomingSimple bullet = obj.GetComponent<BulletHomingSimple>();
            if (bullet != null)
            {
                bullet.speed = currentBulletSpeed;
                // 👇 هذا السطر هو اللي كان ناقص، يخليهم يلاحقون اللاعب
                bullet.SetTarget(player);
            }
        }
    }

    public void StartFight()
    {
        canShoot = true;
        timer = 0f;
        fightTimer = 0f;
    }

    public void StopFight()
    {
        canShoot = false;
    }
}
