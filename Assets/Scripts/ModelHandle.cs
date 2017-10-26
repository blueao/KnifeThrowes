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

    public void SetScore(int score)
    {
        MainGameController main = new MainGameController();
        int scores = PlayerPrefs.GetInt("score");
        scores += score;
        PlayerPrefs.SetInt("score", scores);
        actionSetCoin();
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
