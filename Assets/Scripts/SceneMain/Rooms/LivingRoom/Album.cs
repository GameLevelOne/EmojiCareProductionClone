using System.Collections;
using UnityEngine;

public class Album : ActionableFurniture {
	#region attributes
	[Header("Album Attributes")]
	public ScreenAlbum UIAlbum;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public override void PointerClick ()
	{
		//open album ui
		if(!flagEditMode) UIAlbum.ShowUI(UIAlbum.gameObject);
	} 
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
