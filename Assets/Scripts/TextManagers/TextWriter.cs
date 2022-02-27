using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;

public class TextWriter : Singleton<TextWriter> {

	private Sequence _typeWriter;
	private Queue<string> _queue;

	public float TimeToType = 0.0875f;

	public static bool CanType = true;
	public static bool Skippable = false;

	private Text _text;
	private string _line;

	void Start()
    {
		_typeWriter = DOTween.Sequence();
	}

	public void WriteLine(Text txt, string line, System.Action callBack = null)
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
