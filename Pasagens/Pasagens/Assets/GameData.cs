using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class GameData
{
	private static volatile GameData _instance;
	private static object _lock = new object();
	
	static GameData() {} //Stops the lock being created ahead of time if it's not necessary
	
	public static GameData Instance
	{
		get
		{
			if (_instance == null)
			{
				lock(_lock)
				{
					if (_instance == null)
						_instance = new GameData();
				}
			}
			return _instance;
		}
	}
	
	private GameData() {

	}

	public void Initialize() {
		levelsUnlocked = 1;
		stars[0] = 0;
		stars[1] = 0;
		stars[2] = 0;
		stars[3] = 0;
		stars[4] = 0;
		stars[5] = 0;
		ticks[0] = false;
		ticks[1] = false;
		ticks[2] = false;
		ticks[3] = false;
		ticks[4] = false;
		ticks[5] = false;
		showLevelScreenAtStart = false;
	}


	public int levelsUnlocked;
	public int currentLevel;
	public int [] stars = new int[6];
	public bool [] ticks = new bool[6];
	public string clicked;
	public bool showLevelScreenAtStart;

}