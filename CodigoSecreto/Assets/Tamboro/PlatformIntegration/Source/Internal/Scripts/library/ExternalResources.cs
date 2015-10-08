using UnityEngine;
using System.Collections;

namespace PlatformIntegration
{
	public class ExternalResources 
	{
		private static ExternalResources _instance;
		public static ExternalResources getInstance()
		{
			if(_instance == null) _instance = new ExternalResources();
			return _instance;
		}
		
		private string _resourcesPath;
		
		public ExternalResources()
		{
			#if UNITY_EDITOR
			//pega o caminho do mesmo nivel da Pasta "Assets" e "ProjectSettings" gerada pela unity
			_resourcesPath = Application.dataPath+"/../";
			#else
			_resourcesPath = Application.dataPath+"/";
			#endif
			
			
			#if UNITY_EDITOR_WIN
			_resourcesPath = "file:///"+_resourcesPath;
			#elif UNITY_EDITOR_OSX
			_resourcesPath = "file://"+_resourcesPath;
			#endif
			_resourcesPath += "resources/";
		}
		
		/// <summary>
		/// Retorna o path da pasta resources externa
		/// </summary>
		public string getResourcesPath()
		{
			return _resourcesPath;
		}
		
		/// <summary>
		/// Cria o path usando o endereço relativo a pasta "resouces"
		/// </summary>
		public string createFilePath(string _path)
		{
			return getResourcesPath() + _path;
		}
	}
}
