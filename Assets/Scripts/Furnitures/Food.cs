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
		print(other.name);
		if(other.tag == Tags.EMOJI){
			other.transform.parent.GetComponent<Emoji>().emojiExpressions.SetExpression(FaceExpression.Eat,2f);
			transform.localPosition = startPos;
		}
	}

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
