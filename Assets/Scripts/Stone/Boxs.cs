using UnityEngine;
using System.Collections;

public class Boxs : MonoBehaviour {

    private void Start()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Stupid>())
        {

        }
        else
            transform.GetComponent<BoxCollider2D>().enabled = false;
    }
}
