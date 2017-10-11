using System.Collections;
using UnityEngine;
using System;

public class EmojiStats {
	#region attributes
	//constructor
	public EmojiStats(string prefKey, float emojiModifier, float maxStatValue, float startValue){
		this.prefKey = prefKey;
		this.emojiModifier = emojiModifier;
		this.maxStatValue = maxStatValue;
		if(PlayerPrefs.HasKey(prefKey) == false) this.StatValue = startValue;
	}

	string prefKey;

	float maxStatValue;
	float emojiModifier;
	float roomModifier = 0f;

	/// <summary>
	/// for PanelStatsManager, GET.
	/// </summary>
	public float StatValue{
		get{return PlayerPrefs.GetFloat(prefKey);}
		set{PlayerPrefs.SetFloat(prefKey,value);}
	}

	public float MaxStatValue{
		get{return maxStatValue;}
	}

	public float totalModifier{
		get{return (emojiModifier + roomModifier);}
	}

	public float RoomModifier{
		set{roomModifier = value;}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void TickStats()
	{
		StatValue += (emojiModifier + roomModifier);

		if(StatValue <= 0) StatValue = 0;
		else if(StatValue >= maxStatValue) StatValue = maxStatValue;
	}

	/// <summary>
	/// negative value is written with '-' operator e.g. -1f.
	/// </summary>
	public void ModStats(float mod)
	{
		StatValue += mod;

		if(StatValue <= 0) StatValue = 0;
		else if(StatValue >= maxStatValue) StatValue = maxStatValue;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}