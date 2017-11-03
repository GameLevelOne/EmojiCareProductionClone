using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TutorialType{
	FirstVisit,
	IdleLivingRoom,
	FirstBedroom,
	FirstBathroom,
	FirstKitchen,
	FirstPlayroom,
	FirstGarden,
	FirstProgressUI,
	FirstEditRoomUI,
	TriggerHungerRed,
	TriggerHygieneRed,
	TriggerHappinessRed,
	TriggerStaminaRed,
	TriggerHealthOrange,
	TriggerHealthRed,
	TriggerFirstExpressionFull,
	TriggerFirstDead
}

public enum TutorialPanelType{
	ArrowEmoji,
	ArrowLRRooms,
	ArrowBed,
	ArrowSpongeSoap,
	ArrowSpongeEmoji,
	ArrowCookbook,
	ArrowRefrigerator,
	ArrowTablePan,
	ArrowPlate,
	ArrowPlayroom,
	ArrowSeeds,
	ArrowField,
	ArrowWateringCan,
	ArrowExpressionList,
	ArrowProgressBar,
	ArrowSendButton,
	ArrowMenuButton
}

public class ScreenTutorial : BaseUI {
	public string[] firstVisit;
	public string[] idleLivingRoom;
	public string[] firstBedroom;
	public string[] firstBathroom;
	public string[] firstKitchen;
	public string[] firstPlayroom;
	public string[] firstGarden;
	public string[] firstProgressUI;
	public string[] firstEditRoomsUI;

	public string[] triggerHungerRed;
	public string[] triggerHygieneRed;
	public string[] triggerHappinessRed;
	public string[] triggerStaminaRed;
	public string[] triggerHealthOrange;
	public string[] triggerHealthRed;
	public string[] triggerFirstExpressionFull;
	public string[] triggerFirstDead;

	public GameObject[] tutorialPanels;

	public ProloguePopupName popupName;
	public GameObject screenTutorialObj;
	public Text dialogText;

	public List<GameObject> firstTutorialPanels = new List<GameObject>();
	List<TutorialType> statsTutorialPanels = new List<TutorialType> ();

	TutorialType currentTutorial = TutorialType.FirstVisit;
	int panelCount = 0;
	int dialogCount = 0;
	bool showStatsTutorial = false;

	bool roomChange = false;

	void Awake(){
		PlayerPrefs.DeleteAll ();
	}

	void Start(){
		dialogText.text = firstVisit [0];
	}

	public void Init()
	{
		Emoji emoji = PlayerData.Instance.PlayerEmoji;
		emoji.OnCheckStatsTutorial += CheckStatsTutorial;
	}

	void OnDisable(){
		Emoji emoji = PlayerData.Instance.PlayerEmoji;
		emoji.OnCheckStatsTutorial -= CheckStatsTutorial;
	}

	public void CheckStatsTutorial (float hunger, float hygiene, float happiness, float stamina, float health)
	{
		float redThreshold = 0.2f;
		float orangeThreshold = 0.4f;

		if (!showStatsTutorial) {
			showStatsTutorial = true;
			if (hunger < redThreshold && PlayerData.Instance.TutorialFirstHungerRed == 0) {
				statsTutorialPanels.Add (TutorialType.TriggerHungerRed);
				CheckStatsPlayerPrefs (TutorialType.TriggerHungerRed);
			} 
			if (hygiene < redThreshold && PlayerData.Instance.TutorialFirstHygieneRed == 0) {
				statsTutorialPanels.Add (TutorialType.TriggerHygieneRed);
				CheckStatsPlayerPrefs (TutorialType.TriggerHygieneRed);
			}
			if (happiness < redThreshold && PlayerData.Instance.TutorialFirstHappinessRed == 0) {
				statsTutorialPanels.Add (TutorialType.TriggerHappinessRed);
				CheckStatsPlayerPrefs (TutorialType.TriggerHappinessRed);
			}
			if (stamina < redThreshold && PlayerData.Instance.TutorialFirstStaminaRed == 0) {
				statsTutorialPanels.Add (TutorialType.TriggerStaminaRed);
				CheckStatsPlayerPrefs (TutorialType.TriggerStaminaRed);
			}
			if (health < orangeThreshold && health >= redThreshold && PlayerData.Instance.TutorialFirstHealthOrange == 0) {
				statsTutorialPanels.Add (TutorialType.TriggerHealthOrange);
				CheckStatsPlayerPrefs (TutorialType.TriggerHealthOrange);
			}
			if (health < redThreshold && PlayerData.Instance.TutorialFirstHealthRed == 0) {
				statsTutorialPanels.Add (TutorialType.TriggerHealthRed);
				CheckStatsPlayerPrefs (TutorialType.TriggerHealthRed);
			}
			ShowFirstDialog (statsTutorialPanels [0]);

			Debug.Log ("stacks:" + statsTutorialPanels.Count);
		}
	}

