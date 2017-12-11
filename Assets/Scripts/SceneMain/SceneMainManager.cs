using System.Collections;
using UnityEngine;

public class SceneMainManager : MonoBehaviour {
	#region attributes
	public RoomController roomController;
	public ScreenTutorial screenTutorial;
	public FloatingStatsManager floatingStats;
	public EmojiStatsExpressionController statsExpressionController;
	public GachaReward gachaReward;
	public HotkeysAnimation hotkeys;
	public Fader fader;

	[Header("Emoji Sleeping Event")]
	public RandomBedroomObjectController randomBedroomController;
	public Bedroom bedroom;
	public FloatingStatsManager floatingStatsManager;

	//sementara
	public GameObject[] emojiSamples;

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Start()
	{
//		PlayerPrefs.DeleteAll();
		PlayerData.Instance.PlayerFirstPlay = 1;
		InitMain();
	}

	void OnEmojiDoneLoading ()
	{
		fader.FadeIn();
	}

	void InitMain()
	{
		PlayerData.Instance.emojiParentTransform = roomController.rooms[(int)roomController.currentRoom].transform;
		PlayerData.Instance.InitPlayerEmoji(emojiSamples[PlayerData.Instance.PlayerEmojiType]);

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

		if(PlayerData.Instance.PlayerEmoji.EmojiSleeping){
			floatingStatsManager.OnEmojiSleepEvent(true);
		}

		statsExpressionController.Init();

		if(PlayerPrefs.GetInt(PlayerPrefKeys.Game.HAS_INIT_INGREDIENT,0) == 0){
			PlayerPrefs.SetInt(PlayerPrefKeys.Game.HAS_INIT_INGREDIENT,1);
			for(int i = 0;i<(int)IngredientType.COUNT;i++){
				PlayerData.Instance.inventory.SetIngredientValue((IngredientType)i,2);
			}
		}

		screenTutorial.Init();
		gachaReward.Init ();
		floatingStats.RegisterEvents ();
		hotkeys.RegisterOnSleepEvent ();

		if (PlayerData.Instance.TutorialFirstVisit == 0) {
			screenTutorial.ShowUI (screenTutorial.screenTutorialObj);
		}

		if(AdmobManager.Instance) AdmobManager.Instance.ShowBanner();

		fader.FadeIn();

		SoundManager.Instance.PlayBGM(BGMList.BGMMain);
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