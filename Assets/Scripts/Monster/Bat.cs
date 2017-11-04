using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class Bat : MonoBehaviour, IMonster
{
    SpriteRenderer sprite;
    BoxCollider2D box;
    float width;
    float height;
    Tween move;
    public Sprite[] ListSprite;
    Vector3 startposition;
    DOTweenVisualManager tweenManager;
    Tween tweenPath;
    Vector3[] waypointsDown = new[] { new Vector3(16.86491f, -1.010587f, 0f), new Vector3(13.95837f, -1.027996f, 0f), new Vector3(10.38871f, -1.64771f, 0f), new Vector3(8.216748f, -0.6075552f, 0f), new Vector3(5.729146f, -2.006272f, 0f), new Vector3(2.86819f, -1.020146f, 0f), new Vector3(-1.658837f, -2.465675f, 0f), new Vector3(-6.016564f, -2.307945f, 0f), new Vector3(-10.17516f, -3.108187f, 0f), new Vector3(-15.86872f, -3.698843f, 0f) };
    Vector3[] waypointsUp = new[] { new Vector3(16.6119f, 4.339106f, 0f), new Vector3(14.38006f, 3.905704f, 0f), new Vector3(10.72606f, 3.581168f, 0f), new Vector3(8.42759f, 4.030965f, 0f), new Vector3(5.560472f, 4.529825f, 0f), new Vector3(1.898318f, 3.323196f, 0f), new Vector3(-2.797383f, 4.745114f, 0f), new Vector3(-6.185237f, 3.848635f, 0f), new Vector3(-11.60888f, 4.229108f, 0f), new Vector3(-16.07956f, 3.596285f, 0f) };
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        gameObject.AddComponent<BoxCollider2D>();

        box = GetComponent<BoxCollider2D>();
        startposition = transform.localPosition;
        SetSprite();
        box.isTrigger = true;
    }
    public void Die()
    {
        sprite.enabled = false;
        box.enabled = false;
        if (tweenPath != null)
        {
            tweenPath.Kill();
            tweenPath = null;
        }
        if (move != null)
        {
            move.Kill();
            move = null;
        }
        ModelHandle.Instance.SetScore(1);
        InPool();
    }

    public void Fly()
    {
        throw new NotImplementedException();
    }
    public void setPosition(Vector3 v3)
    {
        transform.localPosition = v3;
    }
    public void InPool()
    {
        ModelHandle.Instance.actiongGetCoin(this.transform.localPosition);
        SetSprite();
        box.isTrigger = true;
        sprite.enabled = true;
        box.enabled = true;
        transform.localPosition = new Vector3(0, 15, 0);
        gameObject.SetActive(false);
    }
    [ContextMenu("Move")]
    public void Move()
    {
        move = DOTween.To(() => 0, x => sprite.sprite = ListSprite[x], ListSprite.Length - 1, 1f).OnComplete(() =>
        {
        }).SetLoops(-1);
        int a = UnityEngine.Random.Range(0, 2);
        if (a == 0)
        {
            tweenPath = transform.DOPath(waypointsUp, 10f, PathType.CatmullRom, PathMode.TopDown2D, 10, Color.black);
        }
        else
            tweenPath = transform.DOPath(waypointsDown, 10f, PathType.CatmullRom, PathMode.TopDown2D, 10, Color.black);
    }

    public void Normal()
    {
        gameObject.SetActive(true);
    }

    public void SetSprite()
    {
        sprite.sprite = ListSprite[0];
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
