using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Santa : MonoBehaviour
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
    Sequence move1;
    Tween anim;
    Tween death;
    Vector3 startposition;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        gameObject.AddComponent<BoxCollider2D>();
        box = GetComponent<BoxCollider2D>();
        startposition = transform.localPosition;

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
        StartCoroutine(DropCoin());
        ModelHandle.Instance.MonsterDeadCount++;
        StartCoroutine(WaitForDead());
        box.enabled = false;

    }
    IEnumerator DropCoin()
    {
        yield return new WaitForSeconds(0.2f);
        ModelHandle.Instance.setPosCoinPool(this.transform, 10);
        yield return new WaitForSeconds(0.2f);
        ModelHandle.Instance.setPosCoinPool(this.transform, 10);
        yield return new WaitForSeconds(0.2f);
        ModelHandle.Instance.setPosCoinPool(this.transform, 10);
        yield return new WaitForSeconds(0.2f);
        ModelHandle.Instance.setPosCoinPool(this.transform, 10);
        yield return new WaitForSeconds(0.2f);
        ModelHandle.Instance.setPosCoinPool(this.transform, 10);
    }
    public IEnumerator WaitForDead()
    {
        death = DOTween.To(() => 0, x => EffectBlood.sprite = ListBloodSprite[x], ListBloodSprite.Length - 1, 1f).OnComplete(() =>
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
        sprite.enabled = false;
        ModelHandle.Instance.SetScore(50);
        InPool();
    }
    public void Fly()
    {
    }

    public void InPool()
    {
        // ModelHandle.Instance.actiongGetCoin(this.transform.localPosition);
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
        if (move1 != null)
        {
            move1.Kill();
            move1 = null;
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
        move = transform.DOLocalMoveX(transform.localPosition.x - 15f, 3f).OnComplete(() =>
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
            ModelHandle.Instance.SetSound(ModelHandle.BoarDead);
            Die();
        }
        if (collision.name == "StartMove")
        {
            ModelHandle.Instance.SetSound(ModelHandle.BoarApp);
            Move();
        }
        if (collision.name == "End")
        {
            Die();
        }
    }
}
