using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum DrawingItem
{
    bomb,
    sword,
    hat,
    chair,
    lantern,
    onigiri,
    paintbrush,
    teacup
}
public class Library : Singleton<Library>
{
    #region Text
    private Dictionary<DrawingItem, string> _prompt = new Dictionary<DrawingItem, string>()
    {
        {DrawingItem.bomb, "I need a bomb to go fishing with! Now give me one!" },
        {DrawingItem.sword, "I must have a sword to practice my kata with." },
        {DrawingItem.hat, "I have a big date tonight and simply must look good… A hat shall do nicely, no?" },
        {DrawingItem.chair,  "I have to stand at my shop all day… I would love a chair to sit in."},
        {DrawingItem.lantern, "I cannot read when the sun goes down. Maybe a lantern would help?" },
        {DrawingItem.onigiri, "I’m always hungry. If only I had some food that would be easy to carry with me and eat…" },
        {DrawingItem.paintbrush, "I need something to paint with." },
        {DrawingItem.teacup, "My mother is coming over for tea, and she’s very judgemental…" }
    };

    private Dictionary<DrawingItem, string[]> _scores = new Dictionary<DrawingItem, string[]>()
    {
        {DrawingItem.bomb, new string[]
            {
                "This certainly looks…round, I guess.",
                "Good enough for fishin'!",
                "It's perfect! Shiny! Round! KABOOM!"
            }
        },
        {DrawingItem.sword, new string[]
            {
                "I'm not sure I can cut anything with this.",
                "It's blunted, but good for practice.",
                "What a sword! You must be a master!"
            }
        },
        {DrawingItem.hat, new string[]
            {
                "Do you not have eyes?",
                "Good enough to cover my head, I suppose.",
                "Tre Magnifique! It's beautiful!"
            }
        },
        {DrawingItem.chair, new string[]
            {
                "I'm not sure this is sturdy...",
                "Time to rest my feet...",
                "Ah, how comfy! I love it!"
            }
        },
        {DrawingItem.lantern, new string[]
            {
                "Maybe I should have invested in glasses instead.",
                "This will do.",
                "So bright! So pretty! Now I can see!"
            }
        },
        {DrawingItem.onigiri, new string[]
            {
                "...Is this edible?",
                "Mmm...rice balls...",
                "Ah! It's delicious! ...Is it too late to ask for another?"
            }
        },
        {DrawingItem.paintbrush, new string[]
            {
                "I'm not sure I can even dip this in ink.",
                "This looks more suited to ink than paint...",
                "Oh! Now I can create my masterpiece!"
            }
        },
        {DrawingItem.teacup, new string[]
            {
                "Looks like Mother will mock me again...",
                "Ah, hopefully she will have nothing to say about this.",
                "How beautiful! Now Mother will praise me for sure!"
            }
        }
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