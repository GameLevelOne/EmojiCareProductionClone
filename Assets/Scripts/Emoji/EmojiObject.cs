using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiObject : MonoBehaviour {
	#region attributes
	Rigidbody2D thisRigidbody;
	Collider2D thisCollider;
	Animator bodyAnimation, faceAnimation;

	List<Collider2D> otherColliders = new List<Collider2D>();
	bool isFalling = false;
	bool isSleeping = false;
	bool isChangingRoom = false;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Awake()
	{
		Init();
	}

	void Init()
	{
		thisRigidbody = GetComponent<Rigidbody2D>();
		thisCollider = GetComponent<Collider2D>();
		bodyAnimation = transform.GetChild(0).GetComponent<Animator>();
		faceAnimation = transform.GetChild(0).FindChild("Face").GetComponent<Animator>();
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	//triggers and colliders
	void OnCollisionEnter2D(Collision2D other)
	{
		if(isFalling){
			isFalling = false;
			bodyAnimation.SetInteger(AnimatorParameters.Ints.BODY_STATE,(int)BodyAnimation.Idle);
		}	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.FLOOR || other.tag == Tags.IMMOVABLE_FURNITURE) {
			otherColliders.Add(other.transform.parent.GetComponent<Collider2D>());
		}
		if(other.tag == Tags.BED){
			isSleeping = true;
		}
	}

	//event triggers
	public void BeginDrag()
	{
		if(!isChangingRoom){
			if(isSleeping){ 
				isSleeping = false; 
				ChangeExpression(FaceAnimation.Default);
			}
			if(isFalling) isFalling = false;

			bodyAnimation.SetInteger(AnimatorParameters.Ints.BODY_STATE,(int)BodyAnimation.Falling);

			thisRigidbody.simulated = false;
			if(otherColliders.Count != 0) foreach(Collider2D c in otherColliders) Physics2D.IgnoreCollision(c,thisCollider,false);
			otherColliders.Clear();
		}
	}

	public void Drag()
	{
		if(!isChangingRoom){
			Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,8f);
			transform.localPosition = Camera.main.ScreenToWorldPoint(tempMousePosition);
		}
	}

	public void EndDrag()
	{	
		if(!isChangingRoom){
			thisRigidbody.simulated = true;
			StartCoroutine(IgnoreCollissions());
		}
	}

	public void OnRoomChangingStart()
	{
		if(!isChangingRoom){
			StartCoroutine(HangEmoji());
		}
	

	}

	public void OnRoomChangingEnd()
	{
		if(isChangingRoom){
			StartCoroutine(ReleaseEmoji());
		}
	}

	//expressions
	public void ChangeBodyAnimation(BodyAnimation anim)
	{
		bodyAnimation.SetInteger(AnimatorParameters.Ints.BODY_STATE,(int)anim);
	}

	public void ChangeExpression(FaceAnimation anim)
	{
		faceAnimation.SetInteger(AnimatorParameters.Ints.FACE_STATE,(int)anim);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	#region coroutines
	IEnumerator IgnoreCollissions()
	{
		yield return null;
		if(otherColliders.Count != 0){
			foreach(Collider2D c in otherColliders){ 
				Physics2D.IgnoreCollision(c,thisCollider);
			}
		}

		if(isSleeping){
			ChangeExpression(FaceAnimation.Sleep);
			ChangeBodyAnimation(BodyAnimation.Idle);
		}else{
			isFalling = true;
			ChangeBodyAnimation(BodyAnimation.Falling);
		}
	}

	IEnumerator HangEmoji()
	{
		isChangingRoom = true;
		thisCollider.enabled = false;
		thisRigidbody.simulated = false;
		thisRigidbody.velocity = Vector2.zero;
		thisRigidbody.angularVelocity = 0f;

		float t = 0f;
		while(t <= 1f){
			t += Time.deltaTime*2f;
			transform.localPosition = Vector3.Lerp(transform.localPosition,new Vector3(0f,1.5f,-2f),Mathf.SmoothStep(0f,1f,t));
			yield return new WaitForSeconds(Time.deltaTime);
		}

	}

	IEnumerator ReleaseEmoji()
	{
		float t = 0f;
		while(t <= 1f){
			t += Time.deltaTime*2f;
			transform.localPosition = Vector3.Lerp(transform.localPosition,new Vector3(0f,0f,-2f),Mathf.SmoothStep(0f,1f,t));
			yield return new WaitForSeconds(Time.deltaTime);
		}

		isChangingRoom = false;
		thisCollider.enabled = true;
		thisRigidbody.simulated = true;
	}

	#endregion
}
