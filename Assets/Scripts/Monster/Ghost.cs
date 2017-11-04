using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using System.Collections.Generic;

public class Ghost : MonoBehaviour, IMonster
{

    public GameObject ghostShadow;
    public List<GameObject> listshadow = new List<GameObject>();
    public void Die()
    {
        isMove = false;
        box.enabled = false;
        StartCoroutine(WaitForDead());

    }

    public void Fly()
    {
        throw new NotImplementedException();
    }
    Tween move;
    public void InPool()
    {
        box.enabled = true;
        transform.localPosition = new Vector3(0, 15, 0);
        gameObject.SetActive(false);
        if (move != null)
        {
            move.Kill();
            move = null;
        }

    }
    bool isMove;
    [ContextMenu("MoveGhost")]
    public void Move()
    {
        isMove = true;
        move = transform.DOLocalMoveX(transform.localPosition.x - 10, 8f).OnComplete(() =>
        {
            InPool();
        });
    }
    int indexShadow;
    float time;
    private void FixedUpdate()
    {
        if (isMove)
        {
            for (int i = 0; i < listshadow.Count; i++)
            {
                time += 1f * Time.fixedDeltaTime;
                if (!listshadow[i].activeSelf)
                {
                    listshadow[i].transform.localPosition = transform.localPosition;
                    listshadow[i].SetActive(true);
                }
                if (listshadow[i].activeSelf && time > 0.2f)
                {
                    listshadow[i].SetActive(false);
                    time = 0;
                }
            }
        }

    }
    public void Normal()
    {
        throw new NotImplementedException();
    }

    public void SetSprite()
    {
        throw new NotImplementedException();
    }
    GameObject shadow;
    public BoxCollider2D box;
    private void Start()
    {
        gameObject.AddComponent<BoxCollider2D>();
        for (int i = 0; i < 5; i++)
        {
            shadow = (GameObject)Instantiate(ghostShadow, this.transform.TransformPoint(transform.localPosition), this.transform.localRotation);
            shadow.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            shadow.SetActive(false);
            shadow.transform.parent = transform.parent;
            listshadow.Add(shadow);
        }
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();

    }
    SpriteRenderer sprite;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Knife")
        {
            Die();
        }
        if (collision.name == "End")
        {
            InPool();
        }
    }
    public void setPosition(Vector3 v3)
    {
        transform.localPosition = v3;
    }
    Tween death;
    public Sprite[] ListBloodSprite;
    public SpriteRenderer EffectBlood;
    public IEnumerator WaitForDead()
    {
        for (int i = 0; i < listshadow.Count; i++)
        {
            listshadow[i].transform.localPosition = transform.localPosition;
            listshadow[i].SetActive(false);
        }
        death = DOTween.To(() => 0, x => EffectBlood.sprite = ListBloodSprite[x], ListBloodSprite.Length - 1, 2f).OnComplete(() =>
        {
            EffectBlood.sprite = null;
            if (death != null)
            {
                death.Kill();
                death = null;
            }
        });
        yield return new WaitUntil(() => death == null);
        sprite.enabled = false;
        ModelHandle.Instance.SetScore(10);
        InPool();
    }
}
