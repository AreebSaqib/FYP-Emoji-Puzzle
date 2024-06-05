using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    private static Sound instance = null;
    public static Sound Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        //GameObject[] obj= GameObject.FindGameObjectsWithTag("Sound");
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        //obj.length>1

        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
    // Update is called once per frame
//    public void SetMusic()
//    {
//        GameObject[] soundObjects = GameObject.FindGameObjectsWithTag("Sound");

//        if (soundObjects.Length > 1)
//        {
//            Destroy(gameObject);
//        }
//        else
//        {
//            DontDestroyOnLoad(gameObject);
//        }
//    }

