using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEditRooms : BaseUI {
	public ScreenPopup screenPopup;
	public GameObject boxVariants;
	public GameObject boxPrice;
	public Image iconVariant;
	public Image textPrice;

	public override void InitUI ()
	{
		Debug.Log("edit room");	
	}

	//register event on click furniture
	void OnEnable(){
		EditFurnitureButton.OnClickToEdit += OnClickFurnitureItem;
	}

	void OnDisable(){
		EditFurnitureButton.OnClickToEdit -= OnClickFurnitureItem;
	}

	void OnClickFurnitureItem(BaseFurniture currentItem){
		//laod furniture variant data
		//display in UI

	}

	public void ChangeVariant(){

	}

	public void OpenChoice(){

	}

	public void CloseChoice(){
		
	}

	public void ConfirmBuyObject(){

	}

	public void CancelBuyObject(){
		
	}

	public void BuyObject(){
		
	}
}
