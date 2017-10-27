using UnityEngine;
using System.Collections;
using System;

public class ModelHandle
{

    private static ModelHandle instance = null;
    private static readonly object padlock = new object();
    public int Score;
    public Transform CoinStart;
    public Action actionSetCoin;
    public Action<Vector3> actiongGetCoin;
    ModelHandle() { }

     ScrollRectController mainRect =   GameObject.FindObjectOfType(typeof(ScrollRectController)) as ScrollRectController;
    public void SetScore(int score)
    {
        int scores = PlayerPrefs.GetInt("score");
        scores += score;
        PlayerPrefs.SetInt("score", scores);
        actionSetCoin();
    }
    public void setSpriTemp(int index)
    {
        mainRect.KnifeTemp.sprite = mainRect.ListSpriteKnifeContempl[index];
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
}
