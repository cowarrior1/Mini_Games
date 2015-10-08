using UnityEngine;
using System.Collections;

public class LevelScreen : MonoBehaviour {

	public GameObject [] stars;
	public GameObject [] ticks;

	public GameManager Game;

	// Use this for initialization
	void OnEnable () {

//		for (int i=0;i<ticks.Length;i++)
//			ticks[i].GetComponent<UISprite>().spriteName = "img_pic_level_locker";

		for (int i=0;i<stars.Length;i++)
			stars[i].SetActive(true);

		for (int i=0;i<GameData.Instance.levelsUnlocked;i++)
		{
			//locks[i].SetActive(false);
			//ticks[i].GetComponent<UISprite>().spriteName = "img_pic_level_ok";
			ticks[i].SetActive(false);
			//ticks[i].GetComponent<UISprite>().MakePixelPerfect();

//			for (int j=0;j<3;j++ )
//			{
//				int val = j+(i*3);
//				stars[val].gameObject.transform.parent.gameObject.SetActive(true);
//				stars[val].SetActive(true);
//			}

			for (int j=0;j<GameData.Instance.stars[i];j++)
			{
				int val = j+(i*3);
				if (GameData.Instance.stars[i] != 0)
					stars[val].GetComponent<UISprite>().spriteName = "img_feedback_01_star_full";
				ticks[i].GetComponent<UISprite>().MakePixelPerfect();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
