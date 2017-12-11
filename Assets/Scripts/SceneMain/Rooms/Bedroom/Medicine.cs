using System.Collections;
using UnityEngine;

public class Medicine : Syringe {

	protected override void CheckEmojiHealth ()
	{
		float healthValue = emoji.health.StatValue/emoji.health.MaxStatValue;
		if(healthValue >= Emoji.statsTresholdLow && healthValue < Emoji.statsTresholdMed){
			//apply
			StopAllCoroutines();
			HideUICoin(true);
			emoji.playerInput.ApplyMedicineOrSyringe(reactionDuration+2f);
			StartCoroutine(_Apply);

			return;
		}else{
			//reject
			emoji.playerInput.Reject();
		}
	}
}
