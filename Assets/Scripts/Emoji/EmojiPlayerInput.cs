using System.Collections;
using UnityEngine;

public class EmojiPlayerInput : MonoBehaviour {
	#region attributes
	public delegate void EmojiPouting();
	public event EmojiPouting OnEmojiPouting;

	public bool interactable;
	public Emoji emoji;
	public GameObject touchInputObject;

	bool flagTapCooldown = false;
	public bool flagHold = false;
	public bool flagStroke = false;
	public bool flagTouching = false;
	public bool flagFalling = false;
	public bool flagSleeping = false;
	int tapCounter = 0;

	float emojiXPos;
	Vector3 touchTargetPosition;
	float touchX;
	Vector2 shakeVector;
	int shakeCounter = 0;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region event trigger		
	public void PointerDown()
	{
		if(interactable){
			if(!flagSleeping){
				flagHold = false;
				flagStroke = false;
				flagTouching = true;
			}
		}
	}

	public void PointerUp()
	{
		if(interactable){
			if(!flagSleeping){
				if ((!flagHold) && (!flagStroke)) {
					Poke();
				} else if (flagHold) {
					StartCoroutine(_Falling);
				} else {
					EndStroke();
				}
				flagHold = false;
				flagStroke = false;
				flagTouching = false;
			}else{
				Wake();
			}
		}
	}

	public void PointerExit()
	{
		if(interactable){
			if(flagStroke){
				flagStroke = false;
				touchInputObject.transform.localScale = Vector3.one;
				StartCoroutine(_StartHold);
			}
		}
	}

	public void BeginDrag()
	{
		if(interactable){
			if(!flagSleeping){
				touchX = getTouchToWorldPosition().x;
			}
		}
	}

