using UnityEngine;
using System.Collections;
using System;

public class Knife : MonoBehaviour,IKnife {

    public SpriteRenderer spriteKnife;
    public bool isIdie;
    public bool isDrag;
    public bool isFly;
    public Transform knifeTransfom;
    public GameObject ChildKnife;
    public Rigidbody2D RBknife;
    public void Fly()
    {
        spriteKnife.GetComponent<Transform>().localRotation = Quaternion.Euler(Vector3.zero);
        isIdie = false;
        isFly = true;
        
    }

    public void Hit()
    {
        throw new NotImplementedException();
    }

    public void Idie()
    {
        isIdie = true;
        isFly = false;
    }

    public void Miss()
    {
        throw new NotImplementedException();
    }

    private void Start()
    {
        Idie();
        knifeTransfom =GetComponent<Transform>();
        spriteKnife = ChildKnife.transform.GetComponent<SpriteRenderer>();
        RBknife = spriteKnife.GetComponent<Rigidbody2D>();
    }
    
}
