using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;

public class TextWriter : Singleton<TextWriter> {

	private Sequence _typeWriter;
	private Queue<string> _queue;

	public float TimeToType = 0.125f;
	//create queue

	void Start()
    {
		_typeWriter = DOTween.Sequence();
	}

    public void WriteLine(Text txt, string line)
    {
		if (!_typeWriter.IsComplete())
		{
			_queue = new Queue<string>();
			txt.text = "";
			string passIn = "";
			_queue.Enqueue(passIn);
			char[] toWrite = line.ToCharArray();

			for (int i = 0; i < toWrite.Length; i++)
			{
				_queue.Enqueue(passIn += toWrite[i]);
			}

			_typeWriter.SetLoops(toWrite.Length);

			_typeWriter.AppendInterval(TimeToType).AppendCallback(() =>
			{
				txt.text = _queue.Dequeue();
			});
		}
    }
}
