using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    public GameObject[] Camera;
    public GameObject CustomerPanel;
    public Text CustomerText;
    public GameObject Tanuki;

    private int _totalScore;
    private DrawingItem _currentItem;
    private enum CamName
    {
        Intro = 0,
        Main = 1,
        Drawing = 2,
        Ending = 3,
        Menu = 4
    }

    /// <summary>
    /// self explanitory... i hope
    /// </summary>
    public void StartGame()
    {
        _totalScore = 0;
        DrawingController.Instance.ResetTimer();
        CharacterEnter();
    }

    /// <summary>
    /// Calls Character enter and passes it callback that does stuff
    /// </summary>
    private void CharacterEnter()
    {
        CustomerPanel.SetActive(true);
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
            CustomerPanel.SetActive(false);
            DrawingController.Instance.StartDrawing(_currentItem, () =>
            {
                ScoreController.Instance.SetNewCheckPoints(_currentItem);
                int score = ScoreController.Instance.ScoreDrawing(Drawing.Instance.FinalPoints());
                _totalScore += score;

                PrizeContainerController.Instance.PlayWin(_currentItem);
                int tier = Library.Instance.DetermineScore(score);
                TransitionCameras(CamName.Drawing, CamName.Main);
                CustomerPanel.SetActive(true);
                Tanuki.transform.DORotate(new Vector3(0f, 0f, 0f), .5f)
                .OnComplete(() =>
                {
                    string customerResponse = Library.Instance.RetrieveScore(_currentItem, tier);
                    TextWriter.Instance.WriteLine(CustomerText, customerResponse, () =>
                    {
                        ExitCustomer(tier);
                    });
                        //TODO: Add scoring effect idk where tho 💕
                });
            });
        });
    }

    private void EndGame()
    {
        MenuController.Instance.UpdateScore(_totalScore);
        MenuController.Instance.MainMenu();
        CustomerPanel.SetActive(false);
    }
    private void ExitCustomer(int tier)
    {
        CharacterController.Instance.ExitCharacter(tier, () =>
        {
            PrizeContainerController.Instance.ExitWin();
            DOTween.Sequence()
                .AppendInterval(.75f)
                .AppendCallback(() =>
                {
                    if (DrawingController.Instance.Timer > 0)
                        CharacterEnter();
                    else
                        EndGame();
                });
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
