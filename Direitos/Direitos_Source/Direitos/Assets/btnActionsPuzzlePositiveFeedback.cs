using UnityEngine;
using System.Collections;
using Tamboro;

public class btnActionsPuzzlePositiveFeedback : MonoBehaviour {

	public GUIManager Game;
	public LevelController lcont;
	public UILabel lblText;
	public AudioClip applauseSound;

	void OnEnable()
	{
		lblText.text = Strings.Instance.getStringByName("positivePuzzleFeedback"+GameData.Instance.currentLevel);
		SoundManager.Instance.Play(applauseSound);
	}
	
	public void btnCloseClicked()
	{
		SoundManager.Instance.Pause();
		lcont.ResumeGame();
		Game.playButtonSound();
		this.gameObject.SetActive(false);
	}
}
