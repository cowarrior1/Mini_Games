using UnityEngine;
using System.Collections;
using Tamboro;

public class btnActionsTutorialScreenOnGame : MonoBehaviour {

	public GUIManager Game;
	public LevelController lcont;

	public AudioClip tutorial1;
	public AudioClip tutorial2;
	public AudioClip tutorial3;
	public AudioClip tutorial4;

	public UILabel lblTutorial1;
	public UILabel lblTutorial2;
	public UILabel lblTutorial3;
	public UILabel lblTutorial4;

	void OnEnable()
	{
		lblTutorial1.text = Strings.Instance.getStringByName("tutorial1");
		lblTutorial2.text = Strings.Instance.getStringByName("tutorial2");
		lblTutorial3.text = Strings.Instance.getStringByName("tutorial3");
		lblTutorial4.text = Strings.Instance.getStringByName("tutorial4");
	}
	
	public void btnCloseClicked()
	{
		SoundManager.Instance.Pause();
		lcont.ResumeGame();
		Game.playButtonSound();
		this.gameObject.SetActive(false);
		CancelInvoke();
	}

	public void btnTutorial1Clicked()
	{
		SoundManager.Instance.Pause();
		Game.playButtonSound();
		SoundManager.Instance.Play(tutorial1);
	}

	public void btnTutorial2Clicked()
	{
		SoundManager.Instance.Pause();
		Game.playButtonSound();
		SoundManager.Instance.Play(tutorial2);
	}

	public void btnTutorial3Clicked()
	{
		SoundManager.Instance.Pause();
		Game.playButtonSound();
		SoundManager.Instance.Play(tutorial3);
	}

	public void btnTutorial4Clicked()
	{
		SoundManager.Instance.Pause();
		Game.playButtonSound();
		SoundManager.Instance.Play(tutorial4);
	}
}
