using System.Collections;
using UnityEngine;

public class EmojiGrowth : MonoBehaviour {
	public delegate void EmojiGrowEvent();
	public event EmojiGrowEvent OnEmojiGrow;

	#region attributes
	public Emoji emoji;
	public float scaleSmall = 0.2f;
	public float scaleMedium = 0.3f;
	public float scaleLarge = 0.4f;



	const float tresholdLow = 0.3f;
	const float tresholdMed = 0.7f;
	const float tresholdHigh = 1f;

	float currentScale{
		get{return PlayerPrefs.GetFloat(PlayerPrefKeys.Emoji.CURRENT_SCALE,scaleSmall);}
		set{PlayerPrefs.SetFloat(PlayerPrefKeys.Emoji.CURRENT_SCALE,value);}
	}


	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void UpdateGrowth(float progress)
	{
		float newScaleValue = GetScaleValue(progress);
		if(newScaleValue > currentScale){
			currentScale = newScaleValue;
			transform.localScale = new Vector3(currentScale,currentScale,1f);
		}

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
