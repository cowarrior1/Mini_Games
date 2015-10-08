using UnityEngine;
using System.Collections;
using Tamboro;

public class LevelController : MonoBehaviour {
	
	public System.Collections.Generic.List<GameObject> items;
	public System.Collections.Generic.List<GameObject> positions;

	public GUIManager _GUI;

	public AudioClip correct;
	public AudioClip wrong;

	public GameObject progressBar;


	public float speed;

	public float gameTime;
	float totalGameTime;

	public bool play = true;

	public GameObject FailScreen;
	public GameObject LevelFeedbackScreen;

	public bool IHAVEMASTERCONTROL = true;

	int mistakes = 0;

	public UILabel lblTime;

	// Use this for initialization
	void Start () {
		InvokeRepeating("CountDown",0.2f,1f);
		totalGameTime = gameTime;
		Time.timeScale = 1;
	}

	void CountDown()
	{
		if (IHAVEMASTERCONTROL)
		{
			gameTime--;
			lblTime.text = ""+gameTime;
			progressBar.GetComponent<UIProgressBar>().value = (gameTime / totalGameTime);

			if (gameTime <= 10)
			{
				TweenAlert(progressBar);
			}
			if (gameTime == 0)
			{
				GameOver();
				if (positions.Count > 0)
				{
					FailScreen.SetActive(true);
					FailScreen.GetComponent<btnActionsFailScreen>().setData("time");
				}
			}
		}
	}

	void GameOver()
	{
		Time.timeScale = 0;
		CancelInvoke();
		play = false;
		StopCoroutine("ResetTween");
	}

	public void mistake()
	{
		mistakes++;
		if (mistakes >= 3)
		{
			GameOver();
			FailScreen.SetActive(true);
			FailScreen.GetComponent<btnActionsFailScreen>().setData("mistakes");
		}
	}

	public void pause()
	{
		play = false;
	}

	public void playAgain()
	{
		play = true;
	}

	public void playCorrectSound()
	{
		SoundManager.Instance.Play(correct);
	}

	public void playWrongSound()
	{
		SoundManager.Instance.Play(wrong);
	}

	public void itemsFinished()
	{
		//win
		play = false;
		CancelInvoke();
		LevelFeedbackScreen.SetActive(true);
		LevelFeedbackScreen.GetComponent<btnActionsLevelFeedbackScreen>().setData((int)gameTime,(int)totalGameTime,(int)mistakes);
	}

	public int getPositionIndex(string objname)
	{
		for(int i=0;i<positions.Count;i++)
		{
			if (positions[i] != null )
				if(positions[i].name.Contains(objname))
					return i;
		}

		return -1;
	}

	public int getItemIndex(string objname)
	{
		for(int i=0;i<items.Count;i++)
		{
			if (items[i] != null )
				if(items[i].name.Contains(objname))
                    return i;
        }
        
        return -1;
    }

	public void PauseGame()
	{
		//Time.timeScale = 0;
		IHAVEMASTERCONTROL = false;
		play = false;
	}

	public void ResumeGame()
	{
		//Time.timeScale = 1;
		IHAVEMASTERCONTROL = true;
		play = true;
	}

	// Update is called once per frame
	void Update () {

		if (play && positions.Count > 0 && IHAVEMASTERCONTROL)
		{
			foreach (GameObject objs in items)
				objs.transform.position = new Vector2(objs.transform.position.x - (speed),objs.transform.position.y);

			GameObject obj = items[0]; // The Leftmost item
			GameObject poss = positions[0]; // The Leftmost position

			if( obj.transform.position.x < -2.0f )
			{
				Vector3 pos = obj.transform.position;
				GameObject lastItem = items[items.Count-1];
				if ( lastItem.transform.position.x < 1.22f )
				{
					pos.x = 2.0f;
					obj.transform.position = pos;
					//obj.transform.position.x = 1.7f;
				}
				else
				{
					pos.x = lastItem.transform.position.x + 0.6f;
					obj.transform.position = pos;
				}
				
				items.RemoveAt(0);
				positions.RemoveAt(0);
				items.Add(obj);
				positions.Add(poss);
			}
		}
	}

	public void TweenAlert(GameObject obj)
	{
		//0.2f,1.2f
		TweenScale.Begin(obj,0.5f,new Vector2(1.08f,1.08f));
		StartCoroutine(ResetTween(obj,0.5f,1.08f));
	}
	
	IEnumerator ResetTween(GameObject obj,float time,float scale)
    {
        yield return new WaitForSeconds(time);
        TweenScale.Begin(obj,time,new Vector2(1f,1f));
    }

	public void btnConfigurationClicked()
	{
		PauseGame();
		_GUI.LoadScreen("ConfigurationScreen");
		_GUI.playButtonSound();
	}

	public void btnTutorialClicked()
	{
		PauseGame();
		_GUI.LoadScreen("TutorialScreenOnGame");
		_GUI.playButtonSound();
	}

	public void btnHomwClicked()
	{
		GameData.Instance.clicked = "home";
		PauseGame();
		_GUI.playButtonSound();
		_GUI.LoadScreen("QuitScreen");
	}

	public void btnRestartClciked()
	{
		GameData.Instance.clicked = "restart";
		PauseGame();
		_GUI.playButtonSound();
		_GUI.LoadScreen("QuitScreen");
	}

	public void btnPauseClicked()
	{
		PauseGame();
		_GUI.LoadScreen("PauseScreen");
		_GUI.playButtonSound();
	}
}
