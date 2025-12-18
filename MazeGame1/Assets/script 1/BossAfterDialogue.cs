using UnityEngine;

public class BossAfterDialogue : MonoBehaviour
{
    [Header("Dialogue")]
    public GameObject dialogueUI;     // Panel حق الحوار

    [Header("Boss")]
    public GameObject bossObject;     // البوس نفسه

    [Header("Fight UI & Arena")]
    public GameObject arenaWall;      // الصندوق/الجدار
    public GameObject heartsUI;       // قروب القلوب (Canvasheart)

    private BossBulletWave bossWave;
    private bool sawDialogueOpen = false;
    private bool done = false;

    void Start()
    {
        // نخفي كل شي بالبداية
        if (bossObject != null)
        {
            bossWave = bossObject.GetComponent<BossBulletWave>();
            bossObject.SetActive(false);
        }

        if (arenaWall != null)
            arenaWall.SetActive(false);

        if (heartsUI != null)
            heartsUI.SetActive(false);
    }

    void Update()
    {
        if (done) return;
        if (dialogueUI == null) return;

        // الحوار انفتح؟
        if (dialogueUI.activeSelf)
            sawDialogueOpen = true;

        // انتهى الحوار
        if (sawDialogueOpen && !dialogueUI.activeSelf)
        {
            done = true;

            // اظهر البوس
            if (bossObject != null)
                bossObject.SetActive(true);

            // اظهر الحلبة
            if (arenaWall != null)
                arenaWall.SetActive(true);

            // اظهر القلوب
            if (heartsUI != null)
                heartsUI.SetActive(true);

            // ابدأ الفايت
            if (bossWave != null)
                bossWave.StartFight();
        }
    }
}
