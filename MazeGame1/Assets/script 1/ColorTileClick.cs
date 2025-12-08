using UnityEngine;

public class ColorTileClick : MonoBehaviour
{
    [Header("Visuals")]
    public Color hiddenColor = Color.red;   // اللون اللي يطلع
    public Color backColor = Color.gray;    // لون البداية

    [Header("Matching ID")]
    public int pairID;                      // رقم الزوج (1,2,3,4...)

    private SpriteRenderer sr;
    private bool isRevealed = false;
    private bool isMatched = false;

    private ColorMatchManager manager;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = backColor;
        manager = FindObjectOfType<ColorMatchManager>();
    }

    void OnMouseDown()
    {
        if (isMatched) return;      // بلاطة خلصت ماتش
        if (isRevealed) return;     // لا نسمح نضغط على نفس المفتوحة

        if (!manager.CanSelect()) return;

        Reveal();
        manager.SelectTile(this);
    }

    public void Reveal()
    {
        sr.color = hiddenColor;
        isRevealed = true;
    }

    public void ResetTile()
    {
        sr.color = backColor;
        isRevealed = false;
    }

    public void HideCompletely()
    {
        sr.color = new Color(0, 0, 0, 0); // يخفيها (ألفا 0)
        isMatched = true;
    }
}
