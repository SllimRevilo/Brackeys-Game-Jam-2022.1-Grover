using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : Singleton<ScoreController> 
{
	public float LengthBetweenCheckPoints;
	public float LengthBetweenDrawnPoints;
	public float PercentAccuracyForPerfect = .9f;
	public Vector2[] CheckPoints;
	public bool[] Checks;
	private float _checkPointsLength;

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
	public void SetNewCheckPoints(Vector2[] newCheckpoints)
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
