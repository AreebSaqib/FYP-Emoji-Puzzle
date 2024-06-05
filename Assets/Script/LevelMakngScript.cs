using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

using Unity.Mathematics;
public class LevelMakngScript : MonoBehaviour
{
   public int counter =0;

    public LineIdentity obj;
    MainMenuScript mainobject;
 
    int random;
    LineManager Manage;
    public List<(Sprite,Sprite)> LevelList = new List<(Sprite, Sprite)>();
    public static LevelMakngScript instance;
    public List<(Sprite,Sprite)> EmojiLevelList = new List<(Sprite, Sprite)>();
  public   List<(Sprite, Sprite)> copy = new List<(Sprite, Sprite)>();



    private void Awake()
    {
        obj = GetComponent<LineIdentity>();
        mainobject = GetComponent<MainMenuScript>();
        if (instance == null)
        {
            instance = this;
        }
        
    }
    
   public void GenerateLevel(int index)
    {
        int getindex = index;
            LevelList = obj.RandomEmojiMatchGeneration(getindex);

      
    }
    
   public void GenerateEmojiLevel(int index)
    {
        
         EmojiLevelList = obj.EmojiMatchGeneration(index);
      
    }

    



}


