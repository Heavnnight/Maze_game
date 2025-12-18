using UnityEngine;

public class BossIntroController : MonoBehaviour
{
    [Header("References")]
    public Transform player;          // اسحبي اللاعب هنا
    public GameObject bossObject;     // اسحبي البوس هنا (GameObject)

    [Header("Dialogue (اختياري)")]
    public GameObject dialogueUI;     // لو عندك UI للمحادثة (اختياري)
    public MonoBehaviour dialogueScript; // سكربت المحادثة (اختياري)

    [Header("Start Settings")]
    public float moveThreshold = 0.02f;   // كم حركة تعتبر "تحرك"

    private Vector3 startPos;
    private bool dialogueStarted = false;

    // راح يتعبّى تلقائياً من bossObject
    private BossBulletWave bossWave;

    void Start()
    {
        // نحفظ مكان بداية اللاعب
        if (player != null) startPos = player.position;

        // نخفي الديالوج بالبداية لو موجود
        if (dialogueUI != null) dialogueUI.SetActive(false);

        // نخفي البوس بالبداية + نجيب BossBulletWave تلقائيًا
        if (bossObject != null)
        {
            bossWave = bossObject.GetComponent<BossBulletWave>();
            bossObject.SetActive(false);
        }
    }

    void Update()
    {
        if (dialogueStarted) return;
        if (player == null) return;

        // أول ما اللاعب يتحرك
        float dist = Vector3.Distance(player.position, startPos);
        if (dist >= moveThreshold)
        {
            dialogueStarted = true;
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        // شغلي واجهة المحادثة لو عندك
        if (dialogueUI != null) dialogueUI.SetActive(true);

        // شغلي سكربت المحادثة لو كان مطفي
        if (dialogueScript != null) dialogueScript.enabled = true;

        // لو سكربت الديالوج عندك فيه دالة StartDialogue() بيتم استدعاؤها (اختياري)
        if (dialogueScript != null)
            dialogueScript.SendMessage("StartDialogue", SendMessageOptions.DontRequireReceiver);
    }

    // 👇 نادِي هذي الدالة من سكربت المحادثة بعد آخر جملة
    public void OnDialogueFinished()
    {
        // اخفي الديالوج
        if (dialogueUI != null) dialogueUI.SetActive(false);

        // اظهر البوس
        if (bossObject != null) bossObject.SetActive(true);

        // ابدأ الفايت
        if (bossWave == null && bossObject != null)
            bossWave = bossObject.GetComponent<BossBulletWave>();

        if (bossWave != null)
            bossWave.StartFight();
    }
}

