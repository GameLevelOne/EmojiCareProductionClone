﻿using System.Collections;
using UnityEngine;

public class Shower : TriggerableFurniture {
	[Header("Shower Attributes")]
	public ShowerTrigger showerTrigger;

	[Header("Custom Attributes")]
	public float hygieneModifier = 5f;
	public float showerSpeed = 2f;

	[Header("Do Not Modify")]
	bool flagDecreasingEmojiFoamState = false;

	public override void InitVariant ()
	{
		base.InitVariant ();
		showerTrigger.Init();
	}

	public void ModEmojiHygiene()
	{
		float hygieneMod = hygieneModifier + ((PlayerData.Instance.PlayerEmoji.body.foamState / 10f) * hygieneModifier);
		if(!flagDecreasingEmojiFoamState){
			flagDecreasingEmojiFoamState = true;
			StartCoroutine(_StartDecreasingEmojiFoamState);
		}
	}

	public void StopDecreasingEmojiFoamState()
	{
		StopCoroutine(_StartDecreasingEmojiFoamState);

		PlayerData.Instance.PlayerEmoji.ResetEmojiStatsModifier();
		PlayerData.Instance.PlayerEmoji.emojiExpressions.ResetExpressionDuration();

		flagDecreasingEmojiFoamState = false;

	}

	const string _StartDecreasingEmojiFoamState = "StartDecreasingEmojiFoamState";
	IEnumerator StartDecreasingEmojiFoamState()
	{
		if(PlayerData.Instance.PlayerEmoji != null && PlayerData.Instance.PlayerEmoji.body.foamState > 0f){
			while(PlayerData.Instance.PlayerEmoji.body.foamState > 0f){
				yield return null;
				PlayerData.Instance.PlayerEmoji.body.ModEmojiFoamedValue(-1f * (Time.fixedDeltaTime * showerSpeed));
			}
			PlayerData.Instance.PlayerEmoji.body.EmojiKinclong();
		}
		flagDecreasingEmojiFoamState = false;

	}
}