using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEditRooms : BaseUI {
	public ScreenPopup screenPopup;
	public ScreenTutorial screenTutorial;
	public RoomController roomController;
	public GameObject boxVariants;
	public GameObject boxPrice;
	public Image iconVariant;
	public Text textPrice;
	public Text textPlayerCoin;

	BaseFurniture currentItem;
	int currentVariant = 0;

	public override void InitUI ()
	{
		Debug.Log("edit room");	
		int currentRoom = (int)roomController.currentRoom;
		textPlayerCoin.text = PlayerData.Instance.PlayerCoin.ToString();
		for(int i=0;i<roomController.rooms[currentRoom].furnitures.Length;i++){
			roomController.rooms [currentRoom].furnitures [i].EnterEditmode ();
		}

		if(PlayerData.Instance.TutorialFirstEditRoom == 0){
			screenTutorial.ShowFirstDialog (TutorialType.FirstEditRoomUI);
		}
	}

	//register event on click furniture
	void OnEnable(){
		EditFurnitureButton.OnClickToEdit += OnClickFurnitureItem;
		ScreenPopup.OnBuyFurniture += OnBuyFurniture;
	}

	void OnDisable(){
		EditFurnitureButton.OnClickToEdit -= OnClickFurnitureItem;
		ScreenPopup.OnBuyFurniture -= OnBuyFurniture;
	}

	void OnClickFurnitureItem(BaseFurniture currentItem){
		//laod furniture variant data
		//display in UI
		this.currentItem = currentItem;
		this.currentVariant = currentItem.currentVariant;
		DisplayItem (currentVariant);
	}

	void OnBuyFurniture ()
	{
		currentItem.variant [currentVariant].bought = true;
		PlayerData.Instance.PlayerCoin -= currentItem.variant [currentVariant].price;
		ApplyVariant ();
	}

	public void OnClickNextItem(){
		int maxVariant = currentItem.variant.Length;

		currentVariant++;
		if(currentVariant == maxVariant){
			currentVariant = 0;
		} 
		DisplayItem (currentVariant);
	}

	public void OnClickPrevItem(){
		int maxVariant = currentItem.variant.Length;

		currentVariant--;
		if(currentVariant<0){
			currentVariant = maxVariant - 1;
		}
		DisplayItem (currentVariant);
	}

	void DisplayItem(int index){
		FurnitureVariant item = currentItem.variant [currentVariant];
		boxVariants.SetActive (true);
		currentItem.transform.GetChild (1).GetComponent<SpriteRenderer> ().sprite = item.sprite[0];
		if(currentItem.variant[index].bought){
			boxPrice.SetActive (false);
		} else {
			boxPrice.SetActive (true);
			textPrice.text = currentItem.variant [index].price.ToString();
		}
		iconVariant.sprite = currentItem.variant [index].sprite[0];
	}

	public void ApplyVariant(){
		FurnitureVariant item = currentItem.variant [currentVariant];
		bool isBought = item.bought;
		if(isBought){
			currentItem.transform.GetChild (1).GetComponent<SpriteRenderer> ().sprite = item.sprite[0];
		} else{
			ConfirmBuyObject (item.price);
		}
	}

	public void DisableEditMode(){
		int currentRoom = (int)roomController.currentRoom;
		for(int i=0;i<roomController.rooms[currentRoom].furnitures.Length;i++){
			roomController.rooms [currentRoom].furnitures [i].ExitEditmode ();
		}
	}

	void ConfirmBuyObject(int price){
		int currentCoin = PlayerData.Instance.PlayerCoin;
//		int currentCoin = 500;
		 if(currentCoin>=price){
			screenPopup.ShowPopup (PopupType.Confirmation, PopupEventType.AbleToBuyFurniture, false, false);
		 } else {
			screenPopup.ShowPopup (PopupType.Confirmation, PopupEventType.NotAbleToBuyFurniture, true, false);
		 }
	}

}