	public override void InitUI ()
	{
		dialogCount = 0;
	}

	public void ShowTutorialPanels ()
	{
		if (panelCount < firstTutorialPanels.Count) {
			firstTutorialPanels [panelCount].SetActive (true);
			panelCount++;
		}
		if(panelCount == firstTutorialPanels.Count){
			PlayerData.Instance.PlayerFirstPlay = 1;
		}
	}

	void ShowPopup(){
		popupName.ShowUI(popupName.gameObject);
	}

	public void ClosePopup(){
		popupName.CloseUI (popupName.gameObject);
		OnClickNext ();
	}

	public void ShowFirstDialog(TutorialType type){
		string emojiName = PlayerData.Instance.EmojiName;
		if(type == TutorialType.IdleLivingRoom){
			dialogText.text = idleLivingRoom [0];
		} else if(type == TutorialType.FirstBathroom){
			dialogText.text = "If "+emojiName+" gets dirty, give it a good bath.";
		} else if(type == TutorialType.FirstBedroom){
			dialogText.text = "Sometimes "+emojiName+" will get tired";
		} else if(type == TutorialType.FirstEditRoomUI){
			dialogText.text = firstEditRoomsUI [0];
		} else if(type == TutorialType.FirstGarden){
			dialogText.text = firstGarden [0];
		} else if(type == TutorialType.FirstKitchen){
			dialogText.text = "Don't forget to feed "+emojiName;
		} else if(type == TutorialType.FirstPlayroom){
			dialogText.text = firstPlayroom [0];
		} else if(type == TutorialType.FirstProgressUI){
			dialogText.text = "Here you can see the growth of "+emojiName;
		} else if(type == TutorialType.TriggerFirstDead){
			dialogText.text = triggerFirstDead [0];
		} else if(type == TutorialType.TriggerFirstExpressionFull){
			dialogText.text = triggerFirstExpressionFull [0];
		} else if(type == TutorialType.TriggerHappinessRed){
			dialogText.text = "Looks like "+emojiName+" is having a bad mood!";
		} else if(type == TutorialType.TriggerHealthOrange){
			dialogText.text = emojiName+" is sick!";
		} else if(type == TutorialType.TriggerHealthRed){
			dialogText.text = emojiName+" is near death, what did you do??!";
		} else if(type == TutorialType.TriggerHungerRed){
			dialogText.text = emojiName +" is starving!";
		} else if(type == TutorialType.TriggerHygieneRed){
			dialogText.text = emojiName +" is very dirty!";
		} else if(type == TutorialType.TriggerStaminaRed){
			dialogText.text = emojiName+" is exhausted!";
		} else if(type == TutorialType.TriggerStaminaRed){
			dialogText.text = triggerStaminaRed [0];
		}
		base.ShowUI (screenTutorialObj);
	}

	public void CheckStatsPlayerPrefs(TutorialType type){
		bool showTutorial = true;

		if(type == TutorialType.TriggerHungerRed){
			Debug.Log ("hunger red pref");
			currentTutorial = TutorialType.TriggerHungerRed;
			if(PlayerData.Instance.TutorialFirstHungerRed == 0){
				PlayerData.Instance.TutorialFirstHungerRed = 1;
			} 
		} else if(type == TutorialType.TriggerHygieneRed){
			Debug.Log ("hygiene red pref");
			currentTutorial = TutorialType.TriggerHygieneRed;
			if(PlayerData.Instance.TutorialFirstHygieneRed == 0){
				PlayerData.Instance.TutorialFirstHygieneRed = 1;
			} 
		} else if(type == TutorialType.TriggerHappinessRed){
			Debug.Log ("happiness red pref");
			currentTutorial = TutorialType.TriggerHappinessRed;
			if(PlayerData.Instance.TutorialFirstHappinessRed == 0){
				PlayerData.Instance.TutorialFirstHappinessRed = 1;
			} 
		} else if(type == TutorialType.TriggerStaminaRed){
			Debug.Log ("stamina red pref");
			currentTutorial = TutorialType.TriggerStaminaRed;
			if(PlayerData.Instance.TutorialFirstStaminaRed == 0){
				PlayerData.Instance.TutorialFirstStaminaRed = 1;
			} 
		} else if(type == TutorialType.TriggerHealthOrange){
			Debug.Log ("health orange pref");
			currentTutorial = TutorialType.TriggerHealthOrange;
			if(PlayerData.Instance.TutorialFirstHealthOrange == 0){
				PlayerData.Instance.TutorialFirstHealthOrange = 1;
			} 
		} else if(type == TutorialType.TriggerHealthRed){
			Debug.Log ("health red pref");
			currentTutorial = TutorialType.TriggerHealthRed;
			if(PlayerData.Instance.TutorialFirstHealthRed == 0){
				PlayerData.Instance.TutorialFirstHealthRed = 1;
			} 
		}
	}

