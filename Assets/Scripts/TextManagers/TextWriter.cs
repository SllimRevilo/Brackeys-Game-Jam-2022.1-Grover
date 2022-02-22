using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;

public class TextWriter : Singleton<TextWriter> {

	private Sequence _typeWriter;
	private List<Sequence> _key;
	//create queue

    public void WriteLine(Text txt, string line)
    {
        if (!_typeWriter.IsPlaying())
        {
			_typeWriter = DOTween.Sequence();
			_key = new List<Sequence>();
			txt.text = "";
			string passIn = "";
			char[] toWrite = line.ToCharArray();
			
			for(int i = 0; i < toWrite.Length; i++)
            {
				_key.Add(TypeWriter(passIn, toWrite[i]));
            }
			
		}
    }

	private Sequence TypeWriter(string t, char c)
    {
		Sequence seq = DOTween.Sequence().AppendInterval(0.125f).AppendCallback(() => 
		{
			t += c;
		});

		return seq;
    }
}
