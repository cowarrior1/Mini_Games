using UnityEngine;
using System.Collections;
using Tamboro;

public class btnActionsRankingScreenOnGame : MonoBehaviour {

	public GUIManager Game;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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

	public void btnLevelsClicked()
	{
		Game.playButtonSound();
		Application.LoadLevel("OpeningScreen");
	}
}
