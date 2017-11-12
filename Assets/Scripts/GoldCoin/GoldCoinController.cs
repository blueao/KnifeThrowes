using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GoldCoinController : MonoBehaviour {

    SpriteRenderer sprite;
    public Sprite Coin1;
    public Sprite Coin2;
    public Sprite Coin10;
    public Sprite Coin20;
    public Sprite[] ListAnimSpriteCoin;
    Tween rotate;
    Tween Move;
    Tween Anim;

    public void RunAnimCoin(Transform parent, int money)
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = true;
        this.gameObject.SetActive(true);
        if (money == 1)
        {
            sprite.sprite = Coin1;
        }
        else if (money == 10)
        {
            sprite.sprite = Coin10;
        }
        //transform.parent = parent;
        transform.position = parent.position;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
        sprite.enabled = true;
        if (money>=10)
        {
            rotate = (transform.DOScaleX(0, 0.3f).OnComplete(() => { transform.DOScaleX(1, 0.3f); }).SetLoops(-1));
        }
        Move = (transform.DOLocalMoveY((transform.localPosition.y + parent.GetComponent<SpriteRenderer>().sprite.rect.height/100f), 1f).OnComplete(() => {
            if (rotate != null)
            {
                rotate.Kill();
                rotate = null;
            }
            transform.localScale = Vector3.one;
            DOTween.To(() => 0, x => sprite.sprite = ListAnimSpriteCoin[x], ListAnimSpriteCoin.Length - 1, 0.3f).OnComplete(() =>
            {
                sprite.enabled = false;
                transform.localPosition = Vector3.zero;

                if (Move != null)
                {
                    Move.Kill();
                    Move = null;
                }
            }).SetAutoKill(true);

        }));
    }
}
