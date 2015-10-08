using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using PlatformIntegration.CVO;
using PlatformIntegration.SVO;
using PlatformIntegration.RM;

namespace PlatformIntegration
{
	public class ExternalCommunicator : MonoBehaviour
	{
		private static ExternalCommunicator _firstInstance;
		public static ExternalCommunicator instance { get { return _firstInstance; } }
		
		public const string OBJECT_NAME = "[PLATFORM_INTEGRATION]";
		
		private List<BaseRequestModel> _requestList = new List<BaseRequestModel>();
		private BaseRequestModel _currentRequestModel;

		void Awake()
		{
			if(_firstInstance != null)
			{
				Debug.LogWarning("[ExternalCommunicator] voce tentou criar uma nova instancia");
				DestroyImmediate(this.gameObject);
				return;
			}

			DontDestroyOnLoad(this.gameObject);
			this.gameObject.name = OBJECT_NAME;
			_firstInstance = this;
			
			//chamo o setup para que os dados da Platforma sejam requisitados
			PlatformDataManager.instance.setup();
		}
		
		public void addRequest(BaseRequestModel requestModel)
		{
			_requestList.Add(requestModel);
			tryCallNextResquest();
		}

		// --------------------------------------------------------
		// ---- JS LISTENER
		private void OnRequestSuccess(string jsonData) 
		{
			_currentRequestModel.OnRequestComplete(jsonData);
			CurrentRequestFinalized();
		}
		
		private void OnRequestError(string error)
		{
			_currentRequestModel.OnRequestError(error);
			CurrentRequestFinalized();
		}
		// --------------------------------------------------------
		// --------------------------------------------------------

		private void CurrentRequestFinalized()
		{
			try{
				//isso serve pra debugar a request (so pro desenvolvedor)
				//coloquei no try pq nao sei se vai ter algum louco que vai jogar isso ate o fim da vida
				_oldRequests = "time_" + (int)Time.realtimeSinceStartup + "___"+  _currentRequestModel.functionName +"\n"+ _oldRequests;
			}catch{}
			_currentRequestModel = null;
			tryCallNextResquest();
		}
		
		//----------------------------------------------------------
		// UTILS // UTILS // UTILS // UTILS // UTILS // UTILS // 
		//----------------------------------------------------------
		private IEnumerator startRequest(BaseRequestModel requestModel)
		{
			_currentRequestModel = requestModel;
			requestModel.OnRequestStart();
			
			//torna a chamada assincrona
			yield return new WaitForSeconds(0.2f);
			
			if(RequestHelper.platformModeOn)
			{
				Application.ExternalCall("sendToFlash", requestModel.functionName, requestModel.cvo.getJsonData());
			}
			else
			{
				//verifico se a request foi cadastrada
				if(_currentRequestModel is GetMinigameRankingRM == false && _currentRequestModel is GetPlatformDataRM == false && 
				   _currentRequestModel is GetPlayerHistoryRM == false && _currentRequestModel is SavePlayerScoreRM == false)
				{
					string error = "Request not registered";
					Debug.LogError(this.GetType() + error);
					OnRequestError(error);
				}
				else
				{
					if(_currentRequestModel is GetMinigameRankingRM)
					{
						OnRequestError("Request available only for minigames integrated with the platform");
					}
					else if(_currentRequestModel is GetPlatformDataRM)
					{
						OnRequestSuccess("{" +
						                 createField("idUser", "-1", true) +
						                 createField("idMinigame", "-1", true) +
						                 createField("nickName", "null", true) +
						                 createField("firstName", "null", true) +
						                 createField("lastName", "null", true) +
						                 createField("grade", "-1", true) +
						                 createField("backendUrl", "null", true) +
						                 createField("minigameFolder", ExternalResources.getInstance().getResourcesPath()+"../", false) +
						                 "}");
					}
					else if(_currentRequestModel is GetPlayerHistoryRM)
					{
						OnRequestSuccess(LocalDataManager.getPlayerHistoryJson());
					}
					else if(_currentRequestModel is SavePlayerScoreRM)
					{
						LocalDataManager.savePlayerScore((_currentRequestModel as SavePlayerScoreRM).cvo);
						OnRequestSuccess("{}");
					}
				}
			}
		}
		
		private string createField(string name, string value, bool withComma)
		{
			string quotationMark = "\"";
			string v = quotationMark+name+quotationMark +     ":"    +  quotationMark+value+quotationMark;
			if(withComma) v += ",";
			return v;
		}
		
		private void tryCallNextResquest()
		{
			if (_requestList.Count > 0 && _currentRequestModel == null)
			{
				BaseRequestModel requestModel = _requestList[0] as BaseRequestModel;
				if (requestModel.requestInProgress == false)
				{
					//torna a chamada assincrona
					StartCoroutine(startRequest(requestModel));
					_requestList.RemoveAt(0);
				}
			}
		}
		// UTILS // UTILS // UTILS // UTILS // UTILS // UTILS // 
		//----------------------------------------------------------
		
		private Vector2 _scrollPosition;
		private string _oldRequests;
		void OnGUI()
		{
			if(RequestHelper.DEBUG_REQUESTS)
			{
				string txt = "## REQUEST LIST ##";
				txt += "\n COUNT: " + _requestList.Count;
				for (int i = 0; i < _requestList.Count; i++) 
				{
					txt += "\n idx_"+i+": " + _requestList[i].functionName;
				}
				
				Rect r = new Rect(10, 10, 500, 100);
				GUI.skin.box.alignment = TextAnchor.UpperLeft;
				_scrollPosition = GUI.BeginScrollView(r, _scrollPosition, new Rect(r.x, r.y, r.width - 20, 1000));
				GUI.color = Color.white;
				GUI.Box(new Rect(r.x, r.y, (r.width - 20)*0.5f, 1000), txt);
				GUI.Box(new Rect(r.x + (r.width-10)*0.5f , r.y, (r.width - 20)*0.5f, 1000), "## OLD REQUESTS ##\n" + _oldRequests);
				GUI.EndScrollView();
			}
		}
	}
}
