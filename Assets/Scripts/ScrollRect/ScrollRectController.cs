using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class ScrollRectController : MonoBehaviour
{

    public RectTransform panel;
    public List<Transform> listGO;
    //public RectTransform center;
    public GameObject preFabKnifeShop;
    public MainGameController MainGame;
    public Sprite[] ListSpriteKnife;
    public Sprite[] ListSpriteKnifeContempl;
    public Sprite[] ListMoney;
    public Sprite[] Unlock;
    public Sprite[] ListSpriteKnifeUseAfterBuy;
    public Sprite[] ListSpriteCut;
    private int start = 1;
    private float[] distance;
    float[] distReposition;
    bool drag;
    private int GODistance;
    private int numberGO;
    int lengt;

    public Image KnifeTemp;
    public Image Lock;
    Sprite imagelock;
    private void Start()
    {
        ModelHandle.Instance.SetScore(PlayerPrefs.GetInt(ModelHandle.KeyScore));
#if TESTTING
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt(ModelHandle.KeyScore, 20000);
        //ModelHandle.Instance.SetScore(PlayerPrefs.GetInt(ModelHandle.KeyScore));
#endif

        for (int i = 0; i < ModelHandle.Instance.isObjBuyed.Length; i++)
        {
            if (PlayerPrefs.HasKey((ModelHandle.KeyKnifeSprite + i)))
            {
                indexItemsBuyed = PlayerPrefs.GetInt(ModelHandle.KeyKnifeSprite + i);
                ModelHandle.Instance.isObjBuyed[indexItemsBuyed] = true;
            }
        }
        imagelock = Lock.sprite;

    }
    Tween anim;
    public void RunAnimUnlock()
    {
        Lock.transform.localScale = new Vector3(7, 7, 7);
        anim = DOTween.To(() => 0, x => Lock.sprite = Unlock[x], Unlock.Length - 1, 1f).OnComplete(() =>
        {
            setActiveLock(false);
            Lock.sprite = imagelock;
            Lock.transform.localScale = Vector3.one;
            if (anim != null)
            {
                anim.Kill();
                anim = null;
            }
        });
    }
    public void setUseSpriteKnife(int index)
    {
        gameObject.GetComponent<MainGameController>().spriteKnife.GetComponent<SpriteRenderer>().sprite = ListSpriteKnifeUseAfterBuy[index];
    }
    public void setUseSpriteKnifeCut(int index)
    {
        gameObject.GetComponent<MainGameController>().KnifeSpriteCut = ListSpriteCut[index+1];
    }
    public void setActiveLock(bool isActive)
    {
        Lock.enabled = isActive;
    }
    int indexItemsBuyed = -1;
}
