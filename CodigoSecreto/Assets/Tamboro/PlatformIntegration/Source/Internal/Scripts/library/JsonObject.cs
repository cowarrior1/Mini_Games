using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlatformIntegration
{
	public class JsonObject 
	{
		private Dictionary<string, string> _dictionary = new Dictionary<string, string>();


		public string getJsonData()
		{
			string jsonData = "{ ";

			Dictionary<string, string>.KeyCollection keyColl = _dictionary.Keys;
			bool isFirstLoop = true;
			foreach( string s in keyColl )
			{
				if(isFirstLoop)
				{
					isFirstLoop = false;
					jsonData += generateJsonParameter(s, _dictionary[s]);
				}
				else
				{
					jsonData += ", " + generateJsonParameter(s, _dictionary[s]);
				}
			}
			jsonData += " }";
			return jsonData;
		}


		public float getFloatValue(string key)
		{
			return float.Parse(_getValue(key, true));
		}
		
		public int getIntegerValue(string key)
		{
			return int.Parse(_getValue(key, true));
		}
		
		public string getValue(string key)
		{
			return _getValue(key);
		}
		
		public void setValue(string key, string value)
		{
			_setValue(key, value);
		}
		
		public void setValue(string key, float value)
		{
			_setValue(key, value.ToString());
		}
		
		public void setValue(string key, int value)
		{
			_setValue(key, value.ToString());
		}
		
		private string _getValue(string key, bool isNumberValue=false)
		{
			string value;
			//tento pegar o value com a key
			_dictionary.TryGetValue(key, out value);
			//se eu nao conseguir, eu seto o valur default e retorno ele
			if(value == null)
			{
				value = isNumberValue ? "0" : "";
				_setValue(key, value);
			}
			return value;
		}
		
		private void _setValue(string key, string value)
		{
			//se nao tiver essa key, eu add
			if(_dictionary.ContainsKey(key) == false)
			{
				_dictionary.Add(key, value);
			}
			else//se ja tiver eu apenas seto
			{
				_dictionary[key] = value;
			}
		}


		private string putQuoteSymbol(string text)
		{
			return "\""+text+"\"";
		}

		private string generateJsonParameter(string key, string value)
		{
			return putQuoteSymbol(key)+":"+putQuoteSymbol(value);
		}
	}
}