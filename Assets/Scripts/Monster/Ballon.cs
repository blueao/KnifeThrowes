using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class Ballon : MonoBehaviour, IMonster
{

    public Sprite[] ListSpriteDeadBallon;
    public Sprite[] ListSpriteBallon;
    SpriteRenderer sprite;
    BoxCollider2D box;
    float width;
    float height;

    Tween tweenMove;
    Tween tweenAnim;
    private void Start()
    {
        gameObject.AddComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        box.isTrigger = true;
        SetSprite();
    }
    public void setPosition(Vector3 v3)
    {
        transform.localPosition = v3;
    }
    public void Die()
    {
        ShowBallonDead();
        box.enabled = false;
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
        box.isTrigger = true;
        SetSprite();
        transform.localPosition = new Vector3(0, 15, 0);
        if (tweenAnim != null)
        {
            tweenAnim.Kill();
            tweenAnim = null;
        }
        sprite.enabled = true;
    }

    public void Move()
    {
        tweenMove = transform.DOLocalMoveY(15, 40f);
    }
    public IEnumerator startMove()
    {
        Move();
        yield return new WaitForSeconds(1f);
    }
    public void Normal()
    {
        throw new NotImplementedException();
    }

    public void SetSprite()
    {
        sprite.sprite = ListSpriteBallon[UnityEngine.Random.Range(0, ListSpriteBallon.Length)];
        width = sprite.sprite.bounds.size.x;
        height = sprite.sprite.bounds.size.y;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Knife")
        {
            Die();
        }
        if (collision.name == "StartMove")
        {
            StartCoroutine(startMove());
        }

    }
    void ShowBallonDead()
    {

        tweenAnim = DOTween.To(() => 0, x => sprite.sprite = ListSpriteDeadBallon[x], ListSpriteDeadBallon.Length - 1, 1f).OnComplete(() =>
         {
             sprite.enabled = false;
             InPool();
         });

    }
}
