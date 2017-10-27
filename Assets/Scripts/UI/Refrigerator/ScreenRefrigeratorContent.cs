using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenRefrigeratorContent : BaseUI {
	public RefrigeratorIngredient[] ingredients;

	void OnEnable(){
		foreach(RefrigeratorIngredient ingredient in ingredients)
		{
			ingredient.UpdateQuantity();
		}
	}
}
