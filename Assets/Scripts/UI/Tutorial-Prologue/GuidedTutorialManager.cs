using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedTutorialManager : MonoBehaviour {

	[Header("Attributes")]
	public RoomController roomController;
	public SceneLoader sceneLoader;
	public Fader fader;

	[Header("Event Attributes")]
	public FloatingStatsManager floatingStats;
	public EmojiStatsExpressionController statsExpressionController;
	public UICelebrationManager celebrationManager;
	public GachaReward gachaReward;
	public HotkeysAnimation hotkeys;
	public RandomBedroomObjectController randomBedroomController;
	public Bedroom bedroom;
	public RandomBedroomObjectController randomBedroomObjectController;
	public FloatingStatsManager floatingStatsManager;
	public GuidedTutorialStork guidedTutorialStork;

	public GameObject emojiObject;

	void Start(){
		PlayerPrefs.DeleteAll ();
		PlayerData.Instance.inventory.SetIngredientValue (IngredientType.Chicken, 1);
		PlayerData.Instance.inventory.SetIngredientValue (IngredientType.Cabbage, 1);
		PlayerData.Instance.inventory.SetIngredientValue (IngredientType.Carrot, 1);
		PlayerData.Instance.inventory.SetIngredientValue (IngredientType.Tomato, 1);

		//init + register events (EMOJI)
		PlayerData.Instance.InitPlayerEmoji (emojiObject);
		PlayerData.Instance.PlayerEmoji.Init ();
		PlayerData.Instance.PlayerEmoji.InitEmojiStats();
		PlayerData.Instance.PlayerEmoji.body.previousRoom = (int)roomController.currentRoom;
		PlayerData.Instance.PlayerEmoji.body.currentRoom = (int)roomController.currentRoom;

		roomController.RegisterEmojiEvents();
		floatingStats.RegisterEmojiEvents();
		gachaReward.RegisterEmojiEvents();
		hotkeys.RegisterEmojiEvents();
		bedroom.RegisterEmojiEvents();
		randomBedroomController.RegisterEmojiEvents();

		//init other events
		statsExpressionController.Init();
		statsExpressionController.RegisterEmojiEvents();

		roomController.Init();
		PlayerData.Instance.emojiParentTransform = roomController.rooms[(int)roomController.currentRoom].transform;
		PlayerData.Instance.PlayerEmoji.transform.SetParent(PlayerData.Instance.emojiParentTransform,true);

		gachaReward.Init ();
		celebrationManager.Init();
		floatingStats.Init ();

		SetExpressionProgress ();

		//unlock kitchen
		PlayerData.Instance.LocationKitchen = 1;

		guidedTutorialStork.RegisterEvents ();
		guidedTutorialStork.ShowFirstDialog ((int)GuidedTutorialIndex.Start);
		SoundManager.Instance.PlayBGM(BGMList.BGMMain);

		PlayerData.Instance.PlayerEmoji.body.OnEmojiEatEvent += OnEmojiFirstEatEvent;
		Fader.OnFadeOutFinished += OnFadeOutFinished;
	}

	void OnEmojiFirstEatEvent (float lockDuration)
	{
		PlayerData.Instance.PlayerEmoji.body.OnEmojiEatEvent -= OnEmojiFirstEatEvent;
		PlayerData.Instance.PlayerEmoji.hunger.SetStats (PlayerData.Instance.PlayerEmoji.hunger.MaxStatValue);
	}

	void SetExpressionProgress ()
	{
		EmojiExpression emojiExpression = PlayerData.Instance.PlayerEmoji.emojiExpressions;
		for (int i = 1; i < emojiExpression.expressionDataInstances.Length; i++) {
			if ((i < emojiExpression.totalExpressionForSendOff-1)) {
				emojiExpression.expressionDataInstances [i].SetCurrentProgress (emojiExpression.expressionDataInstances [i].expressionTotalProgress);
				PlayerPrefs.SetInt (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_STATUS +
				EmojiType.Emoji.ToString () + ((EmojiExpressionState)i).ToString (), 1);
			} else if(i == (int)EmojiExpressionState.BLISS){
				emojiExpression.expressionDataInstances [i].SetCurrentProgress (emojiExpression.expressionDataInstances [i].expressionTotalProgress-1);
				PlayerPrefs.SetInt (PlayerPrefKeys.Emoji.EMOJI_EXPRESSION_STATUS +
				EmojiType.Emoji.ToString () + ((EmojiExpressionState)i).ToString (), 1);
			} 
		}
		celebrationManager.RegisterEmojiEvents();
	}

	void OnFadeOutFinished ()
	{
		Fader.OnFadeOutFinished -= OnFadeOutFinished;
		sceneLoader.gameObject.SetActive(true);
		sceneLoader.NextScene = "SceneSelection_New";
	}

	public void OnClickSkip(){
		fader.FadeOut();	
	}
}
