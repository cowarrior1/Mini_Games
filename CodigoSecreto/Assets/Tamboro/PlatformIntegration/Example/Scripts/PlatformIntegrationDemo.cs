using UnityEngine;
using System.Collections;
using PlatformIntegration;
using PlatformIntegration.RM;

namespace PlatformIntegration.Demo
{
	public class PlatformIntegrationDemo : MonoBehaviour 
	{
		void Start()
		{
			RequestHelper.DEBUG_REQUESTS = true;
		}

		void OnDeleteButtonClicked() 
		{
			DebugConsole.Append("\n\nOnDeleteButtonClicked called");
			RequestManager.deletePlayerHistoryLocal();
		}

		void OnGetMinigameRankingButtonClicked() 
		{
			DebugConsole.Append("\n\nOnGetMinigameRankingButtonClicked called");
			RequestManager.getMinigameRanking(RequestHelper.PERIOD_ALL, Random.Range(1, 4), Random.Range(5, 20), OnGetMinigameRankingRequestSuccess, OnRequestError, OnRequestStarted, OnRequestFinalized);
		}

		void OnGetPlayerHistoryButtonClicked()
		{
			DebugConsole.Append("\n\nOnGetPlayerHistoryButtonClicked called");
			RequestManager.getPlayerHistory(OnPlayerHistoryRequestSuccess, OnRequestError, OnRequestStarted, OnRequestFinalized);
		}

		void OnGetPlatformDataButtonClicked()
		{
			DebugConsole.Append("\n\nOnGetPlatformDataButtonClicked called");
			RequestManager.getPlatformData(OnGetPlatformDataRequestSuccess, OnRequestError, OnRequestStarted, OnRequestFinalized);
		}

		void OnSavePlayerScoreButtonClicked()
		{
			DebugConsole.Append("\n\nOnSavePlayerScoreButtonClicked called");
			float score 	= Random.Range(0f, 100f);
			int stage 		= Random.Range(0, 100);
			int timeSpent 	= Random.Range(0, 100);
			int neurons 	= Random.Range(0, 100);
			int stars 		= Random.Range(0, 4);
			RequestManager.savePlayerScore(score, stage, timeSpent, neurons, stars, OnSavePlayerScoreRequestSuccess, OnRequestError, OnRequestStarted, OnRequestFinalized);
		}

		void OnRequestError(BaseRequestModel request)
		{
			//you can cast the request model to GetMinigameRankingRM, GetPlatformDataRM, GetPlayerHistoryRM, SavePlayerScoreRM
			DebugConsole.Append("OnRequestSuccess called  :: obj received: " + request.errorMessage);
		}

		void OnRequestStarted(BaseRequestModel request)
		{
			//you can cast the request model to GetMinigameRankingRM, GetPlatformDataRM, GetPlayerHistoryRM, SavePlayerScoreRM
			DebugConsole.Append("OnRequestStarted called  :: functionName: " + request.functionName);
		}

		void OnRequestFinalized(BaseRequestModel request)
		{
			//you can cast the request model to GetMinigameRankingRM, GetPlatformDataRM, GetPlayerHistoryRM, SavePlayerScoreRM
			DebugConsole.Append("OnRequestFinalized called  :: functionName: " + request.functionName);
		}

		void OnGetMinigameRankingRequestSuccess(BaseRequestModel request)
		{
			GetMinigameRankingRM r = request as GetMinigameRankingRM;
			DebugConsole.Append("OnGetMinigameRankingRequestSuccess called  :: obj received: " + r.svo.jsonReceived);
		}

		void OnGetPlatformDataRequestSuccess(BaseRequestModel request)
		{
			GetPlatformDataRM r = request as GetPlatformDataRM;
			DebugConsole.Append("OnGetPlatformDataRequestSuccess called  :: obj received: " + r.svo.jsonReceived);
		}

		void OnSavePlayerScoreRequestSuccess(BaseRequestModel request)
		{
			SavePlayerScoreRM r = request as SavePlayerScoreRM;
			DebugConsole.Append("OnSavePlayerScoreRequestSuccess called  :: obj received: " + r.svo.jsonReceived);
		}

		void OnPlayerHistoryRequestSuccess(BaseRequestModel request)
		{
			GetPlayerHistoryRM r = request as GetPlayerHistoryRM;
			DebugConsole.Append("OnPlayerHistoryRequestSuccess called  :: obj received: " + r.svo.jsonReceived);
		}
	}
}