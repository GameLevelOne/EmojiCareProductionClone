using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Foods{
	public GameObject foodObject;
	public List<IngredientType> ingredients;
	public int cookDuration;
}

public class Cookbook : ActionableFurniture {
	public GameObject cookBook;

	public Foods[] recipes;

	public override void PointerClick()
	{
		//showCookBookUI
		cookBook.SetActive(true);
	}
}
