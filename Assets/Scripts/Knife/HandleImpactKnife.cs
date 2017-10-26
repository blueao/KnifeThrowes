using UnityEngine;
using System.Collections;

public class HandleImpactKnife : MonoBehaviour
{
    [SerializeField]
    private MainGameController MainGame;
    private void FinishEffectKnife()
    {
        if (MainGame.knifeObject.isThow)
        {
            //MainGame.knifeObject.isThow = false;
            MainGame.knifeObject.Idie();
        }
    }
  
}
