using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volume : MonoBehaviour
{
   [SerializeField] Slider volumeSlider;
 

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicvolume") ){
             
            PlayerPrefs.SetFloat("musicvolume", 1);
            load();
        }
        
        else
        {
            load();
        }
    }
    public void SetMusicVolume() { 
    }
    void save()
    {
        
            PlayerPrefs.SetFloat("musicvolume", volumeSlider.value);
        
       
    }
    void load()
    {
       
            volumeSlider.value = PlayerPrefs.GetFloat("musicvolume");
        
    }

    
    // Update is called once per frame
    public void changevolume()
    {
        
          AudioListener.volume = volumeSlider.value;
        save();
             
    }
}
