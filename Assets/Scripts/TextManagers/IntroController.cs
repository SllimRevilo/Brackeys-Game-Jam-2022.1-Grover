using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IntroController : MonoBehaviour {

	public Text TextObject;
	public GameObject[] Slideshow;

	private Sequence _transition;

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
	private int _slideIndex;
	bool _inIntro = false;

    private int[] _thresh = new int[] { 2, 4, 6 };

	private float _transTime = 0.25f;

	void Start()
    {
		_lineIndex = 0;
		_slideIndex = 1;
		Slideshow[0].SetActive(true);
		for(int i = 1; i < Slideshow.Length; i++)
        {
			Slideshow[i].GetComponent<Image>().color = Color.black;
			Slideshow[i].SetActive(false);
        }
    }

	void PlayIntro()
    {
		_transition = DOTween.Sequence();
		_transition.AppendCallback(() =>
		{
			Slideshow[0].GetComponent<Image>().DOColor(Color.black, _transTime);
		}).AppendInterval(_transTime).AppendCallback(() =>
		{
			Slideshow[0].SetActive(false);
			Slideshow[_slideIndex++].SetActive(true);
			Slideshow[_slideIndex].GetComponent<Image>().DOColor(Color.white, _transTime);
		}).OnComplete(() => { TextWriter.Instance.WriteLine(TextObject, _intro[_lineIndex++]); _inIntro = true; });
    }

	public void TransitionSlides()
    {
		_transition = DOTween.Sequence();
		_transition.AppendCallback(() =>
		{
			Slideshow[_slideIndex].GetComponent<Image>().DOColor(Color.black, _transTime);
		}).AppendInterval(_transTime).AppendCallback(() =>
		{
			Slideshow[_slideIndex].SetActive(false);
			Slideshow[_slideIndex++].SetActive(true);
			Slideshow[_slideIndex].GetComponent<Image>().DOColor(Color.white, _transTime);
		}).OnComplete(() => { TextWriter.Instance.WriteLine(TextObject, _intro[_lineIndex++]); _inIntro = true; });
	}

	public void SkipButton()
    {
		_transition.Kill(true);
		foreach(GameObject go in Slideshow)
        {
			go.SetActive(false);
        }
    }

	void FixedUpdate()
    {
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) && _inIntro)
		{
			if (TextWriter.CanType)
			{
				if (_lineIndex < _intro.Length)
				{
					if (!_thresh.Contains(_lineIndex))
					{
						TextWriter.Instance.WriteLine(TextObject, _intro[_lineIndex++]);
					}
                    else
                    {
						_inIntro = false;
						TransitionSlides();
                    }
				}
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
