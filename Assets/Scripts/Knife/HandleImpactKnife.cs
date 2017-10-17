using UnityEngine;
using System.Collections;

public class HandleImpactKnife : MonoBehaviour
{
    [SerializeField]
    private MainGameController MainGame;
    private void FinishEffectKnife()
    {
        MainGame.knifeObject.Idie();
    }
  
}
