using UnityEngine;
using System.Collections;
using Tamboro;

public class btnActionsConfigurationScreenOnPauseScren : MonoBehaviour {

	public GUIManager Game;
	public LevelController lcont;
	
	public void btnCloseClicked()
	{
		Game.playButtonSound();
		this.gameObject.SetActive(false);
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
