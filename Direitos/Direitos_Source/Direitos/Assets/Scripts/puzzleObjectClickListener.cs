using UnityEngine;
using System.Collections;

public class puzzleObjectClickListener : MonoBehaviour {

	public LevelController levelController;
	GameObject obj;
	Vector3 startPosition;
	// Use this for initialization
	void Start () {
		obj = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void itemClicked()
	{
		startPosition = obj.transform.localPosition;
		levelController.bringToFront(obj);
		levelController.currentPuzzleItem = obj;
		//Debug.Log("Name : "+obj.name);
	}

	public void itemReleased()
	{
		if (levelController.isInPuzzleZone(obj))
		{
			string index = levelController.getThePositionIndex(obj);
			TweenPosition.Begin(obj,0.3f,GameObject.Find("targetPosition"+index).transform.localPosition);

			if (""+obj.name[6] == index && obj.transform.localRotation.eulerAngles.z == 0)
			{
				Debug.Log("Good Puzzle Move");
				levelController.puzzleItemsState[int.Parse(index) - 1] = true;
			}
			else 
			{
				Debug.Log("Bad Puzzle Move");
				levelController.puzzleItemsState[int.Parse(index) - 1] = false;
			}
        }
		else
		{
			TweenPosition.Begin(obj,0.3f,startPosition);
		}
	}
}
