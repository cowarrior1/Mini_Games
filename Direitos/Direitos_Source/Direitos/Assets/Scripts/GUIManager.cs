using UnityEngine;
using System.Collections;
using Tamboro;

public class GUIManager : MonoBehaviour {

	public GameObject ConfigurationScreen;
	public GameObject ConfigurationScreenOnPauseScreen;
	public GameObject TutorialScreenOnGame;
	public GameObject PauseScreen;
	public GameObject RankingScreenOnGame;
	public GameObject QuitScreen;
	public GameObject PositivePuzzleFeedbackScreen;
	
	public AudioClip buttonClicked;
	// Use this for initialization
	void Start () {
		ConfigurationScreen.SetActive(false);
		ConfigurationScreenOnPauseScreen.SetActive(false);
		PauseScreen.SetActive(false);
		TutorialScreenOnGame.SetActive(false);
		RankingScreenOnGame.SetActive(false);
		QuitScreen.SetActive(false);
		PositivePuzzleFeedbackScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadScreen(string name)
	{
		if(name.Contains("ConfigurationScreenOnPauseScreen"))
		{
			ConfigurationScreenOnPauseScreen.SetActive(true);
		}
		else if (name.Contains("ConfigurationScreen"))
		{
			ConfigurationScreen.SetActive(true);
		}
		else if(name.Contains("PauseScreen"))
		{
			PauseScreen.SetActive(true);
		}
		else if(name.Contains("ConfigurationScreenOnPauseScreen"))
		{
			ConfigurationScreenOnPauseScreen.SetActive(true);
		}
		else if(name.Contains("TutorialScreenOnGame"))
		{
			TutorialScreenOnGame.SetActive(true);
		}
		else if(name.Contains("RankingScreenOnGame"))
		{
			RankingScreenOnGame.SetActive(true);
		}
		else if(name.Contains("QuitScreen"))
		{
			QuitScreen.SetActive(true);
		}
		else if(name.Contains("PositivePuzzleFeedbackScreen"))
		{
			PositivePuzzleFeedbackScreen.SetActive(true);
		}
	}

	public void playButtonSound()
	{
		SoundManager.Instance.Play(buttonClicked);
	}
}
