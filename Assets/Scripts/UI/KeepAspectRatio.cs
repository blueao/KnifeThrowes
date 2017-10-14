using UnityEngine;
using System.Collections;

public class KeepAspectRatio : MonoBehaviour
{

    public float Width;
    public float Height;
    void Awake()
    {
        GetComponent<Camera>().aspect = (Width) / (Height);
      
    }
}
