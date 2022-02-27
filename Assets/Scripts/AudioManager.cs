using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : Singleton<AudioManager> {

	public AudioSource MainBGM;
	public AudioSource Ambience;

	public AudioClip[] MusicTracks;
	public AudioClip IntroBGM;
	public AudioClip DrawingSound;

	// Use this for initialization
	void Awake() 
	{
		AmbienceVolume(false);
		RandomizeTrack();
	}

	public void AmbienceVolume(bool play = true)
    {
		float volume = play ? 0.75f : 0;
		float duration = play ? 0.2f : 0.1f;
		Ambience.DOFade(volume, duration);
    }

	public void RandomizeTrack()
    {
		AudioClip newTrack = MusicTracks[Random.Range(0, MusicTracks.Length)];
		if(MainBGM.clip != newTrack)
        {
			MainBGM.clip = newTrack;
			MainBGM.Play();
        }
    }
}
