using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using TMPro;
public class MainMenuScript : MonoBehaviour
{
    public GameObject addprefreb;
     TextMeshProUGUI LevelNameText;
    public GameObject prefrebobject;// Start is called before the first frame update
    public ScrollRect scrollview;
    public Sprite[] BackgroundArray;
  public Slider vibrateSlide;
    public GameObject NonViberation;
    public GameObject PlaneObjectButton;
    [SerializeField] public GameObject Background, levelogo;
    [SerializeField]public  GameObject SettingPanel;
    [SerializeField] public GameObject MainMenu;  
    LineManager manager;
    public bool isplayed = false;public  bool islevelfunc = false;
  public   int count;
    int temp;
    public GameObject[] RateStartArray;
    public float currenttime;
    [SerializeField] public GameObject LevelMenu;
    public Button[] ButtonArray;
    public int currentlevel;
   public bool mode;
    public GameObject viberateicon;
    public int value;
    public bool vibrate=false;
    Time time;
    public int temp2;
    public int givenvalue;
    LevelMakngScript GetList;
    public List<(Sprite, Sprite)> copyLevelList = new List<(Sprite, Sprite)>();
    float volume;
    [SerializeField] GameObject CrownTAB, DimondTab;
   [SerializeField] AudioSource backgroundSource;
    public static MainMenuScript Instance;
    private void Start()
    {
        LineManager.instance.hometab.SetActive(false);
        LineManager.instance.settingtab.SetActive(false);
        LineManager.instance.hinttab.SetActive(false);
        LineManager.instance.shopbuttonicon.SetActive(false);
      
        if (PlayerPrefs.HasKey("Viberatevalue"))
        {
            if (PlayerPrefs.GetInt("Viberatevalue") == 1)
            {
                
                vibrate = true;
                NonViberation.SetActive(false);
                

            }
        }
        else
        {
            vibrate = true;
            NonViberation.SetActive(false);

        }

        MainMenu.SetActive(true);
        if (PlayerPrefs.HasKey("musicvolume"))
        {
            volume = PlayerPrefs.GetFloat("musicvolume");
            backgroundSource.volume = volume;
        }
        backgroundSource.Play();
        for(int i = 0; i < LineManager.instance.WhiteBoxEmoji.Length; i++)
        {
            LineManager.instance.EmojiLayer[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 255);
            LineManager.instance.MatchLayer[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 255);
        }
        mode = false;
        for(int i=0;i< RateStartArray.Length; i++)
        {
            RateStartArray[i].GetComponent<Image>().color = Color.grey;
        }
        isplayed = false;
        islevelfunc = false;
       

        MainMenu.SetActive(true);
        currentlevel=PlayerPrefs.GetInt("Currentlevel");
        lockFunc();
        if (GetList == null) { GetList = GetList = FindObjectOfType<LevelMakngScript>();  }
        MainMenu.SetActive(true);
        SettingPanel.SetActive(false);
        LevelMenu.SetActive(false);

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }

    }
    public void rateestart(int index)
    {
        for (int i = 0; i < RateStartArray.Length; i++)
        {
            if (i <= index)
            {
                RateStartArray[i].GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                RateStartArray[i].GetComponent<Image>().color = Color.grey;
            }
        }
        
    }

    public void Viberator()
    {

        if (vibrate)
        {
            NonViberation.SetActive(true);
           
            // NonViberation.GetComponent<Button>().enabled = true;
            viberateicon.GetComponent<Image>().enabled = true
 ;           // viberateicon.GetComponent<Button>().enabled = false;
            PlayerPrefs.SetInt("Viberatevalue", 0);
            vibrate = false;
        }
        else
        {
            
            Vibration.Vibrate(100);
            vibrate = true;
            NonViberation.SetActive(false);
            //NonViberation.GetComponent<Button>().enabled = false;
            // viberateicon.GetComponent<Button>().enabled = true;

            PlayerPrefs.SetInt("Viberatevalue", 1);
        }
    }
    public void playFunc()
    {
        LineManager.instance.WiningPopup.transform.localScale= new Vector3(0,0,0);
        PlaneObjectButton.SetActive(false);
        LineManager.instance.ResetFunction();
        if (PlayerPrefs.HasKey("Currentlevel") && PlayerPrefs.GetInt("Currentlevel") >= 2)
        {
            currentlevel = PlayerPrefs.GetInt("Currentlevel");


            LineManager.instance.ResetFunction();

        }
        else
        {
            PlayerPrefs.SetInt("Currentlevel", currentlevel);
            PlayerPrefs.Save();
            if (isplayed)
            {

                LineManager.instance.ResetFunction();
            }
        }

        MainMenu.SetActive(false);

        for (int i = 0; i < LineManager.instance.planeobjectarray.Length; i++)
        {
            if (PlayerPrefs.HasKey("Iscoinless" + i))
            {
                int index = PlayerPrefs.GetInt("Iscoinless"+i);
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
                Background.GetComponent<Image>().sprite = BackgroundArray[1];
            }
        }

            //  Background.GetComponent<Image>().sprite = BackgroundArray[1];

            LevelMenu.SetActive(false);
        levelogo.SetActive(true);
        temp2 = currentlevel + 1;
        LineManager.instance.textMesh.text = "Level " + temp2.ToString();
        LineManager.instance.EmojiElement.SetActive(true);
        LineManager.instance.MatchElement.SetActive(true);
        if (currentlevel >0 && currentlevel % 11 == 0)
        {
           
            mode = true;
            LineManager.instance.mode = mode;
           
            LevelMakngScript.instance.GenerateEmojiLevel(currentlevel);
            LineManager.instance.GetEmoji();
            copyLevelList = GetList.LevelList;
            LineManager.instance.hometab.SetActive(true);
            LineManager.instance.settingtab.SetActive(true);
            LineManager.instance.hinttab.SetActive(true);
            isplayed = true;
        }
        else
        {

            if (currentlevel >= 30)
            {
             
                int randomvalue = Random.Range(0, 29);
                LevelMakngScript.instance.GenerateLevel(randomvalue);
                LineManager.instance.GetSpriteFunc();
            }
            else
            {
                LevelMakngScript.instance.GenerateLevel(currentlevel);
                LineManager.instance.GetSpriteFunc();
            }
            LineManager.instance.hometab.SetActive(true);
            LineManager.instance.settingtab.SetActive(true);
            copyLevelList = GetList.LevelList;
            LineManager.instance.hinttab.SetActive(true);
            isplayed = true;
            
        }
        LineManager.instance.shopbuttonicon.SetActive(true);
        islevelfunc = false;
    }

    public void SettingFunc()
    {
        MainMenu.SetActive(false);
        PlaneObjectButton.SetActive(false);
        SettingPanel.SetActive(true);
    }
    public void CloseSettingFunc()
    {
        if (LineManager.instance.settingbool)
        { MainMenu.SetActive(false);
           // MainMenuScript.Instance.isplayed = true;
            LineManager.instance.MatchElement.SetActive(true);
            LineManager.instance.EmojiElement.SetActive(true);
            LineManager.instance.LevelLogo.SetActive(true);
            LineManager.instance.hinttab.SetActive(true);
            LineManager.instance.hometab.SetActive(true);
            LineManager.instance.settingtab.SetActive(true);
            LineManager.instance.shopbuttonicon.SetActive(true);



            levelogo.SetActive(true);
            foreach (GameObject g1 in LineManager.instance.Lineobjectlist)
            {
                g1.SetActive(true);
            }
        }
        else { MainMenu.SetActive(true); PlaneObjectButton.SetActive(false);
        }
        SettingPanel.SetActive(false);
    
    }
    private void Update()
    {
        currenttime = Time.time;
    }
    public bool UpdateMode(int modevalue)
    {
        
      
            if (modevalue > 0 && modevalue % 11 == 0)
            {
           
                mode = true;
                return mode;
            }
      
        else
        {
            mode = false;
            return mode;
        }
        
    }
    


    public void LevelFuncforLevelIcon()
    {
        PlaneObjectButton.SetActive(false) ;
        MainMenu.SetActive(false);
        //lockFunc() ;
        CrownTAB.SetActive(true);
        DimondTab.SetActive(false);

        LevelMenu.SetActive(true);
        if (currentlevel > ButtonArray.Length)
        {
            for (int i = 16; i <= currentlevel; i++)
            {
                addprefreb = Instantiate(prefrebobject, scrollview.content.transform);
                addprefreb.transform.SetParent(scrollview.content.transform);
                addprefreb.transform.localPosition = Vector3.zero;
                LevelNameText = addprefreb.GetComponentInChildren<TextMeshProUGUI>();
                addprefreb.GetComponent<Button>().onClick.AddListener(() => LevelFunc(currentlevel - 1));
                addprefreb.GetComponent<Button>().interactable = true;

                if (LevelNameText != null)
                {
                    LevelNameText.text = "   Level " + i.ToString();
                    // addprefreb.GetComponent<TextMeshProUGUI>().text = LevelNameText.text;

                }
                int val = i - 1;
                if (PlayerPrefs.HasKey("Level" +val + "Stars"))
                {
                  
                    int starvalue = PlayerPrefs.GetInt("Level" + i + "Stars");
                    for (int k = 0; k < starvalue; k++)
                    {
                        LevelDataBase.instance.levelArray[val].star[k].GetComponent<Image>().sprite = LineManager.instance.starImage;
                    }
                }
            }
        } 
        //LevelNameText.text = currentlevel.ToString();
        // prefrebobject.GetComponent<TextMeshProUGUI>().text = LevelNameText.text;
       

    }
    public void closelevelmenu()
    {
        LevelMenu.SetActive(false);
        MainMenu.SetActive(true);
        PlaneObjectButton.SetActive(false);
    }
    public void LevelFunc(int val)
    {
        LineManager.instance.WiningPopup.transform.localScale = new Vector3(0, 0, 0);
        for (int i = 0; i < LineManager.instance.planeobjectarray.Length; i++)
        {
            if (PlayerPrefs.HasKey("Iscoinless"+i))
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
                else if (index == 5)
                {
                    MainMenuScript.Instance.Background.GetComponent<Image>().sprite = MainMenuScript.Instance.BackgroundArray[1];

                }
                break;
            }
            else
            {

                Background.GetComponent<Image>().sprite = BackgroundArray[1];
            }
        }

        currentlevel = PlayerPrefs.GetInt("Currentlevel");
        LineManager.instance.hometab.SetActive(true);
        LineManager.instance.settingtab.SetActive(true);

        PlaneObjectButton.SetActive(false);
        LineManager.instance.ResetFunction();
        value = val;
        PlayerPrefs.SetInt("levelValue", value);
      
        // Background.GetComponent<Image>().sprite = BackgroundArray[1];
        LevelMenu.SetActive(false);
        LineManager.instance.EmojiElement.SetActive(true);
        LineManager.instance.MatchElement.SetActive(true);
        LineManager.instance.LevelLogo.SetActive( true); 
        LineManager.instance.hinttab.SetActive(true);
       
            if (val <= currentlevel)
            {
           
            LineManager.instance.ResetFunction();
            }
      
        if (currentlevel > val)
        {
          
            temp = val;
           
        }
        temp2 = val + 1;
        LineManager.instance.textMesh.text = "Level " + temp2 .ToString();

        //if (count =val)
        //{

        //    LineManager.instance.ResetFunction();

        //}
        if (val>0 &&  val==11 && val % 11 == 0)
        {
            
            mode = true;
        }
        if (mode)
        {
            LineManager.instance.mode = mode;
            //if (count == val)
            //{
            //    if (islevelfunc)
            //    {
            //        LineManager.instance.ResetFunction();
            //    }
            //}
          
            LevelMakngScript.instance.GenerateEmojiLevel(temp);
            LineManager.instance.GetEmoji();
           
            isplayed = false;
            islevelfunc = true;
        }
        else
        {
            //if (count == val)
            //{
            //    if (islevelfunc)
            //    {
            //        LineManager.instance.ResetFunction();
            //    }
            //}
           

            LevelMakngScript.instance.GenerateLevel(val);
            LineManager.instance.GetSpriteFunc();
            count = currentlevel;
            islevelfunc = true;
           
        }
       
        LineManager.instance.shopbuttonicon.SetActive(true);
        MainMenuScript.Instance.isplayed = false;
        
    }

    public void lockFunc()
    {
        
        if (currentlevel == 0)
        {
            ButtonArray[0].interactable = true;
           

            for (int i = 0; i <ButtonArray.Length-1; i++)
            {
                ButtonArray[i+1].interactable = false;
            }
        
        }
        else
        {
            if (PlayerPrefs.HasKey("Currentlevel"))
            {
                currentlevel = PlayerPrefs.GetInt("Currentlevel");

                for (int i = 0; i < ButtonArray.Length; i++)
                {
                   
                    if (i>=0 || i < currentlevel)
                    {
                     
                        ButtonArray[i].interactable = true;
                        
                        if (PlayerPrefs.HasKey("Level" + i + "Stars"))
                        {
                            
                            int starvalue = PlayerPrefs.GetInt("Level" + i + "Stars");
                            for (int k = 0; k < starvalue; k++)
                            {
                                LevelDataBase.instance.levelArray[i].star[k].GetComponent<Image>().sprite = LineManager.instance.starImage;
                            }
                        }
                    }

                    
                    
                    else
                    {
                        ButtonArray[i].interactable = false;
                        
                    }
                   
                }
               
            }
        }
        for( int k = 0; k < 3; k++)
        {
            if (currentlevel >= 1)
            {

                LevelDataBase.instance.levelArray[0].star[k].GetComponent<Image>().sprite = LineManager.instance.starImage;
            }
        }

    }


    public void Unlock()
    {
       
            int currentLevel = PlayerPrefs.GetInt("Currentlevel"); // Retrieve the current level from PlayerPrefs
        
          for(int i = 0; i < ButtonArray.Length - 1; i++)
            {
                if (i >= currentlevel)
                {
                    ButtonArray[i].interactable = true;
                }
                else
                {
                    ButtonArray[i].interactable = false;
                }
            }
      
    }
}

public static class Constants
{
    public static string currentlevelKey = "jjjkkk";
}