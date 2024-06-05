using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading;
using System.Collections;
using TMPro;
using Random = UnityEngine.Random;
using System.Linq;

public class LineManager : MonoBehaviour
{
    public Vector3 refpos,refposdimondbutton;
    public GameObject buydimondbutton,dimondbutton;
   public GameObject winpos, pausebutton;
    public Text currecyrepresenterbar;
    public int ScoreValue;
    public Text RequiredDiomondtext;
    public GameObject NotEnoughGems;
    public GameObject[] layerBackground;
    int progresscounter = 0;
    public bool isunlock = false;
    public TextMeshProUGUI showtextinpanel;
    public GameObject nextbutton;
    int countmode;
    public GameObject AskingDimondPanel;
    int scorepoint;
    public Text counterbuyhint;
    int counterhintcount = 0;
    int showcounter = 3;
    long[] pattern = { 500, 100, 50 };
    public Animation animationobject;
    public TextMeshProUGUI textMesh;
    public GameObject shopbuttonicon;
    public GameObject videoobject; public Text videoText;
    public int counterhint = 0;
    [SerializeField] public GameObject hinttab;
    public GameObject Alphaobject, betaobject;
    [SerializeField] GameObject[] animatedcoinarray;
    Vector3 animatedposition;
    [SerializeField] GameObject Crownobject;
    public Sprite starImage;
    public bool settingbool = false;
    int elementindex;
    int matchindex;
    public GameObject[] ChildStarObject;
    public bool levelmode;
    public GameObject[] planeobjectarray;
    public GameObject planeobject;
    public bool iscoinless = false;
    Sprite EmojiSprite;
    Sprite MatchSprite;
    public LineIdentity matchlayer;
    bool israndom = false;
    public Image ImageSlider;
    [SerializeField] Text Levelname;
    float percentagevalue;
    [SerializeField] Text TextintoString;
    Vector3[] previouspositionEmoji = new Vector3[5];
    Vector3[] previouspositionMatch = new Vector3[5];
    public Sprite[] spriteforprogressbar;
    [SerializeField] AudioClip[] Sound;
    public GameObject animatedcoin;
    LevelMakngScript GetSprite;   // Level Making Script
    public LineManager Linemanager;
    public int countermood = 1; int Score = 0, linecount = 0, index = 0; public int levelCount;
    float max = 100f, min = 50f, currentvalue;
    public Image slideriqabar;
    public string[] levelnamestring = { "baby", "teen", "boy", "adult", "1baby", "1teen", "1boy", "1adult", "2bab2y", "tee2n", "2boy2", "2adul2t", "2bab3y", "t3een", "bo3y", "adul3t" };
    Vector3 mous, Startpos, endpos, mousepos;
    public Vector3[] desiredposfor3elementemoji, desiredposfor4elementemoji;
    bool isgenerated = false, isclick = false;
    public bool mode = false, isreload = false;
    GameObject positionObject, StartObject = null, EndObject = null, lineObject;
    public Text CoinCurrency, DimondCurrency;
    public int DimondCurrencyint = 0;
    public GameObject WiningPopup, EmojiElement, MatchElement, LevelLogo, losepopup, iqobject;
    public Text LevelogoText;
    public GameObject[] EmojiLayer, MatchLayer, WhiteBoxEmoji, WhiteBoxMatch, layermaskmode;
    public Material material;
    public GameObject[] EmojiArray, MatchArray;
    public bool isnext = false;
    public int scorevar;
    public List<GameObject> Lineobjectlist = new List<GameObject>();
    List<LineRenderer> linelist = new List<LineRenderer>();
    [SerializeField] GameObject posobject;
    LineRenderer Lineshadow, reccordline;
    public static LineManager instance;
    public LineIdentity Selected, Dropped, givecorrect;
    public LineIdentity[] EmojiArrayIdentity, MatchArrayIdentity; // Isko remove bhi ho skty hain
    public List<Vector3> positionlist = new List<Vector3>();
    public GameObject AskingTab;
    public GameObject hometab, settingtab;
    [SerializeField] Image SlideBARbACKgrund;

