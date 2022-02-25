using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : Singleton<ScoreController> 
{
	public float LengthBetweenCheckPoints;
	public float LengthBetweenDrawnPoints;
	public float PercentAccuracyForPerfect = .9f;
	public float LengthAllowedForHitCheckPoint = 1f;
	public Vector2[] CheckPoints;
	[HideInInspector]
	public bool[] Checks;
	private float _checkPointsLength;

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

	/// <summary>
	/// checks each checkpoint to update it
	/// </summary>
	/// <param name="point">The current drawn point</param>
	public void UpdateCheckPoint(Vector2 point)
	{
		// for each checkpoint
		for(int i = 0; i < CheckPoints.Length; i++)
		{
			// if checkpoint not hit and within distance set to true
			if(!Checks[i] && Vector2.Distance(point, CheckPoints[i]) <= LengthAllowedForHitCheckPoint)
			{
				Checks[i] = true;
			}
		}
	}

	/// <summary>
	/// Scores the drawing from 0 - 100
	/// Returns 0 if all checks were not met
	/// </summary>
	/// <param name="drawnLines">the lines drawn by the player</param>
	/// <returns></returns>
	public int ScoreDrawing(List<List<Vector2>> drawnLines)
	{
		if(!IsDrawingComplete())
		{
			return 0;
		}

		// gets total length of lines for the checkpoints and the drawing
		float totalScoreValue = LengthBetweenDrawnPoints * GetScoresLength(drawnLines);
		float totalCheckValue = LengthBetweenCheckPoints* _checkPointsLength;

		// gets how far off the two are from each other as a percent less than 100
		float percentOff;
		if(totalCheckValue > totalScoreValue)
		{
			percentOff = totalScoreValue / totalCheckValue;
		}
		else
		{
			percentOff = totalCheckValue / totalScoreValue;
		}

		// divide the percent off by what is needed for perfect to get a score
		float score = percentOff / PercentAccuracyForPerfect;

		// if the score is over one we set it to one
		if(score > 1)
		{
			score = 1f;
		}

		// we return a percent in the form of 1 - 100 as an int
		return (int)(score * 100f);
	}

	/// <summary>
	/// Sets new checkpoints to be used
	/// </summary>
	/// <param name="newCheckpoints"></param>
	public void SetNewCheckPoints(Object newDrawing)
	{
		CheckPoints = newCheckpoints;
		// this will need to be different if length between points is different between the given line and the traced line
		_checkPointsLength = CheckPoints.Length;
	}
	
	
	/// <summary>
	/// gets the total length of all given scores
	/// </summary>
	/// <param name="scores"></param>
	/// <returns></returns>
	public int GetScoresLength(List<List<Vector2>> scores)
	{
		int length = 0;
		foreach(List<Vector2> line in scores)
		{
			length += line.Count;
		}
		return length;
	}

	/// <summary>
	/// Returns if we are done with the drawing
	/// </summary>
	/// <returns>true if done</returns>
	public bool IsDrawingComplete()
	{
		// for each check we need to make
		for(int i = 0; i < Checks.Length; i++)
		{
			// if one is false
			if(!Checks[i])
			{
				// then we are not complete
				return false;
			}
		}
		// otherwise we are complete
		return true;
	}
}
