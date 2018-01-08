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
	public GameObject currentUsedIcon;
	public Image iconVariant;
	public Text textPrice;
	public Text textPlayerCoin;

	public EditroomButtons[] editRoomButtons;

	BaseFurniture currentItem;
	int currentVariant = 0;
	int currentUsedVariant = 0;
	bool isShowingUI = false;

	public override void InitUI ()
	{
		Debug.Log("edit room");	
		int currentRoom = (int)roomController.currentRoom;
		textPlayerCoin.text = PlayerData.Instance.PlayerCoin.ToString("N0");
		boxVariants.SetActive (false);
		boxPrice.SetActive (false);

		ShowCurrentRoomEditButtons();
//		for(int i=0;i<roomController.rooms[currentRoom].furnitures.Length;i++){
//			roomController.rooms [currentRoom].furnitures [i].EnterEditmode ();
//		}

//		if(PlayerData.Instance.TutorialFirstEditRoom == 0){
//			screenTutorial.ShowFirstDialog (TutorialType.FirstEditRoomUI);
//			PlayerData.Instance.TutorialFirstEditRoom = 1;
//		}
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
		ScreenPopup.OnCancelBuyFurniture += OnCancelBuyFurniture;
	}

	void OnDisable(){
		isShowingUI = false;
		StopCoroutine ("CheckAdBanner");

		EditFurnitureButton.OnClickToEdit -= OnClickFurnitureItem;
		ScreenPopup.OnBuyFurniture -= OnBuyFurniture;
		ScreenPopup.OnCancelBuyFurniture -= OnCancelBuyFurniture;
	}

	public void OnClickFurnitureItem(BaseFurniture currentItem){
		//laod furniture variant data
		//display in UI
		if (AdmobManager.Instance)
			AdmobManager.Instance.HideBanner ();
		this.currentItem = currentItem;
		this.currentVariant = currentItem.currentVariant;
		currentUsedVariant = currentItem.currentVariant;
		currentUsedIcon.SetActive (true);
		DisplayItem (currentVariant);
	}

	void OnBuyFurniture ()
	{
		currentItem.currentVariant = currentVariant;
		currentItem.variant [currentVariant].bought = true;
		currentUsedVariant = currentVariant;
		PlayerData.Instance.PlayerCoin -= currentItem.variant [currentVariant].price;
		currentItem.OnVariantBought (currentVariant);
	}

	void OnCancelBuyFurniture ()
	{
	}

	void ConfirmBuyObject(int price){
		int currentCoin = PlayerData.Instance.PlayerCoin;
		 if(currentCoin>=price){
			screenPopup.ShowPopup (PopupType.Confirmation, PopupEventType.AbleToBuyFurniture, false, false);
		 } else {
			screenPopup.ShowPopup (PopupType.Confirmation, PopupEventType.NotAbleToBuyFurniture, true, false);
		 }
	}

	public void OnClickNextItem(){
		int maxVariant = currentItem.variant.Length;

		currentVariant++;
		if(currentVariant == maxVariant){
			currentVariant = 0;
		} 
		if (currentVariant != currentUsedVariant)
			currentUsedIcon.SetActive (false);
		else
			currentUsedIcon.SetActive (true);
		DisplayItem (currentVariant);
	}

	public void OnClickPrevItem(){
		int maxVariant = currentItem.variant.Length;

		currentVariant--;
		if(currentVariant<0){
			currentVariant = maxVariant - 1;
		}
		if (currentVariant != currentUsedVariant)
			currentUsedIcon.SetActive (false);
		else
			currentUsedIcon.SetActive (true);
		DisplayItem (currentVariant);
	}

	void DisplayItem(int index){
		FurnitureVariant item = currentItem.variant [currentVariant];
		boxVariants.SetActive (true);
		currentItem.currentVariant = currentVariant;
		currentItem.SetCurrentVariant ();
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
			currentItem.currentVariant = currentVariant;
			currentUsedVariant = currentVariant;
			currentItem.SetCurrentVariant ();
			boxPrice.SetActive (false);
			currentUsedIcon.SetActive (true);
		} else{
			ConfirmBuyObject (item.price);
		}
		textPlayerCoin.text = PlayerData.Instance.PlayerCoin.ToString("N0");
	}

	public void DisableEditMode(){
		int currentRoom = (int)roomController.currentRoom;
//		for(int i=0;i<roomController.rooms[currentRoom].furnitures.Length;i++){
//			roomController.rooms [currentRoom].furnitures [i].ExitEditmode ();
//		}
		foreach(EditroomButtons e in editRoomButtons) e.parent.SetActive(false);
	}

	public void OnClickBack ()
	{
		Debug.Log ("used:" + currentUsedVariant);
		Debug.Log ("current:" + currentVariant);

		if (currentItem != null) {
			if (currentUsedVariant != currentItem.currentVariant) {
				currentItem.currentVariant = currentUsedVariant;
			}
			currentItem.SetCurrentVariant ();
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
