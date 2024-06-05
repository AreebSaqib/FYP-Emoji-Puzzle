using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IqProgreesbar : MonoBehaviour
{
    // Start is called before the first frame update
  public Iqlevel[] iqarray;
    public static IqProgreesbar instance;
  private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public string[] levelnamearray;
}

[System.Serializable]
public class Iqlevel
{
  public   string levelname;
     
     public int levelno;
    
   
    public Iqlevel(string levelname,  int levelno)
    {
        this.levelname = levelname;
        this.levelno = levelno;
      
    }
    
}