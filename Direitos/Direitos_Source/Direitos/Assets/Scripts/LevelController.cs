using UnityEngine;
using System.Collections;
using Tamboro;

public class LevelController : MonoBehaviour {


	public GUIManager _GUI;

	public AudioClip correct;
	public AudioClip wrong;

	string index;
	public bool play = true;

	public AudioClip puzzleAttachSound;

	public GameObject FailScreen;
	public GameObject LevelFeedbackScreen;
	public GameObject PositivePuzzleFeedbackScreen;

	public bool IHAVEMASTERCONTROL = true;
	bool puzzleDone = false;

	public UILabel lblMovements;
	public UILabel lblTime;
	public UILabel lblPuzzleText;
	float time;
	int movements;

	public UILabel lblTitleText;

	public GameObject [] sceneItems;
	private bool [] sceneItemsState;
	public GameObject [] puzzleItems;
	public GameObject puzzleFinal;
	public bool [] puzzleItemsState;

	public GameObject LT;
	public GameObject RT;
	public GameObject LB;
	public GameObject RB;
	public GameObject MT;
	public GameObject M;
	public GameObject MB;

	Vector3 leftTop;
	Vector3 rightTop;
	Vector3 leftBottom;
	Vector3 rightBottom;
	Vector3 middleTop;
	Vector3 middle;
	Vector3 middleBottom;

	public GameObject currentPuzzleItem;

	public AudioClip challengeSound;

	// Use this for initialization
	void Start () {

		sceneItemsState = new bool[3] {false,false,false};
		puzzleItemsState = new bool[4] {false,false,false,false};

		leftTop = LT.transform.localPosition;
		rightTop = RT.transform.localPosition;
		leftBottom = LB.transform.localPosition;
		rightBottom = RB.transform.localPosition;
		middleTop = MT.transform.localPosition;
		middle = M.transform.localPosition;
		middleBottom = MB.transform.localPosition;

		IHAVEMASTERCONTROL = true;
		play = true;

		SoundManager.Instance.Play(challengeSound);

		time = 0;
		movements = 0;
		InvokeRepeating("CountDown",0.3f,1f);

		lblTitleText.text = Strings.Instance.getStringByName("challengeLevelScreen");
		lblPuzzleText.text = Strings.Instance.getStringByName("PuzzleText"+GameData.Instance.currentLevel);
	}

	void CountDown()
	{
		if (IHAVEMASTERCONTROL)
		{
			time++;
			lblTime.text = "" + (int)time;
		}
	}

	public void sceneItemMovement(int iSource, int iDestination,string positionReleased,string positionClicked)
	{
		TweenPosition.Begin(sceneItems[iSource],0.3f,GameObject.Find(positionReleased).transform.localPosition);
		TweenPosition.Begin(sceneItems[iDestination],0.3f,GameObject.Find(positionClicked).transform.localPosition);

		GameObject temp = sceneItems[iDestination];
		sceneItems[iDestination] = sceneItems[iSource];
		sceneItems[iSource] = temp;
	}

	public bool isInPuzzleZone(GameObject obj)
	{
		Vector3 pos = obj.transform.localPosition;
		if (pos.x >= leftTop.x && pos.x <= rightTop.x
		    && pos.y <= leftTop.y && pos.y >= leftBottom.y)
		{
			return true;
		}
		return false;
	}

	public string getThePositionIndex(GameObject obj)
	{
		Vector3 pos = obj.transform.localPosition;
		if (pos.x >= leftTop.x && pos.x <= middleTop.x
		    && pos.y <= leftTop.y && pos.y >= middle.y)
		{
			return "1";
		}
		else if (pos.x > middleTop.x && pos.x <= rightTop.x
		         && pos.y < middleTop.y && pos.y >= middle.y)
		{
			return "2";
		}
		else if (pos.x > leftBottom.x && pos.x <= middleBottom.x
		         && pos.y < middle.y && pos.y >= middleBottom.y)
		{
			return "3";
		}
		else if (pos.x > middle.x && pos.x <= rightBottom.x
		         && pos.y < middle.y && pos.y >= middleBottom.y)
		{
			return "4";
		}

		return "-1";
	}


	public void playCorrectSound()
	{
		SoundManager.Instance.Play(correct);
	}

	public void playWrongSound()
	{
		SoundManager.Instance.Play(wrong);
	}

	public void PauseGame()
	{
		//Time.timeScale = 0;
		IHAVEMASTERCONTROL = false;
		play = false;
	}

	public void bringToFront(GameObject obj)
	{
		foreach(GameObject item in puzzleItems)
			item.GetComponent<UISprite>().depth = 40;

		obj.GetComponent<UISprite>().depth = 41;
	}

