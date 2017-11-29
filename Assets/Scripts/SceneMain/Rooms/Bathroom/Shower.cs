using System.Collections;
using UnityEngine;

public class Shower : TriggerableFurniture {
	[Header("Shower Attributes")]
	public ShowerTrigger showerTrigger;
	public Sponge sponge;

	[Header("Custom Attributes")]
	public float hygieneModifier = 5f;

	[Header("Do Not Modify")]
	public Emoji emoji = null;
	bool flagDecreasingEmojiFoamState = false;

	public override void InitVariant ()
	{
		base.InitVariant ();
		showerTrigger.Init();
	}

	public void ModEmojiHygiene()
	{
		float hygieneMod = hygieneModifier + ((emoji.body.foamState / 10f) * hygieneModifier);
		if(!flagDecreasingEmojiFoamState){
			flagDecreasingEmojiFoamState = true;
			StartCoroutine(_StartDecreasingEmojiFoamState);
		}
	}

	public void StopDecreasingEmojiFoamState()
	{
		StopCoroutine(_StartDecreasingEmojiFoamState);

		emoji.ResetEmojiStatsModifier();
		emoji.emojiExpressions.ResetExpressionDuration();
		emoji = null;

		flagDecreasingEmojiFoamState = false;

	}

	const string _StartDecreasingEmojiFoamState = "StartDecreasingEmojiFoamState";
	IEnumerator StartDecreasingEmojiFoamState()
	{
		if(emoji != null && emoji.body.foamState > 0f){
			while(emoji.body.foamState > 0f){
				yield return null;
				emoji.body.ModEmojiFoamedValue(-1f * (Time.fixedDeltaTime * sponge.foamSpeed));
			}
		}
		flagDecreasingEmojiFoamState = false;
	}
}