using UnityEngine;
using System.Collections;

public class clickListener : MonoBehaviour {

	public LevelController levelController;
	GameObject obj;
	Vector3 startPosition;

	GameObject objToMaximize;



	// Use this for initialization
	void Start () {
		obj = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void itemClicked() {

		startPosition = obj.transform.localPosition;

		obj.GetComponent<UISprite>().spriteName = "img_obj_"+obj.name+"_click";

		levelController.pause();
	}

	public void itemReleased() {

		if (obj.transform.localPosition.y > -120f)
		{
			int positionIndex = levelController.getPositionIndex(obj.name);
			if (positionIndex != -1)
			{
				TweenPosition.Begin(obj,0.3f,levelController.positions[positionIndex].transform.localPosition);
				objToMaximize = obj;
				Invoke("maximize",0.3f);
				int itemIndex = levelController.getItemIndex(obj.name);
				levelController.playCorrectSound();
				levelController.items.RemoveAt(itemIndex);
				levelController.positions.RemoveAt(positionIndex);
				if (levelController.positions.Count == 0)
					levelController.itemsFinished();
			}
			else
			{
				TweenPosition.Begin(obj,0.2f,startPosition);
				levelController.playWrongSound();
				levelController.mistake();
			}
		}
		else
		{
			TweenPosition.Begin(obj,0.2f,startPosition);
		}

		obj.GetComponent<UISprite>().spriteName = "img_obj_"+obj.name+"_normal";

		Invoke("play",0.22f);
	}

	void maximize()
	{
		objToMaximize.GetComponent<UISprite>().spriteName = "img_pic_"+objToMaximize.name;
		objToMaximize.GetComponent<UISprite>().MakePixelPerfect();
		objToMaximize.GetComponent<UISprite>().depth -= 5;
		if (objToMaximize.GetComponent<UISprite>().depth <= 0)
			objToMaximize.GetComponent<UISprite>().depth = 1;
	}

	void play()
	{
		levelController.playAgain();
	}
}
