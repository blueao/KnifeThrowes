using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class ScrollRectController : MonoBehaviour
{

    public RectTransform panel;
    public List<GameObject> listGO;
    public RectTransform center;
    public GameObject preFabKnifeShop;
    public MainGameController MainGame;
    public Sprite[] ListSpriteKnife;
    public Sprite[] ListSpriteKnifeContempl;
    public Sprite[] ListMoney;
    public Sprite[] Unlock;
    public Sprite[] ListSpriteKnifeUseAfterBuy;
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
#if TESTTING

        PlayerPrefs.SetInt(ModelHandle.KeyScore, 99999);
        ModelHandle.Instance.SetScore(PlayerPrefs.GetInt(ModelHandle.KeyScore));
#endif
        //CreateObject();
        //lengt = listGO.Count;
        //distance = new float[lengt];
        //distReposition = new float[lengt];
        //GODistance = (int)(listGO[1].GetComponent<RectTransform>().anchoredPosition.y - listGO[0].GetComponent<RectTransform>().anchoredPosition.y);
        imagelock = Lock.sprite;
        InitShop();
        // panel.anchoredPosition = new Vector2(0f,(start - 1)*-115);
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
    public void setActiveLock(bool isActive)
    {
        Lock.enabled = isActive;
    }
    int indexItemsBuyed = -1;
    void InitShop()
    {
        for (int i = 0; i < panel.childCount; i++)
        {
            listGO.Add(panel.GetChild(i).gameObject);
        }
        for (int i = 0; i < listGO.Count; i++)
        {
            if (PlayerPrefs.HasKey((ModelHandle.KeyKnifeSprite + i)))
            {
                indexItemsBuyed = PlayerPrefs.GetInt(ModelHandle.KeyKnifeSprite + i);
            }
            if (indexItemsBuyed == 0)
            {
                setActiveLock(false);
            }
            if (i == indexItemsBuyed)
            {
                listGO[i].transform.GetChild(2).GetComponent<HandleButtonBuy>().isCanbuy = false;
                listGO[i].transform.GetChild(2).GetComponent<HandleButtonBuy>().AnimBuy.enabled = true;
                listGO[i].transform.GetChild(2).GetComponent<HandleButtonBuy>().AnimBuy.transform.GetChild(0).GetComponent<Image>().enabled = true;
                listGO[i].transform.GetChild(2).GetComponent<HandleButtonBuy>().Buy.enabled = false;
                listGO[i].transform.GetChild(2).GetComponent<HandleButtonBuy>().Gold.enabled = false;
                listGO[i].transform.GetChild(2).GetComponent<HandleButtonBuy>().Money.enabled = false;
            }
            else
                listGO[i].transform.GetChild(2).GetComponent<HandleButtonBuy>().isCanbuy = true;
        }
    }
    void CreateObject()
    {
        //for (int i = 0; i < ListSpriteKnife.Length; i++)
        //{
           // GameObject SpriteKnifeLeft = (GameObject)Instantiate(preFabKnifeShop, preFabKnifeShop.transform.localPosition, Quaternion.identity);
            //SpriteKnifeLeft.transform.SetParent(panel, false);
            //SpriteKnifeLeft.name = "KnifeLeft" + i;
            //SpriteKnifeLeft.transform.GetChild(1).GetComponent<Image>().sprite = ListSpriteKnife[i];

            //for (int j = 0; j < ListMoney.Length; j++)
            //{
            //    if (ListMoney[j].name.Remove(0, 5) == (SpriteKnifeLeft.transform.GetChild(1).GetComponent<Image>().sprite.name.Remove(0, 6)))
            //    {
            //        SpriteKnifeLeft.transform.GetChild(2).transform.GetChild(2).GetComponent<Image>().sprite = ListMoney[j];
            //        break;
            //    }
            //}

           // SpriteKnifeLeft.transform.localPosition = new Vector3(SpriteKnifeLeft.transform.localPosition.x, SpriteKnifeLeft.transform.localPosition.y - 115f * i, SpriteKnifeLeft.transform.localPosition.z);

            //listGO.Add(SpriteKnifeLeft);
        //}
    }
    //private void FixedUpdate()
    //{
    //    if (GetComponent<MainGameController>().Menu.activeSelf)
    //    {
    //        for (int i = 0; i < listGO.Count; i++)
    //        {
    //            distReposition[i] = center.GetComponent<RectTransform>().position.y - listGO[i].GetComponent<RectTransform>().position.y;
    //            distance[i] = Mathf.Abs(distReposition[i]);
    //        }

    //        float minDistance = Mathf.Min(distance);
    //        for (int a = 0; a < listGO.Count; a++)
    //        {
    //            if (minDistance == distance[a])
    //            {
    //                numberGO = a;
    //            }
    //            if (!drag)
    //            {
    //                Lerp(-listGO[numberGO].GetComponent<RectTransform>().anchoredPosition.y);
    //            }
    //        }
    //    }
    //}


    //void Lerp(float position)
    //{
    //    float newY = Mathf.Lerp(panel.anchoredPosition.y, position, Time.fixedDeltaTime * 5f);

    //    if (Mathf.Abs(position - newY) < 5f)
    //    {
    //        newY = position;
    //    }
    //    Vector2 newposition = new Vector2(panel.anchoredPosition.x, newY);
    //    panel.anchoredPosition = newposition;
    //}
    //public void StartDrag()
    //{
    //    drag = true;
    //}
    //public void EndDrag()
    //{
    //    drag = false;
    //}
}
