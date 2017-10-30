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
	public delegate void EmojiSleepEvent(bool sleeping);
	public delegate void EmojiEatEvent(float lockDuration);
	public event EmojiBouncingToCurrentRoom OnEmojiBouncingToCurrentRoom;
	public event EmojiSleepEvent OnEmojiSleepEvent;
	public event EmojiEatEvent OnEmojiEatEvent;

	#region attributes
	public Animator thisAnim;
	public Collider2D thisCollider;
	public Rigidbody2D parentRigidbody;
	public Emoji emoji;

	public int previousRoom = -1, currentRoom = -1;

	public float foamState = 1f;
	public bool flagSleep = false;
	public bool flagAfterChangingRoom = false;
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
		if(thisCollider.enabled == false) thisCollider.enabled = true;

		//sementara
		emoji.emojiExpressions.ResetExpressionDuration();
		emoji.emojiExpressions.SetExpression(EmojiExpressionState.DEFAULT,0);
		emoji.transform.localScale = Vector3.one;
	}

	public void OnChangingRoomEnd()
	{
//		flagAfterChangingRoom = true;
//		emoji.playerInput.Fall();
	}

	public void Reposition()
	{
		transform.parent.localPosition = new Vector3(0,-2.485f,-2f);
		emoji.triggerFall.thisCollider.enabled = false;
	}

	public void DisableParentRigidBody()
	{
		parentRigidbody.simulated = false;
		thisCollider.enabled = false;
	}

	public void OnAwakeAnimationEnd()
	{
		emoji.playerInput.flagSleeping = false;
		emoji.emojiExpressions.ResetExpressionDuration();
		emoji.playerInput.interactable = true;
		if(OnEmojiSleepEvent != null) OnEmojiSleepEvent(emoji.playerInput.flagSleeping);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	//colliders
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.BED){
			if(!flagAfterChangingRoom){
				flagSleep = true;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		emoji.playerInput.Landing();

		if(other.gameObject.tag == Tags.BED){
			if(flagSleep){
				emoji.playerInput.Sleep();
				if(OnEmojiSleepEvent != null) OnEmojiSleepEvent(emoji.playerInput.flagSleeping);
				flagSleep = false;
			}
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

	public void StartFoaming()
	{
		StartCoroutine(_Foamed);
	}

	public void StopFoaming()
	{
		StopCoroutine(_Foamed);
	}

	public void OnEmojiEatOrReject(float duration)
	{
		if(OnEmojiEatEvent != null) OnEmojiEatEvent(duration);
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

	const string _Foamed = "Foamed";
	IEnumerator Foamed()
	{
		//for 3 seconds, foam state increase from 1 to 10.
		while(foamState < 10f){
			foamState += (Time.deltaTime * (10f/3f));
			print(foamState);
			yield return null;
		}
		foamState = 10f;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}