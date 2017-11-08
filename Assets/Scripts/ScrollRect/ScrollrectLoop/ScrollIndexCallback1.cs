using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class ScrollIndexCallback1 : MonoBehaviour
{
    public Image imgKnifeDisplay, imgKnifePrice;

    public Sprite[] KnifePrices;
    public Sprite[] KnifeSprites;
    public HandleButtonBuy ControlButtonBuy;
    int indexItemsBuyed;
    void ScrollCellIndex(int idx)
    {
        for (int i = 0; i < ModelHandle.Instance.isObjBuyed.Length; i++)
        {
            if (PlayerPrefs.HasKey((ModelHandle.KeyKnifeSprite + i)))
            {
                indexItemsBuyed = PlayerPrefs.GetInt(ModelHandle.KeyKnifeSprite + i);
                ModelHandle.Instance.isObjBuyed[indexItemsBuyed] = true;
            }
        }
        string name = "KnifeLeft" + idx.ToString();
        if (imgKnifeDisplay != null)
        {
            imgKnifeDisplay.sprite = KnifeSprites[idx];
        }
        if (imgKnifePrice != null)
        {
            imgKnifePrice.sprite = KnifePrices[idx];
        }
        gameObject.name = name;
        ControlButtonBuy.IsCanbuy = !ModelHandle.Instance.isObjBuyed[idx];
        }

    // http://stackoverflow.com/questions/2288498/how-do-i-get-a-rainbow-color-gradient-in-c
    public static Color Rainbow(float progress)
    {
        progress = Mathf.Clamp01(progress);
        float r = 0.0f;
        float g = 0.0f;
        float b = 0.0f;
        int i = (int)(progress * 6);
        float f = progress * 6.0f - i;
        float q = 1 - f;

        switch (i % 6)
        {
            case 0:
                r = 1;
                g = f;
                b = 0;
                break;
            case 1:
                r = q;
                g = 1;
                b = 0;
                break;
            case 2:
                r = 0;
                g = 1;
                b = f;
                break;
            case 3:
                r = 0;
                g = q;
                b = 1;
                break;
            case 4:
                r = f;
                g = 0;
                b = 1;
                break;
            case 5:
                r = 1;
                g = 0;
                b = q;
                break;
        }
        return new Color(r, g, b);
    }
}
