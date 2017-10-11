using System.Collections;
using UnityEngine;

public class Food : TriggerableFurniture {
	#region attributes
	public EmojiExpressionController expressionController;
	Vector3 startPos;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Start()
	{
		startPos = transform.localPosition;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public override void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.EMOJI){
			expressionController.SetEmojiExpression(FaceExpression.Eat);
			transform.localPosition = startPos;
		}
	}

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
