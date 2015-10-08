using UnityEngine;
using System.Collections;
using Tamboro;

public class btnActionsChallengeScreen : MonoBehaviour {

	public GUIManager Game;
	public LevelController lcont;
	public AudioClip challengeScreenSound;

	public UILabel lblText;

	void OnEnable()
	{
		lblText.text = Strings.Instance.getStringByName("challengeLevelScreen");
		SoundManager.Instance.Play(challengeScreenSound);

		lcont.ResumeGame();
		this.gameObject.SetActive(false);
	}
	
	public void btnCloseClicked()
	{
		SoundManager.Instance.Pause();
		lcont.ResumeGame();
		Game.playButtonSound();
		this.gameObject.SetActive(false);
	}
}
