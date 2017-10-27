using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour {

	List<IngredientType> ingredientsInBowl = new List<IngredientType>();

	bool enteredBowl=false;
	int limitIngredients = 4;

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == Tags.UI.INGREDIENTS) {
			Debug.Log ("add obj");
			ingredientsInBowl.Add (col.GetComponent<RefrigeratorIngredient> ().ingredientType);
		}
		foreach (IngredientType type in ingredientsInBowl){
			Debug.Log("ingredients:"+type.ToString());
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if(col.tag == Tags.UI.INGREDIENTS){
			Debug.Log("remove obj");
			ingredientsInBowl.Remove(col.GetComponent<RefrigeratorIngredient>().ingredientType);
		}
	}
}
