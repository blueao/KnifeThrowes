using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class Dummy : MonoBehaviour, IMonster
{
    SpriteRenderer sprite;
    BoxCollider2D box;
    float width;
    float height;
    public Sprite[] SpriteDummy;

    private void Start()
    {
        gameObject.AddComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        SetSprite();
    }
    public void setPosition(Vector3 v3)
    {
        transform.localPosition = v3;
    }
    public void Die()
    {
        box.enabled = false;
        ShowDummyDead();
    }

    public void Fly()
    {
        throw new NotImplementedException();
    }

    public void InPool()
    {
        box.enabled = true;
       // transform.localPosition = new Vector3(0, 15, 0);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        sprite.enabled = true;
        SetSprite();
    }

    public void Move()
    {
        throw new NotImplementedException();
    }

    public void Normal()
    {
        gameObject.SetActive(true);
    }

    public void SetSprite()
    {
        if (this.name.Contains("dummy (1)"))
        {
            sprite.sprite = SpriteDummy[0];
            width = sprite.sprite.bounds.size.x;
            height = sprite.sprite.bounds.size.y;
            box.offset = new Vector2(0, 4.2f);
            box.size = new Vector2(1.3f, 2.7f);
        }
        else if (this.name.Contains("dummy (2)"))
        {
            sprite.sprite = SpriteDummy[1];
            width = sprite.sprite.bounds.size.x;
            height = sprite.sprite.bounds.size.y;
            box.offset = new Vector2(0, 4.4f);
            box.size = new Vector2(1.3f, 2.2f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Knife")
        {
            Die();
        }
        if (collision.name == "End")
        {
            InPool();
        }
    }
    float speed = 1f;
    void ShowDummyDead()
    {
        transform.DOLocalRotate(new Vector3(0, 0, -75), speed).OnComplete(() =>
        {
            //sprite.enabled = false;
        });


    }
}
