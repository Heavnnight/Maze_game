using System.Collections;
using UnityEngine;
using TMPro;

public class BossBulletWave : MonoBehaviour
{
    [Header("Bullet Prefabs")]
    public GameObject whiteBulletPrefab;
    public GameObject blueBulletPrefab;
    public GameObject redBulletPrefab;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Straight Wall Settings")]
    public Vector2 wallDirection = new Vector2(0f, -1f);
    public int gapSize = 3;
    public float bulletLifeTime = 2.0f;

    [Header("Fight Settings (60 sec)")]
    public bool canShoot = false;
    public float fightDuration = 60f;

    [Header("Phases (20 / 20 / 20)")]
    public float easyDuration = 20f;
    public float mediumDuration = 20f;

    [Header("Fire Rates")]
    public float easyFireRate = 1.35f;
    public float mediumFireRate = 1.15f;
    public float hardFireRate = 1.0f;

    [Header("Bullet Speeds")]
    public float easyBulletSpeed = 2.8f;
    public float mediumBulletSpeed = 3.3f;
    public float hardBulletSpeed = 3.8f;

    [Header("Performance Safety")]
    public int maxAliveBullets = 70;
    int aliveBullets = 0;

    [Header("Win UI")]
    public GameObject winPanel;   // 👈 بس هذا، بدون نص

    [Header("Music (Optional)")]
    public AudioSource bossMusic;

    float spawnTimer = 0f;
    float fightTimer = 0f;
    bool fightEnded = false;

    int lastGapStart = -1;
    Coroutine currentWave;

    void Start()
    {
        // البانل يكون مطفي بالبداية
        if (winPanel != null)
            winPanel.SetActive(false);
    }

    void Update()
    {
        // الوقت يمشي دايم
        if (!fightEnded)
            fightTimer += Time.deltaTime;

        // نهاية الفايت
        if (!fightEnded && fightTimer >= fightDuration)
        {
            EndFight();
            return;
        }

        if (!canShoot || fightEnded) return;
        if (spawnPoints == null || spawnPoints.Length == 0) return;

        float fireRate;
        float speed;

        if (fightTimer < easyDuration)
        {
            fireRate = easyFireRate;
            speed = easyBulletSpeed;
        }
        else if (fightTimer < easyDuration + mediumDuration)
        {
            fireRate = mediumFireRate;
            speed = mediumBulletSpeed;
        }
        else
        {
            fireRate = hardFireRate;
            speed = hardBulletSpeed;
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= fireRate)
        {
            spawnTimer = 0f;

            if (currentWave != null)
                StopCoroutine(currentWave);

            currentWave = StartCoroutine(SpawnWall(speed));
        }
    }

    IEnumerator SpawnWall(float speed)
    {
        int n = spawnPoints.Length;
        int gSize = Mathf.Clamp(gapSize, 1, n - 1);

        GameObject prefab = PickWavePrefab();
        if (prefab == null) yield break;

        int gapStart = Random.Range(0, n);
        if (gapStart == lastGapStart)
            gapStart = (gapStart + 1) % n;
        lastGapStart = gapStart;

        int gapEnd = gapStart + gSize - 1;

        for (int i = 0; i < n; i++)
        {
            if (IsInGap(i, gapStart, gapEnd, n)) continue;
            if (aliveBullets >= maxAliveBullets) break;

            GameObject obj = Instantiate(prefab, spawnPoints[i].position, Quaternion.identity);
            aliveBullets++;
            StartCoroutine(TrackDestroy(obj));

            BulletHomingSimple b = obj.GetComponent<BulletHomingSimple>();
            if (b != null)
            {
                b.speed = speed;
                b.lifeTime = bulletLifeTime;
                b.homing = false;
                b.SetFixedDirection(wallDirection);
            }
        }

        yield return null;
    }

    IEnumerator TrackDestroy(GameObject obj)
    {
        while (obj != null) yield return null;
        aliveBullets = Mathf.Max(0, aliveBullets - 1);
    }

    bool IsInGap(int index, int start, int end, int n)
    {
        if (end < n)
            return index >= start && index <= end;

        int wrappedEnd = end % n;
        return index >= start || index <= wrappedEnd;
    }

    GameObject PickWavePrefab()
    {
        float r = Random.value;

        if (r < 0.55f && whiteBulletPrefab != null) return whiteBulletPrefab;
        if (r < 0.90f && blueBulletPrefab != null) return blueBulletPrefab;
        if (redBulletPrefab != null) return redBulletPrefab;

        if (whiteBulletPrefab != null) return whiteBulletPrefab;
        if (blueBulletPrefab != null) return blueBulletPrefab;
        return redBulletPrefab;
    }

    void EndFight()
    {
        fightEnded = true;
        canShoot = false;

        if (currentWave != null)
            StopCoroutine(currentWave);

        if (bossMusic != null && bossMusic.isPlaying)
            bossMusic.Stop();

        // 👇 فقط نظهر البانل، بدون لمس النص
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            winPanel.transform.SetAsLastSibling();
        }
    }

    public void StartFight()
    {
        fightEnded = false;
        canShoot = true;

        fightTimer = 0f;
        spawnTimer = 0f;
        aliveBullets = 0;
        lastGapStart = -1;

        if (currentWave != null)
            StopCoroutine(currentWave);

        if (winPanel != null)
            winPanel.SetActive(false);

        if (bossMusic != null && !bossMusic.isPlaying)
            bossMusic.Play();
    }
}
