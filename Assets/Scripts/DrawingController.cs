using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingController : Singleton<DrawingController> {
    public GameObject button;
    public float Timer;

    private float _totalTime;
    private bool _currentlyDrawing;
    private System.Action _callback;

    private void Start()
    {
        _totalTime = Timer;
        _currentlyDrawing = false;
    }

    private void Update()
    {
        if (_currentlyDrawing)
        {
            Drawing.Instance.Draw();
            Timer -= Time.deltaTime;
        }
    }

    public void ResetTimer()
    {
        Timer = _totalTime;
    }

    public void StartDrawing(DrawingItem item, System.Action callback)
    {
        ScoreController.Instance.ChangeStencil(item);
        Drawing.Instance.DestroyDrawings();
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
