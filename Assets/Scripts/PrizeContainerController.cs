using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeContainerController : MonoBehaviour {

	public Animator WinAnimator;
	private string _winIn = "prizeWinIn";
	private string _winOut = "prizeWinOut";

	public Material PrizeMaterial;
	public Texture2D[] PrizeTexture;

	public void PlayWin(DrawingItem item)
    {
		PrizeMaterial.mainTexture = PrizeTexture[(int)item];
		WinAnimator.Play(_winIn);
    }
	
	public void ExitWin()
    {
		WinAnimator.Play(_winOut);
    }
}
