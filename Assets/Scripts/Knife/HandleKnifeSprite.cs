using UnityEngine;
using System.Collections;

public class HandleKnifeSprite : MonoBehaviour {

    [SerializeField]
    private MainGameController MainGame;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit Obj ");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MainGame.knifeObject.Hit();
        Debug.Log("Hit Trigger Obj");
    }

   
}

