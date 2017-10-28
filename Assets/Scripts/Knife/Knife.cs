using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class Knife : MonoBehaviour, IKnife
{

    public SpriteRenderer spriteKnife;
    [HideInInspector]
    public bool isIdie;
    [HideInInspector]
    public bool isDrag;
    [HideInInspector]
    public bool isFly;
    [HideInInspector]
    public bool isThow;
    public Transform knifeTransfom;
    public GameObject ChildKnife;
    public Rigidbody2D RBknife;
    public Material[] material;
    [HideInInspector]
    public Vector3 startKnifeTransfom;
    public Animator animatorEffectKnife;
    public RuntimeAnimatorController[] ListAnimaterEffect;
    [HideInInspector]
    public Tween KnifeRotate;
    public BoxCollider2D box;
    public bool isMiss;
    public void Fly()
    {
        spriteKnife.GetComponent<Transform>().localRotation = Quaternion.Euler(Vector3.zero);
        isIdie = false;
        isFly = true;
        box.enabled = true;
    }

    public void Hit()
    {
        isFly = false;
        ImpactKnife(true);
        RBknife.isKinematic = true;
        box.enabled = false;
        animatorEffectKnife.gameObject.GetComponent<TrailRenderer>().Clear();
    }

    public void Idie()
    {
        if (ChildKnife.GetComponent<HandleKnifeSprite>().go != null)
        {
            StopCoroutine(ChildKnife.GetComponent<HandleKnifeSprite>().go);
            ChildKnife.GetComponent<HandleKnifeSprite>().go = null;
        }

        KnifeRotate.Kill();
        isIdie = true;
        isFly = false;
        ResetKnife();
        StopCoroutine(ChildKnife.GetComponent<HandleKnifeSprite>().Rigid());
        box.isTrigger = true;

    }

    public void Miss()
    {

    }
    public void SetUpEffectKnife(string color)
    {
        for (int i = 0; i < material.Length; i++)
        {
            if (color == material[i].name.ToLower())
            {
                //ChildKnife.GetComponent<TrailRenderer>().material = material[i];
                animatorEffectKnife.gameObject.GetComponent<TrailRenderer>().material = material[i];
                animatorEffectKnife.runtimeAnimatorController = ListAnimaterEffect[i];
                break;
            }
        }
    }
    public void ImpactKnife(bool isActive)
    {
        animatorEffectKnife.GetComponent<SpriteRenderer>().enabled = isActive;
        animatorEffectKnife.enabled = isActive;
        animatorEffectKnife.Play(0, 0, 0);
        if (isActive)
        {
            RBknife.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
            RBknife.constraints = RigidbodyConstraints2D.None;
    }
    public void ResetKnife()
    {
        animatorEffectKnife.GetComponent<TrailRenderer>().enabled = true;
        isMiss = false;
        ImpactKnife(false);
        ChildKnife.transform.localPosition = startKnifeTransfom;
        RBknife.isKinematic = true;
        isThow = false;
        ChildKnife.transform.localRotation = Quaternion.Euler(new Vector3(180,0,0));
        animatorEffectKnife.gameObject.GetComponent<TrailRenderer>().Clear();
    }
    private void Start()
    {
        box = ChildKnife.GetComponent<BoxCollider2D>();
        box.enabled = false;
        box.isTrigger = true;
        //ChildKnife.GetComponent<TrailRenderer>().sortingOrder = 31000;
        animatorEffectKnife.gameObject.GetComponent<TrailRenderer>().sortingOrder = 31000;
        Idie();
        knifeTransfom = GetComponent<Transform>();
        spriteKnife = ChildKnife.transform.GetComponent<SpriteRenderer>();
        RBknife = spriteKnife.GetComponent<Rigidbody2D>();
    }

    public void SetUpPhysticKnife()
    {
    }
}
