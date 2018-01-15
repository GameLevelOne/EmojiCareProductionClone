using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GuidedTutorialIndex{
	Start = 0,
	Kitchen = 3,
	Bedroom = 21,
	Bathroom = 26,
	Playroom = 36,
	Garden = 46
}

public class GuidedTutorialStork : BaseUI {
	public GameObject tutorialObj;
	public GameObject hotkeyButton;
	public PopupGuidedTutorialUnlockables popupUnlockables;
	public string[] storkDialogs;
	public GameObject[] highlightPanels;
	public Vector3[] dialogBoxPositions; //top,middle,bottom
	public Transform dialogBox;
	public Text dialogText;
	public GameObject buttonNext;
	public Pan pan;
	public UIBowl uiBowl;
	public Cookbook cookbook;
	public Sponge sponge;
	public Shower shower;
	public DartboardMinigame dartboard;

	public GameObject arrowBathroomSponge;
	public GameObject arrowBathroomShower;
	public GameObject arrowSoapToEmoji;
	public GameObject arrowShowerToEmoji;
	public GameObject handBathroomSoap;
	public GameObject arrowGardenSeed;
	public GameObject arrowGardenItem;
	public GameObject arrowGardenSoil;
	public GameObject arrowGardenWateringCan;
	public GameObject arrowGardenCanToSoil;
	public GameObject arrowGardenSoilToBasket;
	public GameObject arrowGardenBasket;
	public GameObject arrowGardenItemToBasket;

	public Seed seed1, seed2, seed3;
	public StallItem item1;
	public WateringCan wateringCan;
	public GardenField gardenField;
	public GameObject refrigerator;

	int dialogCounter = 0;
	int cropCount = 0;

	void OnDisable(){
		Bowl.OnBowlOutsideFridge -= OnBowlOutsideFridge;
		Seed.OnTutorialSeedPlanted -= OnTutorialSeedPlanted;
		Food.OnFoodEaten -= OnFoodEaten;
		Crop.OnStallItemHarvested -= OnStallItemHarvested;
		uiBowl.OnTutorialBowlFull -= OnTutorialBowlFull;
		wateringCan.OnWateringCanPicked -= OnWateringCanPicked;
		wateringCan.OnWateringCanReleased -= OnWateringCanReleased;
		seed1.OnSeedPicked -= OnSeedPicked;
		seed2.OnSeedPicked -= OnSeedPicked;
		seed3.OnSeedPicked -= OnSeedPicked;
		seed1.OnSeedReturned -= OnSeedReturned;
		seed2.OnSeedReturned -= OnSeedReturned;
		seed3.OnSeedReturned -= OnSeedReturned;
		item1.OnItemPicked -= OnItemPicked;
		item1.OnItemDragged -= OnItemDragged;
		item1.OnItemReturned -= OnItemReturned;
		item1.OnItemHarvested -= OnItemHarvested;
	}

	public void RegisterEvents(){
		PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent += OnEmojiFirstSleepEvent;
		PlayerData.Instance.PlayerEmoji.playerInput.OnEmojiWake += OnEmojiFirstWakeEvent;
		PlayerData.Instance.PlayerEmoji.body.OnEmojiApplySponge += OnEmojiApplySponge;
		Crop.OnStallItemHarvested += OnStallItemHarvested;
		Crop.OnCropPicked += OnCropPicked;
		Crop.OnCropReturned += OnCropReturned;
		Seed.OnTutorialSeedPlanted += OnTutorialSeedPlanted;
		Bowl.OnBowlOutsideFridge += OnBowlOutsideFridge;
		Food.OnFoodEaten += OnFoodEaten;
		NotificationNewExpression.OnDismissPopup += OnDismissPopup;
		uiBowl.OnTutorialBowlFull += OnTutorialBowlFull;
		pan.OnCookingStart += OnCookingStart;
		pan.OnCookingDone += OnCookingDone;
		dartboard.OnDartThirdShot += OnDartThirdShot;
		wateringCan.OnWateringCanPicked += OnWateringCanPicked;
		wateringCan.OnWateringCanReleased += OnWateringCanReleased;
		gardenField.OnPlantWatered += OnPlantWatered;
		sponge.OnSpongePicked += OnSpongePicked;
		sponge.OnSpongeReleased += OnSpongeReleased;
		shower.OnShowerPicked += OnShowerPicked;
		shower.OnShowerReleased += OnShowerReleased;
	}

