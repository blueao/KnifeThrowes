using UnityEngine;
using System.Collections;

public class GhostShadow : MonoBehaviour
{

    public SpriteRenderer sprite;
    float time = 100f;
    private void Start()
    {
        Debug.Log(Ghost.Intance == null);
        sprite = GetComponent<SpriteRenderer>();
        transform.TransformPoint(Ghost.Intance.transform.TransformPoint(Ghost.Intance.transform.localPosition.x, Ghost.Intance.transform.localPosition.y, Ghost.Intance.transform.localPosition.z));
        transform.localScale = Ghost.Intance.transform.localScale;
        sprite.sprite = Ghost.Intance.GetComponent<SpriteRenderer>().sprite;
        sprite.color =  new Vector4(50,50,50,0.2f);
        //transform.SetParent(Ghost.Intance.transform);
    }
    private void Update()
    {
        time -= Time.deltaTime;
        if (time<=0)
        {
            Destroy(gameObject);
        }
    }
}
