using UnityEngine;
using System.Collections;
using Tamboro;

public class btnActionsFailScreen : MonoBehaviour {
	
	public GUIManager Game;
	public UILabel lblText;
	public LevelController lcont;
	public AudioClip errorSound;
	public AudioClip failScreenSound;

	void OnEnable()
	{
		SoundManager.Instance.Play(errorSound);
		lblText.text = Strings.Instance.getStringByName("failScreenMistakes");
		Invoke("playnnow",0.5f);
	}

	public void btnCloseClicked()
	{
		SoundManager.Instance.Pause();
		Game.playButtonSound();
		this.gameObject.SetActive(false);
	}
	
	public void btnRestartClicked()
	{
		SoundManager.Instance.Pause();
		Game.playButtonSound();
		Application.LoadLevel(Application.loadedLevel);
	}

	public void btnHomeClicked()
	{
		SoundManager.Instance.Pause();
		Game.playButtonSound();
		Application.LoadLevel("OpeningScreen");
		Debug.Log("Loading : OpeningScreen, CurrentLevel : "+GameData.Instance.currentLevel);
	}

	public void setData(string reason)
	{
		Time.timeScale = 1;
		lcont.IHAVEMASTERCONTROL = false;

		SoundManager.Instance.Play(errorSound);
		Invoke("playnnow",0.5f);
		if (reason == "time")
		{
			lblText.text = Strings.Instance.getStringByName("failScreenTime");
		}
		else if(reason == "mistakes")
		{
			lblText.text = Strings.Instance.getStringByName("failScreenMistakes");
		}
	}

	void playnnow()
	{
		SoundManager.Instance.Play(failScreenSound);
	}
}