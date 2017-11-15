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

	// Use this for initialization
	void Start () {
		OnClickPan ();
	}
	
	void OnClickPan(){
		base.ShowUI (this.gameObject);

		List<IngredientType> temp = new List<IngredientType> ();

		temp.Add (IngredientType.Cabbage);
		temp.Add (IngredientType.Carrot);
		temp.Add (IngredientType.Cheese);

		if(temp.Count > 0){
			buttonClose.gameObject.SetActive (true);
			buttonClose.localPosition = new Vector3 (80 + 35* (temp.Count-1), -320);
		} else{
			buttonClose.gameObject.SetActive (false);
		}

		for(int i=0;i<temp.Count;i++){
			GameObject obj = Instantiate (ingredientIconObj, ingredientBox, false) as GameObject;
			int idx = (int)temp [i];
			obj.transform.GetChild (0).GetComponent<Image> ().sprite = ingredients [idx];
		}

//		if(pan.ingredients.Count > 0){
//			buttonClose.gameObject.SetActive (true);
//			buttonClose.localPosition = new Vector3 (80 * pan.ingredients.Count, -320);
//		} else{
//			buttonClose.gameObject.SetActive (false);
//		}

//		for(int i=0;i<pan.ingredients.Count;i++){
//			Transform obj = Instantiate (ingredientIconObj, ingredientBox, false) as Transform;
//			int idx = (int)pan.ingredients [i].GetComponent<IngredientObject> ().type;
//			obj.GetChild (0).GetComponent<Image> ().sprite = ingredients [idx];
//		}
	}
}
