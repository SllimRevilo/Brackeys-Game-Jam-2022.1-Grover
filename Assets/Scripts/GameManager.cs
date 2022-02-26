using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    public GameObject[] Camera;
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
        TransitionCameras(CamName.Intro, CamName.Main);
        CharacterEnter();
    }

    /// <summary>
    /// Calls Character enter and passes it callback that does stuff
    /// </summary>
    private void CharacterEnter()
    {
        CharacterController.Instance.EnterCharacter(() =>
        {
            //TODO: Select items and send Tanuki to work 💕
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
        Camera[(int)fadeIn].SetActive(false);
    }
}
