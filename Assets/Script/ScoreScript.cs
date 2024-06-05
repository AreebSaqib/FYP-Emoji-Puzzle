using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public  int scorevalue = 0,Dimondvalue=0;
    public Text textscore,DimondText;
    public static ScoreScript Instance;
    // Start is called before the first frame update
    void Start()
    {
        scorevalue = PlayerPrefs.GetInt("Score");
        Dimondvalue = PlayerPrefs.GetInt("Dimond");
        if (PlayerPrefs.HasKey("Score")&& PlayerPrefs.GetInt("scorevalue")>=10)
        {
            LineManager.instance.scorevar = scorevalue;
        }
        else
        {
            PlayerPrefs.SetInt("Score", scorevalue);
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.HasKey("Dimond") && PlayerPrefs.GetInt("Dimond") >= 10)
        {
            LineManager.instance.DimondCurrencyint = Dimondvalue;
        }
        else
        {
            PlayerPrefs.SetInt("Dimond", Dimondvalue);
            PlayerPrefs.Save();
        }
    }
    private void Awake()
    {
       Instance = this;
    }
    // Update is called once per frame
   public void Scoreupdate()
    {
       
        string toString = scorevalue.ToString();
        textscore.text = toString;
        
    }
}
