using UnityEngine;
using System.Collections;
using DG.Tweening;

public class HandleKnifeSprite : MonoBehaviour
{

    [SerializeField]
    public MainGameController MainGame;

    [HideInInspector]
    public Tween RotateKnifeLoop;
    Vector3 startCameraPosition;

    private void Start()
    {
        startCameraPosition = MainGame.transform.localPosition;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ballon>() || collision.GetComponent<Stone>())
        {
            return;
        }
        if (collision.name == "BorderBottom" && !MainGame.knifeObject.isMiss || collision.name == "BorderTop" == !MainGame.knifeObject.isMiss)
        {
            MainGame.transform.DOShakePosition(0.5f, 0.1f).SetAutoKill(true).OnComplete(() =>
            {
                MainGame.transform.localPosition = startCameraPosition;
            });
            MainGame.knifeObject.animatorEffectKnife.GetComponent<TrailRenderer>().enabled = false;
            MainGame.knifeObject.box.isTrigger = false;
            MainGame.knifeObject.box.enabled = true;
            MainGame.knifeObject.isMiss = true;
            if (go == null)
            {
                go = StartCoroutine(Rigid());
            }

            return;
        }

        //if (transform.localRotation.z <= ModelHandle.Instance.currentKnifeLocation +90)
        //{
        //    return;
        //}
        if (collision.name != "BorderBottom" && !MainGame.knifeObject.isMiss /*&&ModelHandle.Instance.isCanHit*/||
            collision.name != "BorderTop" && !MainGame.knifeObject.isMiss /*&& ModelHandle.Instance.isCanHit*/)
        {
            MainGame.transform.DOShakePosition(0.5f, 0.1f).SetAutoKill(true).OnComplete(() =>
            {
                MainGame.transform.localPosition = startCameraPosition;
            });
            MainGame.knifeObject.Hit();
            if (collision.GetComponent<WoodTarget>())
            {
                MainGame.knifeObject.Idie();
            }
            else
            {
                MainGame.knifeObject.ImpactKnife(true);
            }
        }



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "BorderBottom" || collision.gameObject.name == "BorderTop")
        {
            MainGame.knifeObject.isMiss = true;
            if (go == null)
            {
                go = StartCoroutine(Rigid());
            }
        }
    }
    public Coroutine go;
    public IEnumerator Rigid()
    {
        yield return new WaitUntil(() => MainGame.knifeObject.isThow);
        MainGame.knifeObject.box.isTrigger = false;
        if (MainGame.knifeObject.isThow)
        {
            MainGame.knifeObject.isThow = false;
        }
        if (MainGame.knifeObject.isMiss)
        {
            yield return new WaitForSeconds(0.3f);
        }
        else
        {
            if (go != null)
            {
                StopCoroutine(go);
                go = null;
            }
        }
        MainGame.knifeObject.Idie();

    }

}

