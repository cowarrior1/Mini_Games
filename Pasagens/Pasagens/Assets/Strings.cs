using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class Strings
{
	private static volatile Strings _instance;
	private static object _lock = new object();
	
	static Strings() {} //Stops the lock being created ahead of time if it's not necessary
	
	public static Strings Instance
	{
		get
		{
			if (_instance == null)
			{
				lock(_lock)
				{
					if (_instance == null)
						_instance = new Strings();
				}
			}
			return _instance;
		}
	}
	
	private Strings() {}


	public string getStringByName(string name)
	{
		TextAsset textasset = (TextAsset) Resources.Load("Texts");

		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		xmlDoc.LoadXml(textasset.text); // load the file.
		XmlNodeList childNodes = ((XmlNodeList) xmlDoc.GetElementsByTagName("strings"))[0].ChildNodes;
		foreach(XmlNode node in childNodes)
			if (node.Name == name)
				return node.Attributes["value"].Value;

		return "ERROR_STRING NOT FOUND";
	}
}