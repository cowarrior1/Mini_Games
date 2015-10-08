using UnityEngine;
using PlatformIntegration.SimpleJSON;

namespace PlatformIntegration.SVO
{
	public class GetPlatformDataSVO : BaseServerValueObject
	{
		private int _idUser;
		private int _idMinigame;
		private string _nickName;
		private string _firstName;
		private string _lastName;
		private int _grade;
	    private string _backendUrl;
	    private string _minigameFolder;
		private string _externalResourcesPath;

		protected override void parseJsonReceived (JSONNode __jsonNodeReceived)
		{
			_idUser     	= _jsonNode["idUser"].AsInt;
			_idMinigame 	= _jsonNode["idMinigame"].AsInt;
			_nickName   	= _jsonNode["nickName"];
			_firstName  	= _jsonNode["firstName"];
			_lastName   	= _jsonNode["lastName"];
			_grade      	= _jsonNode["grade"].AsInt;
			_backendUrl 	= _jsonNode["backendUrl"];
			_minigameFolder = _jsonNode["minigameFolder"];
			_externalResourcesPath 	= (_minigameFolder + "resources/");
			//removendo duas barras inexplicaveis antes do resources
			_externalResourcesPath = _externalResourcesPath.Replace("//resources/", "/resources/");
		}

		public int 		idUser { get { return _idUser; } }
		public int 		idMinigame { get { return _idMinigame; } }
		public string	nickName { get { return _nickName; } }
		public string 	firstName { get { return _firstName; } }
		public string 	lastName { get { return _lastName; } }
		public int 		grade { get { return _grade; } }
		public string 	backendUrl { get { return _backendUrl; } }
		public string 	minigameFolder { get { return _minigameFolder; } }
		public string 	externalResourcesPath { get { return _externalResourcesPath; } }
	}
}