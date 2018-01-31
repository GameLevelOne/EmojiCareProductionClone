using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSendOff : BaseUI {
	public Fader fader;
	public SceneLoader sceneLoader;

	public Image emojiIcon;
	public Text expressionProgress;
	public Text textRewardCoin;
	public Text textRewardGem;

	public ScreenAlbum screenAlbum;

	void OnEnable(){
		Fader.OnFadeOutFinished += OnFadeOutFinished;
	}

	void OnDisable(){
		Fader.OnFadeOutFinished -= OnFadeOutFinished;
	}

	void OnFadeOutFinished ()
	{
		Fader.OnFadeOutFinished -= OnFadeOutFinished;
	
		sceneLoader.gameObject.SetActive(true);
		sceneLoader.NextScene = "SceneSelection_New";
	}

	public void ShowUI(Sprite sprite,string emojiName,GameObject obj){
		base.ShowUI(obj);
		this.sceneLoader = sceneLoader;

		emojiIcon.sprite = sprite;
		expressionProgress.text =  
		(PlayerData.Instance.PlayerEmoji.emojiExpressions.GetTotalExpressionProgress()*100).ToString()+"%";
		GenerateReward ();
		screenAlbum.AddEmojiRecord();
		ResetExpressionProgress ();
		CheckEmojiExpressionStatus ();
		PlayerData.Instance.PlayerSendOffCount++;

		switch(PlayerData.Instance.PlayerSendOffCount){
			case 2:
				PlayerData.Instance.Shop = 1;
				PlayerData.Instance.EditRoom = 1;
				PlayerData.Instance.GardenField2 = 1;
				PlayerData.Instance.RecipeRamen = 1;
				PlayerData.Instance.IngredientEgg = 1;
				PlayerData.Instance.IngredientMeat = 1;
				PlayerData.Instance.IngredientFlour = 1;
				break;
			case 3:
				PlayerData.Instance.MiniGameDanceMat = 1;
				PlayerData.Instance.RecipeBurger = 1;
				PlayerData.Instance.IngredientCheese = 1;
				break;
			case 4:
				PlayerData.Instance.MiniGamePainting = 1;
				PlayerData.Instance.MiniGameBlocks = 1;
				PlayerData.Instance.RecipeGrilledFish = 1;
				PlayerData.Instance.IngredientFish = 1;
				break;
		}
	}

	public void OnClickContinue(){
		fader.FadeOut();
	}

	public void OnClickShare(){
		
	}

	void GenerateReward(){
		int randCoin = Random.Range (1000, 5000);
		int randGem = Random.Range (10, 50);
		textRewardCoin.text = "x" + randCoin.ToString ();
		textRewardGem.text = "x" + randGem.ToString ();
		PlayerData.Instance.PlayerCoin += randCoin;
		PlayerData.Instance.PlayerGem += randGem;
	}

	void CheckEmojiExpressionStatus(){
		Emoji emoji = PlayerData.Instance.PlayerEmoji;
		for(int i=0;i<emoji.emojiExpressions.totalExpressionAvailable;i++){
			int temp = PlayerPrefs.GetInt (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_STATUS + emoji.emojiBaseData.emojiType.ToString (), 0);

			if(temp == 1){
				temp = 2;
			}
			PlayerPrefs.SetInt (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_STATUS + emoji.emojiBaseData.emojiType.ToString (), temp);
		}
	}

	void ResetExpressionProgress(){
		EmojiExpression expr = PlayerData.Instance.PlayerEmoji.emojiExpressions;
		for(int i=1;i<expr.totalExpressionAvailable;i++){
			expr.expressionDataInstances [i].SetCurrentProgress (0);
		}
		PlayerPrefs.DeleteKey (PlayerPrefKeys.Emoji.UNLOCKED_EXPRESSIONS + PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType.ToString ());
	}
}
