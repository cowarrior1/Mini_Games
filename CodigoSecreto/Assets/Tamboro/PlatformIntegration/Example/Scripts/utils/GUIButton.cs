using UnityEngine;
using System.Collections;

namespace PlatformIntegration.Demo
{
	public class GUIButton : MonoBehaviour 
	{
		public GameObject listener;
		public string message;
		
		public string buttonName = "Button Name";
		public Rect rect = new Rect(100, 100, 100, 30);
		
		void OnGUI()
		{
			if(GUI.Button(rect, buttonName))
			{
				if(listener != null && string.IsNullOrEmpty(message) == false)
					listener.SendMessage(message);
			}
		}
	}
}