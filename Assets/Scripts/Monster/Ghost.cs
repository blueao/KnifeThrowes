using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour {

    private void Start()
    {
        transform.GetComponent<TrailRenderer>().sortingOrder = 31500;
        for (int i = 0; i < 4; i++)
        {
            GameObject go = (GameObject)Instantiate(gameObject, this.transform.localPosition, Quaternion.identity);
            go.transform.parent = transform;
        }
    }
   
}
