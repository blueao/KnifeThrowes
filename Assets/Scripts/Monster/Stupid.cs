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
    Tween anim;
    Tween jumtween;
    Tween movetween;
    bool isActiveMove;
    Sequence se;
    private void Start()
    {
        gameObject.AddComponent<BoxCollider2D>();
        box = GetComponent<BoxCollider2D>();
        spriteItems = GetComponent<SpriteRenderer>();
        rdStupid = GetComponent<Rigidbody2D>();
        box.isTrigger = true;
        rdStupid.isKinematic = true;
        SetSprite();
        se = DOTween.Sequence();

    }
    public void setPosition(Vector3 v3)
    {
        transform.localPosition = v3;
    }
    public void Die()
    {
        ShowStupidDead();
        box.enabled = false;
        if (jumtween != null)
        {
            jumtween.Kill();
            jumtween = null;
        }

    }
    void ShowStupidDead()
    {
        rdStupid.constraints = RigidbodyConstraints2D.FreezeAll;
        anim = DOTween.To(() => 0, x => spriteItems.sprite = usedSprite[x], usedSprite.Length - 1, 1f).OnComplete(() =>
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
        if (isActiveMove)
        {
            se.Append(movetween = transform.DOLocalMoveX(transform.localPosition.x - width / 4, 1f));
        }
        jumb = true;
        float a = UnityEngine.Random.Range(height, height * 2);
        se.Join(jumtween = transform.DOLocalMoveY(transform.localPosition.y + a, 0.5f));

    }

    public void Normal()
    {
        throw new NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Stone>())
        {
            isActiveMove = true;
            rdStupid.isKinematic = false;
            box.isTrigger = false;
        }
        if (collision.name == "Knife")
        {
            Die();
        }
        if (collision.name == "StartMove")
        {
            if (isActiveMove)
            {

                Move();
            }
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
                    StartCoroutine(StartMove());
                });
            });
        }
    }

    public IEnumerator StartMove()
    {
        int a = UnityEngine.Random.Range(0, 2);
        yield return new WaitForSeconds(a);
        Move();
    }
    public void InPool()
    {
        transform.localPosition = new Vector3(0, 15, 0);
        box.enabled = true;
        box.isTrigger = true;
        rdStupid.isKinematic = true;
        spriteItems.enabled = true;
        SetSprite();
        if (anim != null)
        {
            anim.Kill();
            anim = null;
        }
        if (jumtween != null)
        {
            jumtween.Kill();
            jumtween = null;
        }
        if (movetween != null)
        {
            movetween.Kill();
            movetween = null;
        }
        if (se != null)
        {
            se.Kill();
            se = null;
        }
        rdStupid.constraints = RigidbodyConstraints2D.None;
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
