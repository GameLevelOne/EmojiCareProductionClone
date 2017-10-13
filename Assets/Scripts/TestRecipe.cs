using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ingredients{
	Cabbage,
	Tomato,
	Carrot,
	Meat,
	Chicken,
	Fish,
	Cheese,
	Mushroom,
	Flour,
	Egg
}

public class TestRecipe : MonoBehaviour {

	public List<Ingredients> ingredients = new List<Ingredients>();
	public int correctIngredientCounter=0;

	public void InitRecipe(List<Ingredients> ingr){
		foreach(Ingredients item in ingr){
			ingredients.Add(item);
		}
	}
}
