using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using PlatformIntegration.SimpleJSON;

namespace PlatformIntegration.SVO
{
	public abstract class BaseServerValueObject
	{
		/// <summary>
		/// json compelto retornado pelo servico
		/// </summary>
		private string _jsonReceived;

		/// <summary>
		/// JSON NODE completo retornado pelo servico
		/// </summary>
		private JSONNode _jsonNodeReceived;

		/// <summary>
		/// JSON NODE que servira para armazer o objeto que sera lido pela classes SVO (normalmente eh json.entity ou json.entityList)
		/// </summary>
		protected JSONNode _jsonNode;


		public void setup(string __jsonReceived)
		{
			_jsonReceived = __jsonReceived;
			_jsonNodeReceived = JSON.Parse(__jsonReceived);
			_jsonNode = _jsonNodeReceived;
			parseJsonReceived(_jsonNodeReceived);
		}

		public void setup(JSONNode __jsonNode)
		{
			_jsonReceived = __jsonNode.ToString();
			_jsonNodeReceived = __jsonNode;
			_jsonNode = _jsonNodeReceived;
			parseJsonReceived(_jsonNodeReceived);
		}

		public string jsonReceived {
			get {
				return _jsonReceived;
			}
		}


		protected abstract void parseJsonReceived(JSONNode __jsonNodeReceived);


	//	public void setup(SimpleJSON.JSONNode __jsonNode)
	//	{
	//		_jsonNode = __jsonNode;
	//	}
	//
	//	protected SimpleJSON.JSONNode jsonNode {
	//		get {
	//			return _jsonNode;
	//		}
	//	}

		/// <summary>
		/// Converte um json array numa lista de objetos tipados
		/// </summary>
		protected List<T> convertJsonArrayToSVOList<T>(JSONArray __jsonArray) where T : BaseServerValueObject, new()
		{
			List<T> array = new List<T>();
			int LENGTH = __jsonArray.Count;
			for (int i = 0; i < LENGTH; i++) 
			{
				T obj = new T();
				obj.setup(__jsonArray[i]);
				array.Add(obj);
			}
			return array;
		}

		/// <summary>
		/// tenta pegar um json node dentro de outro
		/// </summary>
		/// <returns> retorna se conseguiu pegar ou nao</returns>
		protected bool tryGetJsonNode(JSONNode __node, string __tag, out SimpleJSON.JSONNode __result)
		{
			try 
			{
				__result = __node[__tag];
				return true;
			}
			catch(Exception e)
			{
				Debug.LogException(e);
				__result = null;
				return false;
			}
		}
	}
}