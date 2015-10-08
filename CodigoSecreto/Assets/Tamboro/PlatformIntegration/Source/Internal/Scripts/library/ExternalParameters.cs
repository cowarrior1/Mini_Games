using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatformIntegration
{
	public class ExternalParameters 
	{
		private Dictionary<string, string> _parameters = new Dictionary<string, string>();
		public Dictionary<string, string> parameters { get { return _parameters; } }

		private bool _hasParameter;
		public bool hasParameter { get { return _hasParameter; } }

		private static ExternalParameters _instance;
		public static ExternalParameters GetInstance() {
			if(_instance == null)
			{
				_instance = new ExternalParameters();
				_instance.ReadUrl();
			}
			return _instance;
		}

		private void ReadUrl()
		{
			string separator = ".unity3d?";
			int idx = Application.absoluteURL.IndexOf(separator);
			_hasParameter = idx != -1;		
			idx += separator.Length;
			
			if(_hasParameter == false)
				return;
			
			try {
				ReadParameters(idx);
			}
			catch{}
		}

		private void ReadParameters(int startIndex)
		{
			string parametersText = Application.absoluteURL.Substring(startIndex, Application.absoluteURL.Length-startIndex);
			string[] parameters = parametersText.Split('&');

			for (int i = 0; i < parameters.Length; i++) 
			{
				try {
					string[] parameterAndValue = parameters[i].Split('=');
					_parameters.Add(parameterAndValue[0], parameterAndValue[1]);
				}
				catch{}
			}
		}

		public string GetParameter(string element)
		{
			string val;
			_parameters.TryGetValue(element, out val);
			return val;
		}
	}
}
