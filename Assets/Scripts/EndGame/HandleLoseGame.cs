using UnityEngine;
using System.Collections;
using DG.Tweening;

public class HandleLoseGame : MonoBehaviour {

    public MainGameController MainGame;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Boar>()
            || collision.GetComponent<Stupid>()
            || collision.GetComponent<Bat>()
            || collision.GetComponent<DogCrazy>()
            || collision.GetComponent<WoodTarget>()
            || collision.GetComponent<Spiders>()
            || collision.GetComponent<Ghost>()
            || collision.GetComponent<Bird>()
            )
        {
            MainGame.isGameReadyToPlay = false;
            MainGame.PanelLose.SetActive(true);
            //DOTween.KillAll();
        }
    }
}
