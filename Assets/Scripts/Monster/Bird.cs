using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Bird : MonoBehaviour, IMonster
{
    SpriteRenderer sprite;
    public Sprite spriteDead;
    BoxCollider2D box;
    float width;
    float height;
    public Sprite[] ListSprite;
    public Sprite[] ListBloodSprite;
    public SpriteRenderer EffectBlood;
    Tween move;
    Tween anim;
    Tween death;
    Rigidbody2D rdBird;
    Vector3 startposition;
    private void Start()
    {
       
        sprite = GetComponent<SpriteRenderer>();
        gameObject.AddComponent<BoxCollider2D>();
        box = GetComponent<BoxCollider2D>();
        startposition = transform.localPosition;
        gameObject.AddComponent<Rigidbody2D>();
        rdBird = gameObject.GetComponent<Rigidbody2D>();
        rdBird.gravityScale = 0;
        SetSprite();
        box.isTrigger = true;
    }
    //public int hp;
    public void setPosition(Vector3 v3)
    {
        transform.localPosition = v3;
    }
    [ContextMenu("die")]
    public void Die()
    {
        if (move != null)
        {
            move.Kill();
            move = null;
        }
        //hp++;
        ModelHandle.Instance.MonsterDeadCount++;
        StartCoroutine(WaitForDead());
    }
    public IEnumerator WaitForDead()
    {
        box.enabled = false;
        sprite.enabled = false;
        rdBird.constraints = RigidbodyConstraints2D.FreezeAll;
   
        death = DOTween.To(() => 0, x => EffectBlood.sprite = ListBloodSprite[x], ListBloodSprite.Length - 1, 1f).OnComplete(() =>
        {
            EffectBlood.sprite = null;
            if (death != null)
            {
                death.Kill();
                death = null;
            }
        });
  
        yield return new WaitUntil(() => death == null);
        rdBird.constraints = RigidbodyConstraints2D.None;
        sprite.enabled = true;
        box.enabled = true;
        Move();
        //if (hp == 2)
        //{
            if (anim != null)
            {
                anim.Kill();
                anim = null;
            }

            sprite.enabled = false;
            ModelHandle.Instance.SetScore(10);
            //hp = 0;
            InPool();
            box.enabled = false;
        //}
    }
    public void Fly()
    {
    }
    public void InPool()
    {
        //ModelHandle.Instance.actiongGetCoin(this.transform.localPosition);
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
    [ContextMenu("Move")]
    public void Move()
    {
        anim = DOTween.To(() => 0, x => sprite.sprite = ListSprite[x], ListSprite.Length - 1, 0.3f).OnComplete(() =>
        {
        }).SetLoops(-1);
        move = transform.DOLocalMoveX(transform.localPosition.x - 10, 6f).OnComplete(() =>
        {
            InPool();
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
        box.size = new Vector2(3, 2);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Knife")
        {
            Die();
        }
        if (collision.name == "StartMove")
        {
            Move();
        }
    }
}
