using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : Singleton<ScoreController> 
{
	public float LengthBetweenDrawnPoints;
	public float PercentAccuracyForPerfect = .95f;
	public float LengthAllowedForHitCheckPoint = 1f;


	private List<Vector2> _checkPoints;
	private float _checkPointsLength;

	public Transform[] BombCheckpoints;
	public Transform[] HatCheckpoints;
	public Transform[] CupCheckpoints;
	public Transform[] LanternCheckpoints;
	public Transform[] PaintBrushCheckpoints;
	public Transform[] OnigiriCheckpoints;

	private Dictionary<DrawingItem, float> _drawingLengths = new Dictionary<DrawingItem, float>()
	{
		{DrawingItem.bomb, 5.682515f},
		{DrawingItem.hat, 5.036776f},
		{DrawingItem.lantern, 5.821858f},
		{DrawingItem.onigiri, 4.874483f},
		{DrawingItem.paintbrush, 5.213915f},
		{DrawingItem.teacup, 4.396302f}
	};

	private Dictionary<DrawingItem, List<Vector2>> _drawingCheckpoints;

	public void Start()
	{
		_drawingCheckpoints = new Dictionary<DrawingItem, List<Vector2>>
		{
			{DrawingItem.bomb, ConvertVector3ArrayToVector2Array(BombCheckpoints)},
			{DrawingItem.hat, ConvertVector3ArrayToVector2Array(HatCheckpoints)},
			{DrawingItem.lantern, ConvertVector3ArrayToVector2Array(LanternCheckpoints)},
			{DrawingItem.onigiri, ConvertVector3ArrayToVector2Array(OnigiriCheckpoints)},
			{DrawingItem.paintbrush, ConvertVector3ArrayToVector2Array(PaintBrushCheckpoints)},
			{DrawingItem.teacup, ConvertVector3ArrayToVector2Array(CupCheckpoints)}
		};
	}

	/// <summary>
	/// checks each checkpoint to update it
	/// </summary>
	/// <param name="point">The current drawn point</param>
	public void UpdateCheckPoint(Vector2 point)
	{
		// for each checkpoint
		for(int i = 0; i < _checkPoints.Count; i++)
		{
			// if checkpoint not hit and within distance set to true
			Debug.Log(point + "  -----  " + _checkPoints[i]);
			if(Vector2.Distance(point, _checkPoints[i]) <= LengthAllowedForHitCheckPoint)
			{
				_checkPoints.RemoveAt(i);
				i--;
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
		// check the checkpoints to see if they were reached
		foreach(List<Vector2> list in drawnLines)
        {
			foreach(Vector2 point in list)
            {
				UpdateCheckPoint(point);
            }
        }

		// if not completed the score is 0
		if(!IsDrawingComplete())
		{
			return 0;
		}
		// gets total length of lines for the checkpoints and the drawing
		float totalScoreValue = LengthBetweenDrawnPoints * (float)GetScoresLength(drawnLines);

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
		_checkPointsLength = _drawingLengths[newDrawing];
		_checkPoints = _drawingCheckpoints[newDrawing];
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
		return _checkPoints.Count == 0;
	}
	/// <summary>
	/// Converts a transform array to a vec 2 array
	/// uses the x and z axis
	/// </summary>
	/// <param name="array">array to change</param>
	/// <returns>a vector 2 array</returns>
	public List<Vector2> ConvertVector3ArrayToVector2Array(Transform[] array)
    {
		List<Vector2> newList = new List<Vector2>();
		for(int i = 0; i < array.Length; i++)
        {
			Vector2 vec = new Vector2(array[i].transform.position.x, array[i].transform.position.y);

			newList.Add(vec);
        }
		return newList;
    }
}
