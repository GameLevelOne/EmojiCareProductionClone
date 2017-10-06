using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EmojiBody : MonoBehaviour {
	public Animator thisAnim;
	public Collider2D thisCollider;
	public Rigidbody2D parentRigidbody;
	
	public int previousRoom = -1, currentRoom = -1;

	//animation event
	public void Reset()
	{
		GetComponent<Animator>().SetInteger(AnimatorParameters.Ints.BODY_STATE,(int)BodyAnimation.Idle);
		parentRigidbody.simulated = true;
	}

	//animation event
	public void Reposition()
	{
		transform.parent.position = Vector3.zero;
	}

	public void DisableParentRigidBody()
	{
		parentRigidbody.simulated = false;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == Tags.MOVABLE_FURNITURE){
			Physics2D.IgnoreCollision(thisCollider,other.collider,true);
		} 
	}

	public void BounceToCurrentRoom(int currRoom)
	{
		StopCoroutine(bounceToCurrentRoom);
		StartCoroutine(bounceToCurrentRoom,currRoom);
	}

	const string bounceToCurrentRoom = "_BounceToCurrentRoom";
	IEnumerator _BounceToCurrentRoom(int currRoom)
	{
		currentRoom = currRoom;
		yield return new WaitForSeconds(0.5f);
		if(previousRoom != -1){
			if(currentRoom > previousRoom){
				thisAnim.SetInteger(AnimatorParameters.Ints.BODY_STATE,(int)BodyAnimation.BounceFromLeft);
			}else if(currentRoom < previousRoom){
				thisAnim.SetInteger(AnimatorParameters.Ints.BODY_STATE,(int)BodyAnimation.BounceFromRight);
			}
		}
		previousRoom = currRoom;
	}
}