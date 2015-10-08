using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace PlatformIntegration.Demo
{
	public class DebugConsole : MonoBehaviour 
	{
		[SerializeField]
		private bool enable = true;
		
		[SerializeField]
		private Rect rect = new Rect(10, 10, 1000, 700);
		
		[SerializeField]
		private Vector2 sizeOfButton = new Vector2(100, 20);
		
		private static List<string> logs = new List<string>();
		private static List<string> tags = new List<string>();
		
		private int indexChosen;
		
		private Rect rectOfButtons;
		private Rect rectOfBoxText;
		
		Vector2 scrollPosition;
		
		private GUIStyle styleOfButton;
		
		private GUIStyle styleOfButtonSelected;
		
		private GUIStyle styleOfToggle;
		
		private bool on = true;
		
		void Awake()
		{
			DontDestroyOnLoad(this.gameObject);
			Append("Debug", "");
			scrollPosition = Vector2.zero;
			rectOfButtons = new Rect(rect.xMax - sizeOfButton.x, rect.y + 90, sizeOfButton.x, rect.height);
			rectOfBoxText = new Rect(rect.x, rect.y, rect.width - sizeOfButton.x, rect.height);	
			on = true;
		}
		
		void OnGUI()
		{
			int originalDepth = GUI.depth;
			Color originalGUIColor = GUI.color;
			
			if(enable)
			{
				if(styleOfButton == null)
				{
					styleOfButton = GUI.skin.button;
					styleOfButton.fontSize = 10;
				}
				if(styleOfToggle == null)
				{
					styleOfToggle = GUI.skin.toggle;
					styleOfToggle.fontSize = 8;
					styleOfToggle.alignment = TextAnchor.UpperLeft;
				}
				on = GUI.Toggle(new Rect(rectOfButtons.x, rect.y, rectOfButtons.width, 30), on, "Debug Console\nAberto", styleOfToggle); 
				
				if(on)
				{
					for (int i = 0; i < tags.Count; i++) 
					{
						if(indexChosen == i)
							GUI.color = Color.red;
						else
							GUI.color = originalGUIColor;
						
						bool buttonSelected = GUI.Button(new Rect(rectOfButtons.x, rectOfButtons.y + (i*sizeOfButton.y), rectOfButtons.width, sizeOfButton.y), tags[i].ToString(), styleOfButton);
						if(buttonSelected)
						{
							indexChosen = i;
							break;
						}
					}
					
					
					GUI.color = originalGUIColor;
					
					if(GUI.Button(new Rect(rectOfButtons.x, rectOfButtons.y - 60, rectOfButtons.width, sizeOfButton.y), "Clear"))
						logs[indexChosen] = "";
					
					GUI.skin.box.alignment = TextAnchor.UpperLeft;
			        scrollPosition = GUI.BeginScrollView(rectOfBoxText, scrollPosition, new Rect(rectOfBoxText.x, rectOfBoxText.y, 10000, 50000));
	                GUI.color = Color.red;
			        GUI.Box(new Rect(rectOfBoxText.x, rectOfBoxText.y, 10000, 50000), logs[indexChosen]);
			        GUI.EndScrollView();
				}
			}
			GUI.color = originalGUIColor;
			GUI.depth = originalDepth;
		}
		
		public static void Append(string tag, string text)
		{
			for (int i = 0; i < tags.Count; i++) 
			{
				if(tag == tags[i])
				{
					logs[i] += ("\n" + text);
					goto Finish;
				}
			}
			
			tags.Add(tag);
			logs.Add("");
			logs[tags.Count-1] += ("*** start ***\n\n" + text);
			
		Finish:
			int s = 0;
			s++;
		}
		
		public static void Append(string text)
		{
			Append("Debug", text);
		}
	}
}