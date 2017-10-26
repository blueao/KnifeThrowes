using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class Vulture : MonoBehaviour, IMonster
{
    SpriteRenderer sprite;
    BoxCollider2D box;
    float width;
    float height;
    public Sprite[] SpriteVulture;
    Sequence se;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        gameObject.AddComponent<BoxCollider2D>();
        box = GetComponent<BoxCollider2D>();
        SetSprite();
    }
    public void Die()
    {
        box.enabled = false;
        ShowVultureDead();
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
        if (se!=null)
        {
            se.Kill();
            se = null;
        }
        box.enabled = true;
        sprite.enabled = true;
        gameObject.SetActive(false);
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
        if (this.name.Contains("vulture (1)"))
        {
            sprite.sprite = SpriteVulture[0];
            width = sprite.sprite.bounds.size.x;
            height = sprite.sprite.bounds.size.y;

        }
        else if (this.name.Contains("vulture (2)"))
        {
            sprite.sprite = SpriteVulture[1];
            width = sprite.sprite.bounds.size.x;
            height = sprite.sprite.bounds.size.y;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Knife")
        {
            Die();
        }
    }
    void ShowVultureDead()
    {
        se = DOTween.Sequence();
        se.Append(transform.DOLocalRotate(new Vector3(0, 0, 180), 2f));
        se.Join(transform.DOLocalMoveY(transform.localPosition.y-2, 0.5f).OnComplete(() =>
        {
            sprite.enabled = false;
            InPool();
        }));

    }
}
