using UnityEngine;
using System.Collections;

public class HandleKnifeSprite : MonoBehaviour
{

    [SerializeField]
    private MainGameController MainGame;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
        if (collision.GetComponent<Ballon>() || collision.GetComponent<Stone>())
        {
            return;
        }
        if (collision.name == "BorderBottom" && !MainGame.knifeObject.isMiss || collision.name == "BorderTop" == !MainGame.knifeObject.isMiss)
        {
            MainGame.knifeObject.animatorEffectKnife.GetComponent<TrailRenderer>().enabled=false;
            MainGame.knifeObject.box.isTrigger = false;
            MainGame.knifeObject.isMiss = true;
            StopCoroutine(Rigid());
            StartCoroutine(Rigid());
            return;
        }
            if (collision.name != "BorderBottom" && !MainGame.knifeObject.isMiss || collision.name != "BorderTop" == !MainGame.knifeObject.isMiss)
        {
            MainGame.knifeObject.Hit();
        }

  


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log(collision.gameObject.name);
        Debug.Log(MainGame.knifeObject.isThow);
        if (collision.gameObject.name == "BorderBottom" || collision.gameObject.name == "BorderTop")
        {
            StartCoroutine(Rigid());
        }
    }
    public IEnumerator Rigid()
    {
        
        yield return new WaitUntil(()=> MainGame.knifeObject.isThow);
        MainGame.knifeObject.isMiss = true;
        MainGame.knifeObject.box.isTrigger = false;
        if (MainGame.knifeObject.isThow)
        {
            MainGame.knifeObject.isThow = false;
        }
        yield return new WaitForSeconds(1f);
        MainGame.knifeObject.Idie();
    }
}

