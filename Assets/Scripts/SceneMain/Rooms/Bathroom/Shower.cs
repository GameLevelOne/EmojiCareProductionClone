using System.Collections;
using UnityEngine;

public class Shower : TriggerableFurniture {
	public EmojiExpressionController expressionController;
	public override void OnTriggerEnter2D(Collider2D other)
	{
		
		if(other.tag == Tags.EMOJI){
			expressionController.SetEmojiExpression(FaceExpression.Blushed);
		}
	}
}