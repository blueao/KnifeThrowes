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
    BoxCollider2D box;
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
    }

    public void Idie()
    {
        KnifeRotate.Kill();
        isIdie = true;
        isFly = false;
        ResetKnife();
    }

    public void Miss()
    {
        throw new NotImplementedException();
    }
    public void SetUpEffectKnife(string color)
    {
        for (int i = 0; i < material.Length; i++)
        {
            if (color == material[i].name.ToLower())
            {
                ChildKnife.GetComponent<TrailRenderer>().material = material[i];
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
    private void ResetKnife()
    {
        ImpactKnife(false);
        ChildKnife.transform.localPosition = startKnifeTransfom;
        RBknife.isKinematic = true;
        isThow = false;
        ChildKnife.transform.localRotation = Quaternion.Euler(Vector3.zero);

    }
    private void Start()
    {
        box = ChildKnife.GetComponent<BoxCollider2D>();
        box.enabled = false;
        ChildKnife.GetComponent<TrailRenderer>().sortingOrder = 31000;
        Idie();
        knifeTransfom = GetComponent<Transform>();
        spriteKnife = ChildKnife.transform.GetComponent<SpriteRenderer>();
        RBknife = spriteKnife.GetComponent<Rigidbody2D>();
    }

}
