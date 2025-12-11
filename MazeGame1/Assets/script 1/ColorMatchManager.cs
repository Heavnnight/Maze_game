using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // 👈 مهم عشان نقدر نغير المشهد

public class ColorMatchManager : MonoBehaviour
{
    [Header("Matching Tiles")]
    public ColorTileClick firstTile;
    public ColorTileClick secondTile;

    [Header("Pairs Settings")]
    public int totalPairs = 6;     // 👈 كم زوج عندك في المرحلة (لو عندك 12 بلاطة = 6 أزواج)
    private int matchedPairs = 0;  // 👈 كم زوج انحل صح لحد الآن

    public bool CanSelect()
    {
        return secondTile == null;
    }

    public void SelectTile(ColorTileClick tile)
    {
        if (firstTile == null)
        {
            firstTile = tile;
        }
        else if (secondTile == null)
        {
            secondTile = tile;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        // نخلي اللعبه تنتظر شوي عشان اللاعب يشوف اللونين
        yield return new WaitForSeconds(0.4f);

        if (firstTile.pairID == secondTile.pairID)
        {
            // ✅ ماتش صح → نخفي البلاطتين
            firstTile.HideCompletely();
            secondTile.HideCompletely();

            // نزيد عدد الأزواج اللي انحلت
            matchedPairs++;
            Debug.Log("Matched pair: " + matchedPairs + " / " + totalPairs);

            // لو خلصنا كل الأزواج → نروح للمشهد اللي بعده
            if (matchedPairs >= totalPairs)
            {
                Debug.Log("All pairs matched! Loading next scene...");
                // يحمل المشهد اللي بعده في الـBuild Settings
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                // ولو تبين مشهد باسم معيّن:
                // SceneManager.LoadScene("اسم_المشهد_الرابع");
            }
        }
        else
        {
            // ❌ غلط → نرجعهم للون الخلفية
            firstTile.ResetTile();
            secondTile.ResetTile();
        }

        firstTile = null;
        secondTile = null;
    }
}
