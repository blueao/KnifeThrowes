using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class DogCrazy : MonoBehaviour, IMonster
{
    SpriteRenderer sprite;
    public Sprite spriteDead;
    BoxCollider2D box;
    float width;
    float height;
    public Sprite[] ListSprite;
    Tween move;
    Tween anim;
    Vector3 startposition;
    Vector3 endposition;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        gameObject.AddComponent<BoxCollider2D>();
        box = GetComponent<BoxCollider2D>();
        startposition = transform.localPosition;
        endposition = new Vector3(-12, transform.localPosition.y, transform.localPosition.z);
        SetSprite();
        box.isTrigger = true;
    }
    public void setPosition(Vector3 v3)
    {
        transform.localPosition = v3;
    }
    [ContextMenu("die")]
    public void Die()
    {
        ModelHandle.Instance.MonsterDeadCount++;
        StartCoroutine(WaitForDead());
        box.enabled = false;
       
    }
    public Sprite[] ListBloodSprite;
    public SpriteRenderer EffectBlood;
    Tween death;

    public IEnumerator WaitForDead()
    {
        death = DOTween.To(() => 0, x => EffectBlood.sprite = ListBloodSprite[x], ListBloodSprite.Length - 1, 2f).OnComplete(() =>
        {
            EffectBlood.sprite = null;
            if (death != null)
            {
                death.Kill();
                death = null;
            }
        });
        sprite.sprite = spriteDead;
        if (anim != null)
        {
            anim.Kill();
            anim = null;
        }
     
        yield return new WaitUntil(() => death == null);
        if (move != null)
        {
            move.Kill();
            move = null;
        }
        sprite.enabled = false;
        ModelHandle.Instance.SetScore(10);
        InPool();
    }
    public void Fly()
    {
        throw new NotImplementedException();
    }

    public void InPool()
    {
        ModelHandle.Instance.actiongGetCoin(this.transform.localPosition);
        if (anim != null)
        {
            anim.Kill();
            anim = null;
        }
        if (move != null)
        {
            move.Kill();
            move = null;
        }
        transform.localPosition = new Vector3(0, 15, 0);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        box.enabled = true;
        box.isTrigger = true;
        SetSprite();
        sprite.enabled = true;
        gameObject.SetActive(false);
    }

    public void Move()
    {
        anim = DOTween.To(() => 0, x => sprite.sprite = ListSprite[x], ListSprite.Length - 1, 0.3f).OnComplete(() =>
        {
        }).SetLoops(-1);
        move = transform.DOLocalMoveX(transform.localPosition.x-10, 4f).OnComplete(() =>
        {
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Knife")
        {
            ModelHandle.Instance.SetSound(ModelHandle.DogDead);
            Die();
        }
        if (collision.name == "StartMove")
        {
            ModelHandle.Instance.SetSound(ModelHandle.DogAli);
            Move();
        }
    }
}
