using UnityEngine;
using System.Collections;
using PlatformIntegration.SVO;
using PlatformIntegration.CVO;

namespace PlatformIntegration.RM
{
	public class SavePlayerScoreRM : BaseRequestModel
	{
		public SavePlayerScoreRM() : base("savePlayerScore") {}

		public new SavePlayerScoreSVO svo {
			get {
				return _svo as SavePlayerScoreSVO;
			}
		}

		public new SavePlayerScoreCVO cvo {
			get {
				return _cvo as SavePlayerScoreCVO;
			}
		}

		protected override void createClientValueObject ()
		{
			_cvo = new SavePlayerScoreCVO();
		}

		protected override void createServerValueObject ()
		{
			_svo = new SavePlayerScoreSVO();
		}
	}
}
