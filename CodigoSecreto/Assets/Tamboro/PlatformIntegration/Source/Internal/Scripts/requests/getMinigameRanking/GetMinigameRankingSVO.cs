using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatformIntegration.SimpleJSON;
using System;

namespace PlatformIntegration.SVO
{
	public class GetMinigameRankingSVO : BaseServerValueObject
	{
		private List<MinigameRankingSVO> _minigameRankingList;
	    private MinigameRankingSVO _userMinigameRanking;
	    private bool _hasNextPage;

	    public bool hasNextPage 								{ get { return _hasNextPage; } }
		public List<MinigameRankingSVO> minigameRankingList 	{ get { return _minigameRankingList; } }
	    public MinigameRankingSVO userMinigameRanking 			{ get { return _userMinigameRanking; } }

		protected override void parseJsonReceived (JSONNode __jsonNodeReceived)
		{
			_jsonNode = __jsonNodeReceived["entity"];
			
			JSONNode rankingListNode = _jsonNode["minigameRankingList"];
			if(string.IsNullOrEmpty(rankingListNode.ToString()) == false)
			{
				_minigameRankingList = convertJsonArrayToSVOList<MinigameRankingSVO>(rankingListNode.AsArray);
			}
			
			
			
			JSONNode userRankingNode = _jsonNode["userMinigameRanking"];
			if(string.IsNullOrEmpty(userRankingNode.ToString()) == false)
			{
				_userMinigameRanking = new MinigameRankingSVO();
				_userMinigameRanking.setup(userRankingNode);
			}
			
			
			
			JSONNode hasNextPageNode = _jsonNode["hasNextPage"];
			if(string.IsNullOrEmpty(hasNextPageNode.ToString()) == false)
			{
				_hasNextPage = hasNextPageNode.AsBool;
			}
			else
			{
				_hasNextPage = false;
			}
		}

#if UNITY_EDITOR
		public void filterJsonFake(int page, int itensPerPage)
		{
			Debug.LogWarning("[filterJsonFake] So use esse metodo quando for pra testar o json fake");

			int firstRankingPosition = page * itensPerPage - itensPerPage + 1;
			int lastRankingPosition = firstRankingPosition + itensPerPage - 1;

			int firstIndex = firstRankingPosition - 1;
			int lastIndex = lastRankingPosition - 1;


			_hasNextPage = lastIndex < (minigameRankingList.Count - 1);

			if(firstIndex < minigameRankingList.Count - 1)
			{
				List<MinigameRankingSVO> listTemp;
				listTemp = minigameRankingList.GetRange(firstIndex, hasNextPage == true ? itensPerPage : minigameRankingList.Count - firstIndex);
				_minigameRankingList = listTemp;
        	}
			else
			{
				minigameRankingList.Clear();
			}
		}
#endif
	}

	public class MinigameRankingSVO : BaseServerValueObject
	{
		private int _idUser;
		private int _idMinigame;
		private string _nickName;
		private string _login;
		private string _firstName;
		private string _lastName;
		private int _grade;
		private int _ranking;
		private int _score;
		private int _timeSpent;
		private int _stage;
		private int _program;

		protected override void parseJsonReceived (JSONNode __jsonNodeReceived)
		{
			_idUser     = _jsonNode["idUser"].AsInt;
			_idMinigame = _jsonNode["idMinigame"].AsInt;
			_nickName   = _jsonNode["nikName"]; //nome no json recebido errado 
			_login      = _jsonNode["login"];
			_firstName  = _jsonNode["firstName"];
			_lastName   = _jsonNode["lastName"];
			_grade      = _jsonNode["grade"].AsInt;
			_ranking    = _jsonNode["ranking"].AsInt;
			_score     	= _jsonNode["points"].AsInt; //nome no json recebido errado 
			_timeSpent  = _jsonNode["timeSpent"].AsInt;
			_stage      = _jsonNode["stage"].AsInt;
			_program    = _jsonNode["program"].AsInt;
		}
		
		public int idUser 		{ get { return _idUser; } }
		public int idMinigame 	{ get { return _idMinigame; } }
		public string nickName 	{ get { return _nickName; } }
		public string login 	{ get { return _login; } }
		public string firstName { get { return _firstName; } }
		public string lastName 	{ get { return _lastName; } }
		public int grade 		{ get { return _grade; } }
		public int ranking 		{ get { return _ranking; } }
		public int score 		{ get { return _score; } }
		public int timeSpent 	{ get { return _timeSpent; } }
		public int stage 		{ get { return _stage; } }
		public int program 		{ get { return _program; } }
	}
}