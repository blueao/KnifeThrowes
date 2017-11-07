﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ModelHandle
{
    //public bool isCanHit;
    //Varible Key
    public const string KeyKnifeSprite = "KnifeLeft";
    public const string KeyScore = "score";
    public const string SetBlueTrail = "blue";
    public const string SetGreenTrail = "green";
    public const string SetRedTrail = "red";
    public const string SetSevenTrail = "seven";
    public const string SetWhiteTrail = "white";
    public const string SetYellowTrail = "yellow";
    public const string SetPinkTrail = "pink";
    //
    private int monsterDeadCount;
    private static ModelHandle instance = null;
    private static readonly object padlock = new object();
    public int Score;
    public Transform CoinStart;
    public Action actionSetCoin;
    public Action<Vector3> actiongGetCoin;
    ModelHandle() { }

    public ScrollRectController mainRect = GameObject.FindObjectOfType(typeof(ScrollRectController)) as ScrollRectController;
    public void SetScore(int score)
    {
        int scores = PlayerPrefs.GetInt(KeyScore);
        scores += score;
        PlayerPrefs.SetInt(KeyScore, scores);
        mainRect.GetComponent<MainGameController>().ScoreNumber.text = scores.ToString();
        actionSetCoin();
    }
    public void setSpriTemp(int index)
    {
        mainRect.KnifeTemp.sprite = mainRect.ListSpriteKnifeContempl[index];
        setActiveLock(true);
    }
    public void RunAnimLock()
    {
        mainRect.RunAnimUnlock();
    }
    public void setActiveLock(bool isActive)
    {
        mainRect.setActiveLock(isActive);
    }
    public void setUpKnifeAfterBuy(int index)
    {
        mainRect.setUseSpriteKnife(index);
    }
    public void ActiveShop(bool isActive)
    {
        mainRect.GetComponent<MainGameController>().isActiveShopDao(isActive);
    }
    public void AddGOListShop(GameObject go)
    {
        if (!mainRect.listGO.Contains(go))
        {
            mainRect.listGO.Add(go);
            mainRect.InitShop();
        }
    }
    public void setSpriteKnifePos()
    {
        mainRect.GetComponent<MainGameController>().setPosKnifeSprite();
    }
    public static ModelHandle Instance
    {
        get
        {
            if (instance == null)
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ModelHandle();
                    }
                }
            }
            return instance;
        }
    }

    public int MonsterDeadCount
    {
        get { return monsterDeadCount; }
        set
        {
            monsterDeadCount = value;
            if (mainRect.GetComponent<MainGameController>().ListTotalObject.Count == ModelHandle.Instance.monsterDeadCount)
            {
                mainRect.GetComponent<MainGameController>().CanWin = true;
            }
        }
    }
}
