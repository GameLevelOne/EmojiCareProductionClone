using System.Collections;
using UnityEngine;

public class Medicine : Syringe {
	protected override void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == Tags.EMOJI_BODY){
			Emoji emoji = other.transform.parent.GetComponent<Emoji>();
			float healthValue = emoji.health.StatValue/emoji.health.MaxStatValue;
			if(healthValue >= Emoji.statsTresholdLow && healthValue < Emoji.statsTresholdMed){
				emoji.health.SetStats(emojiHealthSet);
				emoji.emojiExpressions.SetExpression(emojiResponse,2f);
			}else{
				emoji.playerInput.Reject();
			}
			StopCoroutine(_Return);
			ResetPosition();
		}
	}
}