    // Start is called before the first frame update
    void Start()
    {
        refpos = losepopup.transform.position;   
        if (PlayerPrefs.HasKey("Currency"))
        {
            ScoreValue = PlayerPrefs.GetInt("Currency");
            currecyrepresenterbar.text = ScoreValue.ToString();
        }
        else
        {
            ScoreValue = 0;
            currecyrepresenterbar.text = ScoreValue.ToString();
            PlayerPrefs.SetInt("Currency", ScoreValue);
        }
        if (PlayerPrefs.HasKey("Iscoinless"))
        {
            int indexvalue = PlayerPrefs.GetInt("Iscoinless");
            for (int i = 0; i <= indexvalue; i++)
            {
                if (i <= index)
                {
                    planeobjectarray[i].GetComponent<Button>().interactable = false;
                }
                else
                {
                    planeobjectarray[i].GetComponent<Button>().interactable = true;
                }
            }
        }

        if (PlayerPrefs.HasKey("ShowCounter"))
        {

            showcounter = PlayerPrefs.GetInt("ShowCounter");
            counterbuyhint.text = showcounter.ToString();
        }
        else
        {
            // showcounter = PlayerPrefs.GetInt("ShowCounter");
            counterbuyhint.text = showcounter.ToString();
        }
        mode = false;
        levelmode = false;

        for (int i = 0; i < WhiteBoxEmoji.Length; i++)
        {
            WhiteBoxEmoji[i].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
            EmojiLayer[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 255);
            layermaskmode[i].SetActive(false);
            WhiteBoxMatch[i].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
            MatchLayer[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 255);
        }
        GetSprite = GetComponent<LevelMakngScript>();
        matchlayer = GetComponent<LineIdentity>();
        Lineshadow = gameObject.AddComponent<LineRenderer>();
        Lineshadow.startWidth = 0.040f;
        Lineshadow.endWidth = 0.040f;
        Lineshadow.material.color = Color.green;

        Lineshadow.useWorldSpace = true;
        Lineshadow.positionCount = 2;
        Lineshadow.enabled = false;
        Lineshadow.sortingOrder = -350;
        for (int i = 0; i < WhiteBoxEmoji.Length; i++)
        {
            previouspositionEmoji[i] = WhiteBoxEmoji[i].transform.position;
            previouspositionMatch[i] = WhiteBoxMatch[i].transform.position;
        }

    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (StartObject == null)
            {
                StartObject = RayCastFuc();

            }

            if (StartObject != null)
            {

                //Selected = StartObject.GetComponent<LineIdentity>();


                if (MainMenuScript.Instance.vibrate)
                {
                    Vibration.Vibrate(50);

                    //Vibration.Vibrate(50);

                }
                else
                {
                    Selected.GetComponent<AudioSource>().clip = Sound[4];
                    Selected.GetComponent<AudioSource>().Play();
                }
                Startpos = StartObject.transform.position;
                for (int i = 0; i < EmojiArray.Length; i++)
                {

                    if (Startpos == EmojiArray[i].transform.position)
                    {

                        EmojiLayer[i].GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    else
                    {
                        if (Startpos == MatchArray[i].transform.position)
                        {
                            MatchLayer[i].GetComponent<SpriteRenderer>().color = Color.green;
                        }
                    }
                }

                if (linecount > 0 && StartObject != null)
                {
                    Startpos = StartObject.transform.position;
                    OnLeftClick(Startpos);

                }
                if (mode == true)
                {
                    positionObject = new GameObject("PositionObject");
                    positionObject.transform.position = StartObject.transform.position;
                    SpriteRenderer positionSpriteRenderer = positionObject.AddComponent<SpriteRenderer>();
                    positionSpriteRenderer.sprite = StartObject.GetComponent<SpriteRenderer>().sprite;
                    positionSpriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                }

                Selected = StartObject.GetComponent<LineIdentity>();
                Lineshadow.SetPosition(0, StartObject.transform.position);
                Lineshadow.SetPosition(1, StartObject.transform.position);
                Lineshadow.enabled = true;
            }
        }

        if (Input.GetMouseButton(0) && StartObject != null)
        {
            Vector3 mousepos = Vectorpos();
            updatelinepos(StartObject.transform.position, mousepos);

        }
        if (!Input.GetMouseButton(0))
        {
            Destroy(positionObject);
        }
        if (Input.GetMouseButtonUp(0) && StartObject != null)
        {
           
            if (EndObject == null)
            {
                EndObject = RayCastFuc();

            }



            if (EndObject != null && EndObject != StartObject)
            {

                Dropped = EndObject.GetComponent<LineIdentity>();


                //for (int i = 0; i < EmojiLayer.Length; i++)
                //{
                //    if (EndObject.transform.position == EmojiArray[i].transform.position || (EndObject.transform.position == EmojiArray[i].transform.position))
                //    {
                //        Debug.Log("1452");
                //        EmojiLayer[i].GetComponent<SpriteRenderer>().color = Color.yellow;
                //    }
                //    else
                //    {
                //        Debug.Log("1A452");
                //        if (EndObject.transform.position == MatchArray[i].transform.position)
                //        {
                //            Debug.Log("14B52");
                //            MatchLayer[i].GetComponent<SpriteRenderer>().color = Color.yellow;
                //        }
                //    }
                //}



                if (StartObject.tag != EndObject.tag)
                {
                  
                    Vector3 mous = new Vector3(0f, 0f, 0f);
                    endpos = EndObject.transform.position;

                    creatline(Startpos, endpos);
                }
                if (mode == true)
                {

                    if (Dropped.Current == Dropped.Correct && Selected.Current == Selected.Correct)
                    {
                        Lineshadow.enabled = false;

                        EndObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);


                        for (int i = 0; i < EmojiArray.Length; i++)
                        {
                            if (StartObject == EmojiArray[i])
                            {
                                EmojiArrayIdentity[i].enabled = false;
                                WhiteBoxEmoji[i].SetActive(false);
                                EmojiLayer[i].SetActive(false);

                            }
                        }

                        // countmode;
                    }
                    else
                    {
                        mous = new Vector3(0f, 0f, 0f);
                        Lineshadow.enabled = false;
                        StartObject = null;
                    }
                }
                // Update Startpos for the next line

            }
            Lineshadow.enabled = false;
            StartObject = null;
            EndObject = null;
            isclick = true;
           //WinMatch();

        }

    }
    public void closeaskingbar()
    {
        MainMenuScript.Instance.isplayed = true;
        MatchElement.SetActive(true);
        EmojiElement.SetActive(true);
        LevelLogo.SetActive(true);
        hometab.SetActive(true);
        settingtab.SetActive(true);
        hinttab.SetActive(true);
        shopbuttonicon.SetActive(true);
        
        MainMenuScript.Instance.levelogo.SetActive(true);
        foreach (GameObject g1 in Lineobjectlist)
        {
            g1.SetActive(true);
        }
        //MatchElement.SetActive(false);
        //EmojiElement.SetActive(false);
        AskingTab.SetActive(false);
    }
    // creat line START
    void creatline(Vector3 startpos, Vector3 endpos)
    {
        startpos.z = endpos.z = 0;
        GameObject LineObject = new GameObject("Line" + linecount);
        lineObject = LineObject;
        lineObject.transform.position = startpos;
        Lineobjectlist.Add(LineObject);
        LineRenderer newline = LineObject.AddComponent<LineRenderer>();
        newline.positionCount = 2;
        newline.startWidth = 0.040f;
        newline.endWidth = 0.040f;
        newline.material.color = Color.green;
        newline.useWorldSpace = true;
        newline.positionCount = 2;
        newline.SetPosition(0, startpos);
        newline.SetPosition(1, endpos);

        foreach (LineRenderer line in linelist)
        {
          
            if ((line.GetPosition(0) == startpos || line.GetPosition(1) == endpos) || (line.GetPosition(0) == endpos || line.GetPosition(1) == startpos))
            {
                Debug.Log("1452");

                GameObject g1 = line.gameObject;
                for (int i = 0; i < EmojiArrayIdentity.Length; i++)
                {
                    if (EmojiArrayIdentity[i].linegameobject == g1)
                    {
                       

                        EmojiArrayIdentity[i].outline.color = Color.yellow;
                        EmojiArrayIdentity[i].Current = null;


                    }
                    if (MatchArrayIdentity[i].linegameobject == g1 || MatchArrayIdentity[i].linegameobject == null)
                    {
                       

                        MatchArrayIdentity[i].outline.color = Color.yellow;
                        MatchArrayIdentity[i].Current = null;

                    }
                }
                line.enabled = false;
                linelist.Remove(line);
                Lineobjectlist.Remove(line.gameObject);
                Destroy(line.gameObject);
                linecount--;
                break;
            }
        }

        lineObject = LineObject;
        linelist.Add(newline);
        linecount = linelist.Count;
        reccordline = newline;

        isgenerated = true;
        if (Dropped != null)
        {

            Selected.Current = Dropped;
            Dropped.Current = Selected;
            Selected.line = newline;
            Dropped.line = newline;
            Selected.linegameobject = LineObject;
            Dropped.linegameobject = LineObject;

            if (Dropped.Current == Dropped.Correct && Selected.Current == Selected.Correct)
            {
              

                newline.material.color=Color.green;
                Dropped.outline.color = Color.green;
                Selected.outline.color = Color.green;
                if (MainMenuScript.Instance.isplayed)
                {
                    if (MainMenuScript.Instance.vibrate)
                    {
                        Vibration.Vibrate(100);

                        //Selected.GetComponent<AudioSource>().clip = Sound[3];
                        //Selected.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        Selected.GetComponent<AudioSource>().clip = Sound[5];
                        Selected.GetComponent<AudioSource>().Play();
                    }
                }


                if (mode == true)
                {
                    Dropped.GetComponent<SpriteRenderer>().sortingOrder = 150;
                    newline.enabled = false;
                }

            }
            else
            {
                Dropped.outline.color = Color.red;
                Selected.outline.color = Color.red;
                newline.material.color = Color.red;
                if (MainMenuScript.Instance.vibrate)
                {
                    Vibration.Vibrate(70);
                    // Vibration.Vibrate(pattern, 2);
                    //Selected.GetComponent<AudioSource>().clip = Sound[3];
                    //Selected.GetComponent<AudioSource>().Play();
                }
                else
                {
                    Selected.GetComponent<AudioSource>().clip = Sound[2];
                    Selected.GetComponent<AudioSource>().Play();
                }

                if (mode == true)
                {
                    newline.enabled = false;
                }
            }

        }
      WinMatch();
    }
    //creat lineEnd


    //Winning and Restart Start
    void WinMatch()
    { countermood = 0;


        if (levelCount == linelist.Count)
        {

           
            for (int i = 0; i < levelCount; i++)
            {


                if ((MatchArrayIdentity[i].Current == MatchArrayIdentity[i].Correct) && (EmojiArrayIdentity[i].Current == EmojiArrayIdentity[i].Correct))
                {

                    countermood++;
                }
                if (countermood == levelCount)
                {

                    MatchElement.SetActive(false);
                    EmojiElement.SetActive(false);
                    foreach (GameObject g1 in Lineobjectlist) { g1.SetActive(false); }
                    MainMenuScript.Instance.levelogo.SetActive(false);
                    MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[0];
                    for (int j = 0; j < EmojiArrayIdentity.Length; j++)
                    {

                        MatchArray[i].SetActive(false);
                        EmojiArray[i].SetActive(false);
                        MatchArrayIdentity[i].enabled = false;
                        EmojiArrayIdentity[i].enabled = false;


                    }
                    //if (MainMenuScript.Instance.value == 29 || MainMenuScript.Instance.currentlevel == 29)
                    //{
                    //    nextbutton.SetActive(false);
                    //}
                    //else
                    //{
                    //    nextbutton.SetActive(true);
                    //}
                    hinttab.SetActive(false);
                    shopbuttonicon.SetActive(false);
                    WiningPopup.SetActive(true);
                    //  nextbutton.SetActive(false)


                    //DOMove(winpos.transform.position, 1f,false);
                    WiningPopup.transform.DOScale(1, 1f).OnComplete(() => {
                        delayingforcoincurrency();
                       // yield return new WaitForSeconds(1f);
                        nextbutton.SetActive(true);
                        WiningPopup.GetComponent<AudioSource>().clip = Sound[1];
                        WiningPopup.GetComponent<AudioSource>().Play();
                        int randomvalue = Random.Range(0, 10);
                        progressbar(randomvalue);
                        iqobject.SetActive(true);

                    });
                    if (MainMenuScript.Instance.islevelfunc)
                    {
                        MainMenuScript.Instance.value++;
                        if (MainMenuScript.Instance.value <= MainMenuScript.Instance.currentlevel)
                        {
                            MainMenuScript.Instance.currentlevel = PlayerPrefs.GetInt("Currentlevel");
                            PlayerPrefs.SetInt("Currentlevel", MainMenuScript.Instance.currentlevel);
                        }
                        else
                        {

                            MainMenuScript.Instance.currentlevel = MainMenuScript.Instance.value;
                            PlayerPrefs.SetInt("Currentlevel", MainMenuScript.Instance.currentlevel);
                        }

                    }
                    else
                    {

                        MainMenuScript.Instance.currentlevel++;
                        PlayerPrefs.SetInt("Currentlevel", MainMenuScript.Instance.currentlevel);
                    }

                    // animatedcoinarray[0].SetActive(true);
                    //for (int j = 0; j < 10; j++)
                    //{
                    //    animatedcoinarray[j].SetActive(true);
                    //}
                    MainMenuScript.Instance.levelogo.SetActive(false);
                    LineManager.instance.hometab.SetActive(false);
                    LineManager.instance.settingtab.SetActive(false);

                    //DOTween.Sequence().AppendInterval(1f).Append(CoinCurrency.DOText(ScoreScript.Instance.scorevalue.ToString(), 0));

                    //StartCoroutine(RunAnimate());
                   
                    if (LineIdentity.instance.iscountertrue)
                    {
                        LevelDataBase.instance.levelArray[LineIdentity.instance.counter].star[0].GetComponent<Image>().sprite = starImage;
                        LevelDataBase.instance.levelArray[LineIdentity.instance.counter].star[1].GetComponent<Image>().sprite = starImage;
                        LevelDataBase.instance.levelArray[LineIdentity.instance.counter].star[2].GetComponent<Image>().sprite = starImage;
                        PlayerPrefs.SetInt("Level" + LineIdentity.instance.counter + "Stars", 3);
                        PlayerPrefs.Save();
                        //currentvalue = levelCount * 40;
                        for (int j = 0; j < ChildStarObject.Length; j++)
                        {
                            ChildStarObject[j].GetComponent<CanvasGroup>().alpha = 1;
                            ChildStarObject[j].GetComponent<Image>().color = Color.yellow;
                        }
                        //int randomvalue = Random.Range(0, 10);
                        //progressbar(randomvalue);
                        //iqobject.SetActive(true);


                    }
                    else
                    {
                        if (MainMenuScript.Instance.islevelfunc)
                        {
                            for (int j = 0; j < ChildStarObject.Length; j++)
                            {
                                ChildStarObject[j].GetComponent<CanvasGroup>().alpha = 1;
                                ChildStarObject[j].GetComponent<Image>().color = Color.yellow;
                            }

                            LevelDataBase.instance.levelArray[MainMenuScript.Instance.value - 1].star[0].GetComponent<Image>().sprite = starImage;
                            LevelDataBase.instance.levelArray[MainMenuScript.Instance.value - 1].star[1].GetComponent<Image>().sprite = starImage;
                            LevelDataBase.instance.levelArray[MainMenuScript.Instance.value - 1].star[2].GetComponent<Image>().sprite = starImage;
                            //PlayerPrefs.SetInt("Level" + MainMenuScript.Instance.currentlevel + "Stars", 3);
                            //currentvalue = levelCount * 40;
                            int storevalue = MainMenuScript.Instance.value - 1;

                            PlayerPrefs.SetInt("Level" + storevalue + "Stars", 3);
                            //int randomvalue = Random.Range(0, 10);
                            //progressbar(randomvalue);
                            //iqobject.SetActive(true);

                        }
                        else
                        {

                            for (int j = 0; j < ChildStarObject.Length; j++)
                            {
                                ChildStarObject[j].GetComponent<CanvasGroup>().alpha = 1;
                                ChildStarObject[j].GetComponent<Image>().color = Color.yellow;
                            }

                            LevelDataBase.instance.levelArray[MainMenuScript.Instance.currentlevel - 1].star[0].GetComponent<Image>().sprite = starImage;
                            LevelDataBase.instance.levelArray[MainMenuScript.Instance.currentlevel - 1].star[1].GetComponent<Image>().sprite = starImage;
                            LevelDataBase.instance.levelArray[MainMenuScript.Instance.currentlevel - 1].star[2].GetComponent<Image>().sprite = starImage;
                            int storevalue= MainMenuScript.Instance.currentlevel - 1;
                           
                            PlayerPrefs.SetInt("Level" + storevalue + "Stars", 3);
                            //currentvalue = levelCount * 40;

                           
                        }

                    }






                    countermood++;

                    System.Random random = new System.Random();
                    scorepoint = Random.Range(10, 20);
                    ScoreValue += scorepoint;
                    currecyrepresenterbar.text = ScoreValue.ToString();
                   PlayerPrefs.SetInt("Currency",ScoreValue);
                   // delayingforcoincurrency();


                    //ScoreScript.Instance.textscore.text = ScoreValue.ToString();//PlayerPrefs.GetInt("Score", 0).ToString();



                    break;


                }




                if (MatchArrayIdentity[i].Current != MatchArrayIdentity[i].Correct && EmojiArrayIdentity[i].Current != EmojiArrayIdentity[i].Correct)
                {
                    //int value1 = PlayerPrefs.GetInt("Score",0);
                    //int value2 = PlayerPrefs.GetInt("Dimond",0);
                    //ScoreScript.Instance.scorevalue = value1;
                    //ScoreScript.Instance.Dimondvalue = value2;
                    //PlayerPrefs.SetInt("Score", ScoreScript.Instance.scorevalue);
                    //PlayerPrefs.SetInt("Score", ScoreScript.Instance.Dimondvalue);

                    //ScoreScript.Instance.Update();

                    LevelLogo.SetActive(false);
                    WiningPopup.SetActive(false);
                    MatchElement.SetActive(false);
                    EmojiElement.SetActive(false);
                    foreach (GameObject g1 in Lineobjectlist) { g1.SetActive(false); }
                    MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[0];
                    hinttab.SetActive(false);
                    shopbuttonicon.SetActive(false);
                    hometab.SetActive(false);
                    settingtab.SetActive(false);
                    losepopup.SetActive(true);
                    // losepopup.transform.DOMove(winpos.transform.position, 1f, false);
                    losepopup.transform.DOMove(winpos.transform.position, 1f, false).OnComplete(() => {
                        delayingforcoincurrency();
                        // yield return new WaitForSeconds(1f);
                        pausebutton.SetActive(true);
                        losepopup.GetComponent<AudioSource>().clip = Sound[0];
                        losepopup.GetComponent<AudioSource>().Play();

                        iqobject.SetActive(true);
                        int randomvalue = Random.Range(0, 3);
                        progressbar(randomvalue);
                    });
                  
                    isreload = true;
                    


                  
                    MainMenuScript.Instance.currentlevel = PlayerPrefs.GetInt("Currentlevel", 0);
                    for (int j = 0; j < EmojiArrayIdentity.Length; j++)
                    {
                        MatchArray[i].SetActive(false);
                        EmojiArray[i].SetActive(false);
                        MatchArrayIdentity[i].enabled = false;
                        EmojiArrayIdentity[i].enabled = false;

                    }


                    break;




                }
            }


            
            

        }

    }


    void delayingforcoincurrency()
    {


        CoinCurrency.text = "0";
       // DimondCurrency.text = 1.ToString();
        CoinCurrency.DOText(scorepoint.ToString(), 1f, true, ScrambleMode.All);
        CoinCurrency.GetComponent<AudioSource>().clip = Sound[6];
        CoinCurrency.GetComponent<AudioSource>().Play();

        // posobject.transform.position = Crownobject.transform.localPosition;

    }
    IEnumerator RunAnimate()
    {

        yield return new WaitForSeconds(2f);
        Crownobject.GetComponent<Animator>();
        Crownobject.GetComponent<Animator>().DOPlay();
        yield return new WaitForSeconds(2f);
        Crownobject.SetActive(false);
    }


    public void closeFuncLosepoup()
    {
        iqobject.SetActive(false);
        MainMenuScript.Instance.isplayed = false;
        Crownobject.SetActive(false);
        LineManager.instance.hometab.SetActive(false);
        LineManager.instance.settingtab.SetActive(false);
        losepopup.SetActive(false);
        iqobject.SetActive(false);
        MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[0];
        MainMenuScript.Instance.MainMenu.SetActive(true);
        LevelLogo.SetActive(false);
        MainMenuScript.Instance.PlaneObjectButton.SetActive(false);
    }



    public void closeFunc()
    {
        Crownobject.SetActive(false);
        WiningPopup.SetActive(false);
        // MainMenuScript.Instance.isplayed = false;
        // MainMenuScript.Instance.PlaneObjectButton.SetActive(true);
        iqobject.SetActive(false);
        LevelLogo.SetActive(false);
        MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[0];
        MainMenuScript.Instance.MainMenu.SetActive(true);
    }
    public void ResetFunction()
    {
        levelCount = 0;
        //  MainMenuScript.Instance.isplayed = false;
        //if (isreload)
        //{
        //    MainMenuScript.Instance.islevelfunc = true;
        //}

        MainMenuScript.Instance.currentlevel = PlayerPrefs.GetInt("Currentlevel", 0);

        for (int i = 0; i < WhiteBoxEmoji.Length; i++)
        {
            WhiteBoxEmoji[i].transform.position = previouspositionEmoji[i];
            WhiteBoxMatch[i].transform.position = previouspositionMatch[i];
            WhiteBoxEmoji[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            WhiteBoxMatch[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            layermaskmode[i].SetActive(false);
        }
        positionlist.Clear();
        if (MainMenuScript.Instance.currenttime != 0)
        {
            MainMenuScript.Instance.currenttime = 0;
        };
        MatchElement.SetActive(true);
        EmojiElement.SetActive(true);
        if (PlayerPrefs.HasKey("Iscoinless"))
        {

            int index = PlayerPrefs.GetInt("Iscoinless");
            if (index == 0)
            {
                MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[2];
            }
            else if (index == 1)
            {


                MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[3];

            }
            else if (index == 2)
            {


                MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[4];

            }
            else if (index == 3)
            {

                MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[5];

            }
            else if (index == 4)
            {


                MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[6];

            }
            else
            {

                MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[7];
            }
        }
        else
        {

            MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[1];
        }
        for (int j = 0; j < EmojiArrayIdentity.Length; j++)
        {
            MatchArray[j].SetActive(true);
            EmojiArray[j].SetActive(true);

            MatchArrayIdentity[j].enabled = true;
            EmojiArrayIdentity[j].enabled = true;

        }
        for (int i = 0; i < EmojiLayer.Length; i++)
        {
            EmojiLayer[i].GetComponent<SpriteRenderer>().color = Color.yellow;
            MatchLayer[i].GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        linecount = 0;
        index = 0;

        if (mode == true)
        {
            if (Dropped != null)
            {
                Dropped.outline.color = Color.yellow;
            }
        }
        else
        {
            if (Selected != null && Dropped != null)
            {
                Selected.outline.color = Color.yellow;
                Dropped.outline.color = Color.yellow;
            }
        }
        countermood = 0;
        isgenerated = false;
        isclick = false;
        Selected = null;
        Dropped = null;
        givecorrect = null;
        StartObject = null;
        EndObject = null;
        lineObject = null;
        linelist.Clear();
        settingbool = false;

        for (int i = 0; i < EmojiArrayIdentity.Length; i++)
        {
            MatchArray[i].GetComponent<BoxCollider>().enabled = true;

            EmojiArray[i].GetComponent<BoxCollider>().enabled = true;

            MatchArrayIdentity[i].GetComponent<BoxCollider>().enabled = true;

            EmojiArrayIdentity[i].GetComponent<BoxCollider>().enabled = true;

        }
        foreach (GameObject lineObject in Lineobjectlist)
        {
            Destroy(lineObject);
        }
        Lineobjectlist.Clear();
        WiningPopup.SetActive(false);
        losepopup.SetActive(false);

    }
    public void Reset()
    {
        losepopup.transform.position = refpos;
        pausebutton.SetActive(false);
        iqobject.SetActive(false);

        int value;
        //MainMenuScript.Instance.isplayed = true;
        LevelLogo.SetActive(true);
        if (MainMenuScript.Instance.islevelfunc)
        {
            //value = MainMenuScript.Instance.value;
            value = MainMenuScript.Instance.value;
            value = value + 1;
            textMesh.text = "Level " + value.ToString();
        }
        else
        {
          
            value = MainMenuScript.Instance.currentlevel;
            value = value + 1;
            textMesh.text = "Level " + value.ToString();
        }

        MainMenuScript.Instance.levelogo.SetActive(true);

        hinttab.SetActive(true);
        shopbuttonicon.SetActive(true);
        hometab.SetActive(true);
        settingtab.SetActive(true);
        ResetFunction();
        mode = MainMenuScript.Instance.mode = MainMenuScript.Instance.UpdateMode(MainMenuScript.Instance.currentlevel);
        //mode = false;
        if (PlayerPrefs.GetInt("Currentlevel", 0) == MainMenuScript.Instance.currentlevel)
        {

           

            if (mode == true)
            {

                for (int i = 0; i < EmojiArray.Length; i++)
                {
                    EmojiArray[i].GetComponent<SpriteRenderer>().sprite = null;
                    MatchArray[i].GetComponent<SpriteRenderer>().sprite = null;
                    EmojiArrayIdentity[i].GetComponent<SpriteRenderer>().sprite = null;
                    MatchArrayIdentity[i].GetComponent<SpriteRenderer>().sprite = null;
                }
                if (MainMenuScript.Instance.islevelfunc)
                {

                    Debug.Log("145236987D");
                    LevelMakngScript.instance.GenerateEmojiLevel(MainMenuScript.Instance.value - 1);
                    GetEmoji();


                }
                else
                {
                    Debug.Log("145236987C");
                    LevelMakngScript.instance.GenerateEmojiLevel(MainMenuScript.Instance.currentlevel);
                    GetEmoji();

                }

            }
            else
            {
                if (MainMenuScript.Instance.islevelfunc)
                {

                    if (MainMenuScript.Instance.isplayed == false)
                    {

                        if (mode == false)
                        {

                            LevelMakngScript.instance.GenerateLevel(MainMenuScript.Instance.value);
                            //GetSpriteREloadFunc();
                            GetSpriteFunc();
                        }
                    }



                }
                else
                {
                    if (MainMenuScript.Instance.isplayed)
                    {


                        if (mode == false)
                        {

                            LevelMakngScript.instance.GenerateLevel(MainMenuScript.Instance.currentlevel);
                            GetSpriteFunc();
                            value = MainMenuScript.Instance.currentlevel + 1;
                            textMesh.text = "Level " + value.ToString();
                        }
                    }
                }



            }
        }


    }
    // Winnnng and Restart End

    // Reload For TRying Satrt



    // Reload For Trying End
    public void GetSpriteREloadFunc()
    {


        if (MainMenuScript.Instance.copyLevelList != null && MainMenuScript.Instance.copyLevelList.Count > 0)
        {

            levelCount = MainMenuScript.Instance.copyLevelList.Count;
            if (levelCount == 3)
            {

                Vector3 desired;
                float desiredpos = 1f;
                desired = posobject.transform.position = WhiteBoxEmoji[0].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[0].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[0].transform.position;
                desired.y = desiredpos;
                WhiteBoxMatch[0].transform.position = desired;
                positionlist.Add(WhiteBoxMatch[0].transform.position);
                desiredpos = -0.5f;
                desired = posobject.transform.position = WhiteBoxEmoji[1].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[1].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[1].transform.position;
                desired.y = desiredpos;
                WhiteBoxMatch[1].transform.position = desired;
                positionlist.Add(WhiteBoxMatch[1].transform.position);
                desiredpos = -2f;
                desired = posobject.transform.position = WhiteBoxEmoji[2].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[2].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[2].transform.position;
                desired.y = desiredpos;
                WhiteBoxMatch[2].transform.position = desired;
                positionlist.Add(WhiteBoxMatch[2].transform.position);
                for (int i = 0; i < EmojiArray.Length; i++)
                {
                    WhiteBoxEmoji[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    WhiteBoxMatch[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }

            }
            if (levelCount == 4)
            {
                Vector3 desired;
                float desiredpos = 1f;
                desired = posobject.transform.position = WhiteBoxEmoji[0].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[0].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[0].transform.position;
                desired.y = desiredpos;
                WhiteBoxMatch[0].transform.position = desired;

                desiredpos = -0.5f;
                desired = posobject.transform.position = WhiteBoxEmoji[1].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[1].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[1].transform.position;
                desired.y = desiredpos;
                WhiteBoxMatch[1].transform.position = desired;

                desiredpos = -2f;
                desired = posobject.transform.position = WhiteBoxEmoji[2].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[2].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[2].transform.position;
                desired.y = desiredpos;
                WhiteBoxMatch[2].transform.position = desired;

                desiredpos = -3.5f;
                desired = posobject.transform.position = WhiteBoxEmoji[3].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[3].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[3].transform.position;
                desired.y = desiredpos;
                WhiteBoxMatch[3].transform.position = desired;
                positionlist.Add(WhiteBoxMatch[0].transform.position);
                positionlist.Add(WhiteBoxMatch[1].transform.position);
                positionlist.Add(WhiteBoxMatch[2].transform.position);
                positionlist.Add(WhiteBoxMatch[3].transform.position);
                for (int i = 0; i < EmojiArray.Length; i++)
                {
                    WhiteBoxEmoji[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    WhiteBoxMatch[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }

            }
            if (levelCount == 5)
            {
                Vector3 desired;
                for (int i = 0; i < 5; i++)
                {
                    WhiteBoxEmoji[i].transform.position = previouspositionEmoji[i];
                    WhiteBoxMatch[i].transform.position = previouspositionMatch[i];
                    positionlist.Add(WhiteBoxMatch[i].transform.position);
                }
                for (int i = 0; i < WhiteBoxEmoji.Length; i++)
                {
                    WhiteBoxEmoji[i].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    WhiteBoxMatch[i].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                }
                float desiredpos = 2f;
                desired = posobject.transform.position = WhiteBoxEmoji[0].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[0].transform.position = desired;

            }

            for (int i = 0; i < levelCount; i++)
            {
                int emojiindex = levelCount - i - 1;
                Sprite EmojiSprite = MainMenuScript.Instance.copyLevelList[emojiindex].Item1;
                Sprite MatchSprite = MainMenuScript.Instance.copyLevelList[i].Item2;

                EmojiArray[i].GetComponent<SpriteRenderer>().sprite = EmojiSprite;

                EmojiArrayIdentity[i].GetComponent<SpriteRenderer>().sprite = EmojiSprite;
                EmojiArray[i].name = EmojiSprite.name;
                EmojiArrayIdentity[i].name = EmojiSprite.name;

                MatchArray[i].GetComponent<SpriteRenderer>().sprite = MatchSprite;


                MatchArray[i].name = MatchSprite.name;
                MatchArrayIdentity[i].GetComponent<SpriteRenderer>().sprite = MatchSprite;

                MatchArrayIdentity[i].name = MatchSprite.name;
                EmojiArrayIdentity[emojiindex].Correct = MatchArrayIdentity[i];
                MatchArrayIdentity[i].Correct = EmojiArrayIdentity[emojiindex];




            }


        }
        positionlist = ShuffleList(positionlist);
        for (int i = 0; i < MatchArray.Length; i++)
        {
            if (i < positionlist.Count)
            {
                WhiteBoxMatch[i].transform.position = positionlist[i];

            }
            else
            {
                WhiteBoxMatch[i].SetActive(false);
            }

        }

    }
    //Mouse position Strt
    void updatelinepos(Vector3 startpos, Vector3 mousepos)
    {
        if (mode == true)
        {
            Vector3 Startpos = positionObject.transform.position;
            positionObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            mous = Vectorpos();
            positionObject.transform.position = new Vector3(mous.x, mous.y, Startpos.z);

        }
        else
        {
            startpos.z = mousepos.z = 0;
            Lineshadow.SetPosition(0, startpos);
            Lineshadow.SetPosition(1, mousepos);

        }
    }
    //Mouse position end

    public void settingfunc()
    {
        MainMenuScript.Instance.isplayed = false;
        shopbuttonicon.SetActive(false);
        hometab.SetActive(false);
        settingtab.SetActive(false);
        hinttab.SetActive(false);
        if (AskingTab.activeSelf)
        {
            AskingTab.SetActive(false);
        }
        MainMenuScript.Instance.SettingPanel.SetActive(true);
        MatchElement.SetActive(false);
        EmojiElement.SetActive(false);
        LevelLogo.SetActive(false);
        MainMenuScript.Instance.levelogo.SetActive(false);
        foreach (GameObject g1 in Lineobjectlist)
        {
            g1.SetActive(false);
        }
        settingbool = true;
    }
    public void HomeFunc()
    {
        //MainMenuScript.Instance.isplayed = false;
        shopbuttonicon.SetActive(false);
        hinttab.SetActive(false);
        hometab.SetActive(false);
        settingtab.SetActive(false);
        MatchElement.SetActive(false);
        EmojiElement.SetActive(false);
        //videobuttonicon.SetActive(true);
        LevelLogo.SetActive(false);
        foreach (GameObject g1 in Lineobjectlist)
        {
            g1.SetActive(false);
        }
        //MatchElement.SetActive(false);
        //EmojiElement.SetActive(false);
        AskingTab.SetActive(true);
    }
    public void yesfunc()
    {
        //MainMenuScript.Instance.isplayed = false;
        shopbuttonicon.SetActive(false);
        EmojiElement.SetActive(false);
        MatchElement.SetActive(false);
        MainMenuScript.Instance.PlaneObjectButton.SetActive(false);
        ResetFunction();
        hinttab.SetActive(false);
        MainMenuScript.Instance.levelogo.SetActive(false);
        MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[0];
        AskingTab.SetActive(false);
        hometab.SetActive(false);
        settingtab.SetActive(false);
        EmojiElement.SetActive(false);
        MatchElement.SetActive(false);
        MainMenuScript.Instance.MainMenu.SetActive(true);
    }
    public void nofunc()
    {
        //MainMenuScript.Instance.isplayed = true;
        shopbuttonicon.SetActive(true);
        EmojiElement.SetActive(true);
        MatchElement.SetActive(true);
        AskingTab.SetActive(false);
        LevelLogo.SetActive(true);
        hinttab.SetActive(true);
        MainMenuScript.Instance.levelogo.SetActive(true);
        foreach (GameObject g1 in Lineobjectlist)
        {
            g1.SetActive(true);
        }
    }
    // Sprite For Match Satrt
    public void GetSpriteFunc()
    {
        if (GetSprite.LevelList != null && GetSprite.LevelList.Count > 0)
        {


            levelCount = GetSprite.LevelList.Count;

            if (levelCount == 3)
            {

              
                Vector3 desired;
                float desiredpos = 1f;
               
                desired = posobject.transform.position = WhiteBoxEmoji[0].transform.position;
                desired.y = desiredpos;
              
                WhiteBoxEmoji[0].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[0].transform.position;
                desired.y = desiredpos;
               
                WhiteBoxMatch[0].transform.position = desired;
                positionlist.Add(WhiteBoxMatch[0].transform.position);
                desiredpos = -0.5f;
                desired = posobject.transform.position = WhiteBoxEmoji[1].transform.position;
                desired.y = desiredpos;
                
                WhiteBoxEmoji[1].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[1].transform.position;
                desired.y = desiredpos;
              
                WhiteBoxMatch[1].transform.position = desired;
                positionlist.Add(WhiteBoxMatch[1].transform.position);
                desiredpos = -2f;
                desired = posobject.transform.position = WhiteBoxEmoji[2].transform.position;
                desired.y = desiredpos;
               
                WhiteBoxEmoji[2].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[2].transform.position;
                desired.y = desiredpos;
               
                WhiteBoxMatch[2].transform.position = desired;
                positionlist.Add(WhiteBoxMatch[2].transform.position);
                for (int i = 0; i < WhiteBoxEmoji.Length; i++)
                {
                    WhiteBoxEmoji[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    WhiteBoxMatch[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }

            }
            if (levelCount == 4)
            {
                Vector3 desired;
                float desiredpos = 1f;
             
                desired = posobject.transform.position = WhiteBoxEmoji[0].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[0].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[0].transform.position;
                desired.y = desiredpos;
              
                
                WhiteBoxMatch[0].transform.position = desired;

                desiredpos = -0.5f;
                desired = posobject.transform.position = WhiteBoxEmoji[1].transform.position;
                desired.y = desiredpos;
               
                WhiteBoxEmoji[1].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[1].transform.position;
                desired.y = desiredpos;
             
                WhiteBoxMatch[1].transform.position = desired;

                desiredpos = -2f;
                desired = posobject.transform.position = WhiteBoxEmoji[2].transform.position;
                desired.y = desiredpos;
                
                WhiteBoxEmoji[2].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[2].transform.position;
                desired.y = desiredpos;
                
                WhiteBoxMatch[2].transform.position = desired;

                desiredpos = -3.5f;
                desired = posobject.transform.position = WhiteBoxEmoji[3].transform.position;
                desired.y = desiredpos;
               
                WhiteBoxEmoji[3].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[3].transform.position;
                desired.y = desiredpos;
               
                WhiteBoxMatch[3].transform.position = desired;
                positionlist.Add(WhiteBoxMatch[0].transform.position);
                positionlist.Add(WhiteBoxMatch[1].transform.position);
                positionlist.Add(WhiteBoxMatch[2].transform.position);
                positionlist.Add(WhiteBoxMatch[3].transform.position);
                for (int i = 0; i < WhiteBoxEmoji.Length; i++)
                {
                    WhiteBoxEmoji[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    WhiteBoxMatch[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
            }
            if (levelCount == 5)
            {
                //for (int i = 0; i < 5; i++)
                //{
                //    WhiteBoxEmoji[i].transform.position = previouspositionEmoji[i];
                //    WhiteBoxMatch[i].transform.position = previouspositionMatch[i];
                //    // positionlist.Add(WhiteBoxMatch[i].transform.position);
                //}
                for (int i = 0; i < WhiteBoxEmoji.Length; i++)
                {
                    WhiteBoxEmoji[i].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    WhiteBoxMatch[i].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                }
                Vector3 desired;
                float desiredpos = 1f;
               
                desired = posobject.transform.position = WhiteBoxEmoji[0].transform.position;
                desired.y = desiredpos;
               
                WhiteBoxEmoji[0].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[0].transform.position;
                desired.y = desiredpos;
               
                WhiteBoxMatch[0].transform.position = desired;

                desiredpos = 0f;
                desired = posobject.transform.position = WhiteBoxEmoji[1].transform.position;
                desired.y = desiredpos;
               
                WhiteBoxEmoji[1].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[1].transform.position;
                desired.y = desiredpos;
               
                WhiteBoxMatch[1].transform.position = desired;

                desiredpos = -1f;
                desired = posobject.transform.position = WhiteBoxEmoji[2].transform.position;
                desired.y = desiredpos;
              
                WhiteBoxEmoji[2].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[2].transform.position;
                desired.y = desiredpos;
                
                WhiteBoxMatch[2].transform.position = desired;

                desiredpos = -2f;
                desired = posobject.transform.position = WhiteBoxEmoji[3].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[3].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[3].transform.position;
                desired.y = desiredpos;
                WhiteBoxMatch[3].transform.position = desired;

                desiredpos = -3f;
                desired = posobject.transform.position = WhiteBoxEmoji[4].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[4].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[4].transform.position;
                desired.y = desiredpos;
               
                WhiteBoxMatch[4].transform.position = desired;


                positionlist.Add(WhiteBoxMatch[0].transform.position);
                positionlist.Add(WhiteBoxMatch[1].transform.position);
                positionlist.Add(WhiteBoxMatch[2].transform.position);
                positionlist.Add(WhiteBoxMatch[3].transform.position);
                positionlist.Add(WhiteBoxMatch[4].transform.position);






            }


            for (int i = 0; i < EmojiArray.Length; i++)
            {



                if (i < levelCount)
                {

                    EmojiSprite = GetSprite.LevelList[i].Item1;
                    MatchSprite = GetSprite.LevelList[i].Item2;
                    EmojiArray[i].SetActive(true);
                    MatchArray[i].SetActive(true);
                    EmojiArrayIdentity[elementindex].enabled = true;
                    MatchArrayIdentity[matchindex].enabled = true;
                    EmojiArray[i].transform.localScale = new Vector3(1f, 1f, 1f);
                    EmojiArray[i].AddComponent<AudioSource>();
                    EmojiArray[i].GetComponent<AudioSource>().playOnAwake = false;
                    EmojiArrayIdentity[i].transform.localScale = new Vector3(1f, 1f, 1f);
                    EmojiArrayIdentity[i].GetComponent<SpriteRenderer>().sprite = EmojiSprite;
                    EmojiArray[i].GetComponent<SpriteRenderer>().sprite = EmojiSprite;
                    WhiteBoxEmoji[i].SetActive(true);
                    //  WhiteBoxEmoji[i].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                    WhiteBoxEmoji[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    EmojiLayer[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 255);
                    EmojiLayer[i].SetActive(true);

                    EmojiArray[i].name = EmojiSprite.name;
                    EmojiArrayIdentity[i].name = EmojiSprite.name;




                    MatchArrayIdentity[i].GetComponent<SpriteRenderer>().sprite = MatchSprite;
                    MatchArrayIdentity[i].GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
                    MatchArray[i].GetComponent<SpriteRenderer>().sprite = MatchSprite;
                    MatchArray[i].GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;

                    MatchArray[i].transform.localScale = new Vector3(1f, 1f, 1f);
                    MatchArray[i].AddComponent<AudioSource>();
                    MatchArray[i].GetComponent<AudioSource>().playOnAwake = false;
                    MatchArrayIdentity[i].transform.localScale = new Vector3(1f, 1, 1f);

                    MatchArray[i].name = MatchSprite.name;
                    MatchArrayIdentity[i].name = MatchSprite.name;

                    WhiteBoxMatch[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    WhiteBoxMatch[i].SetActive(true);
                    //WhiteBoxMatch[i] .transform.localScale= new Vector3(0.4f, 0.4f, 0.4f);

                    MatchLayer[i].SetActive(true);
                    MatchLayer[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 255);

                    layermaskmode[i].SetActive(false);
                    EmojiArrayIdentity[i].Correct = MatchArrayIdentity[i];
                    MatchArrayIdentity[i].Correct = EmojiArrayIdentity[i];
                    EmojiArrayIdentity[i].Current = null;
                    MatchArrayIdentity[i].Current = null;

                }
                else
                {

                    EmojiArray[i].gameObject.SetActive(false);
                    EmojiArrayIdentity[i].gameObject.SetActive(false);
                    WhiteBoxEmoji[i].SetActive(false);
                    WhiteBoxMatch[i].SetActive(false);
                    EmojiLayer[i].SetActive(false);
                    MatchLayer[i].SetActive(false);
                    MatchArray[i].SetActive(false);
                    MatchArrayIdentity[i].gameObject.SetActive(false);
                }
            }
        }

        positionlist = ShuffleList(positionlist);
        for (int i = 0; i < MatchArray.Length; i++)
        {
            if (i < positionlist.Count)
            {
                WhiteBoxMatch[i].transform.position = positionlist[i];

            }
            else
            {
                WhiteBoxMatch[i].SetActive(false);
            }

        }

    }
    List<Vector3> ShuffleList(List<Vector3> positionList)
    {
        int randomIndex;
        int n = positionList.Count;
        System.Random rng = new System.Random();
        List<Vector3> originalList = new List<Vector3>(positionList);

        do
        {
            for (int i = 0; i < positionList.Count; i++)
            {
                randomIndex = rng.Next(i, n);
                Vector3 temp = positionList[randomIndex];
                positionList[randomIndex] = positionList[i];
                positionList[i] = temp;
            }
        } while (IsSameList(originalList, positionList));

        return positionList;
    }

    bool IsSameList(List<Vector3> list1, List<Vector3> list2)
    {
        if (list1.Count != list2.Count)
            return false;

        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i])
                return false;
        }

        return true;
    }



    //Sprite For MatchEnd
    //Sprite For Drag Start
    public void GetEmoji()
    {

        if (GetSprite.EmojiLevelList != null && GetSprite.EmojiLevelList.Count > 0)
        {

            levelCount = GetSprite.EmojiLevelList.Count;


            if (levelCount == 3)
            {

                Vector3 desired;
                float desiredpos = 1f;
                desired = posobject.transform.position = WhiteBoxEmoji[0].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[0].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[0].transform.position;
                desired.y = desiredpos;
                WhiteBoxMatch[0].transform.position = desired;
                positionlist.Add(WhiteBoxMatch[0].transform.position);
                desiredpos = -0.5f;
                desired = posobject.transform.position = WhiteBoxEmoji[1].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[1].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[1].transform.position;
                desired.y = desiredpos;
                WhiteBoxMatch[1].transform.position = desired;
                positionlist.Add(WhiteBoxMatch[1].transform.position);
                desiredpos = -2f;
                desired = posobject.transform.position = WhiteBoxEmoji[2].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[2].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[2].transform.position;
                desired.y = desiredpos;
                WhiteBoxMatch[2].transform.position = desired;
                positionlist.Add(WhiteBoxMatch[2].transform.position);
                for (int i = 0; i < EmojiArray.Length; i++)
                {
                    WhiteBoxEmoji[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    WhiteBoxMatch[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }

            }
            if (levelCount == 4)
            {
                Vector3 desired;
                float desiredpos = 1f;
                desired = posobject.transform.position = WhiteBoxEmoji[0].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[0].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[0].transform.position;
                desired.y = desiredpos;
                WhiteBoxMatch[0].transform.position = desired;

                desiredpos = -0.5f;
                desired = posobject.transform.position = WhiteBoxEmoji[1].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[1].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[1].transform.position;
                desired.y = desiredpos;
                WhiteBoxMatch[1].transform.position = desired;

                desiredpos = -2f;
                desired = posobject.transform.position = WhiteBoxEmoji[2].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[2].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[2].transform.position;
                desired.y = desiredpos;
                WhiteBoxMatch[2].transform.position = desired;

                desiredpos = -3.5f;
                desired = posobject.transform.position = WhiteBoxEmoji[3].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[3].transform.position = desired;
                desired = posobject.transform.position = WhiteBoxMatch[3].transform.position;
                desired.y = desiredpos;
                WhiteBoxMatch[3].transform.position = desired;
                positionlist.Add(WhiteBoxMatch[0].transform.position);
                positionlist.Add(WhiteBoxMatch[1].transform.position);
                positionlist.Add(WhiteBoxMatch[2].transform.position);
                positionlist.Add(WhiteBoxMatch[3].transform.position);
                for (int i = 0; i < EmojiArray.Length; i++)
                {
                    WhiteBoxEmoji[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    WhiteBoxMatch[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }

            }
            if (levelCount == 5)
            {
                Vector3 desired;
                for (int i = 0; i < 5; i++)
                {
                    WhiteBoxEmoji[i].transform.position = previouspositionEmoji[i];
                    WhiteBoxMatch[i].transform.position = previouspositionMatch[i];
                    positionlist.Add(WhiteBoxMatch[i].transform.position);
                }
                for (int i = 0; i < WhiteBoxEmoji.Length; i++)
                {
                    WhiteBoxEmoji[i].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    WhiteBoxMatch[i].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                }
                float desiredpos = 2f;
                desired = posobject.transform.position = WhiteBoxEmoji[0].transform.position;
                desired.y = desiredpos;
                WhiteBoxEmoji[0].transform.position = desired;

            }
            for (int i = 0; i < EmojiArray.Length; i++)
            {
                if (i < levelCount)
                {
                    Sprite EmojiSprite = GetSprite.EmojiLevelList[i].Item1;
                    Sprite MatchSprite = GetSprite.EmojiLevelList[i].Item2;

                    EmojiArray[i].transform.localScale = Vector3.one;
                    EmojiArray[i].SetActive(true);
                    MatchArray[i].SetActive(true);
                    EmojiArrayIdentity[i].enabled = true;
                    MatchArrayIdentity[i].enabled = true;
                    EmojiArray[i].AddComponent<AudioSource>();
                    EmojiArray[i].GetComponent<AudioSource>().playOnAwake = false;
                    MatchArray[i].AddComponent<AudioSource>();
                    MatchArray[i].GetComponent<AudioSource>().playOnAwake = false;
                    EmojiArrayIdentity[i].transform.localScale = Vector3.one;

                    WhiteBoxEmoji[i].SetActive(true);
                    WhiteBoxEmoji[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    WhiteBoxMatch[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    WhiteBoxMatch[i].SetActive(true);
                    MatchLayer[i].SetActive(true);
                    MatchArray[i].GetComponent<SpriteRenderer>().sortingOrder = 0;
                    MatchLayer[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    EmojiLayer[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    EmojiLayer[i].SetActive(true);

                    EmojiArray[i].GetComponent<SpriteRenderer>().sprite = EmojiSprite;
                    EmojiArrayIdentity[i].GetComponent<SpriteRenderer>().sprite = EmojiSprite;

                    EmojiArray[i].name = EmojiSprite.name;
                    EmojiArrayIdentity[i].name = EmojiSprite.name;

                    MatchArray[i].GetComponent<SpriteRenderer>().sprite = MatchSprite;





                    layermaskmode[i].SetActive(true);

                    layermaskmode[i].GetComponent<SpriteRenderer>().sprite = MatchSprite;

                    MatchArrayIdentity[i].GetComponent<SpriteRenderer>().sprite = MatchSprite;
                    MatchArray[i].transform.localScale = Vector3.one;
                    MatchArrayIdentity[i].transform.localScale = Vector3.one;

                    MatchLayer[i].SetActive(true);

                    MatchArray[i].GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;

                    MatchArray[i].name = MatchSprite.name;
                    MatchArrayIdentity[i].name = MatchSprite.name;

                    EmojiArrayIdentity[i].Correct = MatchArrayIdentity[i];
                    MatchArrayIdentity[i].Correct = EmojiArrayIdentity[i];
                    EmojiArrayIdentity[i].Current = null;
                    MatchArrayIdentity[i].Current = null;
                }
                else
                {

                    EmojiArray[i].gameObject.SetActive(false);
                    EmojiArrayIdentity[i].gameObject.SetActive(false);
                    WhiteBoxEmoji[i].SetActive(false);
                    WhiteBoxMatch[i].SetActive(false);
                    EmojiLayer[i].SetActive(false);
                    MatchLayer[i].SetActive(false);
                    MatchArrayIdentity[i].gameObject.SetActive(false);
                }


            }




        }
        positionlist = ShuffleList(positionlist);
        for (int i = 0; i < MatchArray.Length; i++)
        {
            if (i < positionlist.Count)
            {
                WhiteBoxMatch[i].transform.position = positionlist[i];

            }
            else
            {
                WhiteBoxMatch[i].SetActive(false);
            }

        }
    }
    //Sprite For Drag End
    // Onclick Start
    void OnLeftClick(Vector3 startpos)
    {

        Startpos.z = 0;


        for (int i = 0; i < linelist.Count; i++)
        {
            Debug.Log("15230S");
            LineRenderer line = linelist[i];
            if (Startpos == linelist[i].GetPosition(0) || Startpos == linelist[i].GetPosition(1))
            {
                GameObject g1 = linelist[i].gameObject;
                for (int j = 0; j < EmojiArrayIdentity.Length; j++)
                {
                    if (EmojiArrayIdentity[j].linegameobject == g1)
                    {
                        Debug.Log("15230");
                        EmojiArrayIdentity[j].outline.color = Color.yellow;
                        EmojiArrayIdentity[j].Current = null;

                        Dropped.outline.color = Color.yellow;

                    }
                    if (MatchArrayIdentity[j].linegameobject == g1)
                    {
                        Debug.Log("1S5230");
                        MatchArrayIdentity[j].outline.color = Color.yellow;
                        MatchArrayIdentity[j].Current = null;
                        Dropped.Current = null;
                       Dropped.outline.color = Color.yellow;
                        break;
                    }
                }
                if (StartObject.GetComponent<BoxCollider>().enabled == true)
                {
                    linelist[i].enabled = false;
                    linelist.Remove(linelist[i]);

                    i--;
                    isgenerated = true;
                    Selected.Current = null;
                    Selected.outline.color = Color.yellow;


                }
            }



        }
    }
    //OnclickEnd
    // Satrt Collide Object & Position

    GameObject RayCastFuc()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.enabled)
        {
            return hit.collider.gameObject;
        }
        return null;

    }
    Vector3 RayCastHitFunc()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.enabled)
        {
            return hit.point;
        }
        return Vector3.zero;

    }
    // end
    //Vector Start
    Vector3 Vectorpos()
    {
        Vector3 mousepos = Input.mousePosition;
        mousepos.z = Camera.main.transform.position.y - StartObject.transform.position.y;
        return Camera.main.ScreenToWorldPoint(mousepos);

    }
    //Vector End
    // nextStart
    public void nextReload()
    {

        Crownobject.SetActive(false);
        WiningPopup.SetActive(false);
        //MainMenuScript.Instance.unlock();
        // MainMenuScript.Instance.ButtonArray[MainMenuScript.Instance.currentlevel++].interactable = true;
        // MainMenuScript.Instance.ButtonArray[MainMenuScript.Instance.currentlevel++].interactable = true;
        MainMenuScript.Instance.Unlock();
        if (MainMenuScript.Instance.islevelfunc == true)
        {


            if (MainMenuScript.Instance.currentlevel == 0)
            {


                // MainMenuScript.Instance.currentlevel += 1;
                //  PlayerPrefs.SetInt("Currentlevel", MainMenuScript.Instance.currentlevel);
                // PlayerPrefs.Save();

                MainMenuScript.Instance.islevelfunc = false;
                mode = false;
            }
            else
            {

                MainMenuScript.Instance.islevelfunc = true;
                /* MainMenuScript.Instance.currentlevel = */

                //MainMenuScript.Instance.currentlevel = MainMenuScript.Instance.currentlevel + 1;

                // PlayerPrefs.SetInt("Currentlevel", MainMenuScript.Instance.currentlevel);
                //  MainMenuScript.Instance.Unlock();

                PlayerPrefs.Save();
                mode = false;

            }

        }
        else
        {

            // MainMenuScript.Instance.currentlevel += 1;
            //PlayerPrefs.SetInt("Currentlevel", MainMenuScript.Instance.currentlevel);
            //mode = false;
            // PlayerPrefs.Save();
        }

        for (int i = 0; i < planeobjectarray.Length; i++)
        {

            if (PlayerPrefs.HasKey("Iscoinless" + i))
            {

                index = PlayerPrefs.GetInt("Iscoinless" + i, 0);
                if (index == 0)
                {

                    MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[2];

                }
                else if (index == 1)
                {
                    MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[3];

                }
                else if (index == 2)
                {
                    MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[4];

                }
                else if (index == 3)
                {
                    MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[5];

                }
                else if (index == 4)
                {
                    MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[6];

                }
                else if (index == 5)
                {

                    MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[1];
                }
                break;
            }
            else
            {


                MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[1];

            }

        }




        shopbuttonicon.SetActive(true);
    }
    public void closehintbar()
    {
        MatchElement.SetActive(true);
        EmojiElement.SetActive(true);
        foreach (GameObject ob in Lineobjectlist)
        {
            ob.SetActive(true);
        }
        videoobject.SetActive(false);

    }
    public void Shop()
    {
        MainMenuScript.Instance.levelogo.SetActive(false);
        LevelLogo.SetActive(false);
        planeobject.SetActive(true);
        MatchElement.SetActive(false);
        EmojiElement.SetActive(false);
        hinttab.SetActive(false);
        hometab.SetActive(false);
        settingtab.SetActive(false);

        shopbuttonicon.SetActive(false);
        if (Lineobjectlist != null)
        {
            foreach (GameObject g1 in Lineobjectlist)
            {
                g1.SetActive(false);
            }
        }

    }


    public void closeShop()
    {
        MainMenuScript.Instance.levelogo.SetActive(true);
        LevelLogo.SetActive(true);
        hinttab.SetActive(true);
        hometab.SetActive(true);
        settingtab.SetActive(true);
        shopbuttonicon.SetActive(true);
        planeobject.SetActive(false);
        MatchElement.SetActive(true);
        EmojiElement.SetActive(true);
        if (Lineobjectlist != null)
        {
            foreach (GameObject g1 in Lineobjectlist)
            {
                g1.SetActive(true);

            }
        }
    }
    public void Progresshint()
    {
        {
            counterhint = PlayerPrefs.GetInt("counthint");
            if (counterhint >= 3)
            {
                if (PlayerPrefs.HasKey("Currency"))
                {
                    if (PlayerPrefs.HasKey("ShowCounter") && PlayerPrefs.GetInt("ShowCounter", 0) >= 1)
                    {
                       





                            for (int i = 0; i < levelCount; i++)
                            {

                                if (EmojiArrayIdentity[i].Current == null || EmojiArrayIdentity[i].Current != EmojiArrayIdentity[i].Correct)
                                {

                                    Alphaobject = EmojiArray[i];

                                }
                                if (MatchArrayIdentity[i].Current == null || MatchArrayIdentity[i].Current != MatchArrayIdentity[i].Correct)
                                {
                                    betaobject = MatchArray[i];

                                    break;
                                }

                            }
                            if (Alphaobject != null && betaobject != null)
                            {


                                Alphaobject.transform.DOShakeRotation(duration: 1f, strength: 50f, vibrato: 50, randomness: 50f, fadeOut: true);
                                betaobject.transform.DOShakeRotation(duration: 1f, strength: 50f, vibrato: 50, randomness: 50f, fadeOut: true);

                            }


                            showcounter--;

                            counterbuyhint.text = showcounter.ToString();
                            PlayerPrefs.SetInt("ShowCounter", showcounter);





                    }
                    else
                    {
                        AskingDimondPanel.SetActive(true);
                        AskingDimondPanel.transform.DOScale(1, 1f).OnComplete(()=>
                        {
                            dimondbutton.transform.DOMove(buydimondbutton.transform.position, 1f);

                            });

                            RequiredDiomondtext.text = "5";
                        counterbuyhint.text = showcounter.ToString();
                        showtextinpanel.text = showcounter.ToString();
                        MatchElement.SetActive(false);
                        EmojiElement.SetActive(false);
                        if (Lineobjectlist != null)
                        {
                            foreach (GameObject g1 in Lineobjectlist)
                            {
                                g1.SetActive(false);
                            }
                        }
                    }
                }

            }
            else
            {
                counterhint = PlayerPrefs.GetInt("counthint");
                counterbuyhint.text = showcounter.ToString();
                if (counterhint <= 3)
                {
                    Alphaobject = betaobject = null;
                    for (int i = 0; i < levelCount; i++)
                    {
                        if (EmojiArrayIdentity[i].Current == null || EmojiArrayIdentity[i].Current != EmojiArrayIdentity[i].Correct)
                        {
                            Alphaobject = EmojiArray[i];

                        }
                        if (MatchArrayIdentity[i].Current == null || MatchArrayIdentity[i].Current != MatchArrayIdentity[i].Correct)
                        {
                            betaobject = MatchArray[i];
                            break;
                        }
                    }
                    if (Alphaobject != null && betaobject != null)
                    {
                        Alphaobject.transform.DOShakeRotation(duration: 1f, strength: 30f, vibrato: 30, randomness: 30f, fadeOut: true);
                        betaobject.transform.DOShakeRotation(duration: 1f, strength: 30f, vibrato: 30, randomness: 30f, fadeOut: true);
                        counterhint++;
                        PlayerPrefs.SetInt("counthint", counterhint);
                    }
                }
                else
                {
                    videoobject.SetActive(true);
                    videoText.text = "Watch video";
                    counterhint++;
                    PlayerPrefs.SetInt("counthint", counterhint);
                }
                if (showcounter == 0)
                {
                    showcounter = 0;
                    PlayerPrefs.SetInt("ShowCounter", showcounter);
                }
                else
                {
                    showcounter = showcounter - 1;
                    PlayerPrefs.SetInt("ShowCounter", showcounter);
                    counterbuyhint.text = showcounter.ToString();
                }
            }
            Alphaobject = null;
            betaobject = null;


        }
    }

    public void buydiomond()
    {

        if (PlayerPrefs.HasKey("ShowCounter"))
        {
            if (PlayerPrefs.HasKey("Currency")  && PlayerPrefs.GetInt("Currency", 0)>=5)
            {
                if (PlayerPrefs.GetInt("ShowCounter") >= 1)
                {

                    showcounter += 1;
                    counterbuyhint.text = showcounter.ToString();
                    showtextinpanel.text = showcounter.ToString();
                    PlayerPrefs.SetInt("ShowCounter", showcounter);
                    int value;
                    value = PlayerPrefs.GetInt("Currency");
                    value = value - 5;
                    PlayerPrefs.SetInt("Currency", value);
                    currecyrepresenterbar.text = PlayerPrefs.GetInt("Currency", 0).ToString();
                }
                  else if (PlayerPrefs.GetInt("ShowCounter") == 0){
                    showcounter = 1;
                    counterbuyhint.text = showcounter.ToString();
                    showtextinpanel.text = showcounter.ToString();
                    PlayerPrefs.SetInt("ShowCounter", showcounter);
                    int value;
                    value = PlayerPrefs.GetInt("Currency");
                    value = value - 5;
                    PlayerPrefs.SetInt("Currency", value);
                    currecyrepresenterbar.text = PlayerPrefs.GetInt("Currency", 0).ToString();
                }
               

            }
            else
            {
                NotEnoughGems.SetActive(true);

                AskingDimondPanel.SetActive(false);
                AskingDimondPanel.transform.localScale = new Vector3(0, 0, 0);
                dimondbutton.transform.position = refposdimondbutton;
                //showcounter = 1;
                //counterbuyhint.text = showcounter.ToString();
                //PlayerPrefs.SetInt("ShowCounter", showcounter);
            }
        }

    }
    public void closedimondpanel()
    {
        AskingDimondPanel.SetActive(false);
        AskingDimondPanel.transform.localScale = new Vector3(0, 0, 0);
        dimondbutton.transform.position = refposdimondbutton;
        //showcounter = 1;
        MatchElement.SetActive(true);
        EmojiElement.SetActive(true);
        if (Lineobjectlist != null)
        {
            foreach (GameObject g1 in Lineobjectlist)
            {
                g1.SetActive(true);
            }
        }
    }
   
    public void progressbar(float currentvalue)
    {


        progresscounter = PlayerPrefs.GetInt("ProgressCounter");
        percentagevalue = currentvalue;//(((min - currentvalue) / (max - min)) * 100) / 10;

        float value = percentagevalue;

        if (PlayerPrefs.HasKey("percentage"))
        {
            percentagevalue = PlayerPrefs.GetFloat("percentage");
            percentagevalue = value + percentagevalue;

            if (percentagevalue >= 100)
            {

                percentagevalue = value;
                PlayerPrefs.SetFloat("percentage", percentagevalue);
                Levelname.text = IqProgreesbar.instance.levelnamearray[progresscounter + 1];
                ImageSlider.GetComponent<Image>().sprite = spriteforprogressbar[progresscounter];

                progresscounter++;
                PlayerPrefs.SetInt("ProgressCounter", progresscounter);

            }
            else
            {
                if (isreload)
                {

                    Levelname.text = IqProgreesbar.instance.levelnamearray[progresscounter];
                    ImageSlider.GetComponent<Image>().sprite = spriteforprogressbar[progresscounter];

                    PlayerPrefs.SetFloat("percentage", percentagevalue);
                }
                else
                {

                    Levelname.text = IqProgreesbar.instance.levelnamearray[progresscounter];
                    ImageSlider.GetComponent<Image>().sprite = spriteforprogressbar[progresscounter];

                    PlayerPrefs.SetFloat("percentage", percentagevalue);
                }


            }
            TextintoString.text = "";
            TextintoString.DOText(percentagevalue.ToString() + "%", 1f, true, ScrambleMode.All);
            TextintoString.text = percentagevalue.ToString() + "%";

            slideriqabar.fillAmount = percentagevalue;
           // slideriqabar.interactable = false;
        }
        else
        {
            if (isreload)
            {
                percentagevalue = value + percentagevalue;
                PlayerPrefs.SetFloat("percentage", percentagevalue);
                Levelname.text = IqProgreesbar.instance.levelnamearray[progresscounter];
                ImageSlider.GetComponent<Image>().sprite = spriteforprogressbar[progresscounter];


            }
            else
            {
                percentagevalue = value + percentagevalue;
                PlayerPrefs.SetFloat("percentage", percentagevalue);
                Levelname.text = IqProgreesbar.instance.levelnamearray[progresscounter];
                ImageSlider.GetComponent<Image>().sprite = spriteforprogressbar[progresscounter];


            }
            TextintoString.text = "";
            TextintoString.DOText(percentagevalue.ToString() + "%", 1f, true, ScrambleMode.All);
            TextintoString.text = percentagevalue.ToString() + "%";
            slideriqabar.fillAmount = percentagevalue;
        }
        PlayerPrefs.SetInt("ProgressCounter", progresscounter);
    }

    public void NextFunc()
    {
        WiningPopup.transform.localScale = new Vector3(0, 0, 0);
        nextbutton.SetActive(false);
        // MainMenuScript.Instance.value += 1;
        isreload = false;
        //MainMenuScript.Instance.Unlock();
        //MainMenuScript.Instance.isplayed = true;
        iqobject.SetActive(false);
        //MainMenuScript.Instance.currentlevel= MainMenuScript.Instance.currentlevel+1;
        hinttab.SetActive(true);
        ResetFunction();
        nextReload();
        LevelLogo.SetActive(true);
        LineManager.instance.hometab.SetActive(true);
        LineManager.instance.settingtab.SetActive(true);
        if (MainMenuScript.Instance.islevelfunc)
        {
            MainMenuScript.Instance.temp2 = MainMenuScript.Instance.value + 1;
            textMesh.text = "Level " + MainMenuScript.Instance.temp2;
        }
        else
        {
            MainMenuScript.Instance.temp2 = MainMenuScript.Instance.currentlevel + 1;
            textMesh.text = "Level " + MainMenuScript.Instance.temp2;
        }
        //MainMenuScript.Instance.temp2.ToString();
        if (MainMenuScript.Instance.islevelfunc)
        {

            mode = MainMenuScript.Instance.mode = MainMenuScript.Instance.UpdateMode(MainMenuScript.Instance.value);
        }
        else
        {

            mode = MainMenuScript.Instance.mode = MainMenuScript.Instance.UpdateMode(MainMenuScript.Instance.currentlevel);
        }
        if (mode)
        {
            if (MainMenuScript.Instance.islevelfunc)
            {
                LevelMakngScript.instance.GenerateEmojiLevel(MainMenuScript.Instance.value);
                GetEmoji();
            }
            else
            {

                LevelMakngScript.instance.GenerateEmojiLevel(MainMenuScript.Instance.currentlevel);
                GetEmoji();
            }
        }
        else
        {

            if (MainMenuScript.Instance.islevelfunc == true)
            {


                // MainMenuScript.Instance.value = MainMenuScript.Instance.value + 1;



                MainMenuScript.Instance.mode = mode = false;
                if (MainMenuScript.Instance.value >= 30)
                {
                    int randomvalue = Random.Range(0, 29);
                    LevelMakngScript.instance.GenerateLevel(randomvalue);
                    GetSpriteFunc();
                    MainMenuScript.Instance.copyLevelList = GetSprite.LevelList;
                }
                else
                {
                    LevelMakngScript.instance.GenerateLevel(MainMenuScript.Instance.value);
                    GetSpriteFunc();
                }
                MainMenuScript.Instance.copyLevelList = GetSprite.LevelList;
                MainMenuScript.Instance.islevelfunc = true;
                //PlayerPrefs.SetInt("Score", ScoreScript.Instance.scorevalue);

                //PlayerPrefs.SetInt("Dimond", ScoreScript.Instance.Dimondvalue);

                //ScoreScript.Instance.Scoreupdate();
            }
            else
            {
                if (MainMenuScript.Instance.currentlevel >= 30)
                {
                    int randomvalue = Random.Range(0, 29);
                    LevelMakngScript.instance.GenerateLevel(MainMenuScript.Instance.currentlevel);
                    GetSpriteFunc();
                    MainMenuScript.Instance.copyLevelList = GetSprite.LevelList;
                }
                else
                {
                    LevelMakngScript.instance.GenerateLevel(MainMenuScript.Instance.currentlevel);
                    GetSpriteFunc();
                    MainMenuScript.Instance.copyLevelList = GetSprite.LevelList;
                }
                //PlayerPrefs.SetInt("Score", ScoreScript.Instance.scorevalue);
                //PlayerPrefs.SetInt("Dimond", ScoreScript.Instance.Dimondvalue);
                //ScoreScript.Instance.Update();
            }
            LineManager.instance.hometab.SetActive(true);
            LineManager.instance.settingtab.SetActive(true);

            textMesh.text = "Level " + MainMenuScript.Instance.temp2.ToString();
        }

        for (int i = 0; i < 15; i++)
        {
            int butoncount = PlayerPrefs.GetInt("Currentlevel", 0);
            if (i <= butoncount)
            {
                MainMenuScript.Instance.ButtonArray[i].interactable = true;
            }
            else
            {
                MainMenuScript.Instance.ButtonArray[i].interactable = false;
            }
        }
    }

    public void buybutton(int indexbackkground)
    {

        index = indexbackkground;
        int score = PlayerPrefs.GetInt("Currency");



        if (index == 0)
        {

            if (PlayerPrefs.HasKey("isunlock" + index))
            {

                MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[2];

            }

            else
            {
                if (score >= 50)
                {
                    score = score - 50;

                    ScoreValue = score;
                  
                    PlayerPrefs.SetInt("Currency", ScoreValue);
                    currecyrepresenterbar.text = PlayerPrefs.GetInt("Currency", 0).ToString();
                    MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[2];
                    isunlock = true;
                    PlayerPrefs.SetInt("isunlock" + index, 1);
                    //ScoreScript.Instance.Scoreupdate();
                    PlayerPrefs.SetInt("Iscoinless" + index, index);
                   
                }
                else
                {
                    NotEnoughGems.SetActive(true);
                    MatchElement.SetActive(false);
                    EmojiElement.SetActive(false);
                    foreach (GameObject g1 in Lineobjectlist)
                    {
                        g1.SetActive(false);
                    }
                }
            }

        }
        if (index == 1)
        {
            if (PlayerPrefs.HasKey("isunlock" + index))
            {

                MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[3];

            }

            else
            {
                if (score >= 70)
                {
                    score = score - 70;

                    ScoreValue = score;

                    PlayerPrefs.SetInt("Currency", ScoreValue);
                    currecyrepresenterbar.text = PlayerPrefs.GetInt("Currency", 0).ToString();
                    MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[3];
                    PlayerPrefs.SetInt("Iscoinless" + index, index);
                   
                    
                }
                else
                {
                    NotEnoughGems.SetActive(true);
                    MatchElement.SetActive(false);
                    EmojiElement.SetActive(false);
                    foreach (GameObject g1 in Lineobjectlist)
                    {
                        g1.SetActive(false);
                    }
                }

            }
        }
        if (index == 2)
        {
            if (PlayerPrefs.HasKey("isunlock" + index))
            {

                MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[4];

            }
            else
            {
                if (score >= 100)
                {
                    score = score - 100;
                    ScoreValue = score;

                    PlayerPrefs.SetInt("Currency", ScoreValue);
                    currecyrepresenterbar.text = PlayerPrefs.GetInt("Currency", 0).ToString();
                    PlayerPrefs.SetInt("Score", ScoreScript.Instance.scorevalue);
                    MainMenuScript.Instance.levelogo.SetActive(true);
                   
                    MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[4];
                    PlayerPrefs.SetInt("Iscoinless" + index, index);
                }
                else
                {
                    NotEnoughGems.SetActive(true);
                    MatchElement.SetActive(false);
                    EmojiElement.SetActive(false);
                    foreach (GameObject g1 in Lineobjectlist)
                    {
                        g1.SetActive(false);
                    }
                }
            }

        }
        if (index == 3)
        {
            if (PlayerPrefs.HasKey("isunlock" + index))
            {

                MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[5];

            }
            else
            {
                if (score >= 125)
                {
                    score = score - 125;
                    ScoreValue = score;

                    PlayerPrefs.SetInt("Currency", ScoreValue);
                    currecyrepresenterbar.text = PlayerPrefs.GetInt("Currency", 0).ToString();
                    PlayerPrefs.SetInt("Score", ScoreScript.Instance.scorevalue);
                    MainMenuScript.Instance.levelogo.SetActive(true);
                    LevelLogo.SetActive(true);
                    MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[5];
                    PlayerPrefs.SetInt("Iscoinless" + index, index);
                }
                else
                {
                    NotEnoughGems.SetActive(true);
                    MatchElement.SetActive(false);
                    EmojiElement.SetActive(false);
                    foreach (GameObject g1 in Lineobjectlist)
                    {
                        g1.SetActive(false);
                    }
                }
            }
        }
            if (index == 4)
            {
                if (PlayerPrefs.HasKey("isunlock" + index))
                {

                    MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[6];

                }
                else
                {
                    if (score >= 120)
                    {
                        score = score - 120;

                    ScoreValue = score;

                    PlayerPrefs.SetInt("Currency", ScoreValue);
                    currecyrepresenterbar.text = PlayerPrefs.GetInt("Currency", 0).ToString();
                    
                    MainMenuScript.Instance.levelogo.SetActive(true);
                 
                    MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[6];
                   

                    PlayerPrefs.SetInt("Iscoinless" + index, index);
                    }
                    else
                    {
                        NotEnoughGems.SetActive(true);
                        MatchElement.SetActive(false);
                        EmojiElement.SetActive(false);
                        foreach (GameObject g1 in Lineobjectlist)
                        {
                            g1.SetActive(false);
                        }
                    }
                }
            }
                if (index == 5)
                {

                    MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[1];
                   PlayerPrefs.SetInt("Iscoinless" + index, index);

                }




        iscoinless = true;
             

            }
    public void closenotenoughgems()
            {
                planeobject.SetActive(false);
                MatchElement.SetActive(true);
                NotEnoughGems.SetActive(false);
                 MainMenuScript.Instance.levelogo.SetActive(true);
                LevelLogo.SetActive(true);
                EmojiElement.SetActive(true);
                hinttab.SetActive(true);
                hometab.SetActive(true);
                settingtab.SetActive(true);
                shopbuttonicon.SetActive(true);
                if (Lineobjectlist != null)
                {
                    foreach (GameObject g1 in Lineobjectlist)
                    {
                        g1.SetActive(true);

                    }
                }
            }
            //next end
}
     
