using System.Collections;
using UnityEngine;

public enum IngredientType{
	Meat, //G
	Chicken, //G
	Fish, //G
	Cabbage,
	Tomato,
	Carrot,
	Cheese, //G
	Mushroom,
	Flour, //G
	Egg, //G
	COUNT
}

public class Ingredient : MonoBehaviour {
	public delegate void IngredientPickEvent(PanState state);
	public event IngredientPickEvent OnIngredientPicked;

	[Header("Reference")]
	public Rigidbody2D thisRigidbody;
	public Collider2D thisCollider;

	public SpriteRenderer thisSprite;
	public Animator thisAnim;

	[Header("Edit this!")]
	public IngredientType type;

	public bool instantiated = true;
	bool hold = false;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.PAN){
			if(!instantiated && !hold){
				if(!other.GetComponent<Pan>().isCooking){
					StopAllCoroutines();
					other.GetComponent<Pan>().AddIngredient(this.gameObject);
					this.gameObject.SetActive(false); //sementara
				}
			}
		}

		if(other.tag == Tags.REFRIGERATOR){
			if(!instantiated && !hold){
				PlayerData.Instance.inventory.ModIngredientValue(type,1);
				Destroy(this.gameObject);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == Tags.EMOJI_BODY || other.gameObject.tag == Tags.MOVABLE_FURNITURE){
			Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),thisCollider);
		}

		if(instantiated) instantiated = false;

	}

	public void BeginDrag()
	{
		thisRigidbody.simulated = false;
		thisCollider.enabled = false;
		thisAnim.SetBool(AnimatorParameters.Bools.HOLD,true);

		thisSprite.sortingLayerName = SortingLayers.HELD;
		hold = true;

		if(OnIngredientPicked != null) OnIngredientPicked(PanState.Open);
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
		thisAnim.SetBool(AnimatorParameters.Bools.HOLD,false);
		thisSprite.sortingLayerName = SortingLayers.MOVABLE_FURNITURE;

		thisRigidbody.velocity = Vector2.zero;
		thisRigidbody.simulated = true;
		thisCollider.enabled = true;
		if(OnIngredientPicked != null) OnIngredientPicked(PanState.Close);
		yield return null;
	}
}