using System.Collections;
using UnityEngine;

public enum IngredientType{
	Cabbage,
	Carrot,
	Cheese,
	Chicken,
	Egg,
	Fish,
	Flour,
	Meat,
	Mushroom,
	Tomato
}

public class Ingredient : TriggerableFurniture {
	public IngredientType type;

	bool hold = false;
	Vector3 startPos;

	void Start()
	{
		startPos = transform.localPosition;
	}
		
	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == Tags.PAN){
			if(!hold && (other.GetComponent<Pan>().cookedFoodObject == null || other.GetComponent<Pan>().isCooking)){ 
				StopAllCoroutines();
				other.GetComponent<Pan>().AddIngredient(type);
				this.gameObject.SetActive(false);
				transform.localPosition = startPos;
			}
		}
	}

	public void BeginDrag()
	{
		thisSprite[currentVariant].sortingLayerName = SortingLayers.HELD;
		hold = true;
	}

	public void Drag()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,9f);
		transform.localPosition = Camera.main.ScreenToWorldPoint(tempMousePosition);
	}

	public void EndDrag()
	{
		thisSprite[currentVariant].sortingLayerName = SortingLayers.MOVABLE_FURNITURE;
		hold = false;
		StartCoroutine(CheckPan());
	}

	IEnumerator CheckPan()
	{
		yield return null;
		Vector3 temp = transform.localPosition;
		float t = 0f;
		while(t<= 1f){
			transform.localPosition = Vector3.Lerp(temp,startPos,t);
			t+= Time.deltaTime * 4;
			yield return new WaitForSeconds(Time.deltaTime);
		}

	}

}
