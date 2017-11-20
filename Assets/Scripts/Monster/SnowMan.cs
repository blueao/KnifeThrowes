using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class SnowMan : MonoBehaviour {

    private BoxCollider2D box;
    public Sprite[] SpriteSnowMan;
    public Sprite[] EffectNoelDead;
    private SpriteRenderer spriteItems;
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
            ModelHandle.Instance.SetScore(10);
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
                se.Append(movetween = transform.DOLocalMoveX(transform.localPosition.x - width * 2f, 1f));
                heightJumb = height * 2;
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
        if (collision.name == "BorderBottom")
        {
            speedmove = 1;
            heightJumb = height * 4f;
        }
        if (collision.name == "Knife")
        {
            ModelHandle.Instance.setPosCoinPool(this.transform, 10);
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
                DoScale = transform.DOScaleY(0.5f, 0.2f).OnComplete(() =>
                {
                    transform.DOScaleY(1, 0.3f).OnComplete(() =>
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
        speedmove = 2;
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
            usedSprite = EffectNoelDead;
            spriteItems.sprite = SpriteSnowMan[UnityEngine.Random.Range(0, SpriteSnowMan.Length)];
            width = spriteItems.sprite.bounds.size.x;
            height = spriteItems.sprite.bounds.size.y/2;
    }
}
