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

    public Transform[] Grass;
    public Transform[] Hill;
    public Transform[] Moutain;
    public Transform[] House;
    public Transform[] Sky;
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
    public int ghostCount;
    public int rabitCount;
    public int wizardCount;
    public int birdCount;
    private Vector3 poolPosition;
    public GameObject QuitPanel;
    //MoveObj
    public Transform Map1;
    public Transform Map2;
    private Transform MoveMonster;
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
    public GameObject preRabbit;
    public GameObject preGhost;
    public GameObject preBird;
    public GameObject preWizard;
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
    [HideInInspector]
    public List<GameObject> ListTotalObject = new List<GameObject>();
    //List Boar Type
    [HideInInspector]
    public List<GameObject> ListCoinPool = new List<GameObject>();
    //
    [HideInInspector]
    public List<GameObject> ListBird = new List<GameObject>();
    //
    [HideInInspector]
    public List<GameObject> ListWizard = new List<GameObject>();
    //
    [HideInInspector]
    public List<GameObject> ListGhost = new List<GameObject>();
    //
    [HideInInspector]
    public List<GameObject> ListRabbit = new List<GameObject>();
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
    public Transform[] wizardPos0;
    public Transform[] birdPos0;
    public Transform[] ghostPos0;
    public Transform[] rabbitPos0;
    public Transform[] batPos0;
    public Transform[] cowpos0;
    //ListMap2
    public Transform[] RedTargetPos2;
    public Transform[] Stupid2;
    public Transform[] TargetPos2;
    public Transform[] BoarsPos2;
    public Transform[] VulturePos2;
    public Transform[] DumkinPos2;
    public Transform[] FruitPos2;
    public Transform[] PumKinPos2;
    public Transform[] SpriderPos2;
    public Transform[] BallonPos2;
    public Transform[] CoinPos2;
    public Transform[] CrazyDog2;
    public Transform[] wizardPos;
    public Transform[] birdPos;
    public Transform[] ghostPos;
    public Transform[] rabbitPos;
    public Transform[] batPos;
    public Transform[] cowpos;
    //Map1
    [SerializeField]
    private Sprite[] Grass1;
    [SerializeField]
    private Sprite[] Hill1;
    [SerializeField]
    private Sprite[] Moutain1;
    [SerializeField]
    private Sprite[] Sky1;
    //Map2
    [SerializeField]
    private Sprite[] Grass2;
    [SerializeField]
    private Sprite[] Hill2;
    [SerializeField]
    private Sprite[] House2;
    [SerializeField]
    private Sprite[] Sky2;

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
        widthBG = 26.6f; /*(float)Math.Round(Grass[0].GetComponent<SpriteRenderer>().bounds.size.x, 1);*/
        //heightBG = Mathf.Round(Grass[0].GetComponent<SpriteRenderer>().bounds.size.y) - 1f;
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
        //ModelHandle.Instance.actiongGetCoin += getCoinPool;

        if (RedTargetPos.Length > RedTargetPos2.Length)
        {
            redtarget = RedTargetPos.Length;
        }
        else
        {
            redtarget = RedTargetPos2.Length;
        }
        if (TargetPos.Length > TargetPos2.Length)
        {
            woodtarget1 = TargetPos.Length % 2;
            woodtarget2 = TargetPos.Length - woodtarget1;
        }
        else
        {
            woodtarget1 = TargetPos2.Length % 2;
            woodtarget2 = TargetPos2.Length - woodtarget1;
        }
        if (Stupid.Length > Stupid2.Length)
        {
            StupidCount = Stupid.Length;
        }
        else
            StupidCount = Stupid2.Length;

        if (FruitPos.Length > FruitPos2.Length)
        {
            FruitCount = FruitPos.Length;
        }
        else
            FruitCount = FruitPos2.Length;
        if (BoarsPos.Length > BoarsPos2.Length)
        {
            BoarCount = BoarsPos.Length;
        }
        else
            BoarCount = BoarsPos2.Length;
        if (BallonPos.Length > BallonPos2.Length)
        {
            BallonCount = BallonPos.Length;
        }
        else
            BallonCount = BallonPos2.Length;

        if (PumKinPos.Length > PumKinPos2.Length)
        {
            PumkinCount = PumKinPos.Length;
        }
        else
            PumkinCount = PumKinPos2.Length;

        if (VulturePos.Length > VulturePos2.Length)
        {
            VultureCount = VulturePos.Length;
        }
        else
            VultureCount = VulturePos2.Length;

        if (SpriderPos.Length > SpriderPos2.Length)
        {
            SpiderCount = SpriderPos.Length;
        }
        else
            SpiderCount = SpriderPos2.Length;

        if (DumkinPos.Length > DumkinPos2.Length)
        {
            DummyCount = DumkinPos.Length;
        }
        else
            DummyCount = DumkinPos2.Length;


        if (CrazyDog.Length > CrazyDog2.Length)
        {
            CrazyDogCount = CrazyDog.Length;
        }
        else
            CrazyDogCount = CrazyDog2.Length;

        birdCount = birdPos.Length;
        wizardCount = wizardPos.Length;
        rabitCount = rabbitPos.Length;
        ghostCount = ghostPos.Length;
        BatCount = batPos.Length;
        CowCount = cowpos.Length;

        //SetupPositionObj();
    }

