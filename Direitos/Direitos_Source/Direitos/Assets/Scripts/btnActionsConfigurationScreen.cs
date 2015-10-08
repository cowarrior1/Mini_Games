using UnityEngine;
using System.Collections;
using Tamboro;

public class btnActionsConfigurationScreen : MonoBehaviour {

	public GameManager Game;
	public GameObject silvia;

	public void btnCloseClicked()
	{
		if (GameObject.Find("PresentationScreen") != null)
			GameObject.Find("PresentationScreen").GetComponent<btnActionsPresentationScreen>().ConfigurationScreenClosed();
		Game.playButtonSound();
		this.gameObject.SetActive(false);
		silvia.SetActive(true);
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
