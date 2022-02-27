 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum CharacterType
{
	Tanuki,
	Dog,
	Cat,
	Crow
}

public enum AnimationType
{
	Default,
	Bow,
	Sad,
	Happy,
	Enter,
	Exit
}
public class CharacterController :Singleton<CharacterController> {

	public Animator DogAnimator;
	public Animator CatAnimator;
	public Animator CrowAnimator;
	public Animator TanukiAnimator;

	private string _bowAnimationName = "bow";
	private string _deflateAnimationName = "deflate";
	private string _defaultAnimationName = "defaultStand";
	private string _excitedAnimationName = "exciteJump";
	private string _enterAnimationName = "OLIVER PUT THE WALK IN ANIMATION NAME HERE";
	private string _exitAnimationName = "OLIVER PUT THE WALK OUT ANIMATION NAME HERE";

	//TODO: Fix the anim lenght array 💕
	private float[] _animLengths = new float[] { 0f/*default length*/, 0f/*bow length*/, 0f/*sad length*/, 0f/*Happy length*/, 0f/*enter length*/, 0f/*exit Length*/ };
	public CharacterType currentCustomer;

	/// <summary>
	/// selects a new customer and runs through openning sequence with them and tan chuki
	/// </summary>
	/// <param name="onComplete"></param>
	public void EnterCharacter(System.Action onComplete)
    {
		currentCustomer = (CharacterType)Random.Range(1,4);
		Debug.Log("THIS SEQUENCE WILL CRASH IF YOU DONT FIX THE ANIMATION NAME AnD TIMING THX 💕");
		DOTween.Sequence()
			.AppendCallback(() =>
			{
				DoCharacterAction(currentCustomer, AnimationType.Enter);
			})
			.AppendInterval(_animLengths[(int)AnimationType.Enter])
			.AppendCallback(() =>
			{
				DoCharacterAction(CharacterType.Tanuki, AnimationType.Bow);
			})
			.AppendInterval(_animLengths[(int)AnimationType.Bow])
			.AppendCallback(() =>
			{
				DoCharacterAction(currentCustomer, AnimationType.Bow);
			})
			.AppendInterval(_animLengths[(int)AnimationType.Bow])
			.OnComplete(() => 
			{ 
				onComplete(); 
			});
	}

	public void ExitCharacter(int score, System.Action onComplete)
    {
		float reactionDelay = 0f;
        if (score > 0)
        {
			reactionDelay = _animLengths[(int)AnimationType.Happy];
			DoCharacterAction(currentCustomer, AnimationType.Happy);
        }
        else
        {
			reactionDelay = _animLengths[(int)AnimationType.Sad];
			DoCharacterAction(currentCustomer, AnimationType.Sad);
        }
		DOTween.Sequence()
			.AppendInterval(reactionDelay)
			.AppendCallback(() =>
			{
				DoCharacterAction(CharacterType.Tanuki, AnimationType.Bow);
			})
			.AppendInterval(_animLengths[(int)AnimationType.Bow])
			.AppendCallback(() =>
			{
				DoCharacterAction(currentCustomer, AnimationType.Bow);
			})
			.AppendInterval(_animLengths[(int)AnimationType.Bow])
			.AppendCallback(() =>
			{
				DoCharacterAction(currentCustomer, AnimationType.Exit);
			})
			.AppendInterval(_animLengths[(int)AnimationType.Exit])
			.OnComplete(() => { onComplete(); });

	}

	/// <summary>
	/// Plays the given animation for the given character
	/// </summary>
	/// <param name="character">The cute little murderer</param>
	/// <param name="animationType">The cute little animation</param>
	public void DoCharacterAction(CharacterType character, AnimationType animationType)
    {
		switch(character)
        {
			case CharacterType.Tanuki:
				SelectAnimation(animationType, TanukiAnimator);
				break;
			case CharacterType.Cat:
				SelectAnimation(animationType, CatAnimator);
				break;
			case CharacterType.Dog:
				SelectAnimation(animationType, DogAnimator);
				break;
			case CharacterType.Crow:
				SelectAnimation(animationType, CrowAnimator);
				break;
		}
    }

	/// <summary>
	/// Sets the animation of the specified animation type to the specified animator
	/// </summary>
	/// <param name="animationType"></param>
	/// <param name="anim"></param>
	private void SelectAnimation(AnimationType animationType, Animator anim)
    {
		switch (animationType)
		{
			case AnimationType.Default:
				anim.Play(_defaultAnimationName);
				break;
			case AnimationType.Bow:
				anim.Play(_bowAnimationName);
				break;
			case AnimationType.Happy:
				anim.Play(_excitedAnimationName);
				break;
			case AnimationType.Sad:
				anim.Play(_deflateAnimationName);
				break;
			case AnimationType.Enter:
				anim.Play(_enterAnimationName);
				break;
		}
	}
}
