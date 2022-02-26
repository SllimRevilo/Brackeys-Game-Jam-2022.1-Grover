using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour {

	public Text TextObject;

	private string[] _intro = new string[]
	{
		"Somewhere in a forest lives a little raccoon dog named Tan Chuki.",
		"A raccoon dog’s life is lots of fun!  They play with magic, and eat lots of nuts and berries.",
		"Recently Tan Chuki has heard of this thing called “money.” Other animals use it to buy lots of stuff!",
		"Tan Chuki wants lots of stuff too. He wants to throw a whole party with his other raccoon dog friends!",
		"So, Tan Chuki goes and opens up a small shop in a little town…",
		"But what do raccoon dogs sell?",
		"“Ah!” thinks Tan Chuki. “I will use my magic to create things for people!”",
		"“I just need to make enough money in three days…”",
		"“...Before they notice I have made their riches with leaves and mud!”",
		"And so, the con of Tan Chuki begins….",
	};

	private int _lineIndex;

	void Start()
    {
		_lineIndex = 0;
		PlayIntro();
    }

	void PlayIntro()
    {
		TextWriter.Instance.WriteLine(TextObject, _intro[_lineIndex++]);
    }

	void FixedUpdate()
    {
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
		{
			if (TextWriter.CanType)
			{
				if (_lineIndex < _intro.Length)
					TextWriter.Instance.WriteLine(TextObject, _intro[_lineIndex++]);
                else
                {
					EndIntro();
                }
			}
			else if (TextWriter.Skippable)
			{
				TextWriter.Instance.SkipLine();
			}
		}
    }
	
	private void EndIntro()
    {
		GameManager.Instance.StartGame();
		gameObject.SetActive(false);
	}
}
