using System.Collections;
using UnityEngine;

public enum IngredientType{
	Meat,
	Chicken,
	Fish,
	Cabbage,
	Tomato,
	Carrot,
	Cheese,
	Mushroom,
	Flour,
	Egg,
	COUNT
}

public class Ingredient : MonoBehaviour {
	[Header("Reference")]
	public Rigidbody2D thisRigidbody;
	public Collider2D thisCollider;

	public SpriteRenderer thisSprite;
	public Animator thisAnim;

	[Header("Edit this!")]
	public IngredientType type;

	bool hold = false;
		
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.PAN){
			if(!hold && (other.GetComponent<Pan>().cookedFoodObject == null || other.GetComponent<Pan>().isCooking)){ 
				StopAllCoroutines();
				other.GetComponent<Pan>().AddIngredient(type);
				Destroy(this.gameObject);
			}
		}
	}

	public void BeginDrag()
	{
		thisRigidbody.simulated = false;
		thisCollider.enabled = false;
		thisAnim.SetBool(AnimatorParameters.Bools.HOLD,true);

		thisSprite.sortingLayerName = SortingLayers.HELD;
		hold = true;
	}

	public void Drag()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,9f);
		transform.localPosition = Camera.main.ScreenToWorldPoint(tempMousePosition);
	}

	public void EndDrag()
	{
		StartCoroutine(_Fall);
	}

	const string _Fall = "Fall";
	IEnumerator Fall()
	{
		hold = false;
		thisSprite.sortingLayerName = SortingLayers.MOVABLE_FURNITURE;

		thisRigidbody.velocity = Vector2.zero;
		thisRigidbody.simulated = true;
		thisCollider.enabled = true;

		yield return null;
	}
}
