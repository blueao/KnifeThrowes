
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Observer : IOberser
{
    protected MainGameController MainGame;
    private List<IOberser> obs = new List<IOberser>();
    public void SetCoin()
    {
        
    }
    public void attach(IOberser ob)
    {
        obs.Add(ob);
        notify();
    }
    public void notify()
    {
        foreach (var ob in obs)
        {
            ob.SetCoin();
        }
    }
}
