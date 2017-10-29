using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class MainGameController : MonoBehaviour, IOberser
{
    [SerializeField]
    public Knife knifeObject;
    float degreeKnifeZ;
    public Transform spriteKnife;

    [SerializeField]
    private Transform[] Grass;
    [SerializeField]
    private Transform[] Hill;
    [SerializeField]
    private Transform[] Moutain;
    [SerializeField]
    private Transform[] Sky;
    public bool isGameReadyToPlay;

    //UI
    public GameObject PanelGet;
    public GameObject PanelLose;
    public GameObject PanelWin;
    public GameObject PanelCount;
    public GameObject PanelChooseMap;
    public GameObject Menu;
    public Text textCount;
    public Text CoinMenu;
    public Text Level;
    public Button ClassicBtn;
    public Button PlayBtn;
    //SetCountMonster
    public int StupidCount;
    public int FruitCount;
    public int PumkinCount;
    int woodtarget1;
    int woodtarget2;
    int redtarget;
    public int BallonCount;
    public int DummyCount;
    public int VultureCount;
    public int SpiderCount;
    public int CowCount;
    public int BatCount;
    public int CrazyDogCount;
    public int BoarCount;

    private Vector3 poolPosition;
    public GameObject QuitPanel;
    //MoveObj
    public Transform MoveMonster;
    //prefab Monster
    public GameObject preFab;
    public GameObject preFabWoodTarget;
    public GameObject preFabBallon;
    public GameObject preDummy;
    public GameObject preVulture;
    public GameObject preFabSpider;
    public GameObject preFabCow;
    public GameObject preFabBat;
    public GameObject preFabCrazyDog;
    public GameObject preFabBoar;
    public GameObject preFabCoinImage;


    public ScrollRectController scrollrecController;
    //BG
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
    //List Dummy Type
    [HideInInspector]
    public List<GameObject> ListDummy = new List<GameObject>();
    //List Vulture Type
    [HideInInspector]
    public List<GameObject> ListVulture = new List<GameObject>();
    //List Sprider Type
    [HideInInspector]
    public List<GameObject> ListSprider = new List<GameObject>();
    //List Cow Type
    [HideInInspector]
    public List<GameObject> ListCow = new List<GameObject>();
    //List Bat Type
    [HideInInspector]
    public List<GameObject> ListBat = new List<GameObject>();
    //List CrazyDog Type
    [HideInInspector]
    public List<GameObject> ListCrazyDog = new List<GameObject>();
    //List Boar Type
    [HideInInspector]
    public List<GameObject> ListBoar = new List<GameObject>();
    // Dictionary<string, GameObject> AllMonsterHere = new Dictionary<string, GameObject>();
    //List Boar Type
    [HideInInspector]
    public List<GameObject> ListCoinPool = new List<GameObject>();
    public int Coinpool;
    //ListTransMap1
    public Transform[] RedTargetPos;
    public Transform[] Stupid;
    public Transform[] TargetPos;
    public Transform[] BoarsPos;
    public Transform[] VulturePos;
    public Transform[] DumkinPos;
    public Transform[] FruitPos;
    public Transform[] PumKinPos;
    public Transform[] SpriderPos;
    public Transform[] BallonPos;
    public Transform[] CoinPos;
    public Transform[] CrazyDog;


    //Reset
    Vector3 startPostionMoveMonster;
    Vector3 startBGGrass1;
    Vector3 startBGGrass2;
    Vector3 startBGGrass3;
    Vector3 startBGSky1;
    Vector3 startBGSky2;
    Vector3 startBGSky3;
    Vector3 startBGMoutain1;
    Vector3 startBGMoutain2;
    Vector3 startBGMoutain3;
    Vector3 startBGHill1;
    Vector3 startBGHill2;
    Vector3 startBGHill3;

    void Start()
    {
        spriteKnife = knifeObject.spriteKnife.GetComponent<Transform>();
        spriteKnife.localPosition = new Vector3(spriteKnife.localPosition.x + knifeObject.spriteKnife.bounds.size.y, spriteKnife.localPosition.y, spriteKnife.localPosition.z);
        knifeObject.startKnifeTransfom = spriteKnife.localPosition;
        knifeObject.SetUpEffectKnife(ModelHandle.SetSevenTrail);
        widthBG =(float) Math.Round(Grass[0].GetComponent<SpriteRenderer>().bounds.size.x,1);
        heightBG = Mathf.Round(Grass[0].GetComponent<SpriteRenderer>().bounds.size.y) - 1f;
        endPositionBG = -widthBG - widthBG / 2;
        poolPosition = new Vector3(0, 15, 0);

        Initialized();
    }
    void Initialized()
    {
        int scores = PlayerPrefs.GetInt(ModelHandle.KeyScore);
        ScoreNumber.text = scores.ToString();
        AnimCoin();
        CoinMenu.text = scores.ToString();
        Level.text = "123456";
        //knife
        int indexKnife = PlayerPrefs.GetInt(ModelHandle.KeyKnifeSprite);
        this.GetComponent<ScrollRectController>().setUseSpriteKnife(indexKnife);
        //

        ModelHandle.Instance.actionSetCoin += SetCoin;
        ModelHandle.Instance.actiongGetCoin += getCoinPool;
        redtarget = RedTargetPos.Length;
        woodtarget1 = TargetPos.Length % 2;
        woodtarget2 = TargetPos.Length - woodtarget1;
        StupidCount = Stupid.Length;
        FruitCount = FruitPos.Length;
        BoarCount = BoarsPos.Length;
        BallonCount = BallonPos.Length;
        PumkinCount = PumKinPos.Length;
        VultureCount = VulturePos.Length;
        SpiderCount = SpriderPos.Length;
        DummyCount = DumkinPos.Length;

        setStartPosBG();
        CreateObject();
        SetupPositionObj();
    }


    public void SetupPositionObj()
    {
        int redIndex = 0;
        for (int j = 0; j < ListWoodTarget.Count; j++)
        {
            if (ListWoodTarget[j].name.Contains("red target")
                && (redIndex < RedTargetPos.Length))
            {
                ListWoodTarget[j].transform.localPosition = RedTargetPos[redIndex].transform.localPosition;
                ListWoodTarget[j].SetActive(true);
                redIndex++;
            }
        }
        int woodIndex = 0;
        for (int j = 0; j < ListWoodTarget.Count; j++)
        {
            if (ListWoodTarget[j].name.Contains("wood target")
                && (woodIndex < TargetPos.Length))
            {
                ListWoodTarget[j].transform.localPosition = TargetPos[woodIndex].transform.localPosition;
                ListWoodTarget[j].SetActive(true);
                woodIndex++;
            }
        }
        int stupidnum = 0;
        for (int j = 0; j < ListStupid.Count; j++)
        {
            if (ListStupid[j].name.Contains("Stupid")
                && (stupidnum < Stupid.Length))
            {
                ListStupid[j].transform.localPosition = Stupid[stupidnum].transform.localPosition;
                ListStupid[j].SetActive(true);
                stupidnum++;
            }
        }
        int fruitnum = 0;
        for (int j = 0; j < ListFruit.Count; j++)
        {
            if (ListFruit[j].name.Contains("Fruit")
                && (fruitnum < FruitPos.Length))
            {
                ListFruit[j].transform.localPosition = FruitPos[fruitnum].transform.localPosition;
                ListFruit[j].SetActive(true);
                fruitnum++;
            }
        }
        int boarNum = 0;
        for (int j = 0; j < ListBoar.Count; j++)
        {
            if (ListBoar[j].name.Contains("Boar")
                && (boarNum < BoarsPos.Length))
            {
                ListBoar[j].transform.localPosition = BoarsPos[boarNum].transform.localPosition;
                ListBoar[j].SetActive(true);
                boarNum++;
            }
        }
        int ballonNum = 0;
        for (int j = 0; j < ListBallon.Count; j++)
        {
            if (ListBallon[j].name.Contains("Ballon")
                && (ballonNum < BallonPos.Length))
            {
                ListBallon[j].transform.localPosition = BallonPos[ballonNum].transform.localPosition;
                ListBallon[j].SetActive(true);
                ballonNum++;
            }
        }
        int pumkinNum = 0;
        for (int j = 0; j < ListPumkin.Count; j++)
        {
            if (ListPumkin[j].name.Contains("Pumkin")
                && (pumkinNum < PumKinPos.Length))
            {
                ListPumkin[j].transform.localPosition = PumKinPos[pumkinNum].transform.localPosition;
                ListPumkin[j].SetActive(true);
                pumkinNum++;
            }
        }
        int vutulreNum = 0;
        for (int j = 0; j < ListVulture.Count; j++)
        {
            if (ListVulture[j].name.Contains("vulture")
                && (vutulreNum < VulturePos.Length))
            {
                ListVulture[j].transform.localPosition = VulturePos[vutulreNum].transform.localPosition;
                ListVulture[j].SetActive(true);
                vutulreNum++;
            }
        }
        int spiderNum = 0;
        for (int j = 0; j < ListSprider.Count; j++)
        {
            if (ListSprider[j].name.Contains("spider")
                && (spiderNum < SpriderPos.Length))
            {
                ListSprider[j].transform.localPosition = SpriderPos[spiderNum].transform.localPosition;
                ListSprider[j].SetActive(true);
                spiderNum++;
            }
        }
        int dumkinNum = 0;
        for (int j = 0; j < ListDummy.Count; j++)
        {
            if (ListDummy[j].name.Contains("dummy")
                && (dumkinNum < DumkinPos.Length))
            {
                ListDummy[j].transform.localPosition = DumkinPos[dumkinNum].transform.localPosition;
                ListDummy[j].SetActive(true);
                dumkinNum++;
            }
        }
        int crazyDogNum = 0;
        for (int j = 0; j < ListCrazyDog.Count; j++)
        {
            if (ListCrazyDog[j].name.Contains("CrazyDog")
                && (crazyDogNum < CrazyDog.Length))
            {
                ListCrazyDog[j].transform.localPosition = CrazyDog[crazyDogNum].transform.localPosition;
                ListCrazyDog[j].SetActive(true);
                crazyDogNum++;
            }
        }
    }
    public void CreateObject()
    {
        for (int i = 0; i < Coinpool; i++)
        {
            GameObject coinpool = (GameObject)Instantiate(preFabCoinImage, poolPosition, Quaternion.identity);
            coinpool.name = "CoinPool" + i;
            ListCoinPool.Add(coinpool);
            coinpool.SetActive(false);
        }

        for (int i = 0; i < StupidCount; i++)
        {
            GameObject stupidobj = (GameObject)Instantiate(preFab, poolPosition, Quaternion.identity);
            stupidobj.name = "Stupid" + i;
            ListStupid.Add(stupidobj);
            stupidobj.transform.parent = MoveMonster;
        }
        for (int i = 0; i < PumkinCount; i++)
        {
            GameObject stupidobj = (GameObject)Instantiate(preFab, poolPosition, Quaternion.identity);
            stupidobj.name = "Pumkin" + i;
            ListPumkin.Add(stupidobj);
            stupidobj.transform.parent = MoveMonster;
        }
        for (int i = 0; i < FruitCount; i++)
        {
            GameObject stupidobj = (GameObject)Instantiate(preFab, poolPosition, Quaternion.identity);
            stupidobj.name = "Fruit" + i;
            ListFruit.Add(stupidobj);
            stupidobj.transform.parent = MoveMonster;
        }


        for (int i = 0; i < woodtarget1; i++)
        {
            GameObject woodTarget = (GameObject)Instantiate(preFabWoodTarget, poolPosition, Quaternion.identity);
            woodTarget.name = "wood target 1" + i;
            ListWoodTarget.Add(woodTarget);
            woodTarget.transform.parent = MoveMonster;
        }
        for (int i = 0; i < woodtarget2; i++)
        {
            GameObject woodTarget = (GameObject)Instantiate(preFabWoodTarget, poolPosition, Quaternion.identity);
            woodTarget.name = "wood target 2" + i;
            ListWoodTarget.Add(woodTarget);
            woodTarget.transform.parent = MoveMonster;
        }
        for (int i = 0; i < redtarget; i++)
        {
            GameObject woodTarget = (GameObject)Instantiate(preFabWoodTarget, poolPosition, Quaternion.identity);
            woodTarget.name = "red target" + i;
            ListWoodTarget.Add(woodTarget);
            woodTarget.transform.parent = MoveMonster;
        }
        for (int i = 0; i < BallonCount; i++)
        {
            GameObject ballontarget = (GameObject)Instantiate(preFabBallon, poolPosition, Quaternion.identity);
            ballontarget.name = "Ballon" + i;
            ListBallon.Add(ballontarget);
            ballontarget.transform.parent = MoveMonster;
        }

        for (int i = 0; i < DummyCount; i++)
        {
            int b = UnityEngine.Random.Range(1, 3);
            GameObject dummy = (GameObject)Instantiate(preDummy, poolPosition, Quaternion.identity);
            dummy.name = "dummy (" + b + ")" + i;
            ListDummy.Add(dummy);
            dummy.transform.parent = MoveMonster;
        }

        for (int i = 0; i < VultureCount; i++)
        {
            int b = UnityEngine.Random.Range(1, 3);
            GameObject vulture = (GameObject)Instantiate(preVulture, poolPosition, Quaternion.identity);
            vulture.name = "vulture (" + b + ")" + i;
            ListVulture.Add(vulture);
            vulture.transform.parent = MoveMonster;
        }

        for (int i = 0; i < SpiderCount; i++)
        {
            GameObject spider = (GameObject)Instantiate(preFabSpider, poolPosition, Quaternion.identity);
            spider.name = "spider" + i;
            ListSprider.Add(spider);
            spider.transform.parent = MoveMonster;
        }
        for (int i = 0; i < CowCount; i++)
        {
            GameObject cow = (GameObject)Instantiate(preFabCow, poolPosition, Quaternion.identity);
            cow.name = "Cow" + i;
            ListCow.Add(cow);
            cow.transform.parent = MoveMonster;
        }

        for (int i = 0; i < BatCount; i++)
        {
            GameObject bat = (GameObject)Instantiate(preFabBat, poolPosition, Quaternion.identity);
            bat.name = "Bat" + i;
            ListBat.Add(bat);
            bat.transform.parent = MoveMonster;
        }
        for (int i = 0; i < CrazyDogCount; i++)
        {
            GameObject crazydog = (GameObject)Instantiate(preFabCrazyDog, poolPosition, Quaternion.identity);
            crazydog.name = "CrazyDog" + i;
            ListCrazyDog.Add(crazydog);
            crazydog.transform.parent = MoveMonster;
        }
        for (int i = 0; i < BoarCount; i++)
        {
            GameObject boar = (GameObject)Instantiate(preFabBoar, poolPosition, Quaternion.identity);
            boar.name = "Boar" + i;
            ListBoar.Add(boar);
            boar.transform.parent = MoveMonster;
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
                Grass[i].localPosition = new Vector3(Grass[i].localPosition.x - (speedMoveBG * Grass.Length) + widthBG * Grass.Length - (Mathf.Abs(endPositionBG - Grass[i].localPosition.x )+0.2f), Grass[i].localPosition.y, Grass[i].localPosition.z);
                Hill[i].localPosition = new Vector3(Hill[i].localPosition.x - (speedMoveBG * Grass.Length) + widthBG * Grass.Length- (Mathf.Abs(endPositionBG - Hill[i].localPosition.x) + 0.2f), Hill[i].localPosition.y, Hill[i].localPosition.z);
                Moutain[i].localPosition = new Vector3(Moutain[i].localPosition.x - (speedMoveBG * Grass.Length) + widthBG * Grass.Length - (Mathf.Abs(endPositionBG - Moutain[i].localPosition.x) + 0.2f), Moutain[i].localPosition.y, Moutain[i].localPosition.z);
                Sky[i].localPosition = new Vector3(Sky[i].localPosition.x - (speedMoveBG * Grass.Length) + widthBG * Grass.Length- (Mathf.Abs(endPositionBG - Sky[i].localPosition.x) + 0.2f), Sky[i].localPosition.y, Sky[i].localPosition.z);
            }
        }

        MoveMonster.transform.localPosition += Vector3.left * speedMoveBG;
    }
    public bool CanMove;
    private void FixedUpdate()
    {
        if (isGameReadyToPlay)
        {
            speedMoveBG = Time.fixedDeltaTime * 1f;
            if (CanMove)
            {
                MoveBackGround();
            }
            if (knifeObject.isIdie)
            {
                DragPosition();
            }

            calculatorRotateChildKnife();
        }
    }
    float time = 1f;
    private bool isDrop;
    public bool IsDrop
    {
        get { return isDrop; }
        set
        {

            if (rotateDrop != null)
            {
                rotateDrop.Kill();
                rotateDrop = null;
            }
            isDrop = value;
        }
    }
    Tween rotateDrop;
    public void calculatorRotateChildKnife()
    {
        calculatorRotateKnife();
        if (knifeObject.isFly)
        {
            if (knifeObject.ChildKnife.GetComponent<HandleKnifeSprite>().RotateKnifeLoop == null)
            {
                knifeObject.isThow = true;

                knifeObject.ChildKnife.GetComponent<HandleKnifeSprite>().RotateKnifeLoop = knifeObject.ChildKnife.transform.DOLocalRotate(new Vector3(
                    knifeObject.ChildKnife.transform.localRotation.x,
                    knifeObject.ChildKnife.transform.localRotation.y,
                    -540), time, RotateMode.FastBeyond360).OnComplete(() =>
                    {
                        knifeObject.animatorEffectKnife.GetComponent<TrailRenderer>().enabled = false;
                        IsDrop = true;
                    });
            }
            if (IsDrop && rotateDrop ==null)
            {
                rotateDrop = knifeObject.ChildKnife.transform.DOLocalRotate(new Vector3(knifeObject.ChildKnife.transform.localRotation.x,
                 knifeObject.ChildKnife.transform.localRotation.y, -270), 1f,RotateMode.Fast).OnComplete(() => { IsDrop = false; });
            }

            //if (knifeObject.ChildKnife.transform.localRotation.z>90 && knifeObject.ChildKnife.transform.localRotation.z<270)
            //{
            //    ModelHandle.Instance.isCanHit = true;
            //}
            //else
            //    ModelHandle.Instance.isCanHit = false;

            //if (spriteKnife.localRotation.z == 0)
            //{

            //    knifeObject.KnifeRotate = spriteKnife.DOLocalRotate(new Vector3(0, 0, -180), 0.5f).OnComplete(() =>
            //    {
            //        knifeObject.isThow = true;
            //    });

            //}
            //if (knifeObject.isThow)
            //{
            knifeObject.RBknife.isKinematic = false;
            // knifeObject.RBknife.AddTorque(spriteKnife.right.y * v);
            spriteKnife.localPosition += Vector3.right * Time.fixedDeltaTime * a;
            //}
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

    Vector3 startMousePosition;
    Vector3 endMousePosition;
    public float a = 1f;
    public void DragPosition()
    {
        if (Input.GetMouseButtonDown(0) && knifeObject.isIdie)
        {


            startMousePosition = Input.mousePosition;
            knifeObject.isDrag = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            knifeObject.isDrag = false;
            endMousePosition = Input.mousePosition;
            if (endMousePosition.x - startMousePosition.x > endMousePosition.y - startMousePosition.y)
            {
                a = (Mathf.Abs(endMousePosition.x - startMousePosition.x) / 100);

            }
            else
            {
                a = (Mathf.Abs(endMousePosition.y - startMousePosition.y) / 100);
            }
            float min = 0.5f;
            float max = 3f;
            if (a < min)
            {
                time = max;
            }
            else
                time = 0.75f;
            if (a > 2f)
            {
                a = a * 2f;

            }
            calculorDrag();
        }
    }

    public void OnClickPlayAgain()
    {

        PanelLose.SetActive(false);
        CanMove = true;
        isGameReadyToPlay = true;
        Reset();
    }
    public void OnClickClassic()
    {
        isActiveChooseMap(true);
    }
    public void isActiveChooseMap(bool isActive)
    {
        if (PanelChooseMap.activeSelf)
        {
            PanelChooseMap.SetActive(!isActive);
        }
        else
            PanelChooseMap.SetActive(isActive);
    }
    public void isActiveMenu(bool isActive)
    {
        Menu.SetActive(isActive);
    }
    public void StartGame()
    {
        isActiveMenu(false);
        CountNumber();
    }

    public void CountNumber()
    {
        Sequence se = DOTween.Sequence();
        PanelCount.SetActive(true);
        int count = 3;
        textCount.text = count.ToString();
        se.Append(textCount.rectTransform.DOScale(0.5f, 0.5f).OnComplete(() =>
       {
           count--;
       }));
        se.Append(textCount.rectTransform.DOScale(1, 0.5f).OnComplete(() =>
        {
            textCount.text = count.ToString();
        }));
        se.SetLoops(3).OnComplete(() =>
        {
            count = 3;
            CanMove = true;
            isGameReadyToPlay = true;
            PanelCount.SetActive(false);
            Reset();
        });
    }

    public void WinGame()
    {
        isGameReadyToPlay = false;
        Reset();
        isPanelWinGame(true);
    }
    public void isPanelWinGame(bool isActive)
    {
        PanelWin.SetActive(isActive);
    }
    public void isPanelGet(bool isActive)
    {
        PanelGet.SetActive(isActive);
    }
    public void OnContinueClick()
    {
        isPanelWinGame(false);
        isPanelGet(true);
    }
    public void OnGetClick()
    {
        isPanelGet(false);
        isActiveMenu(true);
    }
    public Text ScoreNumber;
    public Transform Coin;
    public void AnimCoin()
    {
        Sequence se = DOTween.Sequence();
        se.Append(Coin.DOScaleX(0, 0.5f));
        se.Append(Coin.DOScaleX(1, 0.5f));
        se.SetLoops(-1);

    }


    public void Reset()
    {
        SetupPositionObj();
        ResetBG();
        knifeObject.Idie();
    }
    public void setStartPosBG()
    {
        startPostionMoveMonster = MoveMonster.transform.localPosition;
        startBGGrass1 = Grass[0].transform.localPosition;
        startBGGrass2 = Grass[1].transform.localPosition;
        startBGGrass3 = Grass[2].transform.localPosition;
        startBGSky1 = Sky[0].transform.localPosition;
        startBGSky2 = Sky[1].transform.localPosition;
        startBGSky3 = Sky[2].transform.localPosition;
        startBGMoutain1 = Moutain[0].transform.localPosition;
        startBGMoutain2 = Moutain[1].transform.localPosition;
        startBGMoutain3 = Moutain[2].transform.localPosition;
        startBGHill1 = Hill[0].transform.localPosition;
        startBGHill2 = Hill[1].transform.localPosition;
        startBGHill3 = Hill[2].transform.localPosition;
    }
    public void ResetBG()
    {
        MoveMonster.transform.localPosition = startPostionMoveMonster;
        Grass[0].transform.localPosition = startBGGrass1;
        Grass[1].transform.localPosition = startBGGrass2;
        Grass[2].transform.localPosition = startBGGrass3;
        Sky[0].transform.localPosition = startBGSky1;
        Sky[1].transform.localPosition = startBGSky2;
        Sky[2].transform.localPosition = startBGSky3;
        Moutain[0].transform.localPosition = startBGMoutain1;
        Moutain[1].transform.localPosition = startBGMoutain2;
        Moutain[2].transform.localPosition = startBGMoutain3;
        Hill[0].transform.localPosition = startBGHill1;
        Hill[1].transform.localPosition = startBGHill2;
        Hill[2].transform.localPosition = startBGHill3;
    }

    public void getCoinPool(Vector3 start)
    {
        Coin.transform.localRotation = Quaternion.Euler(Vector3.zero);
        Sequence se = DOTween.Sequence();
        Tween a = null; ;
        for (int i = 0; i < ListCoinPool.Count; i++)
        {
            if (!ListCoinPool[i].activeSelf)
            {
                ListCoinPool[i].transform.SetParent(MoveMonster);
                ListCoinPool[i].transform.localPosition = start;
                ListCoinPool[i].SetActive(true);
                se.Append(ListCoinPool[i].transform.DOScaleX(0, 0.5f));
                se.Join(ListCoinPool[i].transform.DOScaleX(1, 0.5f));
                se.SetLoops(-1);

                a = ListCoinPool[i].transform.DOMove(Coin.transform.position, 1f).OnComplete(() =>
                {
                    ListCoinPool[i].transform.localPosition = poolPosition;
                    ListCoinPool[i].SetActive(false);
                    if (se != null)
                    {
                        se.Kill();
                        se = null;
                    }
                    if (a != null)
                    {
                        a.Kill();
                        a = null;
                    }
                });
                break;
            }
            GameObject coinpool = (GameObject)Instantiate(preFabCoinImage, poolPosition, Quaternion.identity);
            coinpool.name = "CoinPool" + i;
            ListCoinPool.Add(coinpool);
            ListCoinPool[i].transform.SetParent(MoveMonster);
            ListCoinPool[i].transform.localPosition = start;
            ListCoinPool[i].SetActive(true);
            se.Append(ListCoinPool[i].transform.DOScaleX(0, 0.5f));
            se.Join(ListCoinPool[i].transform.DOScaleX(0, 0.5f));
            se.SetLoops(-1);
            a = ListCoinPool[i].transform.DOMove(Coin.transform.position, 1f).OnComplete(() =>
            {
                ListCoinPool[i].transform.localPosition = poolPosition;
                ListCoinPool[i].SetActive(false);
                if (se != null)
                {
                    se.Kill();
                    se = null;
                }
                if (a != null)
                {
                    a.Kill();
                    a = null;
                }
            });
        }


    }
    public void SetCoin()
    {
        int scores = PlayerPrefs.GetInt("score");
        ScoreNumber.text = scores.ToString();
    }
    public GameObject ShopDao;



    public void isActiveShopDao(bool isActive)
    {
        ShopDao.SetActive(isActive);
    }
    public void OPenShop()
    {
        isActiveShopDao(true);
    }
    public void CloseShop()
    {
        isActiveShopDao(false);
    }
    public void SetActiveQuitPanel()
    {
        if (QuitPanel.activeSelf)
        {
            QuitPanel.SetActive(false);
        }
        else
            QuitPanel.SetActive(true);
    }

}