	public void CheckRoomPlayerPrefs(RoomType roomType){
		bool showTutorial = true;

		if(roomType == RoomType.Bedroom){
			currentTutorial = TutorialType.FirstBedroom;
			if(PlayerData.Instance.TutorialFirstBedroom == 0){
				PlayerData.Instance.TutorialFirstBedroom = 1;
			}else{
				showTutorial = false;
			}
		} else if(roomType == RoomType.Bathroom){
			currentTutorial = TutorialType.FirstBathroom;
			if(PlayerData.Instance.TutorialFirstBathroom == 0){
				PlayerData.Instance.TutorialFirstBathroom = 1;
			}else{
				showTutorial = false;
			}
		} else if(roomType == RoomType.Garden){
			currentTutorial = TutorialType.FirstGarden;
			if(PlayerData.Instance.TutorialFirstGarden == 0){
				PlayerData.Instance.TutorialFirstGarden = 1;
			}else{
				showTutorial = false;
			}
		} else if(roomType == RoomType.Kitchen){
			currentTutorial = TutorialType.FirstKitchen;
			if(PlayerData.Instance.TutorialFirstKitchen == 0){
				PlayerData.Instance.TutorialFirstKitchen = 1;
			}else{
				showTutorial = false;
			}
		} else if(roomType == RoomType.Playroom){
			currentTutorial = TutorialType.FirstPlayroom;
			if(PlayerData.Instance.TutorialFirstPlayroom == 0){
				PlayerData.Instance.TutorialFirstPlayroom = 1;
			}else{
				showTutorial = false;
			}
		} 

		if(showTutorial){
			ShowFirstDialog (currentTutorial);
		}
	}

