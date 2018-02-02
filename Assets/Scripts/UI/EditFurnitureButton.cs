using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditFurnitureButton : MonoBehaviour {

	public delegate void ClickToEdit(BaseFurniture currentItem);
	public static event ClickToEdit OnClickToEdit;

	void OnMouseDown(){
//		Debug.Log ("edit item");
		OnClickToEdit (gameObject.GetComponentInParent<BaseFurniture>());
	}
}