#region SetupPosObj
    public void SetupPositionObj(
        Transform[] RedTargetPos,
         Transform[] TargetPos,
         Transform[] Stupid,
          Transform[] FruitPos,
           Transform[] BoarsPos,
            Transform[] BallonPos,
             Transform[] PumKinPos,
              Transform[] VulturePos,
               Transform[] SpriderPos,
                Transform[] DumkinPos,
                 Transform[] CrazyDog,
                   Transform[] batPos,
                     Transform[] ghostPos,
                       Transform[] rabbitPos,
                         Transform[] wizardPos,
                           Transform[] birdPos,
                            Transform[] cowpos
        )
    {
        int redIndex = 0;
        for (int j = 0; j < ListWoodTarget.Count; j++)
        {
            if (ListWoodTarget[j].name.Contains("red target")
                && (redIndex < RedTargetPos.Length))
            {
                ListWoodTarget[j].transform.parent = MoveMonster;
                ListWoodTarget[j].transform.localPosition = RedTargetPos[redIndex].transform.localPosition;
                ListWoodTarget[j].SetActive(true);
                redIndex++;
            }
        }
        int woodIndex = 0;
        for (int j = 0; j < ListWoodTarget.Count; j++)
        {
            ListWoodTarget[j].transform.parent = MoveMonster;
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
            ListStupid[j].transform.parent = MoveMonster;
            if (ListStupid[j].name.Contains("Stupid")
                && (stupidnum < Stupid.Length))
            {
                ListStupid[j].transform.localPosition = Stupid[stupidnum].transform.localPosition;
                ListStupid[j].SetActive(true);
                ListStupid[j].GetComponent<Stupid>().isActiveMove = false;
                stupidnum++;
            }
        }
        int fruitnum = 0;
        for (int j = 0; j < ListFruit.Count; j++)
        {
            ListFruit[j].transform.parent = MoveMonster;
            if (ListFruit[j].name.Contains("Fruit")
                && (fruitnum < FruitPos.Length))
            {
                ListFruit[j].transform.localPosition = FruitPos[fruitnum].transform.localPosition;
                ListFruit[j].SetActive(true);
                ListStupid[j].GetComponent<Stupid>().isActiveMove = false;
                fruitnum++;
            }
        }
        int boarNum = 0;
        for (int j = 0; j < ListBoar.Count; j++)
        {
            ListBoar[j].transform.parent = MoveMonster;
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
            ListBallon[j].transform.parent = MoveMonster;
            if (ListBallon[j].name.Contains("Ballon")
                && (ballonNum < BallonPos.Length))
            {
                ListBallon[j].transform.localPosition = BallonPos[ballonNum].transform.localPosition;
                ListBallon[j].SetActive(true);
                if (ListBallon[j].GetComponent<BoxCollider2D>() != null)
                {
                    ListBallon[j].GetComponent<Ballon>().box.enabled = true;
                }

                ballonNum++;
            }
        }
        int pumkinNum = 0;
        for (int j = 0; j < ListPumkin.Count; j++)
        {
            ListPumkin[j].transform.parent = MoveMonster;
            if (ListPumkin[j].name.Contains("Pumkin")
                && (pumkinNum < PumKinPos.Length))
            {
                ListPumkin[j].transform.localPosition = PumKinPos[pumkinNum].transform.localPosition;
                ListPumkin[j].SetActive(true);
                ListStupid[j].GetComponent<Stupid>().isActiveMove = false;
                ListStupid[j].GetComponent<Stupid>().jumb = false;
                pumkinNum++;
            }
        }
        int vutulreNum = 0;
        for (int j = 0; j < ListVulture.Count; j++)
        {
            ListVulture[j].transform.parent = MoveMonster;
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
            ListSprider[j].transform.parent = MoveMonster;
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
            ListDummy[j].transform.parent = MoveMonster;
            if (ListDummy[j].name.Contains("dummy")
                && (dumkinNum < DumkinPos.Length))
            {
                ListDummy[j].transform.localPosition = DumkinPos[dumkinNum].transform.localPosition;
                ListDummy[j].SetActive(true);
                if (ListDummy[j].GetComponent<BoxCollider2D>() != null)
                {
                    ListDummy[j].GetComponent<Dummy>().InPool();
                }

                dumkinNum++;
            }
        }
        int crazyDogNum = 0;
        for (int j = 0; j < ListCrazyDog.Count; j++)
        {
            ListCrazyDog[j].transform.parent = MoveMonster;
            if (ListCrazyDog[j].name.Contains("CrazyDog")
                && (crazyDogNum < CrazyDog.Length))
            {
                ListCrazyDog[j].transform.localPosition = CrazyDog[crazyDogNum].transform.localPosition;
                ListCrazyDog[j].SetActive(true);
                crazyDogNum++;
            }
        }

        //
        int batnum = 0;
        for (int j = 0; j < ListBat.Count; j++)
        {
            ListBat[j].transform.parent = MoveMonster;
            if (ListBat[j].name.Contains("Bat")
                && (batnum < batPos.Length))
            {
                ListBat[j].transform.localPosition = batPos[batnum].transform.localPosition;
                ListBat[j].SetActive(true);
                batnum++;
            }
        }
        //
        int ghostnum = 0;
        for (int j = 0; j < ListGhost.Count; j++)
        {
            ListGhost[j].transform.parent = MoveMonster;
            if (ListGhost[j].name.Contains("Ghost")
                && (ghostnum < ghostPos.Length))
            {
                ListGhost[j].transform.localPosition = ghostPos[ghostnum].transform.localPosition;
                ListGhost[j].SetActive(true);
                ListGhost[j].GetComponent<SpriteRenderer>().enabled = true;
                ghostnum++;
            }
        }
        //
        int rabbitnum = 0;
        for (int j = 0; j < ListRabbit.Count; j++)
        {
            ListRabbit[j].transform.parent = MoveMonster;
            if (ListRabbit[j].name.Contains("rabbit")
                && (rabbitnum < rabbitPos.Length))
            {
                ListRabbit[j].transform.localPosition = rabbitPos[rabbitnum].transform.localPosition;
                ListRabbit[j].SetActive(true);
                ListRabbit[j].GetComponent<SpriteRenderer>().enabled = true;
                rabbitnum++;
            }
        }
        //
        int birdnum = 0;
        for (int j = 0; j < ListBird.Count; j++)
        {
            ListBird[j].transform.parent = MoveMonster;
            if (ListBird[j].name.Contains("Bird")
                && (birdnum < birdPos.Length))
            {
                ListBird[j].transform.localPosition = birdPos[birdnum].transform.localPosition;
                ListBird[j].SetActive(true);
                ListBird[j].GetComponent<Bird>().hp = 0;
                if (ListBird[j].GetComponent<Bird>().GetComponent<BoxCollider2D>()!=null)
                {
                    ListBird[j].GetComponent<Bird>().GetComponent<BoxCollider2D>().enabled = true;
                }
                birdnum++;
            }
        }
        //

        int wizardnum = 0;
        for (int j = 0; j < ListWizard.Count; j++)
        {
            ListWizard[j].transform.parent = MoveMonster;
            if (ListWizard[j].name.Contains("wizard")
                && (wizardnum < wizardPos.Length))
            {
                ListWizard[j].transform.localPosition = wizardPos[wizardnum].transform.localPosition;
                ListWizard[j].SetActive(true);
                wizardnum++;
            }
        }
        //
        int cownum = 0;
        for (int j = 0; j < ListCow.Count; j++)
        {
            ListCow[j].transform.parent = MoveMonster;
            if (ListCow[j].name.Contains("Cow")
                && (cownum < cowpos.Length))
            {
                ListCow[j].transform.localPosition = cowpos[cownum].transform.localPosition;
                ListCow[j].SetActive(true);
                cownum++;
            }
        }
    }
