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
	[Header("Emoji Body Attributes")]
	public Animator thisAnim;

	public Collider2D thisCollider;
	public Rigidbody2D parentRigidbody;
	public Transform hatParent;
	public Emoji emoji;

	public int previousRoom = -1, currentRoom = -1;

	public float foamState = 0f;
	public bool flagSleep = false;
	public bool flagAfterChangingRoom = false;

	[Header("BubbleEffect")]
	public Animator bubbleEffectAnim;
	public Animator kinclongAnim;
	public EmojiBubbleEffect bubbleEffect;

	[Header("Do Not Modify")]
	public GameObject hatObject;

	public Vector3 emojiCurrentScale;
	public Vector3 emojiCurrentMirroredScale;
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

		emoji.transform.localScale = emojiCurrentScale;

	}

	public void OnChangingRoomEnd()
	{
//		flagAfterChangingRoom = true;
//		emoji.playerInput.Fall();
	}

	public void BounceSound()
	{
		SoundManager.Instance.PlaySFXOneShot(SFXList.Bounce);
	}

	public void YoSound()
	{
		SoundManager.Instance.PlayVoice(VoiceList.Yo);
	}

	public void UrrhSound()
	{
		SoundManager.Instance.PlayVoice(VoiceList.Urrh);
	}

	public void Reposition()
	{
		transform.parent.localPosition = new Vector3(0,-2.665f,-2f);
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

		float stamina = emoji.stamina.StatValue/emoji.stamina.MaxStatValue;
		if(stamina < 0.4f){
			SoundManager.Instance.PlayVoice(VoiceList.Sigh);
		}else if(stamina >= 0.4f && stamina < 0.8f){
			SoundManager.Instance.PlayVoice(VoiceList.Mmm);
		}else{
			SoundManager.Instance.PlayVoice(VoiceList.Yo);
		}

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
		if(other.gameObject.tag == Tags.IMMOVABLE_FURNITURE){
			if(flagSleep) Physics2D.IgnoreCollision(thisCollider,other.collider);
		}

		emoji.playerInput.Landing();

		if(other.gameObject.tag == Tags.BED){
			if(flagSleep){
				DoSleep();
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
	public void DoSleep()
	{
		emoji.playerInput.Sleep();
		emoji.EmojiSleeping = true;
		if(OnEmojiSleepEvent != null) OnEmojiSleepEvent(emoji.playerInput.flagSleeping);
		flagSleep = false;
	}

	public void BounceToCurrentRoom(int currRoom)
	{
		StartCoroutine(_Bounce,currRoom);
	}

	public void CancelBouncing()
	{
		StopCoroutine(_Bounce);
	}
		
	public void OnEmojiEatOrReject(float duration)
	{
		if(OnEmojiEatEvent != null) OnEmojiEatEvent(duration);
	}

	//bathrooom
	public void ModEmojiFoamedValue(float value)
	{
		foamState += value;
		if(foamState > 10f) foamState = 10f;
		else if(foamState < 0) foamState = 0;
		print("FOAM STATE = "+foamState);
		bubbleEffectAnim.SetFloat(AnimatorParameters.Floats.FOAM_STATE,foamState);
		bubbleEffect.SetBubbleAlpha(foamState/10f);
	}

	public void EmojiKinclong()
	{
		kinclongAnim.SetTrigger(AnimatorParameters.Triggers.ANIMATE);
	}

	public void CheckRoomForBubbleMechanic(RoomType room)
	{
		if(room != RoomType.Bathroom){
			StartCoroutine(_ReduceBubbleEffect);
		}else{
			StopCoroutine(_ReduceBubbleEffect);
		}
	}

	/// <summary>
	/// ID = 0 >>> Remove hat / emoji does not wear any hat
	/// </summary>
	public void WearHat(string ID, GameObject hatObject)
	{
		RemoveHat();
		if(ID == "0") return;

		this.hatObject = Instantiate(hatObject,hatParent) as GameObject;
		PlayerData.Instance.inventory.SetCurrentHat(ID);
	}

	public void RemoveHat()
	{
		if(hatObject != null){
			Destroy(hatParent.GetChild(0).gameObject);
			PlayerData.Instance.inventory.SetCurrentHat(string.Empty);
			hatObject = null;
		}		
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
				Vector3 emojiScale = emoji.emojiGrowth.GetScale();
				emoji.transform.localScale = emojiCurrentMirroredScale;
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
		emoji.emojiExpressions.ResetExpressionDuration();
	}

	const string _ReduceBubbleEffect = "ReduceBubbleEffect";
	IEnumerator ReduceBubbleEffect()
	{
		while(foamState > 0f){
			ModEmojiFoamedValue((Time.fixedDeltaTime * 0.2f) * -1f);
			print(foamState);
			yield return null;
		}
		foamState = 0f;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}