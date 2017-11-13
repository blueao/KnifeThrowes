using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FadeKnifeCut : MonoBehaviour {
    Tween tween;
    public void DisableSpriteKnife()
    {
        if (tween!=null)
        {
            tween.Kill();
            tween = null;
        }
        transform.GetComponent<SpriteRenderer>().color = Color.white;
       tween =   transform.GetComponent<SpriteRenderer>().DOFade(0, 3f).OnComplete(() =>
        {
            Color tmp = new Color();
            tmp.a = 255;
            tmp.b = 1;
            tmp.r = 1;
            tmp.g = 1;
            transform.GetComponent<SpriteRenderer>().color = tmp;
            ModelHandle.Instance.DesObjSpriteKnifeCut();
            transform.gameObject.SetActive(false);
        });
    }
}
