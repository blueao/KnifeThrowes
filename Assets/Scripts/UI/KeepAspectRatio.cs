using UnityEngine;
using System.Collections;

public class KeepAspectRatio : MonoBehaviour
{

    public float Width;
    public float Height;
    public float widthscreen = 1024f;
    public float heightscreen = 768f;

    void Awake()
    {
        GetComponent<Camera>().aspect = (Width) / (Height);
        Camera.main.orthographicSize = widthscreen / 100f * Screen.height / Screen.width * 0.5f;
    }
}
