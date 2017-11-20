using System.Collections;
using UnityEngine;

public class Food : TriggerableFurniture {
	public delegate void FoodPicked(GameObject obj);
	public event FoodPicked OnFoodPicked;
	#region attributes
	[Header("Food Attribute")]
	public float[] foodFactor;
	public bool hold = false;
	public bool onPlate = false;
	public Collider2D thisCollider;
	public Rigidbody2D thisRigidbody;


	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	protected override void OnTriggerEnter2D(Collider2D other)
	{
		if(!onPlate){
			if(other.tag == Tags.EMOJI_BODY){
				Emoji emoji = other.transform.parent.GetComponent<Emoji>();
				ValidateEmojiHunger(emoji);
			}else if(other.tag == Tags.PLATE){
				other.transform.parent.GetComponent<Plate>().AddFood(gameObject);
			}
		}
	}

	public void OnCollisionEnter2D(Collision2D other)
	{
		if(!onPlate){
			if(other.gameObject.tag == Tags.MOVABLE_FURNITURE){
				Physics2D.IgnoreCollision(thisCollider,other.collider);
			}
		}
	}
		
	public void BeginDrag()
	{
		if(OnFoodPicked != null) OnFoodPicked(gameObject);

		thisSprite[currentVariant].sortingLayerName = SortingLayers.HELD;

		thisCollider.enabled = false;
		thisRigidbody.simulated = false;
		hold = true;

	}

	public void Drag()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,9f);
		transform.localPosition = Camera.main.ScreenToWorldPoint(tempMousePosition);
	}

	public void EndDrag()
	{
		StartCoroutine(_Release);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void ValidateEmojiHunger(Emoji emoji)
	{
		float hungerValue = emoji.hunger.StatValue / emoji.hunger.MaxStatValue;

		if(hungerValue >= 0.95f){ //reject
			emoji.playerInput.Reject();
		}else{ //eat
			emoji.ModAllStats(foodFactor);
			emoji.playerInput.Eat();
			if(OnFoodPicked != null) OnFoodPicked(gameObject);
			Destroy(this.gameObject);
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	#region coroutines
	const string _Release = "Release";
	IEnumerator Release()
	{
		thisSprite[currentVariant].sortingLayerName = SortingLayers.MOVABLE_FURNITURE;
		hold = false;
		thisCollider.isTrigger = true;
		thisCollider.enabled = true;
		thisRigidbody.velocity = Vector2.zero;
		thisRigidbody.simulated = true;
		yield return null;
		yield return null;
		thisCollider.isTrigger = false;
	}
	#endregion
}
