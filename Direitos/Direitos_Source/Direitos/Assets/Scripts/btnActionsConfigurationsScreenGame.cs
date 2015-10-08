using UnityEngine;
using System.Collections;
using Tamboro;

public class btnActionsConfigurationsScreenGame : MonoBehaviour {

	public GUIManager Game;
	public LevelController lcont;

	public void btnCloseClicked()
	{
		lcont.ResumeGame();
		Game.playButtonSound();
		this.gameObject.SetActive(false);
	}

	public void btnRestartClicked()
	{
		Game.playButtonSound();
		Application.LoadLevel(Application.loadedLevel);
	}

	public void btnSoundOnClicked()
	{
		Game.playButtonSound();
		SoundManager.Instance.Unmute();
	}
	
	public void btnSoundOffClicked()
	{
		Game.playButtonSound();
		SoundManager.Instance.Mute();
	}
}
