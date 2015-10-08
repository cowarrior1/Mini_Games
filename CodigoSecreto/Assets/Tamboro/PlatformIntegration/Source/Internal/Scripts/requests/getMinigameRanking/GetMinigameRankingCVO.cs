using UnityEngine;
using System.Collections;

namespace PlatformIntegration.CVO
{
	public class GetMinigameRankingCVO : BaseClientValueObject 
	{
		public GetMinigameRankingCVO() : base(){}

		public void setupParameters (string __period, int __page, int __itensPerPage)
		{
			period = __period;
			page = __page;
			itensPerPage = __itensPerPage;
		}

		public string period 
		{ 
			get { 
				return _jsonObject.getValue("period"); 
			}

			set {
				_jsonObject.setValue("period", value);
			}
		}

		public int itensPerPage 
		{ 
			get { 
				return _jsonObject.getIntegerValue("itensPerPage"); 
			}

			set {
				_jsonObject.setValue("itensPerPage", value);
			}
		}

		public int page 
		{ 
			get { 
				return _jsonObject.getIntegerValue("page"); 
			}

			set {
				_jsonObject.setValue("page", value);
			}
		}
	}
}
