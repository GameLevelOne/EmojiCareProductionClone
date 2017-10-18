using System.Collections;
using UnityEngine;

public class SceneEmojiEditManager : MonoBehaviour {
	#region attributes
	public Emoji emoji;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
//	void Awake()
//	{
//		emoji.GetComponent<Rigidbody2D>().simulated = false;
//	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void ButtonBodyAnimation(int state)
	{
		emoji.body.thisAnim.SetInteger(AnimatorParameters.Ints.BODY_STATE,state);
	}
	public void ButtonFaceAnimation(int state)
	{
		emoji.emojiExpressions.SetExpression((FaceExpression)state,-1);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}