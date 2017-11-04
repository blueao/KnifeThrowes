using UnityEngine;
using System.Collections;

public class GhostShadow : MonoBehaviour
{

    public SpriteRenderer sprite;
    float time = 100f;
    private void Start()
    {

        sprite = GetComponent<SpriteRenderer>();
        sprite.color =  new Vector4(50,50,50,0.2f);
    }
}
