using UnityEngine;
using System.Collections;
using Tamboro;

public class btnActionsPresentationScreen : MonoBehaviour {

	public GameManager Game;
	public AudioClip presentationChallenge;
	public UI2DSpriteAnimation silvia;
	public GameObject silviaObject;
	public GameObject silviaIdleObject;
	public UILabel lblText;

	public bool characterIsSpeaking;

	bool configScreenOpen;

	void OnEnable()
	{
		CancelInvoke();
		SoundManager.Instance.Play(presentationChallenge);
		Invoke("stopSilvia",23f);
		lblText.text = Strings.Instance.getStringByName("presentationScreen");
		characterIsSpeaking = true;


		silviaObject.SetActive(true);
		silviaIdleObject.SetActive(false);
	}

	void stopSilvia()
	{
		//silvia.framerate = 0;
		if (!configScreenOpen)
		{
			silviaObject.SetActive(false);
			silviaIdleObject.SetActive(true);
		}
		characterIsSpeaking = false;
	}

	public void btnConfigurationClicked()
	{
		silvia.framerate = 0;
		//SoundManager.Instance.Pause();
		Game.playButtonSound();
		Game.LoadScreen("ConfigurationScreen");

		silviaObject.SetActive(false);
		silviaIdleObject.SetActive(false);

		configScreenOpen = true;
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
		configScreenOpen = false;

		if (characterIsSpeaking)
		{
			silviaObject.SetActive(true);
			silviaIdleObject.SetActive(false);
		}
		else
		{
			silviaObject.SetActive(false);
			silviaIdleObject.SetActive(true);
		}
		//SoundManager.Instance.Play(presentationChallenge);
	}
}
