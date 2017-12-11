using System.Collections;
using UnityEngine;

public class EmojiPlayerInput : MonoBehaviour {
	#region attributes
	public delegate void EmojiPouting();
	public delegate void EmojiWake();
	public event EmojiPouting OnEmojiPouting;
	public event EmojiWake OnEmojiWake;

	public bool interactable;
	public Emoji emoji;
	public GameObject touchInputObject;

	bool flagTapCooldown = false;
	public bool flagStrokeToHold = false;
	public bool flagHold = false;
	public bool flagStroke = false;
	public bool flagTouching = false;
	public bool flagFalling = false;
	public bool flagSleeping = false;
	public float eatDuration = 3f;
	public float rejectDuration = 2.5f;
	[Header("HoldMove Mechanic")]
	public float slowMediumTreshold = 3f;
	public float mediumFastTreshold = 6f;
	public bool isRetaining = false;
	public float retainCooldown = 1f;

	[Header("Shake Mechanic")]
	public float shakeCounterCooldown = 0.5f;
	public float shakeExpressionCooldown = 2f;

	[Header("Toy mechanic")]
	public float toyBumpResponseCooldown = 2f;
	public float toyBumpRetainCooldown = 7f;
	public int toyBumpCounter = 0;
	public bool toyBumped = false;

	[Header("Minigame Mechanic")]
	public float happinessModOnBlocks = 2.5f;
	public float happinessModOnDanceMat = 1.5f;
	public float happinessModOnDoodle = 2.5f;

	public float dartboardCooldown = 2f;
	public float blocksCooldown = 5f;
	public float danceMatCooldown = 10f;
	public float doodleCooldown = 10f;

	public bool dartboardRetaining = false;
	public bool blocksRetaining = false;
	public bool danceMatRetaining = false;
	public bool doodleRetaining = false;

	[Header("Sound")]
	public bool dizzySound = false;
	public bool barfSound = false;

	Vector3 touchTargetPosition;
	Vector2 prevMoveVector;

	int tapCounter = 0;
	float touchX;
	float prevHoldFactor = 0f;
	float prevXPos = 0f;
	float startSleepStamina = 0f;

	int shakeCounter = 0;
	bool shakeExpressionRetain = false;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region event trigger		
	public void PointerDown()
	{
		if(interactable){
			if(!flagSleeping){
				flagHold = false;
				flagStroke = false;
				flagStrokeToHold = false;
				flagTouching = true;
			}
		}
	}

	public void PointerUp()
	{
		if(interactable){
			if(!flagSleeping){
				StopAllCoroutines();
				if ((!flagHold) && (!flagStroke)) {
					Poke();
				} else if (flagHold) {
					Fall();
				} else if(flagStrokeToHold){
					Fall();
				}else {
					EndStroke();
				}
				flagHold = false;
				flagStroke = false;
				flagTouching = false;
				flagStrokeToHold = false;

				emoji.thisRigidbody.velocity = Vector2.zero;
				emoji.thisRigidbody.simulated = true;
				emoji.body.thisCollider.enabled = true;
//				print("COLLIDER = "+emoji.body.thisCollider.enabled+", RIGIDBODY = "+emoji.thisRigidbody.simulated);
//				print("POINTER UP JING");
			}else{
				Wake();
			}
		}
		dizzySound = barfSound = false;
		isRetaining = false;
	}

