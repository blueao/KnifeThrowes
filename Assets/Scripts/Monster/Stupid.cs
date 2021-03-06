﻿using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class Stupid : MonoBehaviour, IMonster
{
    private BoxCollider2D box;
    public Sprite[] MStupid;
    public Sprite[] Pumkin;
    public Sprite[] Fruit;
    public Sprite[] FruitNoel;
    public Sprite[] PumKinNoel;
    public Sprite[] EffectStupid;
    public Sprite[] EffectPumkin;
    public Sprite[] EffectFruit;
    public Sprite[] EffectNoelDead;
    public SpriteRenderer spriteItems;
    Sprite[] usedSprite;
    private float width;
    private float height;
    public bool jumb;
    public Animator TouchGrass;
    Rigidbody2D rdStupid;
    Tween anim;
    Tween jumtween;
    Tween movetween;
    public bool isActiveMove;
    public Sequence se;

    private void Start()
    {

        spriteItems = GetComponent<SpriteRenderer>();
        SetSprite();
        gameObject.AddComponent<BoxCollider2D>();
        box = GetComponent<BoxCollider2D>();
        rdStupid = GetComponent<Rigidbody2D>();
        if (!transform.name.Contains("Pumkin"))
        {
            box.isTrigger = true;
        }
        rdStupid.isKinematic = true;
        se = DOTween.Sequence();
        heightJumb = 4f;
        rdStupid.constraints = RigidbodyConstraints2D.FreezeRotation;
        // box.enabled = false;
    }
    private void OnEnable()
    {
        heightJumb = 4f;
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
                  ModelHandle.Instance.SetScore(1);
                  ModelHandle.Instance.MonsterDeadCount++;
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
            if (transform.name.Contains("Fruit"))
            {
                se.Append(movetween = transform.DOLocalMoveX(transform.localPosition.x - width * 1.5f, 1f));
                heightJumb = UnityEngine.Random.Range(height, height * 2);
            }
            if (transform.name.Contains("Pumkin"))
            {
                se.Append(movetween = transform.DOLocalMoveX(transform.localPosition.x - width * 2f, 1f));
                heightJumb = height * 2;
            }
            if (transform.name.Contains("Stupid"))
            {
                se.Append(movetween = transform.DOLocalMoveX(transform.localPosition.x - width, 1f));
                heightJumb = UnityEngine.Random.Range(height, height * 2);
            }
        }
        jumb = true;

        se.Join(jumtween = transform.DOLocalMoveY(transform.localPosition.y + heightJumb, 0.5f));
        onTheGround = false;
    }
    float heightJumb;
    int speedmove;
    public void Normal()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.GetComponent<Stone>())
        //{
        //    isActiveMove = true;

        //    box.isTrigger = false;
        //}
        if (collision.name == "BorderBottom")
        {
            speedmove = 1;
            heightJumb = height * 4f;
        }
        if (collision.name == "Knife")
        {
            ModelHandle.Instance.setPosCoinPool(this.transform, 1);
            ModelHandle.Instance.SetSound(ModelHandle.StupidDead);
            Die();
        }
        if (collision.name == "End")
        {
            Die();
        }
        if (collision.name == "StartMove")
        {
            ModelHandle.Instance.SetSound(ModelHandle.StupidAli);
            //rdStupid.isKinematic = false;
            isActiveMove = true;
            rdStupid.isKinematic = false;
            box.isTrigger = false;
            if (isActiveMove)
            {
                Move();
            }
        }


    }
    Tween DoScale;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!rdStupid.isKinematic)
        {
            onTheGround = true;
            if (jumb)
            {
                jumb = false;
                TouchGrass.GetComponent<SpriteRenderer>().enabled = true;
                TouchGrass.enabled = true;
                TouchGrass.Play("touchGrass", 0, 0);
                DoScale = transform.DOScaleY(height / 2, 0.2f).OnComplete(() =>
                {
                    transform.DOScaleY(height, 0.3f).OnComplete(() =>
                    {
                        TouchGrass.GetComponent<SpriteRenderer>().enabled = false;
                        TouchGrass.enabled = false;
                        transform.localScale = Vector3.one;
                        transform.localRotation = Quaternion.Euler(Vector3.zero);
                        if (gameObject.activeSelf)
                        {
                            StartCoroutine(StartMove());
                        }
                    });
                });
            }
        }
    }
    //public Coroutine CoMove;
    bool onTheGround;
    public IEnumerator StartMove()
    {
        int a = UnityEngine.Random.Range(0, 2);
        yield return new WaitUntil(() => onTheGround);
        Move();
    }
    public void InPool()
    {
        //if (CoMove != null)
        //{
        //    StopCoroutine(CoMove);
        //    CoMove = null;
        //}
        speedmove = 2;
        //ModelHandle.Instance.actiongGetCoin(this.transform.localPosition);
        transform.localPosition = new Vector3(0, 15, 0);
        if (GetComponent<BoxCollider2D>() != null)
        {
            box.enabled = true;
            box.isTrigger = true;
        }

        rdStupid.isKinematic = true;
        spriteItems.enabled = true;
        SetSprite();
        if (DoScale != null)
        {
            DoScale.Kill();
            DoScale = null;
        }
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
        gameObject.SetActive(false);
    }
    public void ResetState()
    {
        isActiveMove = false;
        jumb = false;
        StopCoroutine(StartMove());
        gameObject.SetActive(false);
        if (rdStupid != null)
        {
            rdStupid.isKinematic = true;
        }
        //if (CoMove != null)
        //{
        //    StopCoroutine(CoMove);
        //    CoMove = null;
        //}
        //if (anim != null)
        //{
        //    anim.Kill();
        //    anim = null;
        //}
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
            if (ModelHandle.Instance.isNoel)
            {
                usedSprite = EffectNoelDead;
                spriteItems.sprite = FruitNoel[UnityEngine.Random.Range(0, FruitNoel.Length)];
            }
            else
            {
                usedSprite = EffectFruit;
                spriteItems.sprite = Fruit[UnityEngine.Random.Range(0, Fruit.Length)];
            }
            width = spriteItems.sprite.bounds.size.x;
            height = spriteItems.sprite.bounds.size.y;
        }
        else if (this.name.Contains("Pumkin"))
        {
            if (ModelHandle.Instance.isNoel)
            {
                spriteItems.sprite = PumKinNoel[UnityEngine.Random.Range(0, PumKinNoel.Length)];
                usedSprite = EffectNoelDead;
            }
            else
            {
                spriteItems.sprite = Pumkin[UnityEngine.Random.Range(0, Pumkin.Length)];
                usedSprite = EffectPumkin;
            }
            width = spriteItems.sprite.bounds.size.x;
            height = spriteItems.sprite.bounds.size.y;
        }
    }
}
