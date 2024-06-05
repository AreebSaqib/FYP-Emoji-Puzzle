using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataBase : MonoBehaviour
{
    // Start is called before the first frame update
    public level[] levelArray;
    public static LevelDataBase instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
   
}

[System.Serializable]
public class level
{
    public int levelno;
    public bool mode;
    public int[] emojiSpriteindex;
    public int[] MatchSpriteindex;
    public GameObject[] star;
 
    public level(int levelno,int[] emojiSpriteindex, int[] MatchSpriteindex,GameObject[] star)
    {
        this.levelno = levelno;
        this.emojiSpriteindex = emojiSpriteindex;
        for(int i = 0; i < emojiSpriteindex.Length; i++)
        {
            this.emojiSpriteindex = emojiSpriteindex;
        }
        this.MatchSpriteindex = MatchSpriteindex;
        for (int i = 0; i < emojiSpriteindex.Length; i++)
        {
            this.MatchSpriteindex = MatchSpriteindex;
        }
        this.star = star;
        for (int i = 0; i < star.Length; i++)
        {
            this.star = star;
        }
    }
}
