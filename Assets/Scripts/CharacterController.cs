using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	Happy
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
				switch(animationType)
                {
					case AnimationType.Default:
						TanukiAnimator.Play(_defaultAnimationName);
						break;
					case AnimationType.Bow:
						TanukiAnimator.Play(_bowAnimationName);
						break;
					case AnimationType.Happy:
						TanukiAnimator.Play(_excitedAnimationName);
						break;
					case AnimationType.Sad:
						TanukiAnimator.Play(_deflateAnimationName);
						break;
				}
				break;
			case CharacterType.Cat:
				switch (animationType)
				{
					case AnimationType.Default:
						CatAnimator.Play(_defaultAnimationName);
						break;
					case AnimationType.Bow:
						CatAnimator.Play(_bowAnimationName);
						break;
					case AnimationType.Happy:
						CatAnimator.Play(_excitedAnimationName);
						break;
					case AnimationType.Sad:
						CatAnimator.Play(_deflateAnimationName);
						break;
				}
				break;
			case CharacterType.Dog:
				switch (animationType)
				{
					case AnimationType.Default:
						DogAnimator.Play(_defaultAnimationName);
						break;
					case AnimationType.Bow:
						DogAnimator.Play(_bowAnimationName);
						break;
					case AnimationType.Happy:
						DogAnimator.Play(_excitedAnimationName);
						break;
					case AnimationType.Sad:
						DogAnimator.Play(_deflateAnimationName);
						break;
				}
				break;
			case CharacterType.Crow:
				switch (animationType)
				{
					case AnimationType.Default:
						CrowAnimator.Play(_defaultAnimationName);
						break;
					case AnimationType.Bow:
						CrowAnimator.Play(_bowAnimationName);
						break;
					case AnimationType.Happy:
						CrowAnimator.Play(_excitedAnimationName);
						break;
					case AnimationType.Sad:
						CrowAnimator.Play(_deflateAnimationName);
						break;
				}
				break;
		}
    }
}
