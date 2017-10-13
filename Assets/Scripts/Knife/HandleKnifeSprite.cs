using UnityEngine;
using System.Collections;

public class HandleKnifeSprite : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit Obj ");
    }
}
