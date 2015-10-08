using UnityEngine;
using System.Collections;
using PlatformIntegration.SVO;
using PlatformIntegration.CVO;

namespace PlatformIntegration.RM
{
	public class GetMinigameRankingRM : BaseRequestModel
	{
		public GetMinigameRankingRM() : base("getMinigameRanking") {}

		public new GetMinigameRankingCVO cvo
		{
			get { 
				return _cvo as GetMinigameRankingCVO;
			}
		}

		public new GetMinigameRankingSVO svo {
			get {
				return _svo as GetMinigameRankingSVO;
			}
		}

		protected override void createClientValueObject ()
		{
			_cvo = new GetMinigameRankingCVO();
		}

		protected override void createServerValueObject ()
		{
			_svo = new GetMinigameRankingSVO();
		}
	}
}