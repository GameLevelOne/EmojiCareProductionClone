using System.Collections;
using UnityEngine;

public class EmojiPlayerInput : MonoBehaviour {
	public bool interactable;
	public Emoji emoji;


	bool flagTapCooldown = false;
	public bool flagHold = false;
	public bool flagStroke = false;
	int tapCounter = 0;

	float emojiXPos;

	#region event trigger
	public void PointerClick()
	{
		print("CLICK");
		int animDelay = 0;
		if(interactable){
			if(!flagHold && !flagStroke){
				if(!flagTapCooldown){
					tapCounter++;
					switch(tapCounter){
					case 1: print("Poked 1"); 
						animDelay = 4; 
						break;
					case 2: print("Poked 2"); 
						animDelay = 4; 
						break;
					case 3: print("Annoyed 1"); 
						animDelay = 5; 
						break;
					case 4: print("Annoyed 2"); 
						animDelay = 5; 
						break;
					case 5: print("Pouting"); 
						animDelay = 10; 
						emoji.happiness.ModStats(-3);
						break;
					}

					StopCoroutine(_TapCounterCooldown);
					StartCoroutine(_TapCounterCooldown,animDelay);

					if(tapCounter < 5){
						StartCoroutine(_TapCooldown);
					}else{
						StartCoroutine(_LockInteractions,10);
					}
				}
			}
			StopCoroutine(_HoldDelay);
			flagHold = false;
			flagStroke = false;
		}
	}
		
	public void PointerDown()
	{
		if(interactable){
			StartCoroutine(_HoldDelay,getTouchToWorldPosition());
		}
	}

	public void PointerExit()
	{
		if(interactable){
			if(flagStroke){
				EndDrag();
			}
		}
	}
		
	public void BeginDrag()
	{
		if(interactable){
			if(!flagHold){
				StopCoroutine(_HoldDelay);
				flagStroke = true;
				print("START STROKE");
			}else{
				emojiXPos = transform.position.x;
				emoji.triggerFall.ClearColliderList();
			}
		}
	}

	public void Drag()
	{
		if(interactable){
			if(flagHold){
				print("HOLDING");
				Hold();
				CheckShake();
			}else if(flagStroke){
				print("STROKING");
				Stroke();
			}
		}
	}

	public void EndDrag()
	{
		if(interactable){
			if(flagHold){
				flagHold = false;
				StartCoroutine(_StartFalling);
				print("END HOLD");
			}else if(flagStroke){
				flagStroke = false;
				print("END STROKE");
			}
		}
	}
	#endregion

	#region mechanics
	void StartHold()
	{
		emoji.body.thisCollider.enabled = false;
		emoji.thisRigidbody.simulated = false;
	}

	void Hold()
	{
		Vector3 touchPos = getTouchToWorldPosition();
		Vector3 emojiHoldPos = new Vector3(touchPos.x,touchPos.y+0.5f,touchPos.z);
		transform.position = emojiHoldPos;
	}

	void EndHold()
	{
		emoji.body.thisCollider.enabled = true;
		emoji.thisRigidbody.velocity = Vector2.zero;
		emoji.thisRigidbody.simulated = true;
	}

	void CheckShake()
	{
		
	}

	void Stroke(){}

	Vector3 getEmojiPositionOnHold(Vector3 touchWorldPosition)
	{
		return new Vector3(touchWorldPosition.x,touchWorldPosition.y+0.5f,touchWorldPosition.z);
	}

	Vector3 getTouchToWorldPosition()
	{
		//Emoji z position should be -2. Camera position is 0,0,-10f thus -10 + 8 = -2. 
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,8f);
		return Camera.main.ScreenToWorldPoint(tempMousePosition);
	}
	#endregion

	#region coroutines
	const string _TapCooldown = "TapCooldown";
	IEnumerator TapCooldown()
	{
		flagTapCooldown = true;
		yield return new WaitForSeconds(2f);
		flagTapCooldown = false;
	}

	const string _TapCounterCooldown = "TapCounterCooldown";
	IEnumerator TapCounterCooldown(int cooldown)
	{
		yield return new WaitForSeconds(cooldown);
		tapCounter = 0;
	}

	const string _HoldDelay = "HoldDelay";
	IEnumerator HoldDelay(Vector3 destination)
	{
		yield return new WaitForSeconds(1.5f);
		flagHold = true;

		emoji.body.thisCollider.enabled = false;
		emoji.thisRigidbody.simulated = false;

		Vector3 currentPos = transform.position;
		Vector3 targetPos = new Vector3(destination.x,destination.y+0.5f,destination.z);

		//animation here

		float t = 0;
		while(t < 1f){
			transform.position = Vector3.Lerp(currentPos,targetPos,t);
			t+= Time.deltaTime*6;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		transform.position = targetPos;
	}

	const string _StartFalling = "StartFalling";
	IEnumerator StartFalling()
	{
		yield return null;
		emoji.triggerFall.IgnoreCollision();
		EndHold();
	}

	const string _LockInteractions = "LockInteractions";
	IEnumerator LockInteractions(int cooldown)
	{
		print("INTERACTIONS ARE LOCKED!");
		interactable = false;
		yield return new WaitForSeconds(cooldown);
		interactable = true;
	}

	const string _DelayResetShakeCounter = "DelayResetShakeCounter";
	IEnumerator DelayResetShakeCounter()
	{
		yield return null;
	}
	#endregion
}