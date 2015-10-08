using UnityEngine;
using System.Collections;
using Tamboro;

public class btnActionsPauseScreen : MonoBehaviour {

	public UILabel lblText;
	public LevelController lcont;
	public GUIManager Game;
	public AudioClip pauseScreenSound;

	void OnEnable()
	{
		lblText.text = Strings.Instance.getStringByName("pauseScreen");
		SoundManager.Instance.Play(pauseScreenSound);
	}

	public void btnCloseClicked()
	{
		Game.playButtonSound();
		lcont.ResumeGame();
		this.gameObject.SetActive(false);
	}

	public void btnConfigurationClicked()
	{
		Game.playButtonSound();
		SoundManager.Instance.Pause();
		Game.LoadScreen("ConfigurationScreenOnPauseScreen");
	}

	public void btnHomeClicked()
	{
		Game.playButtonSound();
		Application.LoadLevel("OpeningScreen");
	}
}
