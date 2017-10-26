using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class Cow : MonoBehaviour,IMonster {
    SpriteRenderer sprite;
    BoxCollider2D box;
    float width;
    float height;
    public Sprite[] ListSprite;
    Vector3 startposition;
    int heart;
    public void Die()
    {
        box.enabled = false;
        heart++;
        StartCoroutine(WaitForHit());
    }
    public IEnumerator WaitForHit()
    {
        sprite.sprite = ListSprite[1];
        yield return new WaitForSeconds(0.5f);
        if (heart==3)
        {
            InPool();
        }
        sprite.sprite = ListSprite[0];
        box.enabled = true;
;    }
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
        sprite.enabled = true;
        heart = 0;
        transform.localPosition = new Vector3(0, 15, 0);
        box.enabled = true;
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
        sprite.sprite = ListSprite[0];
        box.size = new Vector2(2.5f, 2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Knife")
        {
            Die();
        }
    }
    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        gameObject.AddComponent<BoxCollider2D>();
        box = GetComponent<BoxCollider2D>();
        startposition = transform.localPosition;
        SetSprite();
        box.isTrigger = true;
    }
	

}
