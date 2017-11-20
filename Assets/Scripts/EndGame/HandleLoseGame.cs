using UnityEngine;
using System.Collections;
using DG.Tweening;

public class HandleLoseGame : MonoBehaviour
{

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
            || collision.GetComponent<SnowMan>()
            || collision.GetComponent<Bear>()
            || collision.GetComponent<Santa>()
            )
        {
            MainGame.IsGameReadyToPlay = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            ModelHandle.Instance.isLose = true;
            StartCoroutine(ShowPanelLose());
            //DOTween.KillAll();
        }
    }
    IEnumerator ShowPanelLose()
    {
        yield return new WaitForSeconds(0f);

        if (FunnyKnifeAdsManager.Instance.isLoadInt)
        {
            FunnyKnifeAdsManager.Instance.ShowInterstitialAd();
        }
        else
        {
            if (!MainGame.PanelLose.activeSelf)
            {
                MainGame.PanelLose.SetActive(true);
            }
        }
    }

}
