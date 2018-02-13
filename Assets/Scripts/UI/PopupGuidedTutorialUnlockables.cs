using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UnlockType{
	Room,
	Recipe,
	Ingredient
}

public class PopupGuidedTutorialUnlockables : BaseUI {
	public GuidedTutorialStork guidedTutorial;
	public ParticlePlayer particlePlayer;
	public Sprite[] bgRooms;
	public GameObject ingredientSprites;
	public GameObject unlockRoomSprite;
	public GameObject unlockRecipeSprite;
	public GameObject popupObj;
	public Text unlockText;

	RoomType currentTargetRoom;
	UnlockType currentUnlockType;

	public void SetDisplay(UnlockType unlockType,RoomType roomType = RoomType.LivingRoom){
		currentTargetRoom = roomType;
		currentUnlockType = unlockType;
		if(unlockType == UnlockType.Room){
			unlockRoomSprite.SetActive (true);
			ingredientSprites.SetActive (false);
			unlockRecipeSprite.SetActive (false);
			unlockRoomSprite.GetComponent<Image> ().sprite = bgRooms [(int)roomType];
			unlockText.text = "You have unlocked: \n"+roomType.ToString().ToUpper();
			unlockText.fontSize = 43;
		} else if(unlockType == UnlockType.Ingredient){
			unlockRoomSprite.SetActive (false);
			unlockRecipeSprite.SetActive (false);
			ingredientSprites.SetActive (true);
			unlockText.text = "You have unlocked: \n TOMATO, CARROT, \n CHICKEN, CABBAGE";
			unlockText.fontSize = 35;
		} else if(unlockType == UnlockType.Recipe){
			unlockRoomSprite.SetActive (false);
			unlockRecipeSprite.SetActive (true);
			ingredientSprites.SetActive (false);
			unlockText.text = "You have unlocked: \n RECIPE: SALAD";
			unlockText.fontSize = 43;
		}
		particlePlayer.ShowParticleFireworks ();
		if (SoundManager.Instance)
			SoundManager.Instance.PlaySFXOneShot (SFXList.Achievement);
		ShowUI (popupObj);
	}

	public void OnClickContinue ()
	{
		if (currentUnlockType == UnlockType.Room) {
			if (currentTargetRoom == RoomType.Kitchen) {
				guidedTutorial.ShowFirstDialog (1);
			} else if (currentTargetRoom == RoomType.Bedroom) {
				guidedTutorial.ShowFirstDialog (20);
			} else if (currentTargetRoom == RoomType.Bathroom) {
				guidedTutorial.ShowFirstDialog (25);
			} else if (currentTargetRoom == RoomType.Playroom) {
				guidedTutorial.ShowFirstDialog (35);
			} else if (currentTargetRoom == RoomType.Garden) {
				StartCoroutine (WaitForSecondPopup ());
			}
		} else if(currentUnlockType == UnlockType.Recipe){
			guidedTutorial.ShowFirstDialog (4);
		} else if(currentUnlockType == UnlockType.Ingredient){
			guidedTutorial.ShowFirstDialog (45);
		}
		particlePlayer.StopParticleFireworks ();

		if(!(currentUnlockType == UnlockType.Room && currentTargetRoom == RoomType.Garden)){
			StartCoroutine (WaitToClose ());
		}
	}

	IEnumerator WaitForSecondPopup(){
		CloseUI (popupObj);
		yield return new WaitForSeconds (0.2f);
		SetDisplay (UnlockType.Ingredient);
	}

	IEnumerator WaitToClose(){
		yield return new WaitForSeconds (0.16f);
		CloseUI (popupObj);
	}
}
