using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class Spiders : MonoBehaviour, IMonster
{
    SpriteRenderer sprite;
    BoxCollider2D box;
    float width;
    float height;
    public Sprite[] ListSprite;
    Sequence se;
    Tween move;
    Vector3 startposition;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        gameObject.AddComponent<BoxCollider2D>();
        box = GetComponent<BoxCollider2D>();
        startposition = transform.localPosition;
        SetSprite();
    }
    Tween death;
    public Sprite[] ListBloodSprite;
    public SpriteRenderer EffectBlood;
    public void Die()
    {
        ModelHandle.Instance.setPosCoinPool(this.transform, 1);
        ModelHandle.Instance.MonsterDeadCount++;
        sprite.enabled = false;
        box.enabled = false;
        death = DOTween.To(() => 0, x => EffectBlood.sprite = ListBloodSprite[x], ListBloodSprite.Length - 1, 1f).OnComplete(() =>
        {
            EffectBlood.sprite = null;
            if (death != null)
            {
                death.Kill();
                death = null;
            }
            InPool();
            ModelHandle.Instance.SetScore(1);
        });
    }
    public void setPosition(Vector3 v3)
    {
        transform.localPosition = v3;
    }
    public void Fly()
    {
        throw new NotImplementedException();
    }

    public void InPool()
    {
        //ModelHandle.Instance.actiongGetCoin(this.transform.localPosition);
        SetSprite();
        transform.localPosition = new Vector3(0, 15, 0);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        if (se != null)
        {
            se.Kill();
            se = null;
        }
        if (move !=null)
        {
            move.Kill();
            move = null;
        }
        box.enabled = true;
        sprite.enabled = true;
        gameObject.SetActive(false);
    }

    public void Move()
    {
        se = DOTween.Sequence();
        move = DOTween.To(() => 0, x => sprite.sprite = ListSprite[x], ListSprite.Length - 1, 1f).OnComplete(() =>
        {
        }).SetLoops(-1);
        se.Append(transform.DOLocalMoveY(startposition.y + height/3,5f)).OnComplete(()=> {
            transform.DOLocalMoveY(startposition.y, 5f).OnComplete(()=> {
                Move();
            });
        });
    }

    public void Normal()
    {
        gameObject.SetActive(true);
    }

    public void SetSprite()
    {
        sprite.sprite = ListSprite[0];
        width = sprite.sprite.bounds.size.x;
        height = sprite.sprite.bounds.size.y;
        box.offset = new Vector2(0, -2.3f);
        //box.size = new Vector2(1.3f, 2.2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Knife")
        {
            ModelHandle.Instance.setPosCoinPool(this.transform, 1);
            ModelHandle.Instance.SetSound(ModelHandle.HitWood);
            Die();
        }
        if (collision.name == "StartMove")
        {
            ModelHandle.Instance.SetSound(ModelHandle.SpiderApp);
            Move();
        }
    }
}