	public void Drag()
	{
		if(interactable){
			if(!flagSleeping){
				Vector3 touchPos = getTouchToWorldPosition();
				touchTargetPosition = new Vector3(touchPos.x,touchPos.y+0.5f,touchPos.z);
				if(flagTouching){
					float temp = touchPos.x;
					float deltaX = temp-touchX;
					if(Mathf.Abs(deltaX) > 0.03f){
						flagTouching = false;
						flagStroke = true;
					}
				}else if (flagHold) {
					Hold();
					CheckShake();
				} else if(flagStroke){
					Stroke();
				}
			}
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	void Poke()
	{
		int animDelay = 0;
		if(!flagTapCooldown){
			tapCounter++;
			switch(tapCounter){
			case 1: print("Poked 1"); 
				emoji.emojiExpressions.ResetExpressionDuration();
				emoji.emojiExpressions.SetExpression(EmojiExpressionState.POKED,1f);
				animDelay = 4; 
				break;
			case 2: print("Poked 2"); 
				emoji.emojiExpressions.ResetExpressionDuration();
				emoji.emojiExpressions.SetExpression(EmojiExpressionState.POKED,1f);
				animDelay = 4; 
				break;
			case 3: print("Annoyed 1"); 
				emoji.emojiExpressions.ResetExpressionDuration();
				emoji.emojiExpressions.SetExpression(EmojiExpressionState.ANNOYED,1f);
				animDelay = 5; 
				break;
			case 4: print("Annoyed 2"); 
				emoji.emojiExpressions.ResetExpressionDuration();
				emoji.emojiExpressions.SetExpression(EmojiExpressionState.ANNOYED,1f);
				animDelay = 5; 
				break;
			case 5: print("Pouting"); 
				emoji.emojiExpressions.ResetExpressionDuration();
				emoji.emojiExpressions.SetExpression(EmojiExpressionState.POUTING,-1f);
				if(OnEmojiPouting != null) OnEmojiPouting();
				animDelay = 10; 
				//emoji.happiness.ModStats(-3);
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
		Vector3 touchPos = getTouchToWorldPosition();
		Vector3 emojiHoldPos = new Vector3(touchPos.x,touchPos.y+0.5f,touchPos.z);
		transform.position = emojiHoldPos;
	}

	void CheckShake()
	{
		Vector2 tempTouchWorldPos = new Vector2(getTouchToWorldPosition().x,getTouchToWorldPosition().y);
		float factor = Mathf.Sqrt( Mathf.Pow(Mathf.Abs(tempTouchWorldPos.x - shakeVector.x),2) + Mathf.Pow(Mathf.Abs(tempTouchWorldPos.y - shakeVector.y),2) );

		if(factor <= 2f){//slow move

		}else if(factor >2f && factor <= 4f){//medium move

		}else{//fast move
			StopCoroutine(_ShakeCooldown);
			shakeCounter++;
			print("ShakeCounter = "+shakeCounter);
			if(shakeCounter >= 8 && shakeCounter < 20){
				
				if(emoji.emojiExpressions.currentExpression != EmojiExpressionState.DIZZY && emoji.emojiExpressions.currentExpression != EmojiExpressionState.HOLD_BARF){
					emoji.emojiExpressions.ResetExpressionDuration();
					emoji.emojiExpressions.SetExpression(EmojiExpressionState.DIZZY,-1f);
				}
					
			}else if(shakeCounter >= 20){
				if(emoji.emojiExpressions.currentExpression != EmojiExpressionState.HOLD_BARF){

					emoji.emojiExpressions.ResetExpressionDuration();
					emoji.emojiExpressions.SetExpression(EmojiExpressionState.HOLD_BARF,-1f);
				}
					
			}
			StartCoroutine(_ShakeCooldown);
//			print("ShakeCounter = "+shakeCounter);
		}
	}

	void Stroke()
	{
		if(emoji.emojiExpressions.currentExpression != EmojiExpressionState.CARESSED)
			emoji.emojiExpressions.SetExpression(EmojiExpressionState.CARESSED,-1);
	}

	void EndStroke()
	{
		touchInputObject.transform.localScale = Vector3.one;
		emoji.emojiExpressions.ResetExpressionDuration();
		emoji.emojiExpressions.SetExpression(EmojiExpressionState.DEFAULT,0);
	}

	void Wake()
	{
		float staminaValue = emoji.stamina.StatValue / emoji.stamina.MaxStatValue;
		if(staminaValue < 0.4f){//awake lazily
			emoji.emojiExpressions.SetExpression(EmojiExpressionState.AWAKE_LAZILY,-1);
		}else if(staminaValue >= 0.4f && staminaValue < 0.8f){ //awake normally
			emoji.emojiExpressions.SetExpression(EmojiExpressionState.AWAKE_NORMALLY,-1);
		}else{//awake energetically
			emoji.emojiExpressions.SetExpression(EmojiExpressionState.AWAKE_ENERGETICALLY,-1);
		}
		emoji.ResetEmojiStatsModifier();
		interactable = false;
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
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void Eat()
	{
		emoji.emojiExpressions.SetExpression(EmojiExpressionState.EATING,3f);
		StartCoroutine(_LockInteractions,3f);
	}

	public void Reject()
	{
		emoji.emojiExpressions.SetExpression(EmojiExpressionState.REJECT,1.5f);
		StartCoroutine(_LockInteractions,1.5f);
	}

	public void Landing()
	{
		flagFalling = false;
		emoji.emojiExpressions.ResetExpressionDuration();
	}

	public void Sleep()
	{
		if(!flagSleeping){
			flagSleeping = true;
			emoji.stamina.statsModifier = 0.004f;
			emoji.emojiExpressions.SetExpression(EmojiExpressionState.SLEEP,-1);
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
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

	const string _ShakeCooldown = "ShakeCooldown";
	IEnumerator ShakeCooldown()
	{
		yield return new WaitForSeconds(0.5f);
		shakeCounter = 0;
	}

	const string _StartHold = "StartHold";
	IEnumerator StartHold()
	{
		emoji.body.thisCollider.enabled = false;
		emoji.thisRigidbody.simulated = false;

		emoji.emojiExpressions.ResetExpressionDuration();
		emoji.emojiExpressions.SetExpression(EmojiExpressionState.HOLD,-1);

		emoji.triggerFall.ClearColliderList();

		Vector3 currentPos = transform.position;

		float t = 0;
		while(t < 1f){
			
			transform.position = Vector3.Lerp(currentPos,touchTargetPosition,t);
			t+= Time.deltaTime*5;
			yield return null;
		}
		transform.position = touchTargetPosition;
		shakeVector = new Vector2(touchTargetPosition.x,touchTargetPosition.y);
		flagHold = true;
		Hold();
	}

	const string _Falling = "Falling";
	IEnumerator Falling()
	{
		flagFalling = true;
		emoji.body.thisCollider.enabled = true;
		emoji.thisRigidbody.velocity = Vector2.zero;
		emoji.thisRigidbody.simulated = true;
		emoji.emojiExpressions.SetExpression(EmojiExpressionState.FALL,-1);
		yield return null;
		emoji.triggerFall.IgnoreCollision();

	}

	const string _LockInteractions = "LockInteractions";
	IEnumerator LockInteractions(int cooldown)
	{
		print("INTERACTIONS disabled!");
		interactable = false;

		emoji.thisRigidbody.simulated = false;
		emoji.body.thisCollider.enabled = false;

		yield return new WaitForSeconds(cooldown);
		print("INTERACTIONS Enabled!");
		emoji.emojiExpressions.ResetExpressionDuration();
		interactable = true;

		emoji.body.thisCollider.enabled = true;
		emoji.thisRigidbody.simulated = true;
	}

	const string _DelayResetShakeCounter = "DelayResetShakeCounter";
	IEnumerator DelayResetShakeCounter()
	{
		yield return null;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}