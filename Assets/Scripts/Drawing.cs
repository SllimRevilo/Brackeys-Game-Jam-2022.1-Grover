using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
/**
 * created using this tutorial: 
 * 
 * https://www.youtube.com/watch?v=_ILOVprdq4o
 */

public class Line
{
    public List<Vector2> Points;
    private Vector2 _currentPoint;
    private float _distance;

    public Line(Vector2 startingPoint, float distance)
    {
        Points = new List<Vector2>();
        Points.Add(startingPoint);

        _currentPoint = startingPoint;
        _distance = distance;
    }

    public void NewPoint(Vector2 newPoint)
    {
        if(_distance <= Vector2.Distance(_currentPoint, newPoint))
        {
            _currentPoint = newPoint;
            Points.Add(newPoint);
            ScoreController.Instance.UpdateCheckPoint(newPoint);
        }
    }
}

public class Drawing : Singleton<Drawing> {

    public Camera DrawingCam;
    public GameObject Brush;
    public float Distance;

    public List<Line> Lines { get; private set; }

    private Line _currentLine;
    private LineRenderer _currentLineRenderer;
    private List<GameObject> AllLineObjects;
    private Vector2 _lastPos;
    
    private void Start()
    {
        AllLineObjects = new List<GameObject>();
        Lines = new List<Line>();
    }
    
    private void Update()
    {
        Draw();
    }

    private void Draw()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateBrush();
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = DrawingCam.ScreenToWorldPoint(Input.mousePosition);

            if(_lastPos != mousePos)
            {
                AddPoint(mousePos);
                _lastPos = mousePos;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _currentLineRenderer = null;
        }
    }

    private void CreateBrush() 
    {
        GameObject brushInstance = Instantiate(Brush);
        AllLineObjects.Add(brushInstance);

        _currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        Vector2 mousePos = DrawingCam.ScreenToWorldPoint(Input.mousePosition);

        _currentLineRenderer.SetPosition(0, mousePos);
        _currentLineRenderer.SetPosition(1, mousePos);
        _lastPos = mousePos;

        _currentLine = new Line(mousePos, Distance);
        Lines.Add(_currentLine);
    }

    private void AddPoint(Vector2 point)
    {
        _currentLineRenderer.positionCount++;
        int index = _currentLineRenderer.positionCount-1;

        _currentLineRenderer.SetPosition(index, point);

        _currentLine.NewPoint(point);
    }
}
