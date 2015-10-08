using UnityEngine;
using System.Collections;
using Tamboro;

public class btnActionsLevelScreen : MonoBehaviour {

	public GameManager Game;
	public AudioClip errorSound;

	public void btnConfigurationClicked()
	{
		Game.playButtonSound();
		Game.LoadScreen("ConfigurationScreen");
	}

	public void btnHomeClicked()
	{
		Game.playButtonSound();
		Game.LoadScreen("OpeningScreen");
	}

	public void btnTutorialClicked()
	{
		Game.playButtonSound();
		Game.LoadScreen("TutorialScreen");
	}

	public void btnRankingClicked()
	{
		Game.playButtonSound();
		Game.LoadScreen("RankingScreen");
	}

	public void Level1Clicked()
	{
		LevelCLickHandle(1);
	}
	public void Level2Clicked()
	{
		LevelCLickHandle(2);
	}
	public void Level3Clicked()
	{
		LevelCLickHandle(3);
	}
	public void Level4Clicked()
	{
		LevelCLickHandle(4);
	}
	public void Level5Clicked()
	{
		LevelCLickHandle(5);
	}
	public void Level6Clicked()
	{
		LevelCLickHandle(6);
	}

	public void btnPlayClicked()
	{
		GameData.Instance.currentLevel = GameData.Instance.levelsUnlocked;
		Application.LoadLevel("GameLevel"+GameData.Instance.currentLevel);
		Debug.Log("Loading : "+GameData.Instance.currentLevel);
	}

	void LevelCLickHandle(int i)
	{
		Debug.Log("here");
		if (GameData.Instance.levelsUnlocked >= i)
		{
			Game.playButtonSound();
			GameData.Instance.currentLevel = i;
			Application.LoadLevel("GameLevel"+i);
			Debug.Log("Loading : "+i);
		}
		else
		{
			SoundManager.Instance.Play(errorSound);
		}
	}
}