	public void RegisterSeedAndItemEvents()
	{
		seed1.OnSeedPicked += OnSeedPicked;
		seed2.OnSeedPicked += OnSeedPicked;
		seed3.OnSeedPicked += OnSeedPicked;
		seed1.OnSeedReturned += OnSeedReturned;
		seed2.OnSeedReturned += OnSeedReturned;
		seed3.OnSeedReturned += OnSeedReturned;
		item1.OnItemPicked += OnItemPicked;
		item1.OnItemDragged += OnItemDragged;
		item1.OnItemReturned += OnItemReturned;
		item1.OnItemHarvested += OnItemHarvested;
	}

	void OnDismissPopup ()
	{
		if(dialogCounter == 62)
			ShowFirstDialog (63);
	}

	void OnWateringCanReleased ()
	{
		arrowGardenCanToSoil.SetActive (false);
		arrowGardenWateringCan.SetActive (true);
	}

	void OnWateringCanPicked ()
	{
		arrowGardenCanToSoil.SetActive (true);
		arrowGardenWateringCan.SetActive (false);
	}

	void OnCropReturned ()
	{
		arrowGardenSoilToBasket.SetActive (true);
		arrowGardenBasket.SetActive (false);
	}

	void OnCropPicked ()
	{
		arrowGardenSoilToBasket.SetActive (false);
		arrowGardenBasket.SetActive (true);
	}

	void OnShowerReleased(){
		arrowBathroomShower.SetActive (true);
		arrowShowerToEmoji.SetActive (false);
	}

	void OnShowerPicked ()
	{
		arrowBathroomShower.SetActive (false);
		arrowShowerToEmoji.SetActive (true);
	}

	void OnSpongeReleased ()
	{
		arrowBathroomSponge.SetActive (true);
		handBathroomSoap.SetActive (true);
		arrowSoapToEmoji.SetActive (false);
	}

	void OnSpongePicked ()
	{
		arrowBathroomSponge.SetActive (false);
		handBathroomSoap.SetActive (false);
		arrowSoapToEmoji.SetActive (true);
	}

	void OnPlantWatered ()
	{
		if (dialogCounter == 50) {
			gardenField.OnPlantWatered -= OnPlantWatered;
			ShowFirstDialog (51);
		}
	}

	void OnTutorialSeedPlanted ()
	{
		if (dialogCounter == 49)
			ShowFirstDialog (50);
	}

	void OnItemHarvested ()
	{
		if (dialogCounter == 57)
			ShowFirstDialog (58); //CONTINUE?
	}

	void OnItemReturned ()
	{
		arrowGardenItem.SetActive (true);
		arrowGardenItemToBasket.SetActive (false);
	}

	void OnItemDragged()
	{
		arrowGardenItem.SetActive (false);
		arrowGardenItemToBasket.SetActive (true);
	}

	void OnSeedReturned ()
	{
		arrowGardenSeed.SetActive (true);
		arrowGardenSoil.SetActive (false);
	}

	void OnItemPicked (StallItem item)
	{
		arrowGardenItem.SetActive (false);
		arrowGardenBasket.SetActive (true);
	}

	void OnSeedPicked ()
	{
		arrowGardenSeed.SetActive (false);
		arrowGardenSoil.SetActive (true);
	}

	void OnTutorialItemDragged ()
	{
		if (dialogCounter == 55)
			ShowFirstDialog (56); 
	}

