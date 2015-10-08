using UnityEngine;
using System.Collections;

namespace PlatformIntegration.Demo
{
	public class TextFieldWithName : MonoBehaviour 
	{
		public bool forceIntValue = false;
		public string fieldName = "field name";
		public Rect rect = new Rect(100, 100, 200, 10);
		
		private string text = ""; 
		
		void Start()
		{
			if(forceIntValue) text = "0";
		}
		
		void OnGUI()
		{
			if(forceIntValue)
			{
				int intValue = 0;
				string textTemp = GUI.TextField(rect, text);
				if(string.IsNullOrEmpty(textTemp)) textTemp = "0";
				bool parsed = int.TryParse(textTemp, out intValue);
				if(parsed) text = textTemp;
			}
			else
			{
				text = GUI.TextField(rect, text);
			}
			
			GUI.Label(new Rect(rect.xMax + 10, rect.yMin, rect.width*100, rect.height*100), "" + fieldName);
		}
		
		public int integerValue
		{
			get {
				return int.Parse(text);
			}
		}
		
		public string textValue
		{
			get {
				return text;
			}
		}
	}
}