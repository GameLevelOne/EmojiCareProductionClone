using System.Collections;
using UnityEngine;

public class SceneMainManager : MonoBehaviour {
	#region attributes
	public RoomController roomController;
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