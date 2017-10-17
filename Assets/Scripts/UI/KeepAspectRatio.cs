using UnityEngine;
using System.Collections;

public class KeepAspectRatio : MonoBehaviour
{

    public float Width;
    public float Height;
    public float widthscreen = 1280f;
    public float heightscreen = 720f;

    void Awake()
    {
        GetComponent<Camera>().aspect = (Width) / (Height);
        Camera.main.orthographicSize = widthscreen / 100f * Screen.height / Screen.width * 0.5f;
    }
}
