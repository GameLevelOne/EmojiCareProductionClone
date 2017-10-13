using System.Collections;
using UnityEngine;

public class SceneMainManager : MonoBehaviour {
	#region attributes
	public RoomController roomController;
	public EmojiExpressionController emojiExpressionController;
	public Fader fader;

	//sementara
	public GameObject emojiSample;

	//temp
	public GameObject[] emojiObj = new GameObject[10];

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Start()
	{
		PlayerPrefs.DeleteAll();
		InitMain();
	}

	void OnEmojiDoneLoading ()
	{
		fader.FadeIn();
	}

	void InitMain()
	{
		PlayerData.Instance.emojiParentTransform = roomController.transform;
		PlayerData.Instance.InitPlayerEmoji(emojiObj[(int)PlayerData.Instance.SelectedEmoji]);
		roomController.Init();
		emojiExpressionController.Init();

		AdmobManager.Instance.ShowBanner();
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
