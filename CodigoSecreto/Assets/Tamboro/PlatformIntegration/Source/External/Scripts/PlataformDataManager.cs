using UnityEngine;
using System.Collections;
using PlatformIntegration;
using PlatformIntegration.SVO;
using PlatformIntegration.RM;

public class PlatformDataManager 
{
	private static PlatformDataManager _instance;
	public static PlatformDataManager instance {
		get {
			if(_instance == null)
				_instance = new PlatformDataManager();
			return _instance;
		}
	}

	public delegate void RequestHandlerDelegate();

	private GetPlatformDataSVO _platformDataReceived;

	/// <summary>
	/// Request dos dados da plataforma
	/// </summary>
	private GetPlatformDataRM _request;
	/// <summary>
	/// eh chamado quando a request estiver completa
	/// </summary>
	public RequestHandlerDelegate callbackRequestCompleted;
	/// <summary>
	/// eh chamado quando a request der erro
	/// </summary>
	public RequestHandlerDelegate callbackRequestError;

	/// <summary>
	/// chamado pra fazer a requisicao dos dados da Platforma (responsabilidade do ExternalCommunicator de chamar)
	/// </summary>
	public void setup()
	{
		if(_request != null) return;

		_request = new GetPlatformDataRM();
		_request.cvo.setupParameters();
		_request.callbackRequestCompleted += OnRequestCompleted;
		_request.callbackRequestError += OnRequestError;
		ExternalCommunicator.instance.addRequest(_request);
	}

	/// <summary>
	/// Add um ouvinte pra quando os dados da plataforma estiverem prontos para serem lidos
	/// </summary>
	/// <param name="__callbackRequestCompleted">__callback request completed.</param>
	public void AddListener(RequestHandlerDelegate __callbackRequestCompleted)
	{
		if(_platformDataReceived != null)
			__callbackRequestCompleted.Invoke();

		callbackRequestCompleted += __callbackRequestCompleted;
	}

	/// <summary>
	/// Remove o listener da callback da request
	/// </summary>
	/// <param name="__callbackRequestCompleted">__callback request completed.</param>
	public void RemoveListener(RequestHandlerDelegate __callbackRequestCompleted)
	{	
		callbackRequestCompleted -= __callbackRequestCompleted;
	}

	/// <summary>
	/// Retorna os dados recebidos da request
	/// </summary>
	/// <value>Dados da plataforma recebidos</value>
	public GetPlatformDataSVO platformDataReceived { 
		get {
			return _platformDataReceived;
		} 
	}

	/// <summary>
	/// Listener da request
	/// </summary>
	private void OnRequestCompleted(BaseRequestModel request)
	{
		_platformDataReceived = request.svo as GetPlatformDataSVO;
		if(callbackRequestCompleted != null) callbackRequestCompleted.Invoke();
	}

	/// <summary>
	/// Listener da request
	/// </summary>
	private void OnRequestError(BaseRequestModel request)
	{
		_platformDataReceived = request.svo as GetPlatformDataSVO;
		if(callbackRequestError != null) callbackRequestError.Invoke();
	}
}