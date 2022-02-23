using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Library : Singleton<Library>
{

    public enum Object
    {
        bomb,
        sword
    }

    private Dictionary<Object, string> _prompt = new Dictionary<Object, string>()
    {
        {Object.bomb, "I need a bomb to go fishing with! Now give me one!" },
        {Object.sword, "I must have a sword to practice my kata with." }
    };

    private Dictionary<Object, string[]> _scores = new Dictionary<Object, string[]>()
    {
        {Object.bomb, new string[]
            {
                "This certainly looks…round, I guess.",
                "Good enough for fishin'!",
                "It's perfect! Shiny! Round! KABOOM!"
            } 
        },
        {Object.sword, new string[]
            {
                "I'm not sure I can cut anything with this.",
                "It's blunted, but good for practice.",
                "What a sword! You must be a master!"
            }
        },
    };

    private static int FAIL = 0;
    private static int PASS = 1;
    private static int GREAT = 2;

    public string RetrievePrompt(Object obj)
    {
        string value;
       _prompt.TryGetValue(obj, out value);
        return value;
    }

    public string RetrieveScore(Object obj, int score)
    {
        string[] value;
        _scores.TryGetValue(obj, out value);
        return value[score];
    }

    public int DetermineScore(decimal score)
    {
        if (score > 50)
        {
            int tier = score > 70 ? GREAT : PASS;
            return tier;
        }
        else
            return FAIL;
    }
}
