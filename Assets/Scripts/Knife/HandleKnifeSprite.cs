using UnityEngine;
using System.Collections;

public class HandleKnifeSprite : MonoBehaviour
{

    [SerializeField]
    private MainGameController MainGame;

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Ballon>() || collision.GetComponent<Stone>())
        {
            return;
        }

        //if (MainGame.knifeObject.isThow)
        //{
            MainGame.knifeObject.Hit();
        //}

    }


}

