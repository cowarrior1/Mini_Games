using UnityEngine;
using System.Collections;
using PlatformIntegration.SVO;
using PlatformIntegration.CVO;

namespace PlatformIntegration.RM
{
	public abstract class BaseRequestModel
	{
		public event RequestHelper.RequestHandlerDelegate callbackRequestError;
		public event RequestHelper.RequestHandlerDelegate callbackRequestCompleted;
		public event RequestHelper.RequestHandlerDelegate callbackRequestStarted;
		public event RequestHelper.RequestHandlerDelegate callbackRequestFinalized;

		private bool _requestInProgress = false;
		public bool requestInProgress { get { return _requestInProgress; } }

		public string _errorMessage;
		public string errorMessage { get { return _errorMessage; } }

		private string _functionName;

		protected BaseClientValueObject _cvo;
		protected BaseServerValueObject _svo;

		public BaseRequestModel(string __functionName)
		{
			_functionName = __functionName;
			createClientValueObject();
			createServerValueObject();
		}

		public void OnRequestError(string error)
		{
			_errorMessage = error;
			if(callbackRequestError != null)
				callbackRequestError.Invoke(this);
			OnRequestFinalized();
		}	

		public void OnRequestComplete(string jsonData)
		{
			_svo.setup(jsonData);
			if(callbackRequestCompleted != null)
				callbackRequestCompleted.Invoke(this);
			OnRequestFinalized();
		}	

		public void OnRequestStart()
		{
			_requestInProgress = true;
			if(callbackRequestStarted != null)
				callbackRequestStarted.Invoke(this);
		}	

		private void OnRequestFinalized()
		{
			if(callbackRequestFinalized != null)
				callbackRequestFinalized.Invoke(this);
		}	

		abstract protected void createClientValueObject();
		abstract protected void createServerValueObject();

		/// <summary>
		/// Dados passados na requisiçao (sao gerados automaticamnete)
		/// </summary>
		public BaseClientValueObject cvo { get { return _cvo; } }

		/// <summary>
		/// Dados recebidos na requisicao
		/// </summary>
		public BaseServerValueObject svo { get{ return _svo; } }

		public string functionName {
			get {
				return _functionName;
			}
		}
	}
}