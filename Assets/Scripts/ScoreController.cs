using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : Singleton<ScoreController> 
{
	public float LengthBetweenDrawnPoints;
	public float PercentAccuracyForPerfect = .9f;
	public float LengthAllowedForHitCheckPoint = 1f;


	private Vector2[] _checkPoints;
	private bool[] _checks;
	private float _checkPointsLength;

	public Transform[] BombCheckpoints;

	private Dictionary<DrawingItem, float> DrawingLengths = new Dictionary<DrawingItem, float>()
	{
		{DrawingItem.bomb, 5.682515f},
		{DrawingItem.sword, 0f},
		{DrawingItem.hat, 0f},
		{DrawingItem.chair, 0f},
		{DrawingItem.lantern, 0f},
		{DrawingItem.onigiri, 0f},
		{DrawingItem.paintbrush, 0f},
		{DrawingItem.teacup, 0f}
	};

	/// <summary>
	/// checks each checkpoint to update it
	/// </summary>
	/// <param name="point">The current drawn point</param>
	public void UpdateCheckPoint(Vector2 point)
	{
		// for each checkpoint
		for(int i = 0; i < _checkPoints.Length; i++)
		{
			// if checkpoint not hit and within distance set to true
			if(!_checks[i] && Vector2.Distance(point, _checkPoints[i]) <= LengthAllowedForHitCheckPoint)
			{
				_checks[i] = true;
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

		// gets how far off the two are from each other as a percent less than 100
		float percentOff = 0;
		
		if(_checkPointsLength > totalScoreValue)
		{
			percentOff = totalScoreValue / _checkPointsLength;
		}
		else
		{
			percentOff = _checkPointsLength / totalScoreValue;
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
	public void SetNewCheckPoints(DrawingItem newDrawing)
	{
		switch(newDrawing)
        {
			case DrawingItem.bomb:
				_checkPointsLength = DrawingLengths[DrawingItem.bomb];
				break;
        }

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
		for(int i = 0; i < _checks.Length; i++)
		{
			// if one is false
			if(!_checks[i])
			{
				// then we are not complete
				return false;
			}
		}
		// otherwise we are complete
		return true;
	}
}
