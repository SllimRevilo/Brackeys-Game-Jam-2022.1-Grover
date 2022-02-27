using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum DrawingItem
{
    bomb,
    //sword,
    hat,
    lantern,
    onigiri,
    paintbrush,
    teacup,
    //scroll,
}

public class Library : Singleton<Library>
{

    #region Text
    private Dictionary<DrawingItem, string> _prompt = new Dictionary<DrawingItem, string>()
    {
        {DrawingItem.bomb, "I need a bomb to go fishing with!\nNow give me one!" },
        //{DrawingItem.sword, "I must have a sword to\npractice my kata with." },
        {DrawingItem.hat, "I have a big date tonight and\nsimply must look good…\nA hat shall do nicely, no?" },
        {DrawingItem.lantern, "I cannot read when the sun\ngoes down. Maybe a lantern\nwould help?" },
        {DrawingItem.onigiri, "I’m always hungry. If only I\nhad some food that would\nbe easy to carry with me and eat…" },
        {DrawingItem.paintbrush, "I need something to paint with." },
        {DrawingItem.teacup, "My mother is coming over for tea,\nand she’s very judgemental…" },
        //{DrawingItem.scroll, "I've run out of paper\nto write my novel on!" }
    };

    private Dictionary<DrawingItem, string[]> _scores = new Dictionary<DrawingItem, string[]>()
    {
        {DrawingItem.bomb, new string[]
            {
                "This certainly looks…round,\nI guess.",
                "Good enough for fishin'!",
                "It's perfect! Shiny! Round!\nKABOOM!"
            }
        },
        /*
        {DrawingItem.sword, new string[]
            {
                "I'm not sure I can cut\nanything with this.",
                "It's blunted, but good for practice.",
                "What a sword! You must be a master!"
            }
        },
        */
        {DrawingItem.hat, new string[]
            {
                "Do you not have eyes?",
                "Good enough to cover\nmy head, I suppose.",
                "Tre Magnifique! It's beautiful!"
            }
        },
        {DrawingItem.lantern, new string[]
            {
                "Maybe I should have invested in glasses instead.",
                "This will do.",
                "So bright! So pretty!\nNow I can see!"
            }
        },
        {DrawingItem.onigiri, new string[]
            {
                "...Is this edible?",
                "Mmm...rice balls...",
                "Ah! It's delicious! ...\nIs it too late to ask for another?"
            }
        },
        {DrawingItem.paintbrush, new string[]
            {
                "I'm not sure I can even\ndip this in ink.",
                "This looks more suited\nto ink than paint...",
                "Oh! Now I can create\nmy masterpiece!"
            }        
        },
        {DrawingItem.teacup, new string[]
            {
                "Looks like Mother will mock me\nagain...",
                "Ah, hopefully she will have\nnothing to say about this.",
                "How beautiful! Now Mother\nwill praise me for sure!"
            }
        },
        /*
        {DrawingItem.scroll, new string[]
            {
                "I'm not sure this could be\nused even as toilet paper.",
                "I guess I'll have to write\nreally tiny to fit it all...",
                "How beautiful! How smooth!\nTruly worthy of literature!"
            }
        }
        */
    };
    #endregion

    private static int FAIL = 0;
    private static int PASS = 1;
    private static int GREAT = 2;

    public string RetrievePrompt(DrawingItem obj)
    {
        string value;
       _prompt.TryGetValue(obj, out value);
        return value;
    }

    public string RetrieveScore(DrawingItem obj, int score)
    {
        string[] value;
        _scores.TryGetValue(obj, out value);
        return value[score];
    }

    public int DetermineScore(int score)
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
