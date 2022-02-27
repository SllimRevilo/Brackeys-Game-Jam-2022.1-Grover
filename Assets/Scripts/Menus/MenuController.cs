using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public GameObject Menu;
	public GameObject CreditsMenu;
	public Text Score;

	private const string SCOR = "score";
	private const string HIGH = "High Score: ";

	// Use this for initialization
	void Start () {
		CreditsMenu.SetActive(false);
		Score.text = HIGH + PlayerPrefs.GetFloat(SCOR, 0);
	}

	public void NewGame()
    {
		Menu.SetActive(false);
		AudioManager.Instance.RandomizeTrack();
		AudioManager.Instance.AmbienceVolume();
		GameManager.Instance.StartGame();
    }

	public void ClearProgress()
    {
		PlayerPrefs.SetFloat(SCOR, 0);
		Score.text = HIGH + PlayerPrefs.GetFloat(SCOR, 0);
	}

	public void Quit()
    {
		Application.Quit();
    }

	public void Credits()
    {
		Menu.SetActive(false);
		CreditsMenu.SetActive(true);
    }

	public void MainMenu()
    {
		Menu.SetActive(true);
		CreditsMenu.SetActive(false);
    }
	
}
