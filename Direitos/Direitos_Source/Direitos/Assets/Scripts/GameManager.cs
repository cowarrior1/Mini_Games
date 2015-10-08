using UnityEngine;
using System.Collections;
using Tamboro;
public class GameManager : MonoBehaviour {
	
	public GameObject OpeningScreen;
	public GameObject ConfigurationScreen;
	public GameObject PresentationScreen;
	public GameObject LevelScreen;
	public GameObject TotorialScreen;
	public GameObject RankingScreen;

	public AudioClip buttonClicked;

	private static bool created = false;

	void Awake()
	{
		if (!created)
		{
			DontDestroyOnLoad(this.gameObject);
			GameData.Instance.Initialize();
			created = true;
		}
		else
		{
			Debug.Log("LevelScreen : "+GameData.Instance.showLevelScreenAtStart);
			if (GameData.Instance.showLevelScreenAtStart)
			{
				OpeningScreen.SetActive(false);
				LevelScreen.SetActive(true);
				GameData.Instance.showLevelScreenAtStart = false;
			}
			else
			{
				OpeningScreen.SetActive(true);
				LevelScreen.SetActive(false);
				GameData.Instance.showLevelScreenAtStart = false;
			}

			Destroy(this.gameObject);
		}
	}
    
	// Use this for initialization
	void Start () {

	}

	void OnEnable()
	{	


		ConfigurationScreen.SetActive(false);
		PresentationScreen.SetActive(false);
		TotorialScreen.SetActive(false);
		RankingScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadScreen(string screenName)
	{
		if (screenName == "OpeningScreen")
		{
			OpeningScreen.SetActive(true);
			PresentationScreen.SetActive(false);
			LevelScreen.SetActive(false);
			TotorialScreen.SetActive(false);
			RankingScreen.SetActive(false);
		}
		else if (screenName == "ConfigurationScreen")
		{
			ConfigurationScreen.SetActive(true);
		}
		else if(screenName == "PresentationScreen")
		{
			PresentationScreen.SetActive(true);
			OpeningScreen.SetActive(false);
			LevelScreen.SetActive(false);
			TotorialScreen.SetActive(false);
			RankingScreen.SetActive(false);
		}
		else if (screenName == "LevelScreen")
		{
			PresentationScreen.SetActive(false);
			OpeningScreen.SetActive(false);
			LevelScreen.SetActive(true);
			TotorialScreen.SetActive(false);
			RankingScreen.SetActive(false);
		}
		else if (screenName == "TutorialScreen")
		{
			TotorialScreen.SetActive(true);
		}
		else if (screenName == "RankingScreen")
		{
			RankingScreen.SetActive(true);
		}
	}

	public void playButtonSound()
	{
		SoundManager.Instance.Play(buttonClicked);
	}
}
