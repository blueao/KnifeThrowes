using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class MainGameController : MonoBehaviour
{
    [SerializeField]
    public Knife knifeObject;
    float degreeKnifeZ;
    private Transform spriteKnife;

    [SerializeField]
    private Transform[] Grass;
    [SerializeField]
    private Transform[] Hill;
    [SerializeField]
    private Transform[] Moutain;
    [SerializeField]
    private Transform[] Sky;

    public int StupidCount;
    public int FruitCount;
    public int PumkinCount;

    public int WoodTargetCount;

    public int BallonCount;

    private Vector3 poolPosition;
    public GameObject preFab;
    public GameObject preFabWoodTarget;
    public GameObject preFabBallon;
    private float widthBG;
    private float heightBG;
    private float speedMoveBG;
    private float endPositionBG;
    //List Stupid Type
    [HideInInspector]
    public List<GameObject> ListStupid = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> ListPumkin = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> ListFruit = new List<GameObject>();
    //List WoodTarget Type
    [HideInInspector]
    public List<GameObject> ListWoodTarget = new List<GameObject>();
    //List Ballon Type
    [HideInInspector]
    public List<GameObject> ListBallon = new List<GameObject>();

    // Dictionary<string, GameObject> AllMonsterHere = new Dictionary<string, GameObject>();

    void Start()
    {
        spriteKnife = knifeObject.spriteKnife.GetComponent<Transform>();
        spriteKnife.localPosition = new Vector3(spriteKnife.localPosition.x + knifeObject.spriteKnife.bounds.size.y, spriteKnife.localPosition.y, spriteKnife.localPosition.z);
        knifeObject.startKnifeTransfom = spriteKnife.localPosition;
        knifeObject.SetUpEffectKnife("blue");
        widthBG = Grass[0].GetComponent<SpriteRenderer>().bounds.size.x;
        heightBG = Grass[0].GetComponent<SpriteRenderer>().bounds.size.y;
        endPositionBG = -widthBG - widthBG / 2;
        poolPosition = new Vector3(0, 15, 0);

        Initialized();
    }
    void Initialized()
    {
        CreateObject();
    }

    public void CreateObject()
    {
        for (int i = 0; i < StupidCount; i++)
        {
            GameObject stupidobj = (GameObject)Instantiate(preFab, poolPosition, Quaternion.identity);
            stupidobj.name = "Stupid" + i;
            ListStupid.Add(stupidobj);

        }
        for (int i = 0; i < PumkinCount; i++)
        {
            GameObject stupidobj = (GameObject)Instantiate(preFab, poolPosition, Quaternion.identity);
            stupidobj.name = "Pumkin" + i;
            ListPumkin.Add(stupidobj);
        }
        for (int i = 0; i < FruitCount; i++)
        {
            GameObject stupidobj = (GameObject)Instantiate(preFab, poolPosition, Quaternion.identity);
            stupidobj.name = "Fruit" + i;
            ListFruit.Add(stupidobj);
        }
      
        int woodtarget1 = WoodTargetCount % 3;
        int woodtarget2 = (WoodTargetCount - woodtarget1) % 2;
        int redtarget = WoodTargetCount - woodtarget1 - woodtarget2;
        for (int i = 0; i < woodtarget1; i++)
        {
            GameObject woodTarget = (GameObject)Instantiate(preFabWoodTarget, poolPosition, Quaternion.identity);
            woodTarget.name = "wood target 1" + i;
            ListWoodTarget.Add(woodTarget);
        }
        for (int i = 0; i < woodtarget2; i++)
        {
            GameObject woodTarget = (GameObject)Instantiate(preFabWoodTarget, poolPosition, Quaternion.identity);
            woodTarget.name = "wood target 2" + i;
            ListWoodTarget.Add(woodTarget);
        }
        for (int i = 0; i < redtarget; i++)
        {
            GameObject woodTarget = (GameObject)Instantiate(preFabWoodTarget, poolPosition, Quaternion.identity);
            woodTarget.name = "red target" + i;
            ListWoodTarget.Add(woodTarget);
        }
        for (int i = 0; i < BallonCount; i++)
        {
            GameObject ballontarget = (GameObject)Instantiate(preFabBallon, poolPosition, Quaternion.identity);
            ballontarget.name = "Ballon" + i;
            ListBallon.Add(ballontarget);
        }
    }
    public void MoveBackGround()
    {

        for (int i = 0; i < Grass.Length; i++)
        {
            Grass[i].localPosition += Vector3.left * speedMoveBG;
            Hill[i].localPosition += Vector3.left * speedMoveBG;
            Moutain[i].localPosition += Vector3.left * speedMoveBG;
            Sky[i].localPosition += Vector3.left * speedMoveBG;

            if (Grass[i].localPosition.x <= endPositionBG)
            {
                Grass[i].localPosition = new Vector3(Grass[i].localPosition.x - speedMoveBG * Grass.Length + widthBG * Grass.Length, Grass[i].localPosition.y, Grass[i].localPosition.z);
                Hill[i].localPosition = new Vector3(Hill[i].localPosition.x - speedMoveBG * Grass.Length + widthBG * Grass.Length, Hill[i].localPosition.y, Hill[i].localPosition.z);
                Moutain[i].localPosition = new Vector3(Moutain[i].localPosition.x - speedMoveBG * Grass.Length + widthBG * Grass.Length, Moutain[i].localPosition.y, Moutain[i].localPosition.z);
                Sky[i].localPosition = new Vector3(Sky[i].localPosition.x - speedMoveBG * Grass.Length + widthBG * Grass.Length, Sky[i].localPosition.y, Sky[i].localPosition.z);
            }
        }
    }
    private void FixedUpdate()
    {
        speedMoveBG = Time.fixedDeltaTime * 2f;
        // MoveBackGround();
        if (knifeObject.isIdie)
        {
            DragPosition();
        }
        calculatorRotateKnife();
        calculatorRotateChildKnife();
    }
    public void calculatorRotateChildKnife()
    {
        if (knifeObject.isFly)
        {
            if (spriteKnife.localRotation.z == 0)
            {

                spriteKnife.DOLocalRotate(new Vector3(0, 0, -180), 0.5f).OnComplete(() =>
                {
                    knifeObject.isThow = true;
                });
            }
            if (knifeObject.isThow)
            {
                knifeObject.RBknife.isKinematic = false;
                knifeObject.RBknife.AddTorque(-1f, ForceMode2D.Force);
                spriteKnife.localPosition += Vector3.right * Time.fixedDeltaTime * 12f;
            }
        }
    }
    public void calculatorRotateKnife()
    {
        var pos = Input.mousePosition;
        pos.z = 10;
        if (knifeObject.isIdie)
        {
            Vector2 mouseCamera = Camera.main.ScreenToWorldPoint(pos) - knifeObject.knifeTransfom.position;
            float tan = Mathf.Atan2(mouseCamera.y, mouseCamera.x) * Mathf.Rad2Deg;
            if (tan > 0 && tan < 60f)
            {
                tan = Mathf.Clamp(tan, 1, 60f);
            }
            else if (tan <= 0 && tan > -60)
            {
                tan = Mathf.Clamp(tan, -60, 0);
            }
            else
            {
                if (tan > 60)
                {
                    tan = 60;
                }
                else if (tan < -60)
                {
                    tan = 300;
                }
            }
            knifeObject.knifeTransfom.rotation = Quaternion.Euler(new Vector3(knifeObject.knifeTransfom.rotation.x, knifeObject.knifeTransfom.rotation.y, tan));
        }
    }
    public void calculorDrag()
    {
        knifeObject.Fly();
    }

    public void DragPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            knifeObject.isDrag = true;
        }
        if (Input.GetMouseButtonUp(0) && knifeObject.isDrag)
        {
            knifeObject.isDrag = false;
            calculorDrag();
        }
    }

}