	void OnStallItemHarvested ()
	{
		cropCount++;

		if(cropCount==3){
			ShowFirstDialog (54);
		} else if(cropCount==9){
			ShowFirstDialog (55);
		}
	}

	void OnDartThirdShot ()
	{
		dartboard.OnDartThirdShot -= OnDartThirdShot;
		ShowFirstDialog (40);
	}

	void OnEmojiApplySponge (float value)
	{
		Debug.Log ("foamvalue:" + value);
		if(value >=10){
			sponge.OnSpongePicked -= OnSpongePicked;
			sponge.OnSpongeReleased -= OnSpongeReleased;
			PlayerData.Instance.PlayerEmoji.body.OnEmojiApplySponge -= OnEmojiApplySponge;
			ShowFirstDialog (33);
			PlayerData.Instance.PlayerEmoji.OnEmojiHygieneCheck += OnEmojiHygieneCheck;
		}
	}

	void OnEmojiHygieneCheck (float ratio)
	{
		if(ratio >= 1f){
			shower.OnShowerPicked -= OnShowerPicked;
			shower.OnShowerReleased -= OnShowerReleased;
			PlayerData.Instance.PlayerEmoji.OnEmojiHygieneCheck -= OnEmojiHygieneCheck;
			ShowFirstDialog (34);
		}
	}

	void OnEmojiFirstWakeEvent(){
		PlayerData.Instance.PlayerEmoji.playerInput.OnEmojiWake -= OnEmojiFirstWakeEvent;
		PlayerData.Instance.PlayerEmoji.hygiene.SetStats (0.19f * PlayerData.Instance.PlayerEmoji.hygiene.MaxStatValue);
		ShowFirstDialog (24);
	}

	void OnEmojiFirstSleepEvent(bool sleeping){
		PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent -= OnEmojiFirstSleepEvent;
		PlayerData.Instance.PlayerEmoji.hunger.SetStats (60);
		ShowFirstDialog (22);
	}

	void OnFoodEaten ()
	{
		if (dialogCounter == 11){
			ShowFirstDialog (12);
			PlayerData.Instance.FirstCook = 1;
		} else if (dialogCounter == 61) {
			Emoji playerEmoji = PlayerData.Instance.PlayerEmoji;
			playerEmoji.happiness.SetStats (playerEmoji.happiness.MaxStatValue);
			playerEmoji.hunger.SetStats (0.5f * playerEmoji.hunger.MaxStatValue);
			ShowFirstDialog (62);
		}
	}

	void OnCookingDone ()
	{
		pan.OnCookingDone -= OnCookingDone;
		if(dialogCounter == 9)
			ShowFirstDialog (10);
	}

	void OnCookingStart ()
	{
		pan.OnCookingStart -= OnCookingStart;
		if(dialogCounter == 8)
			ShowFirstDialog (9);
	}

	void OnBowlOutsideFridge (){
		if(dialogCounter == 7)
			ShowFirstDialog (8);
	}

	void OnTutorialBowlFull(){
		if(dialogCounter == 6)
			ShowFirstDialog (7);
	}

	public void ShowFirstDialog(int idx){
		transform.GetChild (0).GetComponent<Image> ().raycastTarget = true;
		buttonNext.SetActive (true);
		dialogCounter = idx;
		dialogText.text = storkDialogs [dialogCounter];
		SetMisc ();
		ShowUI (tutorialObj);
	}

