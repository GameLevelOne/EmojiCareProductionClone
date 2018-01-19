using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class SceneMainManager : MonoBehaviour {
	#region attributes
	[Header("Attributes")]
	public RoomController roomController;
	public Fader fader;

	[Header("Event Attributes")]
	public ScreenTutorial screenTutorial;
	public FloatingStatsManager floatingStats;
	public EmojiStatsExpressionController statsExpressionController;
	public UICelebrationManager celebrationManager;
	public GachaReward gachaReward;
	public HotkeysAnimation hotkeys;
	public RandomBedroomObjectController randomBedroomController;
	public Bedroom bedroom;
	public RandomBedroomObjectController randomBedroomObjectController;
	public FloatingStatsManager floatingStatsManager;

	//sementara
	public GameObject[] emojiSamples;

	//growth
	const float tresholdLow = 0.3f;
	const float tresholdMed = 0.7f;
	const float tresholdHigh = 1f;

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Start()
	{
		if(PlayerData.Instance.PlayerFirstPlay == 0){
			PlayerPrefs.DeleteAll ();
			PlayerData.Instance.PlayerFirstPlay = 1;
			PlayerData.Instance.LocationGarden = 1;
			PlayerData.Instance.LocationPlayroom = 1;
			PlayerData.Instance.LocationKitchen = 1;
			PlayerData.Instance.LocationBedroom = 1;
			PlayerData.Instance.LocationBathroom = 1;
		}

		InitMain();
	}

	void OnEmojiDoneLoading ()
	{
		fader.FadeIn();
	}

	void InitMain()
	{
		//VALIDATE TOTAL PROGRESS
		int totalExpression = 48;
		//load expression data
		EmojiType emojiType = PlayerData.Instance.PlayerEmoji != null ? PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType : (EmojiType) 0;
		//load from json	
		if(PlayerPrefs.HasKey(PlayerPrefKeys.Emoji.UNLOCKED_EXPRESSIONS+emojiType.ToString())){
			string data = PlayerPrefs.GetString(PlayerPrefKeys.Emoji.UNLOCKED_EXPRESSIONS+emojiType.ToString());
			Debug.Log (data);
			JSONNode node = JSON.Parse(data);

			float progress = ((float) node["EmojiUnlockedExpressions"].Count / (float) totalExpression);
			print("PROGRESS = "+progress);
			if(progress < 0.3f){
				//baby
				PlayerData.Instance.InitPlayerBabyEmoji(EmojiAgeType.Baby, emojiSamples[PlayerData.Instance.PlayerEmojiType], emojiSamples[PlayerData.Instance.PlayerEmojiType].GetComponent<Emoji>().emojiBaseData);
				PlayerData.Instance.PlayerEmoji.GetComponent<BabyEmoji>().sceneManager = this;
			}else if(progress >= 0.3f && progress <  0.7f){
				//juvenille
				PlayerData.Instance.InitPlayerBabyEmoji(EmojiAgeType.Juvenille, emojiSamples[PlayerData.Instance.PlayerEmojiType], emojiSamples[PlayerData.Instance.PlayerEmojiType].GetComponent<Emoji>().emojiBaseData);
				PlayerData.Instance.PlayerEmoji.GetComponent<BabyEmoji>().sceneManager = this;
			}else{
				//adult
				PlayerData.Instance.InitPlayerEmoji(emojiSamples[PlayerData.Instance.PlayerEmojiType]);
			}
		}else{
			//baby
			PlayerData.Instance.InitPlayerBabyEmoji(EmojiAgeType.Baby, emojiSamples[PlayerData.Instance.PlayerEmojiType], emojiSamples[PlayerData.Instance.PlayerEmojiType].GetComponent<Emoji>().emojiBaseData);
			PlayerData.Instance.PlayerEmoji.GetComponent<BabyEmoji>().sceneManager = this;
		}
		PlayerData.Instance.PlayerEmoji.OnEmojiInitiated += OnEmojiInitiated;
		PlayerData.Instance.PlayerEmoji.Init();
//		print("INITZSZS");
//		PlayerData.Instance.InitPlayerEmoji(emojiSamples[PlayerData.Instance.PlayerEmojiType]);

		roomController.Init();

		if(PlayerData.Instance.PlayerEmoji.EmojiSleeping){
			roomController.currentRoom = RoomType.Bedroom;
			roomController.transform.position = new Vector3(-32f,0f,0f);
			foreach(BaseRoom r in roomController.rooms) if(r != null) r.OnRoomChanged(roomController.currentRoom);

			PlayerData.Instance.PlayerEmoji.transform.parent = roomController.rooms[(int)roomController.currentRoom].transform;
			PlayerData.Instance.PlayerEmoji.transform.position = new Vector3(0,0.0025f,-2f);
			PlayerData.Instance.PlayerEmoji.emojiExpressions.SetExpression(EmojiExpressionState.SLEEP,-1);
			PlayerData.Instance.PlayerEmoji.body.DoSleep();
			bedroom.DimLight();
			randomBedroomController.StartGeneratingObjects();
		}
		PlayerData.Instance.PlayerEmoji.InitEmojiStats();
//		print("INITSTAT");

		if(PlayerData.Instance.PlayerEmoji.EmojiSleeping){
			floatingStatsManager.OnEmojiSleepEvent(true);
		}

		if(PlayerPrefs.GetInt(PlayerPrefKeys.Game.HAS_INIT_INGREDIENT,0) == 0){
			PlayerPrefs.SetInt(PlayerPrefKeys.Game.HAS_INIT_INGREDIENT,1);
			PlayerData.Instance.inventory.SetIngredientValue (IngredientType.Chicken, 2);
			PlayerData.Instance.inventory.SetIngredientValue (IngredientType.Cabbage, 2);
			PlayerData.Instance.inventory.SetIngredientValue (IngredientType.Carrot, 2);
			PlayerData.Instance.inventory.SetIngredientValue (IngredientType.Tomato, 2);
		}

		statsExpressionController.Init();
		statsExpressionController.RegisterEmojiEvents();


		PlayerData.Instance.emojiParentTransform = roomController.rooms[(int)roomController.currentRoom].transform;
		PlayerData.Instance.PlayerEmoji.transform.SetParent(PlayerData.Instance.emojiParentTransform,true);

		gachaReward.Init ();
		celebrationManager.Init();
		floatingStats.Init ();

//		if (PlayerData.Instance.TutorialFirstVisit == 0) {
//			screenTutorial.ShowUI (screenTutorial.screenTutorialObj);
//		}

		if(AdmobManager.Instance) AdmobManager.Instance.ShowBanner();

		fader.FadeIn();

		SoundManager.Instance.PlayBGM(BGMList.BGMMain);

		//OnEmojiInitiated();

		//PlayerData.Instance.PlayerEmoji.OnEmojiDestroyed += OnEmojiDestroyed;
	}

	public void EmojiGrowToAdult()
	{
		
		GameObject tempAdultObject = PlayerData.Instance.PlayerEmoji.GetComponent<BabyEmoji>().emojiAdultObject;
		PlayerData.Instance.babyEmojiObjToDestroy = PlayerData.Instance.PlayerEmoji.gameObject;

		print("destroying baby");
		OnEmojiDestroyed();
		Destroy(PlayerData.Instance.babyEmojiObjToDestroy);

		print("instantiating adult");
		PlayerData.Instance.InitPlayerEmoji(tempAdultObject);

		//init new adult
		PlayerData.Instance.PlayerEmoji.OnEmojiInitiated += OnEmojiInitiated;
		PlayerData.Instance.PlayerEmoji.Init();


		if(PlayerData.Instance.PlayerEmoji.EmojiSleeping){
			roomController.currentRoom = RoomType.Bedroom;
			roomController.transform.position = new Vector3(-32f,0f,0f);
			foreach(BaseRoom r in roomController.rooms) if(r != null) r.OnRoomChanged(roomController.currentRoom);

			PlayerData.Instance.PlayerEmoji.transform.parent = roomController.rooms[(int)roomController.currentRoom].transform;
			PlayerData.Instance.PlayerEmoji.transform.position = new Vector3(0,0.0025f,-2f);
			PlayerData.Instance.PlayerEmoji.emojiExpressions.SetExpression(EmojiExpressionState.SLEEP,-1);
			PlayerData.Instance.PlayerEmoji.body.DoSleep();
			bedroom.DimLight();
			randomBedroomController.StartGeneratingObjects();

		}else{
			PlayerData.Instance.PlayerEmoji.transform.SetParent(roomController.rooms[(int)roomController.currentRoom].transform,false);
		}
		PlayerData.Instance.PlayerEmoji.InitEmojiStats();

		if(PlayerData.Instance.PlayerEmoji.EmojiSleeping){
			floatingStatsManager.OnEmojiSleepEvent(true);
		}

		PlayerData.Instance.PlayerEmoji.transform.SetParent(PlayerData.Instance.emojiParentTransform,true);

		statsExpressionController.Init();
		statsExpressionController.RegisterEmojiEvents();
	}

	void OnEmojiInitiated()
	{
		print("ASDL:ASKDLKASLDKALSDKASDKLSAKDLSAKDLSAKDLSAKDLSAKDLASKD");
		PlayerData.Instance.PlayerEmoji.OnEmojiInitiated -= OnEmojiInitiated;

		PlayerData.Instance.PlayerEmoji.body.previousRoom = (int)roomController.currentRoom;
		PlayerData.Instance.PlayerEmoji.body.currentRoom = (int)roomController.currentRoom;

		//screenTutorial.RegisterEmojiEvents();

		celebrationManager.RegisterEmojiEvents();
		roomController.RegisterEmojiEvents();
		floatingStats.RegisterEmojiEvents();
		gachaReward.RegisterEmojiEvents();
		hotkeys.RegisterEmojiEvents();
		bedroom.RegisterEmojiEvents();
		randomBedroomController.RegisterEmojiEvents();
	}

	void OnEmojiDestroyed ()
	{
		print("EMOJI IS HILANG FFFF");
		PlayerData.Instance.PlayerEmoji.OnEmojiDestroyed -= OnEmojiDestroyed;

		screenTutorial.UnregisterEmojiEvents();
		statsExpressionController.UnregisterEmojiEvents();
		celebrationManager.UnregisterEmojiEvents();
		roomController.UnregisterEmojiEvents();
		floatingStats.UnregisterEmojiEvents();
		gachaReward.UnregisterEmojiEvents();
		hotkeys.UnregisterEmojiEvents();
		bedroom.UnregisterEmojiEvents();
		randomBedroomController.UnregisterEmojiEvents();

	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}