using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    public GameObject[] Camera;
    public Text CustomerText;
    public GameObject Tanuki;

    private DrawingItem _currentItem;
    private enum CamName
    {
        Intro = 0,
        Main = 1,
        Drawing = 2,
        Ending = 3,
        Menu = 4
    }
	private void Start()
    {
    }

    /// <summary>
    /// self explanitory... i hope
    /// </summary>
    public void StartGame()
    {
       // TransitionCameras(CamName.Intro, CamName.Main);
        CharacterEnter();
    }

    /// <summary>
    /// Calls Character enter and passes it callback that does stuff
    /// </summary>
    private void CharacterEnter()
    {
        CharacterController.Instance.EnterCharacter(() =>
        {
            _currentItem = (DrawingItem)Random.Range(0, 6);
            string prompt = Library.Instance.RetrievePrompt(_currentItem);
            TextWriter.Instance.WriteLine(CustomerText, prompt, DoDrawing);
        });
    }

    private void DoDrawing()
    {
        Tanuki.transform.DORotate(new Vector3(0f, 180f, 0f), .5f)
        .OnComplete(() =>
        {
            TransitionCameras(CamName.Main, CamName.Drawing);
            DrawingController.Instance.StartDrawing(_currentItem, () =>
            {
                PrizeContainerController.Instance.PlayWin(_currentItem);
                ScoreController.Instance.SetNewCheckPoints(_currentItem);
                int score = ScoreController.Instance.ScoreDrawing(Drawing.Instance.FinalPoints());
                score = Library.Instance.DetermineScore(score);
                TransitionCameras(CamName.Drawing, CamName.Main);

                Tanuki.transform.DORotate(new Vector3(0f, 0f, 0f), .5f)
                .OnComplete(() =>
                {
                    string customerResponse = Library.Instance.RetrieveScore(_currentItem, score);
                    TextWriter.Instance.WriteLine(CustomerText, customerResponse, () =>
                    {
                        ExitCustomer(score);
                    });
                    //TODO: Add scoring effect idk where tho 💕
                    
                });

            });
        });
    }

    private void ExitCustomer(int score)
    {
        CharacterController.Instance.ExitCharacter(score, () =>
        {
            PrizeContainerController.Instance.ExitWin();
            DOTween.Sequence()
                .AppendInterval(.75f)
                .AppendCallback(CharacterEnter);
        });
    }

    /// <summary>
    /// transitions between two cameras
    /// </summary>
    /// <param name="fadeOut">current camera</param>
    /// <param name="fadeIn">new camera</param>
    private void TransitionCameras(CamName fadeOut, CamName fadeIn)
    {
        Camera[(int)fadeOut].SetActive(false);
        Camera[(int)fadeIn].SetActive(true);
    }
}
