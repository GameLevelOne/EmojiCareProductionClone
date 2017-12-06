using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EditroomButtons
{	
	public GameObject parent;
	public GameObject[] Buttons;
}

public class ScreenEditRooms : BaseUI {
	public ScreenPopup screenPopup;
	public ScreenTutorial screenTutorial;
	public RoomController roomController;
	public GameObject boxVariants;
	public GameObject boxPrice;
	public Image iconVariant;
	public Text textPrice;
	public Text textPlayerCoin;

	public EditroomButtons[] editRoomButtons;

	BaseFurniture currentItem;
	int currentVariant = 0;
	bool isShowingUI = false;

	public override void InitUI ()
	{
		Debug.Log("edit room");	
		int currentRoom = (int)roomController.currentRoom;
		textPlayerCoin.text = PlayerData.Instance.PlayerCoin.ToString();

		ShowCurrentRoomEditButtons();
//		for(int i=0;i<roomController.rooms[currentRoom].furnitures.Length;i++){
//			roomController.rooms [currentRoom].furnitures [i].EnterEditmode ();
//		}

		if(PlayerData.Instance.TutorialFirstEditRoom == 0){
			screenTutorial.ShowFirstDialog (TutorialType.FirstEditRoomUI);
			PlayerData.Instance.TutorialFirstEditRoom = 1;
		}
		isShowingUI = true;
	}

	public void ShowCurrentRoomEditButtons()
	{
		foreach(EditroomButtons e in editRoomButtons) e.parent.SetActive(false);
		editRoomButtons[(int)(roomController.currentRoom)-1].parent.SetActive(true);
		foreach(GameObject g in editRoomButtons[(int)(roomController.currentRoom)-1].Buttons) g.SetActive(true);
	}

	//register event on click furniture
	void OnEnable(){
		EditFurnitureButton.OnClickToEdit += OnClickFurnitureItem;
		ScreenPopup.OnBuyFurniture += OnBuyFurniture;
	}

	void OnDisable(){
		isShowingUI = false;
		StopCoroutine ("CheckAdBanner");

		EditFurnitureButton.OnClickToEdit -= OnClickFurnitureItem;
		ScreenPopup.OnBuyFurniture -= OnBuyFurniture;
	}

	public void OnClickFurnitureItem(BaseFurniture currentItem){
		//laod furniture variant data
		//display in UI
		if (AdmobManager.Instance)
			AdmobManager.Instance.HideBanner ();
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

	public void HideDisplayItem()
	{
		boxVariants.SetActive(false);
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
//		for(int i=0;i<roomController.rooms[currentRoom].furnitures.Length;i++){
//			roomController.rooms [currentRoom].furnitures [i].ExitEditmode ();
//		}
		foreach(EditroomButtons e in editRoomButtons) e.parent.SetActive(false);
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

	IEnumerator CheckAdBanner(){
		while(true){
			if(isShowingUI){
				AdmobManager.Instance.HideBanner ();
			}
		}
	}

}
