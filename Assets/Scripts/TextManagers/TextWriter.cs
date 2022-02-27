using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;

public class TextWriter : Singleton<TextWriter> {

	public GameObject Panel;

	private Sequence _typeWriter;
	private Queue<string> _queue;

	public float TimeToType = 0.0875f;

	public static bool CanType = true;
	public static bool Skippable = false;

	private Text _text;
	private string _line;

	void Start()
    {
		Panel.GetComponent<Image>().enabled = false;
		_typeWriter = DOTween.Sequence();
	}

    public void WriteLine(Text txt, string line, System.Action callback = null, bool inBubble = true)
    {
		if (CanType)
		{
			_text = txt;
			_line = line;
			_typeWriter = DOTween.Sequence();
			CanType = false;
			Skippable = true;
			_queue = new Queue<string>();
			txt.text = "";
			string passIn = "";
			_queue.Enqueue(passIn);
			char[] toWrite = line.ToCharArray();

			Panel.GetComponent<Image>().enabled = inBubble;

			for (int i = 0; i < toWrite.Length; i++)
			{
				_queue.Enqueue(passIn += toWrite[i]);
			}

			_typeWriter.SetLoops(toWrite.Length + 4);

			_typeWriter.AppendInterval(TimeToType).AppendCallback(() =>
			{
				txt.text = _queue.Dequeue();
			})
			.OnComplete(() => 
			{ 
				CanType = true; 
				Skippable = false; 
				if(callBack != null)
                {
					callBack();
                }
			});
		}
    }

	public void TextBubbleFinished (Text txt, System.Action callback = null)
    {
		txt.text = "";
		Panel.GetComponent<Image>().enabled = false;
	}

	public void SkipLine()
    {
        if (Skippable)
        {
			_queue.Clear();
			_typeWriter.Kill(true);
			_text.text = _line;
        }
    }
}
