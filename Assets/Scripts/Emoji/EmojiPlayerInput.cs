using System.Collections;
using UnityEngine;

public class EmojiPlayerInput : MonoBehaviour {
	public bool interactable;
	public Emoji emoji;
	public GameObject touchInputObject;

	bool flagTapCooldown = false;
	public bool flagHold = false;
	public bool flagStroke = false;
	int tapCounter = 0;

	float emojiXPos;

	public float holdThreshold = 1.5f;
	float startTap;

	#region event trigger		
	public void PointerDown()
	{
		if(interactable){
			startTap = Time.timeSinceLevelLoad;
			flagHold = false;
			flagStroke = false;
			StartCoroutine(_DelayHold);
		}
	}

	public void PointerUp()
	{
		StopCoroutine(_DelayHold);
		if(interactable){
			if ((!flagHold) && (!flagStroke)) {
				Poke();
			} else if (flagHold) {
				StartCoroutine(_Falling);
			} else {
				EndStroke();
			}
		}
		touchInputObject.GetComponent<Collider2D>().enabled = true;
	}


	public void PointerExit()
	{
		if(interactable){
			if(flagStroke){
				EndStroke();
				touchInputObject.GetComponent<Collider2D>().enabled = false;
			}
		}
	}

	public void BeginDrag()
	{
		if(interactable){
			if(!flagHold){
				print("coroutine stopped");
				StopCoroutine(_DelayHold);
				flagHold = false;
				flagStroke = true;
				Stroke();
				touchInputObject.transform.localScale = new Vector3(1.5f,1.5f,1f);
			}
		}
	}

	public void Drag()
	{
		if(interactable){
			if (flagHold) {
				Hold();
			} else {
				Stroke();
			}
		}
	}
	#endregion

	#region mechanics
	void Poke()
	{
		print("Poke");
		int animDelay = 0;
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

	void Hold()
	{
		print("Hold");
		Vector3 touchPos = getTouchToWorldPosition();
		Vector3 emojiHoldPos = new Vector3(touchPos.x,touchPos.y+0.5f,touchPos.z);
		transform.position = emojiHoldPos;
	}

	void Stroke()
	{
		//set anim
		print("Stroke");
	}

	void EndStroke()
	{
		//set anim
		print("EndStroke");
		touchInputObject.transform.localScale = Vector3.one;
	}

	//----------------------------------------------------------------=====NON-VOID MODULES=====----------------------------------------------------------------
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

	const string _DelayHold = "DelayHold";
	IEnumerator DelayHold()
	{
		yield return new WaitForSeconds(1.5f);
		flagHold = true;

		Vector3 touchPos = getTouchToWorldPosition();
		emoji.body.thisCollider.enabled = false;
		emoji.thisRigidbody.simulated = false;

		Vector3 currentPos = transform.position;
		Vector3 targetPos = new Vector3(touchPos.x,touchPos.y+0.5f,touchPos.z);

		//animation here

		float t = 0;
		while(t < 1f){
			transform.position = Vector3.Lerp(currentPos,targetPos,t);
			t+= Time.deltaTime*10;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		transform.position = targetPos;
	}

	const string _Falling = "Falling";
	IEnumerator Falling()
	{
		emoji.body.thisCollider.enabled = true;
		emoji.thisRigidbody.velocity = Vector2.zero;
		emoji.thisRigidbody.simulated = true;
		yield return null;
		emoji.triggerFall.IgnoreCollision();

	}

	const string _LockInteractions = "LockInteractions";
	IEnumerator LockInteractions(int cooldown)
	{
		print("INTERACTIONS disabled!");
		interactable = false;
		yield return new WaitForSeconds(cooldown);
		print("INTERACTIONS Enabled!");
		interactable = true;
	}

	const string _DelayResetShakeCounter = "DelayResetShakeCounter";
	IEnumerator DelayResetShakeCounter()
	{
		yield return null;
	}
	#endregion
}