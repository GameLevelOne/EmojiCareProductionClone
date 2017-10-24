using System.Collections;
using UnityEngine;

public class Towel : TriggerableFurniture {
	public override void OnTriggerEnter2D(Collider2D other)
	{
		base.OnTriggerEnter2D(other);
		if(other.tag == Tags.EMOJI){
			other.transform.parent.GetComponent<Emoji>().emojiExpressions.SetExpression(EmojiExpressionState.BATHING,-1);
		}
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI){
			other.transform.parent.GetComponent<Emoji>().emojiExpressions.ResetExpressionDuration();
		}
	}
}