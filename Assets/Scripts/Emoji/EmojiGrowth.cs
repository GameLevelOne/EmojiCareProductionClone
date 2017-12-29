using System.Collections;
using UnityEngine;

public enum EmojiAgeType{
	Baby,
	Juvenille,
	Adult
}

public class EmojiGrowth : MonoBehaviour {

	#region attributes
	public Emoji emoji;
	public float scaleSmall = 0.5f;
	public float scaleMedium = 0.65f;
	public float scaleLarge = 0.8f;

	const float tresholdLow = 0.3f;
	const float tresholdMed = 0.7f;
	const float tresholdHigh = 1f;

	float currentScale{
		get{return PlayerPrefs.GetFloat(PlayerPrefKeys.Emoji.CURRENT_SCALE,scaleSmall);}
		set{PlayerPrefs.SetFloat(PlayerPrefKeys.Emoji.CURRENT_SCALE,value);}
	}

	public Vector3 GetScale()
	{
		return new Vector3(currentScale,currentScale,1f);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init()
	{
		
	}

	public void UpdateGrowth(float progress)
	{
		float newScaleValue = GetScaleValue(progress);
		if(emoji.transform.localScale.x != scaleLarge && newScaleValue == scaleLarge){
			emoji.GetComponent<BabyEmoji>().GrowToAdult();
		}else if(newScaleValue >= currentScale){
			currentScale = newScaleValue;
			emoji.GetComponent<BabyEmoji>().GrowToJuvenille();
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