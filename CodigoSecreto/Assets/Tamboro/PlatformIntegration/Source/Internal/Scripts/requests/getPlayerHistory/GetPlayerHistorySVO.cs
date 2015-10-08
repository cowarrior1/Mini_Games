using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatformIntegration.SimpleJSON;
using System;
using PlatformIntegration.SVO;

namespace PlatformIntegration.SVO
{
	public class GetPlayerHistorySVO : BaseServerValueObject
	{
		private List<PlayerScoreSVO> _playerScoresList;

		public List<PlayerScoreSVO> playerScoresList { get { return _playerScoresList; } }

		protected override void parseJsonReceived (JSONNode __jsonNodeReceived)
		{
			_jsonNode = __jsonNodeReceived["entityList"];
			
			JSONNode scoresListNode = _jsonNode;
			if(string.IsNullOrEmpty(scoresListNode.ToString()) == false) {
				_playerScoresList = convertJsonArrayToSVOList<PlayerScoreSVO>(scoresListNode.AsArray);
			}
		}

		public static string generateJsonData(List<string> scores)
		{
			string jsonData = "{\"entityList\":[";

			int LENGTH = scores.Count;
			for (int i = 0; i < LENGTH; i++) 
			{
				jsonData += scores[i];
				if(i != LENGTH-1)
					jsonData += ",";
			}

			jsonData += "]}";

			return jsonData;
		}
	}

	public class PlayerScoreSVO : BaseServerValueObject
	{
		private int _ranking;     
		private float _score;     
		private float _timeSpent;     
		private int _stage;	 
		private int _stars;

		protected override void parseJsonReceived (JSONNode __jsonNodeReceived)
		{
			_ranking    = _jsonNode["ranking"].AsInt;
			_score     	= _jsonNode["points"].AsFloat;
			_timeSpent  = _jsonNode["timeSpent"].AsFloat;
			_stage      = _jsonNode["stage"].AsInt;
			_stars		= _jsonNode["stars"].AsInt;
		}

		public static string generateJsonData(int __ranking, float __score, float __timeSpent, int __stage, int __stars)
		{
			return 	"{" +
						"\"ranking\":" 		+ __ranking +
						",\"points\":" 		+ __score +
						",\"timeSpent\":" 	+ __timeSpent +
						",\"stage\":" 		+ __stage +
						",\"stars\":" 		+ __stars +
					"}";
		}

		public int ranking { get { return _ranking; } }   
		public float score { get { return _score; } }   
		public float timeSpent { get { return _timeSpent; } }   
		public int stage { get { return _stage; } }
		public int stars { get { return _stars; } }
	}
}