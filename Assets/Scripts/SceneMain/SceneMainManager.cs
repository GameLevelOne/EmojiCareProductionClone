using System.Collections;
using UnityEngine;

public class SceneMainManager : MonoBehaviour {
	#region attributes
	public RoomController roomController;
	public EmojiExpressionController emojiExpressionController;
	public Fader fader;

	//sementara
	public GameObject[] emojiSamples;

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Start()
	{
//		PlayerPrefs.DeleteAll();
		InitMain();
	}

	void OnEmojiDoneLoading ()
	{
		fader.FadeIn();
	}

	void InitMain()
	{
		PlayerData.Instance.emojiParentTransform = roomController.transform;
		PlayerData.Instance.InitPlayerEmoji(emojiSamples[PlayerData.Instance.playerEmojiType]);
		roomController.Init();
		emojiExpressionController.Init();
		roomController.RegisterLockRoomEvent();
		if(AdmobManager.Instance) AdmobManager.Instance.ShowBanner();
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