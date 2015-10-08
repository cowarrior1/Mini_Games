using UnityEngine;
using System.Collections;
using PlatformIntegration;
using PlatformIntegration.RM;

public class RequestManager  
{
	/// <summary>
	/// Deleta o historico local do jogador (apenas para exe e webplayer nao integrado com a plataforma)
	/// </summary>
	public static void deletePlayerHistoryLocal()
	{
		LocalDataManager.deleteAll();
	}

	/// <summary>
	/// Retorna o ranking do minigame (disponivel apenas pra webplayer integrado com a plataforma)
	/// </summary>
	/// <returns>RequestModel com todas informaçoes desse tipo de requisicao</returns>
	/// <param name="__period">Periodo do ranking (usar constantes da classe RequestHelper)</param>
	/// <param name="__page">Numero da pagina</param>
	/// <param name="__itensPerPage">Quantidade de pontuaçoes por pagina</param>
	/// <param name="__callbackRequestCompleted">__callback request completed.</param>
	/// <param name="__callbackRequestError">__callback request error.</param>
	/// <param name="__callbackRequestStarted">__callback request started.</param>
	/// <param name="__callbackRequestFinalized">__callback request finalized.</param>
	public static GetMinigameRankingRM getMinigameRanking(  string __period, 
															int __page, 
															int __itensPerPage,
															RequestHelper.RequestHandlerDelegate __callbackRequestCompleted = null,
															RequestHelper.RequestHandlerDelegate __callbackRequestError = null,
															RequestHelper.RequestHandlerDelegate __callbackRequestStarted = null,
															RequestHelper.RequestHandlerDelegate __callbackRequestFinalized = null)
	{
		GetMinigameRankingRM request = new GetMinigameRankingRM();
		request.cvo.setupParameters(__period, __page, __itensPerPage);
		setupRequest(request, __callbackRequestCompleted, __callbackRequestError, __callbackRequestStarted, __callbackRequestFinalized);
		return request;
	}

	/// <summary>
	/// Salva a pontuaçao do jogador
	/// </summary>
	/// <returns>RequestModel com todas informaçoes desse tipo de requisicao</returns>
	/// <param name="__score">Pontuaçao do jogador</param>
	/// <param name="__stage">A fase dessa pontuaçao</param>
	/// <param name="__timeSpent">Tempo gasto nessa fase</param>
	/// <param name="__coins">Quantidade de coins ganhos nessa fase</param>
	/// <param name="__stars">Quantidade de estrelas ganhas nessa fase</param>
	/// <param name="__callbackRequestCompleted">__callback request completed.</param>
	/// <param name="__callbackRequestError">__callback request error.</param>
	/// <param name="__callbackRequestStarted">__callback request started.</param>
	/// <param name="__callbackRequestFinalized">__callback request finalized.</param>
	public static SavePlayerScoreRM savePlayerScore(float __score,
													int __stage,
													int __timeSpent,
													int __coins,
													int __stars,
													RequestHelper.RequestHandlerDelegate __callbackRequestCompleted = null,
													RequestHelper.RequestHandlerDelegate __callbackRequestError = null,
													RequestHelper.RequestHandlerDelegate __callbackRequestStarted = null,
													RequestHelper.RequestHandlerDelegate __callbackRequestFinalized = null)
	{
		SavePlayerScoreRM request = new SavePlayerScoreRM();
		request.cvo.setupParameters(__score, __stage, __timeSpent, __coins, __stars);
		setupRequest(request, __callbackRequestCompleted, __callbackRequestError, __callbackRequestStarted, __callbackRequestFinalized);
		return request;
	}

