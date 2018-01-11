using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : BaseFurniture {
	#region attributes
	[Header("Plate Attributes")]
	public Transform furnitureTransform;
	public Transform plateContentTransform;
	public Collider2D thisCollider;
	public Rigidbody2D thisRigidbody;

	public float startY = 0.2f;
	public float nextY = 0.3f;

	public bool flagHold = false;
//	public bool flagDoneCooking = false;
	[Header("")]
	public List<GameObject> FoodsOnPlate = new List<GameObject>();
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == Tags.EMOJI_BODY)
		{
			Physics2D.IgnoreCollision(thisCollider,other.collider);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(flagHold){
			if(other.tag == Tags.EMOJI_BODY){
				FoodsOnPlate[FoodsOnPlate.Count-1].GetComponent<Food>().ValidateEmojiHunger(other.transform.parent.GetComponent<Emoji>());

			}
		}

	}

	void AdjustFoodStacks()
	{
		for(int i = 0;i<FoodsOnPlate.Count;i++){
			FoodsOnPlate[i].transform.localPosition = new Vector3(0,(FoodsOnPlate.Count * nextY)+startY);
			Food currentFood = FoodsOnPlate[i].GetComponent<Food>();
			currentFood.thisSprite[currentFood.currentVariant].sortingOrder = i+1;
		}
	}

	//event trigger
	public void BeginDrag()
	{
		thisCollider.enabled = false;
		thisRigidbody.simulated = false;
		thisSprite[currentVariant].sortingLayerName = SortingLayers.HELD;
		flagHold = true;

		foreach(GameObject g in FoodsOnPlate){
			g.GetComponent<Food>().thisSprite[g.GetComponent<Food>().currentVariant].sortingLayerName = SortingLayers.HELD;
		}

	}

	public void Drag()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,19.5f);
		transform.localPosition = Camera.main.ScreenToWorldPoint(tempMousePosition);
	}

	public void EndDrag()
	{
		StartCoroutine(ReleasePlate());
		
	}

	void ReleaseFood(GameObject foodObject)
	{
		Food food = foodObject.GetComponent<Food>();
		food.OnFoodPicked -= ReleaseFood;
		food.onPlate = false;
		food.thisRigidbody.gravityScale = 0.8f;
		food.thisCollider.isTrigger = false;

		foodObject.transform.SetParent(furnitureTransform);

		if(FoodsOnPlate.Count == 1){
			FoodsOnPlate.Clear ();
		} else{
			FoodsOnPlate.Remove(foodObject);
			AdjustFoodStacks();
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void AddFood(GameObject foodObject)
	{
		Food food = foodObject.GetComponent<Food>();
		food.onPlate = true;
		food.thisRigidbody.gravityScale = 0f;
		food.thisCollider.isTrigger = true;
		food.OnFoodPicked += ReleaseFood;

		foodObject.transform.SetParent(plateContentTransform);
		foodObject.transform.localPosition = new Vector3(0,(FoodsOnPlate.Count * nextY)+startY);

		FoodsOnPlate.Add(foodObject);

		food.thisSprite[food.currentVariant].sortingOrder = FoodsOnPlate.Count;
	}

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	#region coroutine
	IEnumerator ReleasePlate()
	{
		thisCollider.enabled = true;
		thisRigidbody.velocity = Vector2.zero;
		thisRigidbody.simulated = true;
		thisSprite[currentVariant].sortingLayerName = SortingLayers.MOVABLE_FURNITURE;

		foreach(GameObject g in FoodsOnPlate){
			g.GetComponent<Food>().thisSprite[g.GetComponent<Food>().currentVariant].sortingLayerName = SortingLayers.MOVABLE_FURNITURE;
		}

		yield return null;
		flagHold = false;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	void LateUpdate ()
	{
		if (FoodsOnPlate.Count > 0) {
			for (int i = 0; i < FoodsOnPlate.Count; i++) {

				if (FoodsOnPlate [i] != null && FoodsOnPlate [i].GetComponent<Food> ().onPlate) {
					FoodsOnPlate [i].transform.localPosition = new Vector3 (0, (i * nextY) + startY);
				} else if (FoodsOnPlate [i] == null) {
					FoodsOnPlate.RemoveAt (i);
				}
					
			}
		}
	}
}
