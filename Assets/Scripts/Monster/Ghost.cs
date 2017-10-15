using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class Ghost : MonoBehaviour,IMonster {

    public GhostShadow ghostShadow;

    private static Ghost intance = null;
    private static readonly object padlock = new object();
    private Ghost() { }
    public static Ghost Intance
    {
        get
        {
            lock (padlock)
            {


                if (intance == null)
                {
                    intance = FindObjectOfType<Ghost>();
                }
                return intance;
            }
        }

    }

    public void Die()
    {
        throw new NotImplementedException();
    }

    public void Fly()
    {
        throw new NotImplementedException();
    }

    public void InPool()
    {
        throw new NotImplementedException();
    }
    [ContextMenu("MoveGhost")]
    public void Move()
    {
        GameObject a = (GameObject) Instantiate(ghostShadow,transform.localPosition,transform.localRotation);
        transform.DOLocalMoveX(-100, 50f);
    }

    public void Normal()
    {
        throw new NotImplementedException();
    }

    public void SetSprite()
    {
        throw new NotImplementedException();
    }

    private void Start()
    {
        //transform.GetComponent<TrailRenderer>().sortingOrder = 31500;
    }
    public void setPosition(Vector3 v3)
    {
        transform.localPosition = v3;
    }

}
