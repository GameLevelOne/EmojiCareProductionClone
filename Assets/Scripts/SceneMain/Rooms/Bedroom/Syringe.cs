using System.Collections;
using UnityEngine;

public class Syringe : MonoBehaviour {
	#region attributes
	public Rigidbody2D thisRigidbody;
	public Collider2D thisCollider;
	public SpriteRenderer thisSprite;
	public EmojiExpressionState emojiResponse;
	public float emojiHealthSet = 0f;
	public float returnSpeed = 5f;

	Vector3 fixedPosition;
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
		if(other.tag == Tags.EMOJI_BODY){
			Emoji emoji = other.transform.parent.GetComponent<Emoji>();
			float healthValue = emoji.health.StatValue/emoji.health.MaxStatValue;
			if(healthValue < Emoji.statsTresholdLow){
				emoji.health.SetStats(emojiHealthSet);
				emoji.emojiExpressions.SetExpression(emojiResponse,2f);
			}else{
				emoji.playerInput.Reject();
			}
			StopCoroutine(_Return);
			ResetPosition();
		}
	}

	public void BeginDrag()
	{
		thisRigidbody.simulated = false;
		thisCollider.enabled = false;
		thisSprite.sortingLayerName = SortingLayers.HELD;
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
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void ResetPosition()
	{
		thisRigidbody.simulated = true;
		thisCollider.enabled = true;
		transform.localPosition = fixedPosition;
		thisSprite.sortingLayerName = SortingLayers.MOVABLE_FURNITURE;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	#region coroutine
	protected const string _Return = "Return";
	IEnumerator Return()
	{
		thisRigidbody.simulated = true;
		thisCollider.enabled = true;
		thisSprite.sortingLayerName = SortingLayers.MOVABLE_FURNITURE;
		yield return null;
		Vector3 temp = transform.localPosition;

		float t = 0;
		while(t < 1){
			transform.localPosition = Vector3.Lerp(temp,fixedPosition,t);
			t += Time.deltaTime*returnSpeed;
			yield return null;
		}
		transform.localPosition = fixedPosition;
	}
	#endregion
}