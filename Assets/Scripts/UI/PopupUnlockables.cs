using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UnlockableList{
	RecipeRamen,
	RecipeBurger,
	RecipeGrilledFish,
	MinigameDrawingBoard,
	MinigameDanceMat,
	MinigamePlayBlock,
	EmojiSet1,
	EmojiSet2,
	EmojiSet3,
	EmojiSet4,
	GardenField2,
	GardenField3
}

public class PopupUnlockables : BaseUI {
	public ParticlePlayer particlePlayer;
	public GameObject popupObj;
	public GameObject[] unlockableObj;
	public Text unlockText;

	public void SetDisplay(UnlockableList type){
		for(int i=0;i<unlockableObj.Length;i++){
			if(i == (int)type){
				unlockableObj [i].SetActive (true);
			} else{
				unlockableObj [i].SetActive (false);
			}
		}
		switch(type){
		case UnlockableList.RecipeRamen:
			unlockText.text = "You have unlocked: \n RECIPE: RAMEN \n INGREDIENT: EGG, FLOUR";
			PlayerData.Instance.RecipeRamen = 1;
			PlayerData.Instance.IngredientEgg = 1;
			PlayerData.Instance.IngredientFlour = 1;
			break;
		case UnlockableList.RecipeBurger:
			unlockText.text = "You have unlocked: \n RECIPE: BURGER \n INGREDIENT: CHEESE, MEAT";
			PlayerData.Instance.RecipeBurger = 1;
			PlayerData.Instance.IngredientCheese = 1;
			PlayerData.Instance.IngredientMeat = 1;
			break;
		case UnlockableList.RecipeGrilledFish:
			unlockText.text = "You have unlocked: \n RECIPE: GRILLED FISH \n INGREDIENT: FISH";
			PlayerData.Instance.RecipeGrilledFish = 1;
			PlayerData.Instance.IngredientFish = 1;
			break;
		case UnlockableList.MinigameDrawingBoard:
			unlockText.text = "You have unlocked: \n MINIGAME: DRAWING BOARD";
			PlayerData.Instance.MiniGamePainting = 1;
			break;
		case UnlockableList.MinigameDanceMat:
			unlockText.text = "You have unlocked: \n MINIGAME: DANCE MAT";
			PlayerData.Instance.MiniGameDanceMat = 1;
			break;
		case UnlockableList.MinigamePlayBlock:
			unlockText.text = "You have unlocked: \n MINIGAME: PLAY BLOCK";
			PlayerData.Instance.MiniGameBlocks = 1;
			break;
		case UnlockableList.EmojiSet1:
			unlockText.text = "You have unlocked: \n EMOJI BLUE \n EMOJI DOGE";
			break;
		case UnlockableList.EmojiSet2:
			unlockText.text = "You have unlocked: \n EMOJI SLOTH \n EMOJI TOMATO";
			break;
		case UnlockableList.EmojiSet3:
			unlockText.text = "You have unlocked: \n EMOJI CLOWN \n EMOJI LIME";
			break;
		case UnlockableList.EmojiSet4:
			unlockText.text = "You have unlocked: \n EMOJI PIRATE \n EMOJI SANTA";
			break;
		case UnlockableList.GardenField2:
			unlockText.text = "You have unlocked: \n SECOND GARDEN FIELD";
			PlayerData.Instance.GardenField1 = 1;
			break;
		case UnlockableList.GardenField3:
			unlockText.text = "You have unlocked: \n THIRD GARDEN FIELD";
			PlayerData.Instance.GardenField1 = 2;
			break;
		}

		particlePlayer.ShowParticleFireworks ();
		if (SoundManager.Instance)
			SoundManager.Instance.PlaySFXOneShot (SFXList.Achievement);
		ShowUI (popupObj);
	}

	public void OnClickContinue(){
		particlePlayer.StopParticleFireworks ();
		StartCoroutine (WaitToClose ());
	}

	public void WaitForGrowthPopup(){
		StartCoroutine (WaitForPopup ());
	}

	IEnumerator WaitToClose(){
		yield return new WaitForSeconds (0.16f);
		CloseUI (popupObj);
	}

	IEnumerator WaitForPopup(){
		yield return new WaitForSeconds (0.2f);
		SetDisplay (UnlockableList.GardenField2);
	}
}
