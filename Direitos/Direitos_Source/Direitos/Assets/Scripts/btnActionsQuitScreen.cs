using UnityEngine;
using System.Collections;
using Tamboro;

public class btnActionsQuitScreen : MonoBehaviour {

	public GUIManager Game;
	public LevelController lcont;

	public UILabel lblText;
	public AudioClip quitScreenSoundReload;
	public AudioClip quitScreenSoundHome;

	void OnEnable()
	{
		if (GameData.Instance.clicked == "home")
		{
			lblText.text = Strings.Instance.getStringByName("quitScreenHome");
			SoundManager.Instance.Play(quitScreenSoundHome);
		}
		else
		{
			lblText.text = Strings.Instance.getStringByName("quitScreenRestart");
			SoundManager.Instance.Play(quitScreenSoundReload);
		}
	}

	public void btnCloseClicked()
	{
		SoundManager.Instance.Pause();
		lcont.ResumeGame();
		SoundManager.Instance.Pause();
		Game.playButtonSound();
		this.gameObject.SetActive(false);
	}

	public void btnYesClicked()
	{
		SoundManager.Instance.Pause();
		SoundManager.Instance.Pause();
		if (GameData.Instance.clicked == "home")
			Application.LoadLevel("OpeningScreen");
		else
			Application.LoadLevel(Application.loadedLevel);
	}
}
