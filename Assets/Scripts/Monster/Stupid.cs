using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class Stupid : MonoBehaviour, IMonster
{
    private BoxCollider2D box;
    public Sprite[] MStupid;
    public Sprite[] Pumkin;
    public Sprite[] Fruit;
    public Sprite[] EffectStupid;
    public Sprite[] EffectPumkin;
    public Sprite[] EffectFruit;
    private SpriteRenderer spriteItems;
    Sprite[] usedSprite;
    private float width;
    private float height;
    bool jumb;
    public Animator TouchGrass;
    Rigidbody2D rdStupid;
    private void Start()
    {
        gameObject.AddComponent<BoxCollider2D>();
        box = GetComponent<BoxCollider2D>();
        spriteItems = GetComponent<SpriteRenderer>();
        rdStupid = GetComponent<Rigidbody2D>();
        SetSprite();

    }
    public void setPosition(Vector3 v3)
    {
        transform.localPosition = v3;
    }
    public void Die()
    {
        ShowStupidDead();
        box.enabled = false;
    }
    void ShowStupidDead()
    {
        rdStupid.constraints = RigidbodyConstraints2D.FreezeAll;
        DOTween.To(() => 0, x => spriteItems.sprite = usedSprite[x], usedSprite.Length - 1, 1f).OnComplete(() =>
             {
                 spriteItems.enabled = false;
                 InPool();
             });

    }
    public void Fly()
    {
        throw new NotImplementedException();
    }
    [ContextMenu("Jumb")]
    public void Move()
    {
        jumb = true;
        float a = UnityEngine.Random.Range(height, height * 3);
        Debug.Log(a);
        transform.DOLocalMoveY(a, 0.5f).OnComplete(() =>
        {

        });
    }

    public void Normal()
    {
        throw new NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Knife")
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (jumb)
        {
            jumb = false;
            TouchGrass.GetComponent<SpriteRenderer>().enabled = true;
            TouchGrass.enabled = true;
            TouchGrass.Play("touchGrass", 0, 0);
            transform.DOScaleY(height / 2, 0.2f).OnComplete(() =>
            {
                transform.DOScaleY(height, 0.3f).OnComplete(() =>
                {
                    TouchGrass.GetComponent<SpriteRenderer>().enabled = false;
                    TouchGrass.enabled = false;
                    transform.localScale = Vector3.one;
                    transform.localRotation = Quaternion.Euler(Vector3.zero);
                });
            });
        }
    }

    public void InPool()
    {
        transform.localPosition = new Vector3(0, 15, 0);
        box.enabled = true;
    }

    public void SetSprite()
    {
        if (this.name.Contains("Stupid"))
        {
            usedSprite = EffectStupid;
            spriteItems.sprite = MStupid[UnityEngine.Random.Range(0, MStupid.Length)];
            width = spriteItems.sprite.bounds.size.x;
            height = spriteItems.sprite.bounds.size.y;
        }
        else if (this.name.Contains("Fruit"))
        {
            usedSprite = EffectFruit;
            spriteItems.sprite = Fruit[UnityEngine.Random.Range(0, Fruit.Length)];
            width = spriteItems.sprite.bounds.size.x;
            height = spriteItems.sprite.bounds.size.y;
        }
        else if (this.name.Contains("Pumkin"))
        {
            spriteItems.sprite = Pumkin[UnityEngine.Random.Range(0, Pumkin.Length)];
            usedSprite = EffectPumkin;
            width = spriteItems.sprite.bounds.size.x;
            height = spriteItems.sprite.bounds.size.y;
        }
    }
}
