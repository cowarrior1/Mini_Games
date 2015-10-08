using UnityEngine;
using System.Collections;
using Tamboro;

public class btnActionsLevelFeedbackScreen : MonoBehaviour {

	public GUIManager Game;

	public GameObject [] stars;
	public UILabel lblFeedback;

	public GameObject playButton;

	public AudioClip feedback1;
	public AudioClip feedback2;
	public AudioClip feedback3;
	public AudioClip feedback4;
	public AudioClip feedback5;
	public AudioClip feedback6;


	public void btnConfigurationClicked()
	{
		Game.playButtonSound();
		Game.LoadScreen("ConfigurationScreen");
	}

	public void btnHomeClicked()
	{
		Game.playButtonSound();
		Application.LoadLevel("OpeningScreen");
	}

	public void btnPlayClicked()
	{
//		SoundManager.Instance.Pause();
//		Game.playButtonSound();
//		GameData.Instance.showLevelScreenAtStart = true;
//		Application.LoadLevel("OpeningScreen");

		GameData.Instance.currentLevel = GameData.Instance.levelsUnlocked;
		Application.LoadLevel("GameLevel"+GameData.Instance.currentLevel);
	}

	public void btnLockClicked()
	{
		SoundManager.Instance.Pause();
		Game.playButtonSound();
		GameData.Instance.showLevelScreenAtStart = true;
		Application.LoadLevel("OpeningScreen");
	}

	public void btnRankingClicked()
	{
		SoundManager.Instance.Pause();
		Game.playButtonSound();
		Game.LoadScreen("RankingScreenOnGame");
	}

	public void setData(int timeLeft,int totalGameTime,int mistakes)
	{
		int currentLevel = GameData.Instance.currentLevel;

		int score;
		int goodStars = 0;
		if (mistakes == 0)
			goodStars = 3;
		else if(mistakes == 1)
			goodStars = 2;
		else if(mistakes == 2)
			goodStars = 1;

		GameData.Instance.stars[currentLevel-1] = goodStars;
		GameData.Instance.ticks[currentLevel-1] = true;

		for (int i=0;i<goodStars;i++)
			stars[i].GetComponent<UISprite>().spriteName = "img_pic_level_feedback_stars_full";

		if (currentLevel == 1)
		{
			lblFeedback.text = Strings.Instance.getStringByName("positiveFeedback1");
			SoundManager.Instance.Play(feedback1);
			this.gameObject.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "img_box_level_feedback_background_bem";
		}
		else if (currentLevel == 2)
		{
			lblFeedback.text = Strings.Instance.getStringByName("positiveFeedback2");
			SoundManager.Instance.Play(feedback2);
			this.gameObject.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "img_box_level_feedback_background_bem";
		}
		else if (currentLevel == 3)
		{
			lblFeedback.text = Strings.Instance.getStringByName("positiveFeedback3");
			SoundManager.Instance.Play(feedback3);
            this.gameObject.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "img_box_level_feedback_background_perfect";
		}
		else if (currentLevel == 4)
		{
			lblFeedback.text = Strings.Instance.getStringByName("positiveFeedback4");
			SoundManager.Instance.Play(feedback4);
            this.gameObject.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "img_box_level_feedback_background_bem";
		}
		else if (currentLevel == 5)
		{
			lblFeedback.text = Strings.Instance.getStringByName("positiveFeedback5");
			SoundManager.Instance.Play(feedback5);
			this.gameObject.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "img_box_level_feedback_background_bem";
		}
		else if (currentLevel == 6)
		{
			lblFeedback.text = Strings.Instance.getStringByName("positiveFeedback6");
			SoundManager.Instance.Play(feedback6);
			playButton.SetActive(false);
			this.gameObject.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "img_box_level_feedback_background_parabens";
		}

		score = (totalGameTime - timeLeft) * goodStars;

		GameData.Instance.levelsUnlocked++;
		if (GameData.Instance.levelsUnlocked >= 6)
			GameData.Instance.levelsUnlocked = 6;
		GameData.Instance.currentLevel++;
		if (GameData.Instance.currentLevel >= 6)
			GameData.Instance.currentLevel=6;

	}
}
