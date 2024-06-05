using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineIdentity : MonoBehaviour
{


    // public static LineIdentity LineIdentityInstance;
    [SerializeField] public LineIdentity Current; // isLine Start have the GameObject of Startpoint; 
    [SerializeField] public LineIdentity Correct; // isLine Start have the GameObject of Endpoint; 
    public int counter;
    [SerializeField] public LineIdentity matchlayer;

    public bool iscountertrue;
    public GameObject linegameobject;
    public static LineIdentity instance;
    public LineRenderer line;
    public List<Sprite> EmojiSpriteRender = new List<Sprite>();
    public List<Sprite> MatchSpriteRender = new List<Sprite>();

    public SpriteRenderer outline;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public List<(Sprite, Sprite)> RandomEmojiMatchGeneration(int index)
    {
        int indexnum = index;
      
        List<(Sprite, Sprite)> RandomEmojiMatchSpriteList = new List<(Sprite, Sprite)>();
        if (EmojiSpriteRender.Count > 0 && MatchSpriteRender.Count > 0)
        {
            if (index>=0 && indexnum <= LevelDataBase.instance.levelArray.Length - 1)
            {
              
                for (int i = 0; i < LevelDataBase.instance.levelArray[index].emojiSpriteindex.Length; i++)
                {

                    int randomEmojiElementIndex = LevelDataBase.instance.levelArray[index].emojiSpriteindex[i];
                    Sprite randomElement = EmojiSpriteRender[randomEmojiElementIndex];
                    Sprite randomMatchElement = MatchSpriteRender[randomEmojiElementIndex];



                    RandomEmojiMatchSpriteList.Add((randomElement, randomMatchElement));
                    RandomEmojiMatchSpriteList = ShuffleList(RandomEmojiMatchSpriteList);

                }
            }

            else
            {
               
                if (index>=15 && indexnum > LevelDataBase.instance.levelArray.Length - 1)
                {
                   
                    counter = LevelDataBase.instance.levelArray.Length;
                    counter = index - counter;

                    for (int i = 0; i < LevelDataBase.instance.levelArray[counter].emojiSpriteindex.Length; i++)
                    {

                        int randomEmojiElementIndex = LevelDataBase.instance.levelArray[counter].emojiSpriteindex[i];
                        Sprite randomElement = EmojiSpriteRender[randomEmojiElementIndex];
                        Sprite randomMatchElement = MatchSpriteRender[randomEmojiElementIndex];



                        RandomEmojiMatchSpriteList.Add((randomElement, randomMatchElement));
                        RandomEmojiMatchSpriteList = ShuffleList(RandomEmojiMatchSpriteList);
                    }
                    iscountertrue = true;
                }
               
            }


        }
        
       
        return RandomEmojiMatchSpriteList;

    }

    public List<(Sprite, Sprite)> EmojiMatchGeneration(int index)
    {

        List<(Sprite, Sprite)> EmojiMatchSpriteList = new List<(Sprite, Sprite)>();

        if (EmojiSpriteRender.Count > 0 && MatchSpriteRender.Count > 0)
        {
            if (index > LevelDataBase.instance.levelArray.Length - 1)
            {

                counter = LevelDataBase.instance.levelArray.Length;
                counter = index - counter;

                for (int i = 0; i < LevelDataBase.instance.levelArray[counter].emojiSpriteindex.Length; i++)
                {

                    int randomEmojiElementIndex = LevelDataBase.instance.levelArray[counter].emojiSpriteindex[i];
                    Sprite randomElement = EmojiSpriteRender[randomEmojiElementIndex];
                    EmojiMatchSpriteList.Add((randomElement, randomElement));

                }
                iscountertrue = true;
            }
            else
            {
                for (int i = 0; i < LevelDataBase.instance.levelArray[index].emojiSpriteindex.Length; i++)
                {
                    int randomEmojiElementIndex = LevelDataBase.instance.levelArray[index].emojiSpriteindex[i];
                    Sprite randomElement = EmojiSpriteRender[randomEmojiElementIndex];





                    EmojiMatchSpriteList.Add((randomElement, randomElement));
                   

                }
            }
        }
        return EmojiMatchSpriteList;

    }

    public List<(Sprite, Sprite)> ShuffleList(List<(Sprite, Sprite)> Shufflelist)
    {
        System.Random random = new System.Random();

        // Fisher-Yates shuffle algorithm
        for (int i = Shufflelist.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (Sprite, Sprite) temp = Shufflelist[i];
            Shufflelist[i] = Shufflelist[j];
            Shufflelist[j] = temp;
        }

        return Shufflelist;
    }
}











