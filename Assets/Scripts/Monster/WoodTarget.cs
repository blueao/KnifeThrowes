using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class WoodTarget : MonoBehaviour, IMonster
{

    public SpriteRenderer spriteItems;
    public BoxCollider2D box;
    private Vector3 startposition;
    private Sequence tweenMove;
    public Sprite[] ListSprite;
    float width;
    float height;
    private void Start()
    {
        SetSprite();
        gameObject.AddComponent<BoxCollider2D>();
        spriteItems = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        if (this.name.Contains("wood target 1"))
        {

            box.size = new Vector2(0.3f, 1.4f);
        }
        else if (this.name.Contains("wood target 2"))
        {
            box.size = new Vector2(0.3f, 1.4f);
        }
        else if (this.name.Contains("red target"))
        {
            box.size = new Vector2(0.2f, 1.1f);
        }
        startposition = transform.localPosition;
      
    }
    public void Die()
    {
        //ModelHandle.Instance.actiongGetCoin(this.transform.localPosition);
        if (tweenMove != null)
        {
            tweenMove.Kill();
            tweenMove = null;
        }
        ModelHandle.Instance.SetScore(1);
        ModelHandle.Instance.MonsterDeadCount++;
        box.enabled = false;
        // spriteItems.enabled = false;
        Dead = gameObject.GetComponent<SpriteRenderer>().DOFade(0, 5f).OnComplete(() =>
        {
            InPool();
            gameObject.GetComponent<SpriteRenderer>().DOFade(1, 0f);
        });
    }

    public void Fly()
    {
        throw new NotImplementedException();
    }

    public void InPool()
    {
        
        transform.localPosition = new Vector3(0, 15, 0);
        //box.enabled = true;
        spriteItems.enabled = true;
        SetSprite();
        spriteItems.enabled = true;

        //gameObject.SetActive(false);

    }
    public void ResetState()
    {
        if (tweenMove != null)
        {
            tweenMove.Kill();
            tweenMove = null;
        }
    }
    public void Move()
    {
        tweenMove = DOTween.Sequence();
        tweenMove.Append(transform.DOLocalMoveY(transform.localPosition.y + 1, 3f));
        tweenMove.Append(transform.DOLocalMoveY(startposition.y, 3f));
        tweenMove.SetLoops(-1);
        tweenMove.Play();

    }

    public void Normal()
    {
        gameObject.SetActive(true);
    }

    public void SetSprite()
    {
        if (this.name.Contains("wood target 1"))
        {
            spriteItems.sprite = ListSprite[0];
            width = spriteItems.sprite.bounds.size.x;
            height = spriteItems.sprite.bounds.size.y;
        }
        else if (this.name.Contains("wood target 2"))
        {
            spriteItems.sprite = ListSprite[1];
            width = spriteItems.sprite.bounds.size.x;
            height = spriteItems.sprite.bounds.size.y;
        }
        else if (this.name.Contains("red target"))
        {
            spriteItems.sprite = ListSprite[2];
            width = spriteItems.sprite.bounds.size.x;
            height = spriteItems.sprite.bounds.size.y;
        }
    }
   public Tween Dead;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Knife" /*&& ModelHandle.Instance.isCanHit*/)
        {
            ModelHandle.Instance.setPosCoinPool(this.transform, 1);
            ModelHandle.Instance.SetSound(ModelHandle.HitWood);
            if (tweenMove != null)
            {
                tweenMove.Kill();
                tweenMove = null;
            }
            Die();
            ModelHandle.Instance.setSpriteKnifePos(this.transform.localPosition.x);
        }
        if (collision.name == "StartMove")
        {
            Move();
        }
    }
}
