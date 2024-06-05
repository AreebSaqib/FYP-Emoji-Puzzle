using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnSound : MonoBehaviour
{ 
    public AudioSource effectsound;
    // Start is called before the first frame update
    public static PlayOnSound Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void playSoundonClick()
    {
       
        effectsound.Play();
    }
    // Update is called once per frame


}
