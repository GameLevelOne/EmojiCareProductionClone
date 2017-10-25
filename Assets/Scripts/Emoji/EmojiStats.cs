﻿using System.Collections;
using UnityEngine;
using System;

public enum EmojiStatsState{
	Hunger,
	Hygiene,
	Happiness,
	Stamina,
	Health
}

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
	public bool Debug = false;
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

	public float statsModifier{
		set{emojiModifier = value;}
	}

	public float totalModifier{
		get{return (emojiModifier + roomModifier) * DebugTickStats;}
	}

	public float RoomModifier{
		set{roomModifier = value;}
	}
		
	public float DebugTickStats{
		get{return Debug ? 150f : 1f;}
	}

	public float DebugModStats{
		get{return Debug ? 5f : 1f;}
	}

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void TickStats()
	{
		StatValue += (emojiModifier + roomModifier) * DebugTickStats;
		AdjustValue();
	}

	/// <summary>
	/// negative value is written with '-' operator e.g. -1f.
	/// </summary>
	public void ModStats(float mod)
	{
		StatValue += (mod * DebugModStats);
		AdjustValue();
	}

	/// <summary>
	/// negative value is written with '-' operator e.g. -1f.
	/// </summary>
	public void SetStats(float mod)
	{
		StatValue = mod;
		AdjustValue();
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	void AdjustValue()
	{
		if(StatValue <= 0) StatValue = 0;
		else if(StatValue >= maxStatValue) StatValue = maxStatValue;
	}
}