using UnityEngine;
using System.Collections;

public class clickListener : MonoBehaviour {

	public LevelController levelController;
	GameObject obj;

	int iSource;
	int iDestination;

	string positionClicked;
	string positionReleased;

	// Use this for initialization
	void Start () {
		obj = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void itemClicked() {

		increaseDepth();
		if (GameData.Instance.currentLevel == 1)
		{
			if (obj.transform.localPosition.x <= -140f)
			{
				positionClicked = "Position1";
				iSource = 0;
			}
			else if (obj.transform.localPosition.x > -140f && obj.transform.localPosition.x <= 150f)
			{
				positionClicked = "Position2";
				iSource = 1;
			}
			else if (obj.transform.localPosition.x > 150f)
			{
				positionClicked = "Position3";
				iSource = 2;
			}
		}
		else
		{
			if (obj.transform.localPosition.x <= -241f)
			{
				positionClicked = "Position1";
				iSource = 0;
			}
			else if (obj.transform.localPosition.x > -241f && obj.transform.localPosition.x <= -8.1f)
			{
				positionClicked = "Position2";
				iSource = 1;
			}
			else if (obj.transform.localPosition.x > -8.1f && obj.transform.localPosition.x <= 227f)
			{
				positionClicked = "Position3";
				iSource = 2;
			}
			else if (obj.transform.localPosition.x > 227f)
			{
				positionClicked = "Position4";
				iSource = 3;
			}

			Debug.Log("Position Clicked : "+positionClicked);
			Debug.Log("iSource : "+iSource);
			Debug.Log("X pos : "+obj.transform.localPosition.x);
		}
	}

	public void itemReleased() {

		Invoke("decreaseDepth",0.3f);

		if (obj.transform.localPosition.y > -125f  && obj.transform.localPosition.y < 190f) 
		{
			if (GameData.Instance.currentLevel == 1)
			{
				if (obj.transform.localPosition.x <= -140f)
				{
					positionReleased = "Position1";
					iDestination = 0;
				}
				else if (obj.transform.localPosition.x > -140f && obj.transform.localPosition.x <= 150f)
				{
					positionReleased = "Position2";
					iDestination = 1;
				}
				else if (obj.transform.localPosition.x > 150f)
				{
					positionReleased = "Position3";
					iDestination = 2;
				}
			}
			else
			{
				if (obj.transform.localPosition.x <= -241f)
				{
					positionReleased = "Position1";
					iDestination = 0;
				}
				else if (obj.transform.localPosition.x > -241f && obj.transform.localPosition.x <= -8.1f)
				{
					positionReleased = "Position2";
					iDestination = 1;
				}
				else if (obj.transform.localPosition.x > -8.1f && obj.transform.localPosition.x <= 227f)
				{
					positionReleased = "Position3";
					iDestination = 2;
				}
				else if (obj.transform.localPosition.x > 227f)
				{
					positionReleased = "Position4";
					iDestination = 3;
				}
			}

			Debug.Log("Position Clicked : "+positionClicked+" , "+"Game object : "+gameObject.name);
			if (positionClicked == positionReleased)
				TweenPosition.Begin(gameObject,0.3f,GameObject.Find(positionClicked).transform.localPosition);
			else
			{
				if (gameObject.name[5] == positionReleased[8])
				{//good move
					Debug.Log("Good Move");
				}
				else
				{
					Debug.Log("Bad Move");
				}

				levelController.AddMove();

				levelController.sceneItemMovement(iSource,iDestination,positionReleased,positionClicked);
			}
		}
		else
			TweenPosition.Begin(this.gameObject,0.3f,GameObject.Find(positionClicked).transform.localPosition);
	}

	void increaseDepth()
	{
		gameObject.GetComponent<UISprite>().depth += 5;
	}

	void decreaseDepth()
	{
		gameObject.GetComponent<UISprite>().depth -= 5;
	}
}