	public void PointerExit()
	{
		if(interactable){
			if(flagStroke){
				flagStroke = false;
				touchInputObject.transform.localScale = Vector3.one;
				StartCoroutine(_StartHold);
				SoundManager.Instance.PlayVoice(VoiceList.Huh);
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
						SoundManager.Instance.PlayVoice(VoiceList.Laugh);
					}
				}else if (flagHold) {
					HoldMove();

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
				SoundManager.Instance.PlayVoice(VoiceList.Huh);
				animDelay = 4; 
				break;
			case 2: print("Poked 2"); 
				emoji.emojiExpressions.ResetExpressionDuration();
				emoji.emojiExpressions.SetExpression(EmojiExpressionState.POKED,1f);
				SoundManager.Instance.PlayVoice(VoiceList.Huh);
				animDelay = 4; 
				break;
			case 3: print("Annoyed 1"); 
				emoji.emojiExpressions.ResetExpressionDuration();
				emoji.emojiExpressions.SetExpression(EmojiExpressionState.ANNOYED,1f);
				SoundManager.Instance.PlayVoice(VoiceList.Urrh);
				animDelay = 5; 
				break;
			case 4: print("Annoyed 2"); 
				emoji.emojiExpressions.ResetExpressionDuration();
				emoji.emojiExpressions.SetExpression(EmojiExpressionState.ANNOYED,1f);
				SoundManager.Instance.PlayVoice(VoiceList.Urrh);
				animDelay = 5; 
				break;
			case 5: print("Pouting"); 
				emoji.emojiExpressions.ResetExpressionDuration();
				emoji.emojiExpressions.SetExpression(EmojiExpressionState.POUTING,-1f);
				SoundManager.Instance.PlayVoice(VoiceList.UghYuck);
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

	void Stroke()
	{
		if(emoji.emojiExpressions.currentExpression != EmojiExpressionState.CARESSED)
			emoji.emojiExpressions.SetExpression(EmojiExpressionState.CARESSED,-1);
	}

	void HoldMove()
	{
		Vector3 touchPos = getTouchToWorldPosition();
		Vector3 emojiHoldPos = new Vector3(touchPos.x,touchPos.y+0.5f,touchPos.z);
		transform.position = emojiHoldPos;

		float factor = Mathf.Sqrt( 
			Mathf.Pow(touchPos.x - prevMoveVector.x,2) + 
			Mathf.Pow(touchPos.y - prevMoveVector.y,2) 
		);
		float tempX = touchPos.x - prevXPos;

		CheckMove(factor, tempX);

		prevMoveVector = new Vector2(touchPos.x,touchPos.y);
		prevXPos = touchPos.x;
	}

	void CheckMove (float factor, float x)
	{
		if (factor < slowMediumTreshold) {
			if (!isRetaining) {
				if (prevHoldFactor > slowMediumTreshold) {
					StartCoroutine (_RetainMoveSpeed,retainCooldown);
				} else {
//					print ("Slow");
					if(!shakeExpressionRetain){
						emoji.emojiExpressions.ResetExpressionDuration ();
						emoji.emojiExpressions.SetExpression (EmojiExpressionState.HOLD, -1);
					}


				}
			}
		} else if (factor >= slowMediumTreshold && factor < mediumFastTreshold) {
			
			if (!isRetaining) {
				if (prevHoldFactor > mediumFastTreshold) {
					StartCoroutine (_RetainMoveSpeed,retainCooldown);
				} else {
//					print ("Med");
					if(!shakeExpressionRetain){
						emoji.emojiExpressions.ResetExpressionDuration ();
						emoji.emojiExpressions.SetExpression (EmojiExpressionState.WORRIED, -1);
					}

				}
			} else {
				if (prevHoldFactor < slowMediumTreshold) {
					ResetRetainMoveState ();
				}
			}
		} else {
			if (isRetaining) {
				if (prevHoldFactor < mediumFastTreshold) {
					ResetRetainMoveState ();
				}
			} else {
				//fast move
//				print ("Fast");
				if(!shakeExpressionRetain){
					emoji.emojiExpressions.ResetExpressionDuration ();
					emoji.emojiExpressions.SetExpression (EmojiExpressionState.AFRAID, -1);
				}


				CheckShake(x);
			}

		}
		prevHoldFactor = factor;
	}

	void ResetRetainMoveState()
	{
		StopCoroutine(_RetainMoveSpeed);
		isRetaining = false;
	}


	void CheckShake (float x)
	{
		if ((prevXPos >= 0 && x < 0) ||
		    prevXPos < 0 && x >= 0) {
			shakeCounter++;
			StopCoroutine (_RetainShakeCounter);
			StartCoroutine (_RetainShakeCounter);
		
			if (shakeCounter >= 8 && shakeCounter < 20) {
				if (!shakeExpressionRetain) {
					print ("DIZZY!");
					if(!dizzySound){
						dizzySound = true;
						SoundManager.Instance.PlayVoice(VoiceList.UghYuck);
					}
					emoji.emojiExpressions.ResetExpressionDuration ();
					emoji.emojiExpressions.SetExpression (EmojiExpressionState.DIZZY, -1);
					StartCoroutine (_RetainShakeExpression,shakeExpressionCooldown);
				}
			} else if (shakeCounter >= 20) {
				if (shakeExpressionRetain && emoji.emojiExpressions.currentExpression == EmojiExpressionState.DIZZY ||
					!shakeExpressionRetain && emoji.emojiExpressions.currentExpression != EmojiExpressionState.HOLD_BARF) {
					print ("MUNTAH!");
					if(!barfSound){
						barfSound = true;
						SoundManager.Instance.PlayVoice(VoiceList.Yuck);
					}
					emoji.emojiExpressions.ResetExpressionDuration ();
					emoji.emojiExpressions.SetExpression (EmojiExpressionState.HOLD_BARF, -1);
					StartCoroutine (_RetainShakeExpression,shakeExpressionCooldown);
				}
				float emojiHealth = emoji.health.StatValue/emoji.health.MaxStatValue;
				if(emojiHealth >= 0.3f){
					emoji.health.statsModifier = -3f;

				}else{
					emoji.ResetEmojiStatsModifier();
				}
			}
		}
	}



	void EndStroke()
	{
		touchInputObject.transform.localScale = Vector3.one;
		emoji.emojiExpressions.ResetExpressionDuration();
		emoji.emojiExpressions.SetExpression(EmojiExpressionState.DEFAULT,0);
	}

	public void Wake()
	{
		print("wake");
		float staminaValue = emoji.stamina.StatValue / emoji.stamina.MaxStatValue;
		if(staminaValue < 0.4f){//awake lazily
			emoji.emojiExpressions.SetExpression(EmojiExpressionState.AWAKE_LAZILY,-1);

		}else if(staminaValue >= 0.4f && staminaValue < 0.8f){ //awake normally
			emoji.emojiExpressions.SetExpression(EmojiExpressionState.AWAKE_NORMALLY,-1);
		}else{//awake energetically
			emoji.emojiExpressions.SetExpression(EmojiExpressionState.AWAKE_ENERGETICALLY,-1);
		}
		emoji.EmojiSleeping = false;
		emoji.ResetEmojiStatsModifier();
		interactable = false;
		if(OnEmojiWake != null) OnEmojiWake();
	}
	//----------------------------------------------------------------=====NON-VOID MODULES=====----------------------------------------------------------------
	Vector3 getEmojiPositionOnHold(Vector3 touchWorldPosition)
	{
		return new Vector3(touchWorldPosition.x,touchWorldPosition.y+0.5f,touchWorldPosition.z);
	}

	Vector3 getTouchToWorldPosition()
	{
		//Emoji z position should be -2. Camera position is 0,0,-10f thus -10 + 8 = -2. 
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,18f);
		return Camera.main.ScreenToWorldPoint(tempMousePosition);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void Eat()
	{
		emoji.emojiExpressions.SetExpression(EmojiExpressionState.EATING,eatDuration);
		StartCoroutine(_LockInteractions,eatDuration);
		emoji.body.OnEmojiEatOrReject(eatDuration);
	}

	public void Reject()
	{
		emoji.emojiExpressions.SetExpression(EmojiExpressionState.REJECT,rejectDuration);
		StartCoroutine(_LockInteractions,rejectDuration);
		emoji.body.OnEmojiEatOrReject(rejectDuration);
	}

	public void ApplyMedicineOrSyringe(float duration)
	{
		StartCoroutine(_LockInteractions,duration);
	}

	public void Fall()
	{
		StartCoroutine(_Falling);
	}

	public void Landing()
	{
		flagFalling = false;
		if(!emoji.EmojiSleeping) emoji.emojiExpressions.ResetExpressionDuration();
	}

	public void OnToyBumped()
	{
		if(!toyBumped){
			toyBumpCounter++;
			if(toyBumpCounter < 6){
				emoji.emojiExpressions.SetExpression(EmojiExpressionState.HAPPY,toyBumpResponseCooldown);
				emoji.happiness.ModStats(7f);
				SoundManager.Instance.PlayVoice(VoiceList.Laugh);
			}else if(toyBumpCounter >= 6 && toyBumpCounter < 9){
				emoji.emojiExpressions.SetExpression(EmojiExpressionState.BORED,toyBumpResponseCooldown);
				SoundManager.Instance.PlayVoice(VoiceList.Sigh);
			}else if(toyBumpCounter >= 9 && toyBumpCounter < 14){
				emoji.emojiExpressions.SetExpression(EmojiExpressionState.ANNOYED,toyBumpResponseCooldown);
				SoundManager.Instance.PlayVoice(VoiceList.Urrh);
				emoji.happiness.ModStats(-3f);
			}else{
				emoji.emojiExpressions.SetExpression(EmojiExpressionState.ANGERED,toyBumpResponseCooldown);
				SoundManager.Instance.PlayVoice(VoiceList.UghYuck);
				emoji.happiness.ModStats(-7f);
			}
			StartCoroutine(_RetainToyBumpingResponse);

			StopCoroutine(_RetainToyBumpCounter);
			StartCoroutine(_RetainToyBumpCounter);
		}
	}

	/// <summary>
	/// Factor = +1 / +2 / +3 / +4 depends on target
	/// </summary>
	public void OnDartboardMingameDone(float factor)
	{
		if(!dartboardRetaining){
			emoji.emojiExpressions.SetExpression (EmojiExpressionState.HAPPY, 2f);
			emoji.happiness.ModStats(factor);
			StartCoroutine(_RetainDartboardMinigameCooldown);
		}
	}

	public void OnBlocksMinigameDone()
	{
		if(!blocksRetaining){
			//PlayerData.Instance.PlayerEmoji.gameObject.SetActive (true);
			emoji.emojiExpressions.SetExpression (EmojiExpressionState.HAPPY, 2f);
			emoji.happiness.ModStats(happinessModOnBlocks);
			StartCoroutine(_RetainBlocksMinigameCooldown);
		}
	}

	public void OnDoodleMinigameDone()
	{
		if(!doodleRetaining){
			emoji.emojiExpressions.SetExpression (EmojiExpressionState.HAPPY, 2f);
			emoji.happiness.ModStats(happinessModOnDoodle);
			StartCoroutine(_RetainDoodleMinigameCooldown);
		}
	}

	public void OnDanceMatMinigameDone()
	{
		if(!danceMatRetaining){
			Debug.Log ("ondancematdone");
			emoji.emojiExpressions.SetExpression (EmojiExpressionState.HAPPY, 2f);
			emoji.happiness.ModStats(happinessModOnDanceMat);
			StartCoroutine(_RetainDanceMatMinigameCooldown);
		}	
	}

	public void Sleep ()
	{
		if (!flagSleeping) {
			flagSleeping = true;
			if (emoji.stamina != null) {
				startSleepStamina = emoji.stamina.StatValue;
				emoji.stamina.statsModifier = 0.004f;
			}
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
		emoji.ResetEmojiStatsModifier();
	}

	const string _RetainMoveSpeed = "RetainMoveSpeed";
	IEnumerator RetainMoveSpeed(float cooldown)
	{
		isRetaining = true;
		yield return new WaitForSeconds(cooldown);
		print ("RESET RETAIN HOLD MOVE");
		isRetaining = false;
	}

	const string _RetainShakeCounter = "RetainShakeCounter";
	IEnumerator RetainShakeCounter()
	{
		yield return new WaitForSeconds(shakeCounterCooldown);
		shakeCounter = 0;

		emoji.ResetEmojiStatsModifier();
	}

	const string _RetainShakeExpression = "RetainShakeExpression";
	IEnumerator RetainShakeExpression(float cooldown)
	{
		shakeExpressionRetain = true;
		yield return new WaitForSeconds (cooldown);
		shakeExpressionRetain = false;
		barfSound = false;
		dizzySound = false;
	}

	const string _StartHold = "StartHold";
	IEnumerator StartHold()
	{
		flagStrokeToHold = true;
		emoji.triggerFall.thisCollider.enabled = true;
		emoji.triggerFall.ClearColliderList();
		emoji.body.thisCollider.enabled = false;
		emoji.thisRigidbody.simulated = false;

		emoji.emojiExpressions.ResetExpressionDuration();
		emoji.emojiExpressions.SetExpression(EmojiExpressionState.HOLD,-1);

		Vector3 currentPos = transform.position;
//		print("START HOLD WOI AAAAAAAA");
		float t = 0;
		while(t < 1f){
			
			transform.position = Vector3.Lerp(currentPos,touchTargetPosition,t);
			t+= Time.deltaTime*5;
			yield return null;
		}
//		print("START HOLD WOI BBBBBBBB");
		transform.position = touchTargetPosition;
		prevMoveVector = new Vector2(touchTargetPosition.x,touchTargetPosition.y);
		flagHold = true;
		flagStrokeToHold = false;
	}

	const string _Falling = "Falling";
	IEnumerator Falling()
	{
		flagFalling = true;

		emoji.thisRigidbody.velocity = Vector2.zero;
		emoji.thisRigidbody.simulated = true;
		emoji.triggerFall.isFalling = true;

		emoji.emojiExpressions.SetExpression(EmojiExpressionState.FALL,-1);
		yield return null;
		emoji.triggerFall.IgnoreCollision();

		emoji.body.thisCollider.enabled = true;
		emoji.triggerFall.isFalling = false;
		emoji.body.flagAfterChangingRoom = false;
	}

	const string _RetainToyBumpingResponse = "RetainToyBumpingResponse";
	IEnumerator RetainToyBumpingResponse()
	{
		toyBumped = true;
		yield return new WaitForSeconds(toyBumpResponseCooldown);
		toyBumped = false;
	}

	const string _RetainToyBumpCounter = "RetainToyBumpCounter";
	IEnumerator RetainToyBumpCounter()
	{
		yield return new WaitForSeconds(toyBumpRetainCooldown);
		toyBumpCounter = 0;
	}

	const string _RetainDartboardMinigameCooldown = "RetainDartboardMinigameCooldown";
	IEnumerator RetainDartboardMinigameCooldown()
	{
		dartboardRetaining = true;
		yield return new WaitForSeconds(dartboardCooldown);
		dartboardRetaining = false;

	}
	const string _RetainBlocksMinigameCooldown = "RetainBlocksMinigameCooldown";
	IEnumerator RetainBlocksMinigameCooldown()
	{
		blocksRetaining = true;
		yield return new WaitForSeconds(blocksCooldown);
		blocksRetaining = false;

	}
	const string _RetainDoodleMinigameCooldown = "RetainDoodleMinigameCooldown";
	IEnumerator RetainDoodleMinigameCooldown()
	{
		doodleRetaining = true;
		yield return new WaitForSeconds(doodleCooldown);
		doodleRetaining = false;

	}
	const string _RetainDanceMatMinigameCooldown = "RetainDanceMatMinigameCooldown";
	IEnumerator RetainDanceMatMinigameCooldown()
	{
		danceMatRetaining = true;
		yield return new WaitForSeconds(danceMatCooldown);
		danceMatRetaining = false;
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
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}