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

    public MainGameController MainGame;
    public BoxCollider2D box;
    public bool isMiss;
    public void Fly()
    {
        spriteKnife.GetComponent<Transform>().localRotation = Quaternion.Euler(Vector3.zero);
        isIdie = false;
        isFly = true;
        box.enabled = true;
    }

    public void Hit(bool active)
    {

        MainGame.IsDrop = false;
        if (ChildKnife.GetComponent<HandleKnifeSprite>().RotateKnifeLoop != null)
        {
            ChildKnife.GetComponent<HandleKnifeSprite>().RotateKnifeLoop.Kill();
            ChildKnife.GetComponent<HandleKnifeSprite>().RotateKnifeLoop = null;
        }
        isFly = false;
        ImpactKnife(active);
        RBknife.isKinematic = true;
        box.enabled = false;
        animatorEffectKnife.gameObject.GetComponent<TrailRenderer>().Clear();
    }

    public void Idie()
    {
        ChildKnife.name = "Knife";
        MainGame.IsDrop = false;
        if (ChildKnife.GetComponent<HandleKnifeSprite>().RotateKnifeLoop != null)
        {
            ChildKnife.GetComponent<HandleKnifeSprite>().RotateKnifeLoop.Kill();
            ChildKnife.GetComponent<HandleKnifeSprite>().RotateKnifeLoop = null;
        }
        if (ChildKnife.GetComponent<HandleKnifeSprite>().go != null)
        {
            StopCoroutine(ChildKnife.GetComponent<HandleKnifeSprite>().go);
            ChildKnife.GetComponent<HandleKnifeSprite>().go = null;
        }
        if (KnifeRotate != null)
        {
            KnifeRotate.Kill();
            KnifeRotate = null;
        }
        isIdie = true;
        isFly = false;
        ResetKnife();
        box.isTrigger = true;
        box.enabled = false;
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
                // animatorEffectKnife.gameObject.GetComponent<TrailRenderer>().material = material[i];
                animatorEffectKnife.runtimeAnimatorController = ListAnimaterEffect[i];
                break;
            }
        }
    }
    public void ImpactKnife(bool isActive)
    {
        animatorEffectKnife.GetComponent<SpriteRenderer>().enabled = isActive;
        animatorEffectKnife.enabled = isActive;
        if (isActive)
        {
            animatorEffectKnife.Play(0, 0, 0);
            RBknife.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            RBknife.constraints = RigidbodyConstraints2D.None;
        }

    }
    public void ResetKnife()
    {
        isIdie = true;
        MainGame.IsDrop = false;
        animatorEffectKnife.GetComponent<TrailRenderer>().enabled = true;
        isMiss = false;
        ImpactKnife(false);
        ChildKnife.transform.localPosition = startKnifeTransfom;
        RBknife.isKinematic = true;
        isThow = false;
        ChildKnife.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
        animatorEffectKnife.gameObject.GetComponent<TrailRenderer>().Clear();
        RBknife.constraints = RigidbodyConstraints2D.None;
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
