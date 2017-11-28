using System.Collections;
using UnityEngine;

public class Syringe : MonoBehaviour {
	
	#region attributes
	[Header("Attributes")]
	public Collider2D thisCollider;
	public SpriteRenderer thisSprite;
	public Animator thisAnim;
	public EmojiExpressionState emojiResponse;
	public UICoin uiCoin;
	[Header("Custom Attributes")]
	public float emojiHealthSet = 0f;
	public float reactionDuration;
	public float returnSpeed = 5f;
	public int price;
	public Vector2 emojiOffset;

	Vector3 fixedPosition;
	protected Emoji emoji;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Awake()
	{
		fixedPosition = transform.localPosition;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	protected virtual void OnTriggerEnter2D (Collider2D other)
	{
		print(other.name);
		if(other.tag == Tags.EMOJI_BODY){
			if(!other.GetComponent<EmojiBody>().emoji.playerInput.flagSleeping){
				emoji = other.transform.parent.GetComponent<Emoji>();
				CheckPlayerCoin();
			}
		}
	}

	protected virtual void CheckPlayerCoin()
	{
		//check price, if enough coin
		print("Price = "+price+", you have "+PlayerData.Instance.PlayerCoin);
		if(PlayerData.Instance.PlayerCoin >= price){
			//check stats, if low
			float healthValue = emoji.health.StatValue/emoji.health.MaxStatValue;
			if(healthValue < Emoji.statsTresholdLow){
				//apply
				StopAllCoroutines();
				HideUICoin(true);
				emoji.playerInput.ApplyMedicineOrSyringe(reactionDuration+2f);
				StartCoroutine(_Apply);

				return;
			}else{
				//reject
				emoji.playerInput.Reject();
			}
		}

		HideUICoin(false);
		
	}

	public void BeginDrag()
	{
		thisCollider.enabled = false;
		thisSprite.sortingLayerName = SortingLayers.HELD;
		ShowUICoin();
	}

	public void Drag()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,19f);
		transform.localPosition = Camera.main.ScreenToWorldPoint(tempMousePosition);
	}

	public void EndDrag()
	{
		StartCoroutine(_Return);
	}

	protected void ShowUICoin()
	{
		uiCoin.gameObject.SetActive(true);
		uiCoin.ShowUI(price);
	}
	protected void HideUICoin(bool bought)
	{
		uiCoin.CloseUI(bought);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void AnimEvent_OnStabEmoji()
	{
		emoji.health.SetStats(emojiHealthSet);
		emoji.emojiExpressions.SetExpression(emojiResponse,reactionDuration);
	}

	public void ResetPosition()
	{
		thisCollider.enabled = true;
		transform.localPosition = fixedPosition;
		thisSprite.sortingLayerName = SortingLayers.MOVABLE_FURNITURE;
		emoji = null;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	#region coroutine
	protected const string _Return = "Return";
	IEnumerator Return()
	{
		thisCollider.enabled = true;
		yield return null;
		HideUICoin(false);
		Vector3 temp = transform.localPosition;

		float t = 0;
		while(t < 1){
			transform.localPosition = Vector3.Lerp(temp,fixedPosition,t);
			t += Time.deltaTime*returnSpeed;
			yield return null;
		}
		transform.localPosition = fixedPosition;
		thisSprite.sortingLayerName = SortingLayers.MOVABLE_FURNITURE;
	}
		
	protected const string _Apply = "Apply";
	IEnumerator Apply()
	{
		thisCollider.enabled = false;
		Vector3 startPos = transform.position;

		Vector3 emojiPos = PlayerData.Instance.PlayerEmoji.transform.position;
		Vector3 targetPos = new Vector3(emojiPos.x+emojiOffset.x,emojiPos.y+emojiOffset.y,-1f);

		//move syringe to target position, and animate stab
		float t = 0;
		while(t < 1){
			transform.position = Vector3.Lerp(startPos,targetPos,t);
			t += Time.deltaTime*(returnSpeed / 1.2f);
			yield return null;
		}
		//animate stab
		thisAnim.SetTrigger(AnimatorParameters.Triggers.ANIMATE);
	}
	#endregion
}