#endregion
    public void CreateObject()
    {
        for (int i = 0; i < Coinpool; i++)
        {
            GameObject coinpool = (GameObject)Instantiate(preFabCoinImage, poolPosition, Quaternion.identity);
            coinpool.name = "CoinPool" + i;
            ListCoinPool.Add(coinpool);
            coinpool.SetActive(false);
            ListTotalObject.Add(coinpool);
        }
        for (int i = 0; i < birdCount; i++)
        {
            GameObject Bird = (GameObject)Instantiate(preBird, poolPosition, Quaternion.identity);
            Bird.name = "Bird" + i;
            ListBird.Add(Bird);
            Bird.SetActive(false);
            Bird.transform.parent = MoveMonster;
            ListTotalObject.Add(Bird);
        }
        for (int i = 0; i < ghostCount; i++)
        {
            GameObject ghost = (GameObject)Instantiate(preGhost, poolPosition, Quaternion.identity);
            ghost.name = "Ghost" + i;
            ListGhost.Add(ghost);
            ghost.SetActive(false);
            ghost.transform.parent = MoveMonster;
            ListTotalObject.Add(ghost);
        }
        for (int i = 0; i < rabitCount; i++)
        {
            GameObject rabbit = (GameObject)Instantiate(preRabbit, poolPosition, Quaternion.identity);
            rabbit.name = "rabbit" + i;
            ListRabbit.Add(rabbit);
            rabbit.SetActive(false);
            rabbit.transform.parent = MoveMonster;
            ListTotalObject.Add(rabbit);
        }
        for (int i = 0; i < wizardCount; i++)
        {
            GameObject wizard = (GameObject)Instantiate(preWizard, poolPosition, Quaternion.identity);
            wizard.name = "wizard" + i;
            ListWizard.Add(wizard);
            wizard.SetActive(false);
            wizard.transform.parent = MoveMonster;
            ListTotalObject.Add(wizard);
        }
        for (int i = 0; i < StupidCount; i++)
        {
            GameObject stupidobj = (GameObject)Instantiate(preFab, poolPosition, Quaternion.identity);
            stupidobj.name = "Stupid" + i;
            ListStupid.Add(stupidobj);
            stupidobj.transform.parent = MoveMonster;
            ListTotalObject.Add(stupidobj);
        }
        for (int i = 0; i < PumkinCount; i++)
        {
            GameObject stupidobj = (GameObject)Instantiate(preFab, poolPosition, Quaternion.identity);
            stupidobj.name = "Pumkin" + i;
            ListPumkin.Add(stupidobj);
            stupidobj.transform.parent = MoveMonster;
            ListTotalObject.Add(stupidobj);
        }
        for (int i = 0; i < FruitCount; i++)
        {
            GameObject stupidobj = (GameObject)Instantiate(preFab, poolPosition, Quaternion.identity);
            stupidobj.name = "Fruit" + i;
            ListFruit.Add(stupidobj);
            stupidobj.transform.parent = MoveMonster;
            ListTotalObject.Add(stupidobj);

        }


        for (int i = 0; i < woodtarget1; i++)
        {
            GameObject woodTarget = (GameObject)Instantiate(preFabWoodTarget, poolPosition, Quaternion.identity);
            woodTarget.name = "wood target 1" + i;
            ListWoodTarget.Add(woodTarget);
            woodTarget.transform.parent = MoveMonster;
            ListTotalObject.Add(woodTarget);
        }
        for (int i = 0; i < woodtarget2; i++)
        {
            GameObject woodTarget = (GameObject)Instantiate(preFabWoodTarget, poolPosition, Quaternion.identity);
            woodTarget.name = "wood target 2" + i;
            ListWoodTarget.Add(woodTarget);
            woodTarget.transform.parent = MoveMonster;
            ListTotalObject.Add(woodTarget);
        }
        for (int i = 0; i < redtarget; i++)
        {
            GameObject woodTarget = (GameObject)Instantiate(preFabWoodTarget, poolPosition, Quaternion.identity);
            woodTarget.name = "red target" + i;
            ListWoodTarget.Add(woodTarget);
            woodTarget.transform.parent = MoveMonster;
            ListTotalObject.Add(woodTarget);
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
            ListTotalObject.Add(spider);
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
            ListTotalObject.Add(bat);
        }
        for (int i = 0; i < CrazyDogCount; i++)
        {
            GameObject crazydog = (GameObject)Instantiate(preFabCrazyDog, poolPosition, Quaternion.identity);
            crazydog.name = "CrazyDog" + i;
            ListCrazyDog.Add(crazydog);
            crazydog.transform.parent = MoveMonster;
            ListTotalObject.Add(crazydog);
        }
        for (int i = 0; i < BoarCount; i++)
        {
            GameObject boar = (GameObject)Instantiate(preFabBoar, poolPosition, Quaternion.identity);
            boar.name = "Boar" + i;
            ListBoar.Add(boar);
            boar.transform.parent = MoveMonster;
            ListTotalObject.Add(boar);
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
                Grass[i].localPosition = new Vector3(Grass[i].localPosition.x - (speedMoveBG * Grass.Length) + widthBG * Grass.Length - (Mathf.Abs(endPositionBG - Grass[i].localPosition.x) + 0.2f), Grass[i].localPosition.y, Grass[i].localPosition.z);
                Hill[i].localPosition = new Vector3(Hill[i].localPosition.x - (speedMoveBG * Grass.Length) + widthBG * Grass.Length - (Mathf.Abs(endPositionBG - Hill[i].localPosition.x) + 0.2f), Hill[i].localPosition.y, Hill[i].localPosition.z);
                Moutain[i].localPosition = new Vector3(Moutain[i].localPosition.x - (speedMoveBG * Grass.Length) + widthBG * Grass.Length - (Mathf.Abs(endPositionBG - Moutain[i].localPosition.x) + 0.2f), Moutain[i].localPosition.y, Moutain[i].localPosition.z);
                Sky[i].localPosition = new Vector3(Sky[i].localPosition.x - (speedMoveBG * Grass.Length) + widthBG * Grass.Length - (Mathf.Abs(endPositionBG - Sky[i].localPosition.x) + 0.2f), Sky[i].localPosition.y, Sky[i].localPosition.z);
            }
        }

        MoveMonster.transform.localPosition += Vector3.left * speedMoveBG;
    }
    public bool CanMove;
    public bool CanWin;
    private void FixedUpdate()
    {
        if (isGameReadyToPlay)
        {
            speedMoveBG = Time.fixedDeltaTime * 1.7f;
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
            //if (knifeObject.ChildKnife.GetComponent<HandleKnifeSprite>().RotateKnifeLoop == null)
            //{
            //    knifeObject.isThow = true;

            //    knifeObject.ChildKnife.GetComponent<HandleKnifeSprite>().RotateKnifeLoop = knifeObject.ChildKnife.transform.DOLocalRotate(new Vector3(
            //        knifeObject.ChildKnife.transform.localRotation.x,
            //        knifeObject.ChildKnife.transform.localRotation.y,
            //        -540), time, RotateMode.FastBeyond360).OnComplete(() =>
            //        {
            //            knifeObject.animatorEffectKnife.GetComponent<TrailRenderer>().enabled = false;
            //            IsDrop = true;
            //        });
            //}
            if (spriteKnife.localRotation.z == 0)
            {

                knifeObject.KnifeRotate = spriteKnife.DOLocalRotate(new Vector3(0, 0, -180), 0.5f).OnComplete(() =>
                {
                    knifeObject.isThow = true;
                    IsDrop = true;
                });

            }
            if (IsDrop && rotateDrop == null)
            {
                rotateDrop = knifeObject.ChildKnife.transform.DOLocalRotate(new Vector3(knifeObject.ChildKnife.transform.localRotation.x,
                 knifeObject.ChildKnife.transform.localRotation.y, -270), 1f, RotateMode.Fast).OnComplete(() => { IsDrop = false; });
            }
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
        for (int i = 0; i < ListPumkin.Count; i++)
        {
            ListPumkin[i].GetComponent<Stupid>().ResetState();
        }
        for (int i = 0; i < ListWoodTarget.Count; i++)
        {
            ListWoodTarget[i].GetComponent<WoodTarget>().ResetState();
        }
        PanelLose.SetActive(false);
        CanMove = true;
        isGameReadyToPlay = true;
        Reset();
    }
    public void OnClickExit()
    {
        isGameReadyToPlay = false;
        for (int i = 0; i < ListPumkin.Count; i++)
        {
            ListPumkin[i].GetComponent<Stupid>().ResetState();
        }
        for (int i = 0; i < ListWoodTarget.Count; i++)
        {
            ListWoodTarget[i].GetComponent<WoodTarget>().ResetState();
        }
        Menu.SetActive(true);
        PanelLose.SetActive(false);
        CanMove = false;
        ResetAllObjToPool();
        ResetBG();
    }
#region resetOBj
    public void ResetAllObjToPool()
    {
        int redIndex = 0;
        for (int j = 0; j < ListWoodTarget.Count; j++)
        {
            if (ListWoodTarget[j].name.Contains("red target"))
            {
                
                ListWoodTarget[j].transform.parent = MoveMonster;
                ListWoodTarget[j].transform.localPosition = poolPosition;
                ListWoodTarget[j].SetActive(false);
                redIndex++;
            }
        }
        int woodIndex = 0;
        for (int j = 0; j < ListWoodTarget.Count; j++)
        {
            ListWoodTarget[j].transform.parent = MoveMonster;
            if (ListWoodTarget[j].name.Contains("wood target"))
            {
                ListWoodTarget[j].transform.localPosition = poolPosition;
                ListWoodTarget[j].SetActive(false);
                woodIndex++;
            }
        }
        int stupidnum = 0;
        for (int j = 0; j < ListStupid.Count; j++)
        {
            ListStupid[j].transform.parent = MoveMonster;
            if (ListStupid[j].name.Contains("Stupid"))
            {
                ListStupid[j].transform.localPosition = poolPosition;
                ListStupid[j].SetActive(false);
                ListStupid[j].GetComponent<Stupid>().isActiveMove = false;
                stupidnum++;
            }
        }
        int fruitnum = 0;
        for (int j = 0; j < ListFruit.Count; j++)
        {
            ListFruit[j].transform.parent = MoveMonster;
            if (ListFruit[j].name.Contains("Fruit"))
            {
                ListFruit[j].transform.localPosition = poolPosition;
                ListFruit[j].SetActive(false);
                ListStupid[j].GetComponent<Stupid>().isActiveMove = false;
                fruitnum++;
            }
        }
        int boarNum = 0;
        for (int j = 0; j < ListBoar.Count; j++)
        {
            ListBoar[j].transform.parent = MoveMonster;
            if (ListBoar[j].name.Contains("Boar"))
            {
                ListBoar[j].transform.localPosition = poolPosition;
                ListBoar[j].SetActive(false);
                boarNum++;
            }
        }
        int ballonNum = 0;
        for (int j = 0; j < ListBallon.Count; j++)
        {
            ListBallon[j].transform.parent = MoveMonster;
            if (ListBallon[j].name.Contains("Ballon"))
            {
                ListBallon[j].transform.localPosition = poolPosition;
                ListBallon[j].SetActive(false);
                if (ListBallon[j].GetComponent<BoxCollider2D>() != null)
                {
                    ListBallon[j].GetComponent<Ballon>().box.enabled = true;
                }

                ballonNum++;
            }
        }
        int pumkinNum = 0;
        for (int j = 0; j < ListPumkin.Count; j++)
        {
            ListPumkin[j].transform.parent = MoveMonster;
            if (ListPumkin[j].name.Contains("Pumkin"))
            {
                ListPumkin[j].transform.localPosition = poolPosition;
                ListPumkin[j].SetActive(false);
                ListStupid[j].GetComponent<Stupid>().isActiveMove = false;
                ListStupid[j].GetComponent<Stupid>().jumb = false;
                pumkinNum++;
            }
        }
        int vutulreNum = 0;
        for (int j = 0; j < ListVulture.Count; j++)
        {
            ListVulture[j].transform.parent = MoveMonster;
            if (ListVulture[j].name.Contains("vulture"))
            {
                ListVulture[j].transform.localPosition = poolPosition;
                ListVulture[j].SetActive(false);
                vutulreNum++;
            }
        }
        int spiderNum = 0;
        for (int j = 0; j < ListSprider.Count; j++)
        {
            ListSprider[j].transform.parent = MoveMonster;
            if (ListSprider[j].name.Contains("spider"))
            {
                ListSprider[j].transform.localPosition = poolPosition;
                ListSprider[j].SetActive(false);
                spiderNum++;
            }
        }
        int dumkinNum = 0;
        for (int j = 0; j < ListDummy.Count; j++)
        {
            ListDummy[j].transform.parent = MoveMonster;
            if (ListDummy[j].name.Contains("dummy"))
            {
                ListDummy[j].transform.localPosition = poolPosition;
                ListDummy[j].SetActive(false);
                if (ListDummy[j].GetComponent<BoxCollider2D>() != null)
                {
                    ListDummy[j].GetComponent<Dummy>().InPool();
                }

                dumkinNum++;
            }
        }
        int crazyDogNum = 0;
        for (int j = 0; j < ListCrazyDog.Count; j++)
        {
            ListCrazyDog[j].transform.parent = MoveMonster;
            if (ListCrazyDog[j].name.Contains("CrazyDog"))
            {
                ListCrazyDog[j].transform.localPosition = poolPosition;
                ListCrazyDog[j].SetActive(false);
                crazyDogNum++;
            }
        }

        //
        int batnum = 0;
        for (int j = 0; j < ListBat.Count; j++)
        {
            ListBat[j].transform.parent = MoveMonster;
            if (ListBat[j].name.Contains("Bat"))
            {
                ListBat[j].transform.localPosition = poolPosition;
                ListBat[j].SetActive(false);
                batnum++;
            }
        }
        //
        int ghostnum = 0;
        for (int j = 0; j < ListGhost.Count; j++)
        {
            ListGhost[j].transform.parent = MoveMonster;
            if (ListGhost[j].name.Contains("Ghost"))
            {
                ListGhost[j].transform.localPosition = poolPosition;
                ListGhost[j].SetActive(false);
                ghostnum++;
            }
        }
        //
        int rabbitnum = 0;
        for (int j = 0; j < ListRabbit.Count; j++)
        {
            ListRabbit[j].transform.parent = MoveMonster;
            if (ListRabbit[j].name.Contains("rabbit"))
            {
                ListRabbit[j].transform.localPosition = poolPosition;
                ListRabbit[j].SetActive(false);
                rabbitnum++;
            }
        }
        //
        int birdnum = 0;
        for (int j = 0; j < ListBird.Count; j++)
        {
            ListBird[j].transform.parent = MoveMonster;
            if (ListBird[j].name.Contains("Bird"))
            {
                ListBird[j].transform.localPosition = poolPosition;
                ListBird[j].SetActive(false);
                birdnum++;
            }
        }
        //

        int wizardnum = 0;
        for (int j = 0; j < ListWizard.Count; j++)
        {
            ListWizard[j].transform.parent = MoveMonster;
            if (ListWizard[j].name.Contains("wizard"))
            {
                ListWizard[j].transform.localPosition = poolPosition;
                ListWizard[j].SetActive(false);
                wizardnum++;
            }
        }
        //
        int cownum = 0;
        for (int j = 0; j < ListCow.Count; j++)
        {
            ListCow[j].transform.parent = MoveMonster;
            if (ListCow[j].name.Contains("Cow"))
            {
                ListCow[j].transform.localPosition = poolPosition;
                ListCow[j].SetActive(false);
                cownum++;
            }
        }
    }
#endregion
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
    int ChooseMapNumber;
    public void StartGame()
    {
        SetUpMap1();

        setStartPosBG();
        CreateObject();
        ResetBG();
        ChooseMapNumber = 0;

        SetupPositionObj(RedTargetPos, TargetPos, Stupid, FruitPos, BoarsPos, BallonPos, PumKinPos, VulturePos, SpriderPos, DumkinPos, CrazyDog, batPos0, ghostPos0, rabbitPos0, wizardPos0, birdPos0, cowpos0);
        isActionMonsterNeverUse();
        isActiveMenu(false);
        CountNumber();
    }
    public void StartGame2()
    {
        SetUpMap2();
        setStartPosBG();
        CreateObject();
        ChooseMapNumber = 1;
        ResetBG();
        SetupPositionObj(RedTargetPos2, TargetPos2, Stupid2, FruitPos2, BoarsPos2, BallonPos2, PumKinPos2, VulturePos2, SpriderPos2, DumkinPos2, CrazyDog2, batPos, ghostPos, rabbitPos, wizardPos, birdPos, cowpos);
        isActionMonsterNeverUse();
        isActiveMenu(false);
        CountNumber();
    }
    public void isActionMonsterNeverUse()
    {
        for (int i = 0; i < ListTotalObject.Count; i++)
        {
            if (ListTotalObject[i].transform.localPosition.y == 15)
            {
                ListTotalObject[i].SetActive(false);
            }
        }
    }
    public void SetUpMap1()
    {
        for (int i = 0; i < Grass.Length; i++)
        {
            Grass[i].GetComponent<SpriteRenderer>().sprite = Grass1[i];
        }
        for (int i = 0; i < Grass.Length; i++)
        {
            Hill[i].GetComponent<SpriteRenderer>().sprite = Hill1[i];
        }
        for (int i = 0; i < Grass.Length; i++)
        {
            Moutain[i].GetComponent<SpriteRenderer>().sprite = Moutain1[i];
            Moutain[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < Grass.Length; i++)
        {
            Sky[i].GetComponent<SpriteRenderer>().sprite = Sky1[i];
        }
        for (int i = 0; i < House.Length; i++)
        {
            House[i].gameObject.SetActive(false);
        }
        Map2.gameObject.SetActive(false);
        Map1.gameObject.SetActive(true);
        MoveMonster = Map1;
    }
    public void SetUpMap2()
    {
     
        for (int i = 0; i < Grass.Length; i++)
        {
            Grass[i].GetComponent<SpriteRenderer>().sprite = Grass2[i];
        }
        for (int i = 0; i < Grass.Length; i++)
        {
            Hill[i].GetComponent<SpriteRenderer>().sprite = Hill2[i];
        }
        for (int i = 0; i < Grass.Length; i++)
        {
            Moutain[i].GetComponent<SpriteRenderer>().sprite = House2[i];
            Moutain[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < Grass.Length; i++)
        {
            Sky[i].GetComponent<SpriteRenderer>().sprite = Sky2[i];
        }
        for (int i = 0; i < House.Length; i++)
        {
            House[i].gameObject.SetActive(true);
        }
        Map1.gameObject.SetActive(false);
        Map2.gameObject.SetActive(true);
        MoveMonster = Map2;
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
            if (se != null)
            {
                se.Kill();
                se = null;
            }
        });
    }

    public void WinGame()
    {
        isGameReadyToPlay = false;
        //Reset();
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
   
        if (ChooseMapNumber == 0)
        {
            SetupPositionObj(RedTargetPos, TargetPos, Stupid, FruitPos, BoarsPos, BallonPos, PumKinPos, VulturePos, SpriderPos, DumkinPos, CrazyDog, batPos0, ghostPos0, rabbitPos0, wizardPos0, birdPos0, cowpos0);
        }
        if(ChooseMapNumber == 1)
        {
            SetupPositionObj(RedTargetPos2, TargetPos2, Stupid2, FruitPos2, BoarsPos2, BallonPos2, PumKinPos2, VulturePos2, SpriderPos2, DumkinPos2, CrazyDog2, batPos, ghostPos, rabbitPos, wizardPos, birdPos, cowpos);
        }
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
