using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefrigeratorIngredient : MonoBehaviour {
	[Header("Reference")]
	public RectTransform parent;
	public GameObject objectQuantity;
	public Text textQuantity;

	public GameObject prefabObject;
	[Header("Modify this!")]
	public IngredientType type;

	IngredientObject ingredientObject;

	public void UpdateQuantity()
	{
		int qty = PlayerData.Instance.inventory.GetIngredientValue(type);
		if(qty <= 0){
			this.gameObject.SetActive(false);
		}else{
			this.gameObject.SetActive(true);
			if(qty == 1){
				objectQuantity.SetActive(false);
			}else{
				objectQuantity.SetActive(true);
				textQuantity.text = qty > 99 ? qty.ToString()+"+" : qty.ToString();
			}
		} 
	}

	public void BeginDrag()
	{
		GameObject tempPrefab = Instantiate(prefabObject,parent,false) as GameObject;
		ingredientObject = tempPrefab.GetComponent<IngredientObject>();
		ingredientObject.initialPos = GetComponent<RectTransform>().anchoredPosition;
		ingredientObject.type = type;
		ingredientObject.OnIngredientCancel += CancelIngredient;

		PlayerData.Instance.inventory.ModIngredientValue(type,-1);
		UpdateQuantity();
	}

	public void CancelIngredient(IngredientObject obj)
	{
		obj.OnIngredientCancel -= CancelIngredient;
		Destroy(obj.gameObject);
		PlayerData.Instance.inventory.ModIngredientValue(type,1);
		UpdateQuantity();
	}
}