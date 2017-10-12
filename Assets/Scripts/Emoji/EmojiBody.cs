using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EmojiBody : MonoBehaviour {
	#region attributes
	public Animator thisAnim;
	public Collider2D thisCollider;
	public Rigidbody2D parentRigidbody;
	public Emoji parent;

	public int previousRoom = -1, currentRoom = -1;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Awake()
	{
		parent.emojiExpressions.OnChangeExpression += OnChangeExpression;
	}
		
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region animation event
	public void Reset()
	{
		GetComponent<Animator>().SetInteger(AnimatorParameters.Ints.BODY_STATE,(int)BodyAnimation.Idle);
		parentRigidbody.simulated = true;
	}
		
	public void Reposition()
	{
		transform.parent.position = Vector3.zero;
	}

	public void DisableParentRigidBody()
	{
		parentRigidbody.simulated = false;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	//colliders
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == Tags.MOVABLE_FURNITURE){
			Physics2D.IgnoreCollision(thisCollider,other.collider,true);
		} 
		if(other.gameObject.tag == Tags.BED){
			parent.emojiExpressions.SetExpression(FaceExpression.Sleep,true);
		}

	}

	//delegate events
	void OnChangeExpression (bool expressionStay)
	{
		StopCoroutine("resetFaceExpression");
		Reposition();
		StartCoroutine("resetFaceExpression",expressionStay);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void BounceToCurrentRoom(int currRoom)
	{
		StopCoroutine(bounceToCurrentRoom);
		StartCoroutine(bounceToCurrentRoom,currRoom);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region coroutines
	const string bounceToCurrentRoom = "_BounceToCurrentRoom";
	IEnumerator _BounceToCurrentRoom(int currRoom)
	{
		currentRoom = currRoom;
		yield return new WaitForSeconds(1f);
		if(previousRoom != -1){
			if(currentRoom > previousRoom){
				thisAnim.SetInteger(AnimatorParameters.Ints.BODY_STATE,(int)BodyAnimation.BounceFromLeft);
			}else if(currentRoom < previousRoom){
				thisAnim.SetInteger(AnimatorParameters.Ints.BODY_STATE,(int)BodyAnimation.BounceFromRight);
			}
		}
		previousRoom = currRoom;
	}

	IEnumerator resetFaceExpression(bool expressionStay)
	{
		if(!expressionStay){
			yield return new WaitForSeconds(3f);
			parent.emojiExpressions.SetExpression(parent.emojiExpressions.staticExpression,true);
			parent.emojiExpressions.isExpressing = false;
		}
		yield return null;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}