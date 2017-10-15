using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Boar : MonoBehaviour,IMonster {
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
    public void Die()
    {
        StartCoroutine(WaitForDead());
        box.enabled = false;

    }
    public IEnumerator WaitForDead()
    {
        sprite.sprite = spriteDead;
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
        yield return new WaitForSeconds(0.5f);
        sprite.enabled = false;
        InPool();
    }
    public void Fly()
    {
    }

    public void InPool()
    {
        transform.localPosition = new Vector3(0, 15, 0);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        box.enabled = true;
    }

    public void Move()
    {
        anim = DOTween.To(() => 0, x => sprite.sprite = ListSprite[x], ListSprite.Length - 1, 1f).OnComplete(() =>
        {
        }).SetLoops(-1);
        move = transform.DOLocalMoveX(endposition.x, 8f).OnComplete(() =>
        {
        });
    }

    public void Normal()
    {
    }

    public void SetSprite()
    {
        sprite.sprite = ListSprite[0];
        width = sprite.sprite.bounds.size.x;
        height = sprite.sprite.bounds.size.y;
        box.size = new Vector2(3,2);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Knife")
        {
            Die();
        }
    }
}
