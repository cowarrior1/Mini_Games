using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatformIntegration.CVO;
using PlatformIntegration.SVO;

namespace PlatformIntegration
{
	public class LocalDataManager : MonoBehaviour 
	{
		public static void deleteAll()
		{
			PlayerPrefs.DeleteAll();
		}
		
		public static void savePlayerScore(SavePlayerScoreCVO __cvo)
		{
			int stage = __cvo.stage;

			//so salvo se o score for maior que o score ja salvo pro mesmo stage
			float scoreSaved = PlayerPrefs.GetFloat(getScoreKey(stage));
			if(scoreSaved > __cvo.score) return;
			
			trySaveStage(stage);
			PlayerPrefs.SetFloat(getScoreKey(stage), __cvo.score);
			PlayerPrefs.SetFloat(getTimeSpentKey(stage), __cvo.timeSpent);
			PlayerPrefs.SetInt(getNeuronsKey(stage), __cvo.neurons);
			PlayerPrefs.SetInt(getStarsKey(stage), __cvo.stars);
		}
		
		public static string getPlayerHistoryJson()
		{
			int[] stageList = getStageListSaved();
			int LENGTH = stageList.Length;

			List<string> scores = new List<string>();
			for (int i = 0; i < LENGTH; i++) 
			{
				int stage 		= stageList[i];
				float points 	= PlayerPrefs.GetFloat(getScoreKey(stage));
				float timeSpent = PlayerPrefs.GetInt(getTimeSpentKey(stage));
				int stars 		= PlayerPrefs.GetInt(getStarsKey(stage));

				scores.Add(PlayerScoreSVO.generateJsonData(-1, points, timeSpent, stage, stars));
			}

			return GetPlayerHistorySVO.generateJsonData(scores);
		}		
					
					
			
		
		//utils
				
		private static string getStageKey(int stage)
		{
			return "stage-"+stage;
		}
			
		private static string getNeuronsKey(int stage)
		{
			return getStageKey(stage)+"__neurons";
		}
			
		private static string getScoreKey(int stage)
		{
			return getStageKey(stage)+"__score";
		}
			
		private static string getTimeSpentKey(int stage)
		{
			return getStageKey(stage)+"__timespent";
		}
				
		private static string getStarsKey(int stage)
		{
			return getStageKey(stage)+"__stars";
		}
		
		/// <summary>
		/// Tenta salvar o stage na lista de stages que me informara quais stages foram salvos e quais eu devo ler
		/// </summary>
		private static void trySaveStage(int stage)
		{
			int[] stageListSaved = getStageListSaved();
			bool stageAlreadyOnList = false;
			
			int LENGTH = stageListSaved.Length;
			for (int i = 0; i < LENGTH; i++) {
				if(stageListSaved[i] == stage)
					stageAlreadyOnList = true;
			}
			
			if(stageAlreadyOnList == false)
				stagesSaved = stagesSaved + stage + " ";
		}
		
		/// <summary>
		/// retorna a string formatada dos stages salvos
		/// </summary>
		private static string stagesSaved
		{
			get 
			{
				return PlayerPrefs.GetString("stagesSaved");
			}
			set
			{
				PlayerPrefs.SetString("stagesSaved", value);
			}
		}
		
		/// <summary>
		/// retorna a lista dos stages salvos
		/// </summary>
		private static int[] getStageListSaved()
		{
			string stagesSavedText = stagesSaved;
			int LENGTH = 0;
			string[] stages = new string[0];
			if(stagesSavedText != "" && stagesSavedText != " ") {
				//-1 pq o ultimo e sempre nada
				stages = stagesSaved.Split(' ');
				LENGTH = stages.Length - 1;
			}
			int[] stageArray = new int[LENGTH];
			int parseAmount = 0;
			for (int i = 0; i < LENGTH; i++) 
			{
				string currentStageText = stages[i];
				int stageNumber;
				bool parsed = int.TryParse(currentStageText, out stageNumber);
				
				//verifico se ele conseguiu converter o dado pra inteiro (deveria conseguir)
				if(parsed)
				{
					parseAmount++;
					stageArray[i] = stageNumber;
				}
				else
				{
	//				if(currentStageText != "" && currentStageText != " ")
						Debug.LogError("[LocalDataManager] nao consegui ler o numero do stage que ja foi salvo :: " + currentStageText);
				}
			}
	//		
	//		int[] stageArrayTemp = new int[parseAmount];
	//		for (int i = 0; i < parseAmount; i++) {
	//			stageArrayTemp[i] = stageArray[i];
	//		}
			
			return stageArray;
		}
	}
}