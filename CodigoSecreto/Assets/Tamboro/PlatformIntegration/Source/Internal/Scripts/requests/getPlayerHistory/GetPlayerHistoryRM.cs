using UnityEngine;
using System.Collections;
using PlatformIntegration.SVO;
using PlatformIntegration.CVO;

namespace PlatformIntegration.RM
{
	public class GetPlayerHistoryRM : BaseRequestModel
	{
		public GetPlayerHistoryRM() : base("getPlayerHistory") {}

		public new GetPlayerHistorySVO svo {
			get {
				return _svo as GetPlayerHistorySVO;
			}
		}
		
		public new GetPlayerHistoryCVO cvo {
			get {
				return _cvo as GetPlayerHistoryCVO;
			}
		}

		protected override void createClientValueObject ()
		{
			_cvo = new GetPlayerHistoryCVO();
		}

		protected override void createServerValueObject ()
		{
			_svo = new GetPlayerHistorySVO();
		}
	}
}