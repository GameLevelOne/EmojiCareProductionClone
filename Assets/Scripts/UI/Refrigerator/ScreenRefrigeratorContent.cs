using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenRefrigeratorContent : BaseUI {
	public RefrigeratorIngredient[] ingredients;
	public Refrigerator refrigerator;
	public Image arrow;
	public UIBowl bowl;

	void OnEnable(){
		foreach(RefrigeratorIngredient ingredient in ingredients)
		{
			ingredient.UpdateQuantity();
		}
	}

	public void CloseRefrigerator(){
		refrigerator.CloseRefrigerator ();
		bowl.ReturnObject();
		base.CloseUI (this.gameObject);
	}


}
