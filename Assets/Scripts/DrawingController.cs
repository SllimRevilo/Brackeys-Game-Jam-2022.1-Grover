using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingController : Singleton<DrawingController> {
    public GameObject button;
    private bool _currentlyDrawing;
    private System.Action _callback;

    private void Start()
    {
        _currentlyDrawing = false;
    }

    private void Update()
    {
        if (_currentlyDrawing)
        {
            Drawing.Instance.Draw();
        }
    }

    public void StartDrawing(DrawingItem item, System.Action callback)
    {
        button.SetActive(true);
        _callback = callback;
        _currentlyDrawing = true;
    }

    public void StopDrawing()
    {
        _currentlyDrawing = false;
        button.SetActive(false);
        if (_callback != null)
        {
            _callback();
        }
    }
}
