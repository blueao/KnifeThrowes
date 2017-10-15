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
    public void Die()
    {
        sprite.enabled = false;
        box.enabled = false;
        InPool();
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
        throw new NotImplementedException();
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
            Die();
        }
        if (collision.name == "StartMove")
        {
            Move();
        }
    }
}


