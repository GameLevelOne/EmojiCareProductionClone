using System.Collections;
using UnityEngine;

public class BathroomGordyn : ActionableFurniture {
	#region attributes
	[Header("BathroomGordyn attributes")]
	public GameObject buttonOpenGordyn;
	public Sprite spriteOpen;
	public Sprite spriteClose;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public override void InitVariant ()
	{
		base.InitVariant ();
		spriteOpen = variant[currentVariant].sprite[0];
		spriteClose = variant[currentVariant].sprite[1];
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public override void PointerClick ()
	{
		//nothin
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public override void SetCurrentVariant ()
	{
		base.SetCurrentVariant ();
		spriteOpen = variant[currentVariant].sprite[0];
		spriteClose = variant[currentVariant].sprite[1];
	}

	public void OpenGordyn()
	{
		if(!flagEditMode){
			thisSprite[0].sprite = spriteOpen;

			buttonOpenGordyn.SetActive(false);
		}
	}

	public void CloseGordyn()
	{
		if(!flagEditMode){
			thisSprite[0].sprite = spriteClose;

			buttonOpenGordyn.SetActive(true);
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
