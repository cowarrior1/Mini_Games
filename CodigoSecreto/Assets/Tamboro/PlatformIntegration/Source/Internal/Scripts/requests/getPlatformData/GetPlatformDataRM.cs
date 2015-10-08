using UnityEngine;
using System.Collections;
using PlatformIntegration.SVO;
using PlatformIntegration.CVO;

namespace PlatformIntegration.RM
{
	public class GetPlatformDataRM : BaseRequestModel
	{
		public GetPlatformDataRM() : base("getPlatformData") {}


		public new GetPlatformDataSVO svo {
			get {
				return _svo as GetPlatformDataSVO;
			}
		}

		public new GetPlatformDataCVO cvo {
			get {
				return _cvo as GetPlatformDataCVO;
			}
		}

		protected override void createClientValueObject ()
		{
			_cvo = new GetPlatformDataCVO();
		}

		protected override void createServerValueObject ()
		{
			_svo = new GetPlatformDataSVO();
		}
	}
}