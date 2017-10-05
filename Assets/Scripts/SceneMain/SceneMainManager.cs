using System.Collections;
using UnityEngine;

public class SceneMainManager : MonoBehaviour {
	#region attributes
	public RoomController roomController;
	public Fader fader;

	//sementara
	public GameObject emojiSample;

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Start()
	{
//		Emoji.Instance.OnEmojiDoneLoading += OnEmojiDoneLoading;
		PlayerData.Instance.emojiParentTransform = roomController.transform;
		InitMain();
	}

	void OnEmojiDoneLoading ()
	{
		fader.FadeIn();
	}

	void InitMain()
	{
//		GameSparkManager.Instance.GetDownloadableURL((EmojiType)PlayerData.Instance.playerEmojiType);
		PlayerData.Instance.InitPlayerEmoji(emojiSample);
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
