using System.Collections;
using UnityEngine;

public class Towel : TriggerableFurniture {
	public EmojiExpressionController expressionController;
	public override void OnTriggerEnter2D(Collider2D other)
	{
		base.OnTriggerEnter2D(other);
		if(other.tag == Tags.EMOJI){
			expressionController.SetEmojiExpression(FaceExpression.Blushed);
		}
	}
}