	public void OnNextDialog(){
		if(dialogCounter+1 == 1){
			popupUnlockables.SetDisplay (UnlockType.Room, RoomType.Kitchen);
		} else if(dialogCounter+1 == 4){
			popupUnlockables.SetDisplay (UnlockType.Recipe);
		} else if(dialogCounter+1 == 20){
			popupUnlockables.SetDisplay (UnlockType.Room, RoomType.Bedroom);
		} else if(dialogCounter+1 == 25){
			popupUnlockables.SetDisplay (UnlockType.Room, RoomType.Bathroom);
		} else if(dialogCounter+1 == 35){
			popupUnlockables.SetDisplay (UnlockType.Room, RoomType.Playroom);
		} else if(dialogCounter+1 == 45){
			popupUnlockables.SetDisplay (UnlockType.Room, RoomType.Garden);
		} else if(dialogCounter == 61){
			refrigerator.GetComponent<BoxCollider2D> ().enabled = true;
			CloseUI (tutorialObj);
		} else{
			transform.GetChild (0).GetComponent<Image> ().raycastTarget = true;
			dialogBox.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			buttonNext.SetActive (true);
			if(dialogCounter == 27){
				dialogCounter = 30;
			} else 
				dialogCounter++;
			dialogText.text = storkDialogs [dialogCounter];
			SetMisc ();
		}
	}

	public void SetMisc(){
		SetDialogBoxPosition ();
		SetHighlightPanels ();
		SetEvents ();
	}

	public void SetDialogBoxPosition(){
//		if(dialogCounter >= 0 && dialogCounter <= 6){
//			
//		} 
		if(dialogCounter == 7 || (dialogCounter >= 46 && dialogCounter <= 59)){
			dialogBox.localPosition = dialogBoxPositions [0];
		} else{
			dialogBox.localPosition = dialogBoxPositions [2];
		}
	}

	public void SetHighlightPanels ()
	{
		foreach (GameObject obj in highlightPanels) {
			if (obj.activeSelf) {
				obj.SetActive (false);
			}
		}

		hotkeyButton.GetComponent<Button> ().interactable = false;

		if (dialogCounter == 1 || dialogCounter == 20 || dialogCounter == 25 || dialogCounter == 35 || dialogCounter == 45 || dialogCounter == 59) {
			highlightPanels [0].SetActive (true);
			hotkeyButton.GetComponent<Button> ().interactable = true;
		}
		else if (dialogCounter == 2)
			highlightPanels [1].SetActive (true);
		else if (dialogCounter == 4)
			highlightPanels [3].SetActive (true);
		else if (dialogCounter == 6) {
			highlightPanels [4].SetActive (true);
			refrigerator.GetComponent<BoxCollider2D> ().enabled = false;
		} else if (dialogCounter == 7)
			highlightPanels [5].SetActive (true);
		else if (dialogCounter == 8)
			highlightPanels [6].SetActive (true);
		else if (dialogCounter == 11)
			highlightPanels [7].SetActive (true);
		else if (dialogCounter == 13) {
			highlightPanels [8].SetActive (true);
			cookbook.GetComponent<Rigidbody2D> ().simulated = true;
			cookbook.GetComponent<BoxCollider2D> ().enabled = true;
		} else if (dialogCounter == 16)
			highlightPanels [9].SetActive (true);
		else if (dialogCounter == 21)
			highlightPanels [11].SetActive (true);
		else if (dialogCounter == 22)
			highlightPanels [12].SetActive (true);
		else if (dialogCounter == 23)
			highlightPanels [13].SetActive (true);
		else if (dialogCounter == 30)
			highlightPanels [15].SetActive (true);
		else if (dialogCounter == 31)
			highlightPanels [16].SetActive (true);
		else if (dialogCounter == 33)
			highlightPanels [17].SetActive (true);
		else if (dialogCounter == 38)
			highlightPanels [19].SetActive (true);
		else if (dialogCounter == 39)
			highlightPanels [20].SetActive (true);
		else if (dialogCounter == 40)
			highlightPanels [21].SetActive (true);
		else if (dialogCounter == 49)
			highlightPanels [23].SetActive (true);
		else if (dialogCounter == 50)
			highlightPanels [24].SetActive (true);
//		else if(dialogCounter == 52)
//			highlightPanels [25].SetActive (true);
		else if (dialogCounter == 53)
			highlightPanels [26].SetActive (true);
		else if (dialogCounter == 57)
			highlightPanels [27].SetActive (true);
		else if (dialogCounter == 54)
			highlightPanels [28].SetActive (true);
		else if (dialogCounter == 62)
			highlightPanels [31].SetActive (true);

		foreach (GameObject obj in highlightPanels) {
			if (obj.activeSelf) {
				buttonNext.SetActive (false);
				transform.GetChild (0).GetComponent<Image> ().raycastTarget = false;
				break;
			}
		}
	}

