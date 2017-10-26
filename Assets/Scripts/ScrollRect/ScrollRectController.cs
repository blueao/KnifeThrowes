using UnityEngine;
using System.Collections;

public class ScrollRectController : MonoBehaviour
{

    public RectTransform panel;
    public GameObject[] listGO;
    public RectTransform center;

    private int start = 1;
    private float[] distance;
    float[] distReposition;
    bool drag;
    private int GODistance;
    private int numberGO;
    int lengt;
    private void Start()
    {
        lengt = listGO.Length;
        distance = new float[lengt];
        distReposition = new float[lengt];
        GODistance = (int)(listGO[1].GetComponent<RectTransform>().anchoredPosition.y - listGO[0].GetComponent<RectTransform>().anchoredPosition.y);
       // panel.anchoredPosition = new Vector2(0f,(start - 1)*-115);
    }
    private void Update()
    {
            for (int i = 0; i < listGO.Length; i++)
        {
            distReposition[i] = center.GetComponent<RectTransform>().position.y - listGO[i].GetComponent<RectTransform>().position.y;
            distance[i] = Mathf.Abs(distReposition[i]);
            if (distReposition[i] > 460)
            {
                float curX = listGO[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = listGO[i].GetComponent<RectTransform>().anchoredPosition.y;
                Vector2 newAnchor = new Vector2(curX, curY + (lengt + GODistance));
                listGO[i].GetComponent<RectTransform>().anchoredPosition = newAnchor;
            }
            //if (distReposition[i] > -460)
            //{
            //    float curX = listGO[i].GetComponent<RectTransform>().anchoredPosition.x;
            //    float curY = listGO[i].GetComponent<RectTransform>().anchoredPosition.y;
            //    Vector2 newAnchor = new Vector2(curX, curY - (lengt + GODistance));
            //    listGO[i].GetComponent<RectTransform>().anchoredPosition = newAnchor;
            //}
        }
        
        float minDistance = Mathf.Min(distance);
        for (int a = 0; a < listGO.Length; a++)
        {
            if (minDistance == distance[a])
            {
                numberGO = a;
            }
            if (!drag)
            {
                //Lerp(numberGO * -GODistance);
                Debug.Log(numberGO);
                Lerp(-listGO[numberGO].GetComponent<RectTransform>().anchoredPosition.y);
            }
        }
    }


    void Lerp(float position)
    {
        float newY = Mathf.Lerp(panel.anchoredPosition.y, position, Time.deltaTime * 5f);

        if (Mathf.Abs(position-newY)<5f)
        {
            newY = position;
        }
        Vector2 newposition = new Vector2(panel.anchoredPosition.x, newY);
        panel.anchoredPosition = newposition;
    }
    public void StartDrag()
    {
        drag = true;
    }
    public void EndDrag()
    {
        drag = false;
    }
}
