using UnityEngine;
using System.Collections;
using Tamboro;

public class btnActionsOpeningScreen : MonoBehaviour {

	public GameManager Game;
	public AudioClip openingScreenSound;

	void OnEnable()
	{
		SoundManager.Instance.Play(openingScreenSound);
	}

	public void btnConfigurationsClicked()
	{
		Game.playButtonSound();
		Game.LoadScreen("ConfigurationScreen");
	}

	public void btnPlayClicked()
	{
		Game.playButtonSound();
		Game.LoadScreen("PresentationScreen");
	}
}