	public void SetEvents ()
	{
		float tresholdLow = 0.19f;
		Emoji playerEmoji = PlayerData.Instance.PlayerEmoji;
		if (dialogCounter == 0) {
			playerEmoji.hunger.SetStats (tresholdLow * playerEmoji.hunger.MaxStatValue);
		} else if(dialogCounter == 7 || dialogCounter == 8 || dialogCounter == 11){
			dialogBox.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		} else if(dialogCounter == 9){
			buttonNext.SetActive (false);
		} else if (dialogCounter == 18) {
			playerEmoji.stamina.SetStats (tresholdLow * playerEmoji.stamina.MaxStatValue);
		} else if (dialogCounter == 20) {
			PlayerData.Instance.LocationBedroom = 1;
		} else if (dialogCounter == 22) {
			StartCoroutine (WaitForWakeUp ());
		} else if (dialogCounter == 25) {
			PlayerData.Instance.LocationBathroom = 1;
		} else if (dialogCounter == 35) {
			PlayerData.Instance.LocationPlayroom = 1;
		} else if(dialogCounter == 45){
			PlayerData.Instance.LocationGarden = 1;
		} 
	}

	public void OnClickHotkey(){
		StartCoroutine (WaitForHotkeyAnimation ());
	}

	public void SetLocationHighlight ()
	{
		if (dialogCounter == 2 || dialogCounter == 59) {
			highlightPanels [2].SetActive (true);
		} else if (dialogCounter == 20) {
			highlightPanels [10].SetActive (true);
		} else if (dialogCounter == 25) {
			highlightPanels [14].SetActive (true);
		} else if (dialogCounter == 35) {
			highlightPanels [18].SetActive (true);
		} else if(dialogCounter == 45){
			highlightPanels [22].SetActive (true);
		}
		
	}

	public void OnClickBackAfterDartboard(){
		StartCoroutine (WaitForHappinessMeterInDartboard ());
	}

	public void SetDialogInKitchen(){
		if(dialogCounter == 2){
			ShowFirstDialog (3);
		} else if(dialogCounter == 59){
			ShowFirstDialog (60);
		}
	}

	IEnumerator WaitForWakeUp(){
		buttonNext.SetActive (false);
		yield return new WaitForSeconds (7);
		buttonNext.SetActive (true);
		ShowFirstDialog (23);
	}

	IEnumerator WaitForHappinessMeterInDartboard(){
		buttonNext.SetActive (false);
		yield return new WaitForSeconds (2);
		buttonNext.SetActive (true);
		PlayerData.Instance.PlayerEmoji.hunger.SetStats (0.19f * PlayerData.Instance.PlayerEmoji.hunger.MaxStatValue);
		ShowFirstDialog (41);
	}

	IEnumerator WaitForHotkeyAnimation(){
		highlightPanels [29].SetActive (true);
		highlightPanels [0].SetActive (false);
		buttonNext.SetActive (false);
		yield return new WaitForSeconds (0.4f);
		highlightPanels [29].SetActive (false);
		if(dialogCounter == 1){
			ShowFirstDialog (2);
		} else if(dialogCounter == 20 || dialogCounter == 25 || dialogCounter == 35 || dialogCounter == 45 || dialogCounter == 59){
			highlightPanels [0].SetActive (false);
			highlightPanels [1].SetActive (true);
		} else if(dialogCounter == 62){
			highlightPanels [0].SetActive (false);
			highlightPanels [30].SetActive (true);
		}
	}

}
