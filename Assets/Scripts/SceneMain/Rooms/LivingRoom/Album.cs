using System.Collections;
using UnityEngine;

public class Album : ActionableFurniture {
	#region attributes
	[Header("Album Attributes")]
	public ScreenAlbum UIAlbum;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public override void InitVariant ()
	{
		base.InitVariant ();


		if(PlayerData.Instance.EmojiAlbumData.Count <= 0) gameObject.SetActive(false);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public override void PointerClick ()
	{
		//open album ui
		UIAlbum.ShowUI(UIAlbum.gameObject);
	} 
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
