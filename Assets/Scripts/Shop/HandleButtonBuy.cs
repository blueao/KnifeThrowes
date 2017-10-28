using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class HandleButtonBuy : MonoBehaviour
{

    public Image Buy;
    public Image Gold;
    public Image Money;
    public Image AnimBuy;
    public Sprite PlayNow;
    public Sprite[] EffectBuy;
    public Sprite[] EffectUnlock;
    [HideInInspector]
    public bool isCanbuy;
    Tween anim;
    public void ButtonClickBuy()
    {
        int index = int.Parse(gameObject.transform.parent.name.Remove(0, 9));
        if (isCanbuy == true)
        {
            int scores = PlayerPrefs.GetInt(ModelHandle.KeyScore);
            if (scores >= int.Parse(Money.sprite.name.Remove(0, 5)))
            {
                PlayerPrefs.SetInt(ModelHandle.KeyScore, scores - int.Parse(Money.sprite.name.Remove(0, 5)));
                ModelHandle.Instance.SetScore(scores - int.Parse(Money.sprite.name.Remove(0, 5)));
                ModelHandle.Instance.setSpriTemp(index);
                isCanbuy = false;
                AnimBuy.enabled = true;
                Buy.enabled = false;
                Gold.enabled = false;
                Money.enabled = false;
                anim = DOTween.To(() => 0, x => AnimBuy.sprite = EffectBuy[x], EffectBuy.Length - 1, 1f).OnComplete(() =>
                  {
                      ModelHandle.Instance.RunAnimLock();
                      AnimBuy.sprite = PlayNow;
                      AnimBuy.transform.GetChild(0).GetComponent<Image>().enabled = true;
                      PlayerPrefs.SetInt(ModelHandle.KeyKnifeSprite + index, index);
                      if (anim != null)
                      {
                          anim.Kill();
                          anim = null;
                      }
                  });
            }
        }
        else
        {
            SaveDataAfterBuyCompleted(index);
        }

    }
    public void SaveDataAfterBuyCompleted(int index)
    {
        ModelHandle.Instance.setUpKnifeAfterBuy(index);
        PlayerPrefs.SetInt(ModelHandle.KeyKnifeSprite, index);
        PlayerPrefs.SetInt(ModelHandle.KeyKnifeSprite + index, index);
        ModelHandle.Instance.ActiveShop(false);
    }
}
