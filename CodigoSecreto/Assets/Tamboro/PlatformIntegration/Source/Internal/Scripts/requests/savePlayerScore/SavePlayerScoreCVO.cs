using UnityEngine;
using System.Collections;

namespace PlatformIntegration.CVO
{
	public class SavePlayerScoreCVO : BaseClientValueObject 
	{
		public SavePlayerScoreCVO() : base(){}

		public void setupParameters(float __score, int __stage, int __timeSpent, int __neurons, int __stars)
		{
			score = __score;
			stage = __stage;
			timeSpent = __timeSpent;
			neurons = __neurons;
			stars = __stars;
		}

		public int timeSpent 
		{ 
			get { 
				return _jsonObject.getIntegerValue("timeSpent"); 
			}

			set {
				_jsonObject.setValue("timeSpent", value);
			}
		}

		public int stage 
		{ 
			get { 
				return _jsonObject.getIntegerValue("stage"); 
			}
			
			set {
				_jsonObject.setValue("stage", value);
			}
		}

		public int neurons 
		{ 
			get { 
				return _jsonObject.getIntegerValue("neurons"); 
			}
			
			set {
				_jsonObject.setValue("neurons", value);
			}
		}

		public float score 
		{ 
			get { 
				return _jsonObject.getFloatValue("score"); 
			}

			set {
				_jsonObject.setValue("score", value);
			}
		}

		public int stars 
		{ 
			get { 
				return _jsonObject.getIntegerValue("stars"); 
			}
			
			set {
				_jsonObject.setValue("stars", value);
			}
		}
	}
}
