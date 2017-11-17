using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIngredientsInPan : BaseUI {
	public Pan pan;
	public Transform ingredientBox;
	public Transform buttonClose;
	public GameObject ingredientIconObj;
	public Sprite[] ingredients;
	public List<GameObject> ingredientListObject;

	public void ShowIngredient(){
		base.ShowUI (this.gameObject);

		if(pan.ingredients.Count > 0){
			buttonClose.gameObject.SetActive (true);
			buttonClose.localPosition = new Vector3 (80 * pan.ingredients.Count, -320);
		} else{
			buttonClose.gameObject.SetActive (false);
		}

		for(int i=0;i<pan.ingredients.Count;i++){
			GameObject obj = Instantiate (ingredientIconObj, ingredientBox, false) as GameObject;
			int idx = (int)pan.ingredients [i].GetComponent<Ingredient> ().type;
			obj.transform.GetChild (0).GetComponent<Image> ().sprite = ingredients [idx];
			ingredientListObject.Add(obj);
		}
	}

	public void ClearObjectsOnClose()
	{
		foreach(GameObject g in ingredientListObject) Destroy(g);
	}

	public void ClearIngredient()
	{
		foreach(GameObject g in ingredientListObject) Destroy(g);
		ingredientListObject.Clear();
		pan.ClearIngredient();
	}
}
