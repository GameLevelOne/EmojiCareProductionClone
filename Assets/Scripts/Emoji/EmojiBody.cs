using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public enum BodyAnimation{
	Idle,
	Bounce,
	Play,
	HappyBounce,
	Falling,
	BounceFromLeft,
	BounceFromRight,
	Eat,
	Bath,
	StrokeRight,
	StrokeLeft,
	Lift,
	Tap
}

public class EmojiBody : MonoBehaviour {
	public delegate void EmojiBouncingToCurrentRoom();
	public event EmojiBouncingToCurrentRoom OnEmojiBouncingToCurrentRoom;

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
		if(parentRigidbody.simulated == false) parentRigidbody.simulated = true;

		//sementara
		parent.emojiExpressions.ResetExpressionDuration();
		parent.transform.localScale = Vector3.one;
	}
		
	public void Reposition()
	{
		transform.parent.position = new Vector3(0,0,-2f);
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
//			parent.emojiExpressions.ResetExpressionDuration();
//			parent.emojiExpressions.SetExpression(EmojiExpressionState.SLEEP,-1);
			//set sleep

		}
	}

	//delegate events
	void OnChangeExpression ()
	{
		StopCoroutine(_ResetFaceExpression);
		StartCoroutine(_ResetFaceExpression);
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
		yield return new WaitForSeconds(0.5f);
		if(previousRoom != -1){
			if(currentRoom > previousRoom){
				//thisAnim.SetInteger(AnimatorParameters.Ints.BODY_STATE,(int)BodyAnimation.BounceFromLeft);
				parent.emojiExpressions.SetExpression(EmojiExpressionState.CHANGE_ROOM,-1f);
				if(OnEmojiBouncingToCurrentRoom != null) OnEmojiBouncingToCurrentRoom();
			}else if(currentRoom < previousRoom){
				//thisAnim.SetInteger(AnimatorParameters.Ints.BODY_STATE,(int)BodyAnimation.BounceFromRight);
				parent.transform.localScale = new Vector3(-1f,1f,1f);
				parent.emojiExpressions.SetExpression(EmojiExpressionState.CHANGE_ROOM,-1f);
				if(OnEmojiBouncingToCurrentRoom != null) OnEmojiBouncingToCurrentRoom();
			}
		}
		previousRoom = currRoom;
	}

	const string _ResetFaceExpression = "ResetFaceExpression";
	IEnumerator ResetFaceExpression()
	{
		while(parent.emojiExpressions.currentDuration >= 0){
			Debug.Log("CurDur: "+parent.emojiExpressions.currentDuration);
			parent.emojiExpressions.currentDuration -= Time.deltaTime;
			yield return null;
		}
		parent.emojiExpressions.currentDuration = 0;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}