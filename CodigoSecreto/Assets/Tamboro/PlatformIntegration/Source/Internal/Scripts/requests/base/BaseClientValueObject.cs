using UnityEngine;
using System.Collections;

namespace PlatformIntegration.CVO
{
	public class BaseClientValueObject
	{
		protected JsonObject _jsonObject = new JsonObject();

		public BaseClientValueObject()
		{
			idUser = PlatformDataManager.instance.platformDataReceived != null ? PlatformDataManager.instance.platformDataReceived.idUser : -1;
			idMinigame = PlatformDataManager.instance.platformDataReceived != null ? PlatformDataManager.instance.platformDataReceived.idMinigame : -1;
			listenerName = ExternalCommunicator.OBJECT_NAME;
			apiVersion = Resources.Load<TextAsset>("api-version").text;
		}

		public string getJsonData()
		{
			return _jsonObject.getJsonData();
		}

		public virtual void setupParameters()
		{
			Debug.LogError("[BaseClientValueObject] use o setupParameters() da classe filho");
		}

		public int idUser
		{
			get {
				return _jsonObject.getIntegerValue("idUser");
			}

			private set {
				_jsonObject.setValue("idUser", value);
			}
		}

		public int idMinigame
		{
			get {
				return _jsonObject.getIntegerValue("idMinigame");
			}
			
			private set {
				_jsonObject.setValue("idMinigame", value);
			}
		}
		
		public string listenerName 
		{ 
			get { 
				return _jsonObject.getValue("listenerName"); 
			}
			
			private set {
				_jsonObject.setValue("listenerName", value);
			}
		}
		
		public string apiVersion 
		{ 
			get { 
				return _jsonObject.getValue("apiVersion"); 
			}
			
			private set {
				_jsonObject.setValue("apiVersion", value);
			}
		}
	}
}
