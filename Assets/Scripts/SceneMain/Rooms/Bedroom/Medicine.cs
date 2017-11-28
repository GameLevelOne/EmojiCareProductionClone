using System.Collections;
using UnityEngine;

public class Medicine : Syringe {
	protected override void CheckPlayerCoin()
	{
		//check price, if enough coin
		print("Price = "+price+", you have "+PlayerData.Instance.PlayerCoin);
		if(PlayerData.Instance.PlayerCoin >= price){
			//check stats, if low
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
		HideUICoin(false);
	}
}
