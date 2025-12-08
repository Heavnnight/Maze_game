using UnityEngine;
using System.Collections;

public class ColorMatchManager : MonoBehaviour
{
    public ColorTileClick firstTile;
    public ColorTileClick secondTile;

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
        yield return new WaitForSeconds(0.4f); // تقدرين تزودين الرقم لو تبينها أبطأ

        if (firstTile.pairID == secondTile.pairID)
        {
            // صح → يختفون بعد ما شفناهم شوي
            firstTile.HideCompletely();
            secondTile.HideCompletely();
        }
        else
        {
            // غلط → نرجعهم للون الخلفية
            firstTile.ResetTile();
            secondTile.ResetTile();
        }

        firstTile = null;
        secondTile = null;
    }
}


