using UnityEngine;

public class WinCanvasFixer : MonoBehaviour
{
    public Canvas canvas;

    void Reset()
    {
        canvas = GetComponent<Canvas>();
    }

    void Awake()
    {
        if (canvas == null) canvas = GetComponent<Canvas>();
        if (canvas == null) return;

        // يخليه دايم Overlay وفوق كل شيء
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;
        canvas.overrideSorting = true;

        // مهم إذا فيه CanvasGroup بالغلط
        var cg = GetComponent<CanvasGroup>();
        if (cg != null) cg.alpha = 1f;
    }
}
