using System.Collections;
using UnityEngine;

public class SceneMainManager : MonoBehaviour {
	#region attributes
	public RoomController roomController;
	public ScreenTutorial screenTutorial;
	public EmojiStatsExpressionController statsExpressionController;
	public Fader fader;

	//sementara
	public GameObject[] emojiSamples;

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Start()
	{
		PlayerData.Instance.PlayerFirstPlay = 1;
//		PlayerPrefs.DeleteAll();



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
		statsExpressionController.Init();
		roomController.Init();
		roomController.RegisterLockRoomEvent();
		if(AdmobManager.Instance) AdmobManager.Instance.ShowBanner();

		for(int i = 0;i<(int)IngredientType.COUNT;i++){
			PlayerData.Instance.inventory.SetIngredientValue((IngredientType)i,99);
		}

		if (PlayerData.Instance.TutorialFirstVisit == 0) {
			PlayerData.Instance.TutorialFirstVisit = 1;
			screenTutorial.ShowUI (screenTutorial.screenTutorialObj);
		}
		fader.FadeIn();
	}

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void OnToggleDebug(bool debug)
	{
		print("debug = "+debug);
		PlayerData.Instance.PlayerEmoji.SwitchDebugMode(debug);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}