	public void ResumeGame()
	{
		//Time.timeScale = 1;
		IHAVEMASTERCONTROL = true;
		play = true;
		Debug.Log("Game Resumed");
	}

	public void AddMove()
	{
		movements++;
		lblMovements.text = ""+movements;
	}

	// Update is called once per frame
	void Update () {
	
		if (IHAVEMASTERCONTROL && !puzzleDone)
		{
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				if (currentPuzzleItem != null)
				{
					index = getThePositionIndex(currentPuzzleItem);
					AddMove();
					currentPuzzleItem.transform.Rotate(new Vector3(0f,0f,-90f));

					if (index != "-1")
                    {
						if (currentPuzzleItem.transform.localEulerAngles.z == 0)
						{
							if (""+currentPuzzleItem.name[6] == index)
							{
								Debug.Log("Good Puzzle Move rotation");
								puzzleItemsState[int.Parse(index) - 1] = true;
	                        }
	                        else
							{
	                            Debug.Log("Bad Puzzle Move rotation");
								puzzleItemsState[int.Parse(index) - 1] = false;
							}
						}
						else
						{
							Debug.Log("Bad Puzzle Move rotation");
							index = getThePositionIndex(currentPuzzleItem);
							puzzleItemsState[int.Parse(index) - 1] = false;
						}
					}
				}
			}
			else if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				if (currentPuzzleItem != null)
                {
					index = getThePositionIndex(currentPuzzleItem);
					AddMove();
					currentPuzzleItem.transform.Rotate(new Vector3(0f,0f,90f));

					if (index != "-1")
					{
                        if (currentPuzzleItem.transform.localEulerAngles.z == 0)
						{
							if (""+currentPuzzleItem.name[6] == index)
							{
								Debug.Log("Good Puzzle Move rotation");
								puzzleItemsState[int.Parse(index) - 1] = true;
	                        }
	                        else
							{
	                            Debug.Log("Bad Puzzle Move rotation");
								puzzleItemsState[int.Parse(index) - 1] = false;
							}
						}
						else
						{
							Debug.Log("Bad Puzzle Move rotation");
							index = getThePositionIndex(currentPuzzleItem);
							puzzleItemsState[int.Parse(index) - 1] = false;
						}
					}
				}
            }

			bool puzzleGood = true;
			foreach (bool state in puzzleItemsState)
				if (!state)
					puzzleGood = false;

			if (puzzleGood)
				PuzzleComplete();
			else
				puzzleDone = false;

		}
	}

	void PuzzleComplete()
	{
		puzzleDone = true;
		foreach(GameObject obj in puzzleItems)
			obj.SetActive(false);
		puzzleFinal.SetActive(true);

		SoundManager.Instance.Play(puzzleAttachSound);
//		PauseGame();
//		PositivePuzzleFeedbackScreen.SetActive(true);
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
		SoundManager.Instance.Pause();
		PauseGame();
		_GUI.LoadScreen("ConfigurationScreen");
		_GUI.playButtonSound();
	}

	public void btnTutorialClicked()
	{
		SoundManager.Instance.Pause();
		PauseGame();
		_GUI.LoadScreen("TutorialScreenOnGame");
		_GUI.playButtonSound();
	}

	public void btnHomwClicked()
	{
		SoundManager.Instance.Pause();
		GameData.Instance.clicked = "home";
		PauseGame();
		_GUI.playButtonSound();
		_GUI.LoadScreen("QuitScreen");
	}

	public void btnRestartClciked()
	{
		SoundManager.Instance.Pause();
		GameData.Instance.clicked = "restart";
		PauseGame();
		_GUI.playButtonSound();
		_GUI.LoadScreen("QuitScreen");
	}

	public void btnPauseClicked()
	{
		SoundManager.Instance.Pause();
		PauseGame();
		_GUI.LoadScreen("PauseScreen");
		_GUI.playButtonSound();
	}

	public void btnFinalizeClicked()
	{
		SoundManager.Instance.Pause();
		PauseGame();
		if (sceneItems[0].name == "Scene1" && 
		    sceneItems[1].name == "Scene2" &&
		   	sceneItems[2].name == "Scene3" &&
		    puzzleItemsState[0] &&
		    puzzleItemsState[1] &&
		    puzzleItemsState[2] &&
		    puzzleItemsState[3])
		{
			LevelFeedbackScreen.SetActive(true);
			LevelFeedbackScreen.GetComponent<btnActionsLevelFeedbackScreen>().setData((int)time);
		}
		else
			FailScreen.SetActive(true);
	}
}
