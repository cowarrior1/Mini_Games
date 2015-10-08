using UnityEngine;
using System.Collections;
using Tamboro;

public class btnActionsLevelFeedbackScreen : MonoBehaviour {

	public GUIManager Game;
	public GameObject [] stars;
	public UILabel lblFeedback;
	public UILabel lblTitle;
	public GameObject playButton;
	public AudioClip feedback1;
	public AudioClip feedback2;
	public AudioClip feedback3;
	public AudioClip feedback4;
	public AudioClip feedback5;
	
	public void btnConfigurationClicked()
	{
		Game.playButtonSound();
		Game.LoadScreen("ConfigurationScreen");
	}

	public void btnHomeClicked()
	{
		SoundManager.Instance.Pause();
		Game.playButtonSound();
		Application.LoadLevel("OpeningScreen");
		Debug.Log("Loading : OpeningScreen, CurrentLevel : "+GameData.Instance.currentLevel);
	}

	public void btnPlayClicked()
	{
		SoundManager.Instance.Pause();
		Game.playButtonSound();
		GameData.Instance.showLevelScreenAtStart = true;
		Application.LoadLevel("OpeningScreen");
		Debug.Log("Loading : OpeningScreen, CurrentLevel : "+GameData.Instance.currentLevel);
	}

	public void btnRankingClicked()
	{
		SoundManager.Instance.Pause();
		Game.playButtonSound();
		Game.LoadScreen("RankingScreenOnGame");
	}

	public void btnLevelsClicked()
	{
		SoundManager.Instance.Pause();
		Game.playButtonSound();
		GameData.Instance.showLevelScreenAtStart = true;
		Application.LoadLevel("OpeningScreen");
	}

	public void btnRestartClicked()
	{
		SoundManager.Instance.Pause();
		Game.playButtonSound();
		Application.LoadLevel(Application.loadedLevel);
	}

	public void setData(int timespan)
	{
		int currentLevel = GameData.Instance.currentLevel;

		int goodStars = 0;
		if (timespan < 15)
			goodStars = 3;
		else if(timespan >= 15 && timespan <25)
			goodStars = 2;
		else if(timespan >= 25)
			goodStars = 1;

		GameData.Instance.stars[currentLevel-1] = goodStars;
		GameData.Instance.ticks[currentLevel-1] = true;

		for (int i=0;i<goodStars;i++)
			stars[i].GetComponent<UISprite>().spriteName = "img_feedback_01_star_full";

		//lblTitle.text = "MUITO BEM!";
		lblTitle.text = "";

		if (currentLevel == 1)
		{
			lblFeedback.text = Strings.Instance.getStringByName("positiveFeedback1");
			SoundManager.Instance.Play(feedback1);
			//this.gameObject.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "img_box_level_feedback_background_bom";
		}
		else if (currentLevel == 2)
		{
			lblFeedback.text = Strings.Instance.getStringByName("positiveFeedback2");
			SoundManager.Instance.Play(feedback2);
			//this.gameObject.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "img_box_level_feedback_background_bem";
		}
		else if (currentLevel == 3)
		{
			lblFeedback.text = Strings.Instance.getStringByName("positiveFeedback3");
			SoundManager.Instance.Play(feedback3);
			//this.gameObject.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "img_box_level_feedback_background_perfect";
		}
		else if (currentLevel == 4)
		{
			lblFeedback.text = Strings.Instance.getStringByName("positiveFeedback4");
			SoundManager.Instance.Play(feedback4);
			//this.gameObject.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "img_box_level_feedback_background_otimo";
		}
		else if (currentLevel == 5)
		{
			lblFeedback.text = Strings.Instance.getStringByName("positiveFeedback5");
			SoundManager.Instance.Play(feedback5);
			playButton.SetActive(false);
			//lblTitle.text = "PARABÉNS!";
			lblTitle.text = "";
		}

		if (GameData.Instance.currentLevel == GameData.Instance.levelsUnlocked)
		{
			GameData.Instance.levelsUnlocked++;
			if (GameData.Instance.levelsUnlocked >= GameData.Instance.totalLevels)
				GameData.Instance.levelsUnlocked = GameData.Instance.totalLevels;
			GameData.Instance.currentLevel++;
			if (GameData.Instance.currentLevel >= GameData.Instance.totalLevels)
				GameData.Instance.currentLevel=GameData.Instance.totalLevels;
		}

	}
}
