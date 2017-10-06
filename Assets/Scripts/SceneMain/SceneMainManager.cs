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
		InitMain();
	}

	void OnEmojiDoneLoading ()
	{
		fader.FadeIn();
	}

	void InitMain()
	{
		PlayerData.Instance.emojiParentTransform = roomController.transform;
		PlayerData.Instance.InitPlayerEmoji(emojiSample);
		roomController.Init();
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
