using System.Collections;
using UnityEngine;

public class SceneMainManager : MonoBehaviour {
	#region attributes
	public RoomController roomController;
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	void Start()
	{
		InitMain();
	}

	void InitMain()
	{
//		roomController.GoToRoom(RoomName.LivingRoom);
//		GameSparkManager.Instance.GetDownloadableURL(Emoji.Instance.emojiType);
		StartCoroutine(a());
	}

	IEnumerator a()
	{
		yield return new WaitForSeconds(1f);
		GameSparkManager.Instance.DeviceAuth();
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