	/// <summary>
	/// Salva a pontuaçao do jogador deixando a plataforma decidir quantas moedas serao dadas
	/// </summary>
	/// <returns>RequestModel com todas informaçoes desse tipo de requisicao</returns>
	/// <param name="__score">Pontuaçao do jogador</param>
	/// <param name="__stage">A fase dessa pontuaçao</param>
	/// <param name="__timeSpent">Tempo gasto nessa fase</param>
	/// <param name="__stars">Quantidade de estrelas ganhas nessa fase</param>
	/// <param name="__callbackRequestCompleted">__callback request completed.</param>
	/// <param name="__callbackRequestError">__callback request error.</param>
	/// <param name="__callbackRequestStarted">__callback request started.</param>
	/// <param name="__callbackRequestFinalized">__callback request finalized.</param>
	public static SavePlayerScoreRM savePlayerScore(float __score,
	                                                int __stage,
	                                                int __timeSpent,
	                                                int __stars,
	                                                RequestHelper.RequestHandlerDelegate __callbackRequestCompleted = null,
	                                                RequestHelper.RequestHandlerDelegate __callbackRequestError = null,
	                                                RequestHelper.RequestHandlerDelegate __callbackRequestStarted = null,
	                                                RequestHelper.RequestHandlerDelegate __callbackRequestFinalized = null)
	{
		return savePlayerScore(__score, __stage, __timeSpent, -1, __stars, __callbackRequestCompleted, __callbackRequestError, __callbackRequestStarted, __callbackRequestFinalized);
	}

	/// <summary>
	/// Retorna o historico do jogador com todas as fases jogadas por ele
	/// </summary>
	/// <returns>RequestModel com todas informaçoes desse tipo de requisicao</returns>
	/// <param name="__callbackRequestCompleted">__callback request completed.</param>
	/// <param name="__callbackRequestError">__callback request error.</param>
	/// <param name="__callbackRequestStarted">__callback request started.</param>
	/// <param name="__callbackRequestFinalized">__callback request finalized.</param>
	public static GetPlayerHistoryRM getPlayerHistory(RequestHelper.RequestHandlerDelegate __callbackRequestCompleted = null,
														RequestHelper.RequestHandlerDelegate __callbackRequestError = null,
														RequestHelper.RequestHandlerDelegate __callbackRequestStarted = null,
														RequestHelper.RequestHandlerDelegate __callbackRequestFinalized = null)
	{
		GetPlayerHistoryRM request = new GetPlayerHistoryRM();
		request.cvo.setupParameters();
		setupRequest(request, __callbackRequestCompleted, __callbackRequestError, __callbackRequestStarted, __callbackRequestFinalized);
		return request;
	}

	/// <summary>
	/// Retorna os dados da plataforma (isso e chamado automaticamente atraves do prefab de integracao)
	/// </summary>
	/// <returns>RequestModel com todas informaçoes desse tipo de requisicao</returns>
	/// <param name="__callbackRequestCompleted">__callback request completed.</param>
	/// <param name="__callbackRequestError">__callback request error.</param>
	/// <param name="__callbackRequestStarted">__callback request started.</param>
	/// <param name="__callbackRequestFinalized">__callback request finalized.</param>
	public static GetPlatformDataRM getPlatformData(RequestHelper.RequestHandlerDelegate __callbackRequestCompleted = null,
														RequestHelper.RequestHandlerDelegate __callbackRequestError = null,
														RequestHelper.RequestHandlerDelegate __callbackRequestStarted = null,
														RequestHelper.RequestHandlerDelegate __callbackRequestFinalized = null)
	{
		throw new System.Exception("This request was already made on game start. You can get it through class PlatformDataManager.");
//		GetPlatformDataRM request = new GetPlatformDataRM();
//		request.cvo.setupParameters();
//		setupRequest(request, __callbackRequestCompleted, __callbackRequestError, __callbackRequestStarted, __callbackRequestFinalized);
//		return request;
	}

	private static void setupRequest(BaseRequestModel __r,
			                          RequestHelper.RequestHandlerDelegate __callbackRequestCompleted = null,
			                          RequestHelper.RequestHandlerDelegate __callbackRequestError = null,
			                          RequestHelper.RequestHandlerDelegate __callbackRequestStarted = null,
			                          RequestHelper.RequestHandlerDelegate __callbackRequestFinalized = null)
	{
		__r.callbackRequestCompleted += __callbackRequestCompleted;
		__r.callbackRequestError += __callbackRequestError;
		__r.callbackRequestStarted += __callbackRequestStarted;
		__r.callbackRequestFinalized += __callbackRequestFinalized;
		ExternalCommunicator.instance.addRequest(__r);
	}
}