using UnityEngine;
using System.Collections;

public class KeepAspectRatio : MonoBehaviour
{
    private float widthscreen = 1365f;
    private float heightscreen = 768f;

    void Start()
    {
        GetComponent<Camera>().aspect = widthscreen / heightscreen;
        if (Screen.width <= 1365)
        {
            Camera.main.orthographicSize = widthscreen / 100f * Screen.height / Screen.width * 0.5f;
        }
        else
            Camera.main.orthographicSize = widthscreen / 100f *  (float)Screen.height /(float) Screen.width * 0.5f;
      
        SetCamera();
    }

    public void SetCamera()
    {
        float targetaspect = 16f / 9f;
        float windowaspect = (float)Screen.width / (float)Screen.height;
        float scaleheight = windowaspect / targetaspect;
        if (scaleheight < 1.0f)
        {
            Rect rect = GetComponent<Camera>().rect;
                
            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            GetComponent<Camera>().rect = rect;
        }
        else 
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = GetComponent<Camera>().rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;
            GetComponent<Camera>().rect = rect;
        }
    }
}
