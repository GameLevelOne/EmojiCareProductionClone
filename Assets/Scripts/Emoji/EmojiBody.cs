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
	public Emoji emoji;

	public int previousRoom = -1, currentRoom = -1;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Awake()
	{
		emoji.emojiExpressions.OnChangeExpression += OnChangeExpression;
	}
		
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region animation event
	public void Reset()
	{
		if(parentRigidbody.simulated == false) parentRigidbody.simulated = true;

		//sementara
		emoji.emojiExpressions.ResetExpressionDuration();
		emoji.transform.localScale = Vector3.one;
	}
		
	public void Reposition()
	{
		transform.parent.localPosition = new Vector3(0,0,-2f);
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
		if(emoji.playerInput.flagFalling == true) emoji.playerInput.Landing();
		if(other.gameObject.tag == Tags.MOVABLE_FURNITURE){
			Physics2D.IgnoreCollision(thisCollider,other.collider,true);
		} 
		if(other.gameObject.tag == Tags.BED){
			
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
		StartCoroutine(_Bounce,currRoom);
	}

	public void CancelBouncing()
	{
		StopCoroutine(_Bounce);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region coroutines
	const string _Bounce = "Bounce";
	IEnumerator Bounce(int currRoom)
	{
		currentRoom = currRoom;
		yield return new WaitForSeconds(0.5f);
		if(previousRoom != -1){
			if(currentRoom > previousRoom){
				
				PlayerData.Instance.PlayerEmoji.emojiExpressions.ResetExpressionDuration();
				emoji.emojiExpressions.SetExpression(EmojiExpressionState.CHANGE_ROOM,-1f);
				if(OnEmojiBouncingToCurrentRoom != null) OnEmojiBouncingToCurrentRoom();

			}else if(currentRoom < previousRoom){
				
				emoji.transform.localScale = new Vector3(-1f,1f,1f);
				PlayerData.Instance.PlayerEmoji.emojiExpressions.ResetExpressionDuration();
				emoji.emojiExpressions.SetExpression(EmojiExpressionState.CHANGE_ROOM,-1f);
				if(OnEmojiBouncingToCurrentRoom != null) OnEmojiBouncingToCurrentRoom();
			}
		}
		previousRoom = currRoom;
	}

	const string _ResetFaceExpression = "ResetFaceExpression";
	IEnumerator ResetFaceExpression()
	{
		while(emoji.emojiExpressions.currentDuration >= 0){
			emoji.emojiExpressions.currentDuration -= Time.deltaTime;
			yield return null;
		}
		emoji.emojiExpressions.currentDuration = 0;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}