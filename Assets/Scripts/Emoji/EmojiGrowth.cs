using System.Collections;
using UnityEngine;

public class EmojiGrowth : MonoBehaviour {
	#region attributes
	public Emoji emoji;
	public float scaleSmall = 0.2f;
	public float scaleMedium = 0.3f;
	public float scaleLarge = 0.4f;



	const float tresholdLow = 0.3f;
	const float tresholdMed = 0.7f;
	const float tresholdHigh = 1f;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init(float progress)
	{
		float scale = GetScaleValue(progress);
		transform.localScale = new Vector3(scale,scale,1f);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	float GetScaleValue(float progress)
	{
		if(progress < tresholdLow){
			return scaleSmall;
		}else if(progress < tresholdMed){
			return scaleMedium;
		}else{
			return scaleLarge;
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
