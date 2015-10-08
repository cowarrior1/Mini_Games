using UnityEngine;
using System.Collections;
using Tamboro;

public class btnActionsPresentationScreen : MonoBehaviour {

	public GameManager Game;
	public AudioClip presentationChallenge;
	public UI2DSpriteAnimation silvia;
	public GameObject silviaObject;
	public UILabel lblText;

	void OnEnable()
	{
		SoundManager.Instance.Play(presentationChallenge);
		Invoke("stopSilvia",14f);
		lblText.text = Strings.Instance.getStringByName("presentationScreen");
	}

	void stopSilvia()
	{
		silvia.framerate = 0;
	}

	public void btnConfigurationClicked()
	{
		silvia.framerate = 0;
		SoundManager.Instance.Pause();
		Game.playButtonSound();
		Game.LoadScreen("ConfigurationScreen");
		silviaObject.SetActive(false);
	}

	public void btnHomeClicked()
	{
		SoundManager.Instance.Pause();
		Game.playButtonSound();
		Game.LoadScreen("OpeningScreen");
	}

	public void btnPlayClicked()
	{
		SoundManager.Instance.Pause();
		Game.playButtonSound();
		Game.LoadScreen("LevelScreen");
	}

	public void ConfigurationScreenClosed()
	{
		silvia.framerate = 20;
		SoundManager.Instance.Play(presentationChallenge);
	}
}
