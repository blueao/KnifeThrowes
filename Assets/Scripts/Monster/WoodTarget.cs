using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class WoodTarget : MonoBehaviour, IMonster
{


    public SpriteRenderer spriteItems;
    public BoxCollider2D box;
    private Vector3 startposition;
    private Tween tweenMove;
    public Sprite[] ListSprite;
    float width;
    float height;
    private void Start()
    {
        gameObject.AddComponent<BoxCollider2D>();
        spriteItems = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        startposition = transform.localPosition;
        SetSprite();
    }
    public void Die()
    {
        InPool();
        box.enabled = false;
        spriteItems.enabled = false;
        if (tweenMove != null)
        {
            tweenMove.Kill();
            tweenMove = null;
        }
      

    }

    public void Fly()
    {
        throw new NotImplementedException();
    }

    public void InPool()
    {
        transform.localPosition = new Vector3(0, 15, 0);
        box.enabled = true;
        spriteItems.enabled = true;
        SetSprite();
        spriteItems.enabled = true;
        
    }

    public void Move()
    {
        tweenMove = transform.DOLocalMoveY(transform.localPosition.y + 1, 3f).OnComplete(() =>
        {
            transform.DOLocalMoveY(startposition.y, 3f).OnComplete(() =>
            {
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
