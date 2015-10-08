using UnityEngine;
using System.Collections;
using PlatformIntegration;
using PlatformIntegration.RM;

public class RequestHelper
{
	/// <summary>
	/// Delegate usado nas callbacks das requisicoes
	/// </summary>
	public delegate void RequestHandlerDelegate(BaseRequestModel request);
	/// <summary>
	/// Usado na chamada da requisiçao de ranking
	/// </summary>
	public const string PERIOD_ALL 		= "all";
	/// <summary>
	/// Usado na chamada da requisiçao de ranking
	/// </summary>
	public const string PERIOD_MONTH 	= "month";
	/// <summary>
	/// Usado na chamada da requisiçao de ranking
	/// </summary>
	public const string PERIOD_WEEK 	= "week";
	/// <summary>
	/// Usado na chamada da requisiçao de ranking
	/// </summary>
	public const string PERIOD_DAY 		= "day";
	/// <summary>
	///  Caso queira debugar as requests na tela
	/// </summary>
	public static bool DEBUG_REQUESTS = false;
	/// <summary>
	/// Caso esteja integrado com a plataforma
	/// </summary>
	public static bool platformModeOn {
		get {
			return ExternalParameters.GetInstance().GetParameter("platformMode") == "true";
		}
	}
}
