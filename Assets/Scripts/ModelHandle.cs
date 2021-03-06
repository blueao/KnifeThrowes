﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class ModelHandle
{
    //public bool isCanHit;
    //Varible Key
    public const string KeyKnifeSprite = "KnifeLeft";
    public const string KeyKnifeSpriteCut = "KnifeLeftCut";
    public const string KeyScore = "score";
    public const string SetBlueTrail = "blue";
    public const string SetGreenTrail = "green";
    public const string SetRedTrail = "red";
    public const string SetSevenTrail = "seven";
    public const string SetWhiteTrail = "white";
    public const string SetYellowTrail = "yellow";
    public const string SetPinkTrail = "pink";

    // SoundKey
    public const string StupidDead = "StupidDie";
    public const string StupidAli = "StupidAlive";
    public const string SpiderApp = "SpiderAppear";
    public const string RavenDead = "RavenDie";
    public const string RavenApp = "RavenAppear";
    public const string HitWood = "KnifeHitWood";
    public const string GhostDead = "GhostDie";
    public const string FruitDead = "FruitDie";
    public const string DogDead = "DogDie";
    public const string DogAli = "DogAlive";
    public const string CowDead = "CowDie";
    public const string CowApp = "CowAppear";
    public const string BoarDead = "BoarDie";
    public const string BoarApp = "BoarAppear";
    public const string ButtonCli = "ButtonClick";
    public const string BatDead = "BatDie";
    public const string BatApp = "BatAppear";
    public const string BallonEx = "BallonExp";
    //Key Load Map2 
    public const string SetMap2 = "ActiveMap2";
    public const string SetMap3 = "ActiveMap3";
    //
    public const string GetrewardMap1 = "GetrewardMap1";
    public const string GetrewardMap2 = "GetrewardMap2";
    public const string GetrewardMap3 = "GetrewardMap3";
    //
    public bool isNoel;
    public bool isLose;
        //
    private int monsterDeadCount;
    private static ModelHandle instance = null;
    private static readonly object padlock = new object();
    public int Score;
    public Transform CoinStart;
    public Action actionSetCoin;
    public Action<Vector3> actiongGetCoin;
    [HideInInspector]
    public List<GameObject> ListNewCoinPool = new List<GameObject>();
    public bool[] isObjBuyed = new bool[34];
    public float cameraSetting;
    ModelHandle() { }
    public float currentKnifeLocation;

    public ScrollRectController mainRect;/* = GameObject.FindObjectOfType(typeof(ScrollRectController)) as ScrollRectController;*/
    public void SetScore(int score)
    {
        int scores = PlayerPrefs.GetInt(KeyScore);
        scores += score;
        PlayerPrefs.SetInt(KeyScore, scores);
        mainRect.GetComponent<MainGameController>().ScoreNumber.text = scores.ToString();
        mainRect.GetComponent<MainGameController>().CoinMenu.text = scores.ToString();
        //actionSetCoin();
    }
    public void setScoreText(int score)
    {
        int scores = PlayerPrefs.GetInt(KeyScore);
        PlayerPrefs.SetInt(KeyScore, scores);
        mainRect.GetComponent<MainGameController>().ScoreNumber.text = scores.ToString();
        mainRect.GetComponent<MainGameController>().CoinMenu.text = scores.ToString();
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
    public void setUpSpriteCutAfterBuy(int index)
    {
        mainRect.setUseSpriteKnifeCut(index);
    }
    public void ActiveShop(bool isActive)
    {
        mainRect.GetComponent<MainGameController>().isActiveShopDao(isActive);
    }
    public void SetSound(string sound)
    {
        mainRect.GetComponent<MainGameController>().SetSound(sound);
    }
    public void setSpriteKnifePos(float xTarget,float yTarget)
    {
        mainRect.GetComponent<MainGameController>().setPosKnifeSprite(xTarget, yTarget);
    }
    public void setVolumn(float background, float Sound)
    {
        mainRect.GetComponent<MainGameController>().BackGround.volume = background;
        mainRect.GetComponent<MainGameController>().vollumn = Sound;
        mainRect.GetComponent<MainGameController>().SoundManager.volume = Sound;
    }
    public void ClosePanelSound()
    {
        mainRect.GetComponent<MainGameController>().PanelSound.SetActive(false);
    }
    public void ActivePanelAdsmobUn(bool isActive)
    {
        mainRect.GetComponent<MainGameController>().isActivePanelAdsModUn(isActive);
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
    public void setPosCoinPool(Transform parent,int point)
    {
        mainRect.GetComponent<MainGameController>().setPosCoinPool(parent, point);
    }

    public void PurchaseSuccess(int score)
    {
        SetScore(score);
    }
    public void HandleLoseGame()
    {
        mainRect.GetComponent<MainGameController>().MainGameHandleLoseGame();
    }
    public void DesObjSpriteKnifeCut()
    {
        mainRect.GetComponent<MainGameController>().objSpriteActive--;
    }
}
