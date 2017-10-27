using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class HandleButtonBuy : MonoBehaviour {

    public Image Buy;
    public Image Gold;
    public Image Money;
    public Image AnimBuy;
    public Image Lock;
    public Sprite PlayNow;
    public Sprite[] EffectBuy;
    public Sprite[] EffectUnlock;
    [HideInInspector]
    public bool isCanbuy;
    Tween anim;
    private void Start()
    {
        isCanbuy = true;
    }
    public void ButtonClickBuy()
    {
        if (isCanbuy==true)
        {
            int scores = PlayerPrefs.GetInt("score");
            if (scores >= int.Parse(Money.sprite.name.Remove(0, 5)))
            {
                int index = int.Parse(gameObject.transform.parent.name.Remove(0, 9));
                ModelHandle.Instance.setSpriTemp(index);
                isCanbuy = false;
                AnimBuy.enabled = true;
                Buy.enabled = false;
                Gold.enabled = false;
                Money.enabled = false;
                anim = DOTween.To(() => 0, x => AnimBuy.sprite = EffectBuy[x], EffectBuy.Length - 1, 1f).OnComplete(() =>
                  {
                      AnimBuy.sprite = PlayNow;
                      AnimBuy.transform.GetChild(0).GetComponent<Image>().enabled = true;
                      if (anim != null)
                      {
                          anim.Kill();
                          anim = null;
                      }
                  });
            }
        }

    }
    public void SaveData()
    {

    }
}