	public void OnClickNext(){
		bool loadCustomDialog = false;
		string emojiName = PlayerData.Instance.EmojiName;

		if(currentTutorial == TutorialType.FirstVisit){
			if(dialogCount < (firstVisit.Length-1)){
				dialogCount++;
			} else{
				base.CloseUI (screenTutorialObj);
				PlayerData.Instance.TutorialFirstVisit = 1;
				StartCoroutine (WaitForRoomChange ());
			}

			if(dialogCount == 1){
				ShowPopup ();
			} else if(dialogCount==2){
				dialogText.text = emojiName + firstVisit [dialogCount];
				loadCustomDialog = true;
			} else if(dialogCount == 3){
				dialogText.text = firstVisit [dialogCount] + emojiName;
				loadCustomDialog = true;
			} else if(dialogCount == 4){
				dialogText.text = "You can caress and hold "+emojiName+" with your finger.";
				loadCustomDialog = true;
			} else if(dialogCount == 5){
				dialogText.text = "If you raise with "+emojiName+" care, it will have many various expressions! ";
				loadCustomDialog = true;
			}
			if (!loadCustomDialog) {
				dialogText.text = firstVisit [dialogCount];
			}
		} else if(currentTutorial == TutorialType.IdleLivingRoom){
			LoadDialogs (idleLivingRoom, PlayerData.Instance.TutorialIdleLivingRoom,false);

		} else if(currentTutorial == TutorialType.FirstBedroom){
			LoadDialogs (firstBedroom, PlayerData.Instance.TutorialFirstBedroom,false);

		} else if(currentTutorial == TutorialType.FirstBathroom){
			firstBathroom [2] = "wash them nicely on " + emojiName;
//			if(dialogCount < (firstBathroom.Length-1)){
//				dialogCount++;
//			} else{
//				base.CloseUI (screenTutorialObj);
//				PlayerData.Instance.TutorialFirstBathroom = 1;
//			}
//
//			if(dialogCount == 2){
//				dialogText.text = "wash them nicely on "+emojiName;
//				loadCustomDialog = true;
//			}
//			if(!loadCustomDialog)
//				dialogText.text = firstBathroom [dialogCount];
			LoadDialogs (firstBathroom, PlayerData.Instance.TutorialFirstBathroom, false);
			
		} else if(currentTutorial == TutorialType.FirstKitchen){
			if(dialogCount < (firstKitchen.Length-1)){
				dialogCount++;
			} else{
				base.CloseUI (screenTutorialObj);
				PlayerData.Instance.TutorialFirstKitchen = 1;
			}

			if(dialogCount == 5){
				dialogText.text = "When cooked, bring them to " + emojiName + " for eat";
				loadCustomDialog = true;
			}

			if(!loadCustomDialog)
				dialogText.text = firstKitchen [dialogCount];

		} else if(currentTutorial == TutorialType.FirstPlayroom){
			LoadDialogs (firstPlayroom, PlayerData.Instance.TutorialFirstPlayroom,false);

		} else if(currentTutorial == TutorialType.FirstGarden){
			LoadDialogs (firstGarden, PlayerData.Instance.TutorialFirstGarden,false);
		} else if(currentTutorial == TutorialType.FirstProgressUI){ //TODO: check this later
			if(dialogCount < (firstProgressUI.Length-1)){
				dialogCount++;
			} else{
				base.CloseUI (screenTutorialObj);
				PlayerData.Instance.TutorialFirstProgressUI = 1;
			}
			dialogText.text = firstProgressUI [dialogCount];
		} else if(currentTutorial == TutorialType.FirstEditRoomUI){
			LoadDialogs (firstEditRoomsUI, PlayerData.Instance.TutorialFirstEditRoom,false);
		} else if(currentTutorial == TutorialType.TriggerHungerRed){
			LoadDialogs (triggerHungerRed, PlayerData.Instance.TutorialFirstHungerRed,true);
		} else if(currentTutorial == TutorialType.TriggerHygieneRed){
			LoadDialogs (triggerHygieneRed, PlayerData.Instance.TutorialFirstHygieneRed,true);
		} else if(currentTutorial == TutorialType.TriggerHappinessRed){
			LoadDialogs (triggerHappinessRed, PlayerData.Instance.TutorialFirstHappinessRed,true);
		} else if(currentTutorial == TutorialType.TriggerStaminaRed){
			LoadDialogs (triggerStaminaRed, PlayerData.Instance.TutorialFirstStaminaRed,true);
		} else if(currentTutorial == TutorialType.TriggerHealthOrange){
			triggerHealthOrange[2] = "Pick up the medicine from medicine box and give it to "+emojiName+"!";
			LoadDialogs (triggerHealthOrange, PlayerData.Instance.TutorialFirstHealthOrange,true);
		} else if(currentTutorial == TutorialType.TriggerHealthRed){
			triggerHealthRed[2] = "Pick up the syringe from medicine box and give it to "+emojiName+"!";
			LoadDialogs (triggerHealthRed, PlayerData.Instance.TutorialFirstHealthRed,true);
		} else if(currentTutorial == TutorialType.TriggerFirstDead){
			LoadDialogs (triggerFirstDead, PlayerData.Instance.TutorialFirstEmojiDead,false);
		} else if(currentTutorial == TutorialType.TriggerFirstExpressionFull){
			if(dialogCount < (triggerFirstExpressionFull.Length-1)){
				dialogCount++;
			} else{
				base.CloseUI (screenTutorialObj);
				PlayerData.Instance.TutorialFirstExpressionFull = 1;
			}

			if(dialogCount == 1){
				dialogText.text = "I can see that " + emojiName + " is mature enough.";
				loadCustomDialog = true;
			} else if(dialogCount == 8){
				dialogText.text = "If you want to send "+emojiName+" off, and raise another emoji,";
				loadCustomDialog = true;
			}

			if(!loadCustomDialog)
				dialogText.text = triggerFirstExpressionFull [dialogCount];
		}
	}

	public void LoadDialogs (string[] currentDialog, int currentPref, bool isStatsTutorial)
	{
		if (dialogCount < (currentDialog.Length - 1)) {
			dialogCount++;
		} else {
			//currentPref = 1;
			base.CloseUI (screenTutorialObj);
			Debug.Log (statsTutorialPanels.Count);
			if (isStatsTutorial) {
				if (statsTutorialPanels.Count > 1) {
					statsTutorialPanels.RemoveAt (0);
					StartCoroutine (LoadNextInStack ());
				} else {
					statsTutorialPanels.RemoveAt (0);
				}
			}
		}
		dialogText.text = currentDialog [dialogCount];
	}

	public void TriggerRoomChange(){
		roomChange = true;
	}

	IEnumerator WaitForRoomChange(){
		Debug.Log ("START WAITING");
		yield return new WaitForSeconds (10f);
		if (!roomChange) {
			currentTutorial = TutorialType.IdleLivingRoom;
			ShowFirstDialog (currentTutorial);
		}else{
			PlayerData.Instance.TutorialIdleLivingRoom = 1;
		}
	}

	IEnumerator LoadNextInStack(){
		yield return new WaitForSeconds (0.16f);
		ShowFirstDialog (statsTutorialPanels [0]);
		showStatsTutorial = false;
	}
}
