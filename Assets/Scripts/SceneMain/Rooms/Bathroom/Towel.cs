using System.Collections;
using UnityEngine;

public class Towel : TriggerableFurniture {
	protected override void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI_BODY){
			other.transform.parent.GetComponent<Emoji>().emojiExpressions.SetExpression(EmojiExpressionState.BATHING,-1);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI_BODY){
			other.transform.parent.GetComponent<Emoji>().emojiExpressions.ResetExpressionDuration();
		}
	}
}