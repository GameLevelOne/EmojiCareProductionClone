using System.Collections;
using UnityEngine;

public class BathroomGordyn : ActionableFurniture {
	#region attributes
	[Header("BathroomGordyn attributes")]
	public GameObject buttonOpenGordyn;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
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
	public void OpenGordyn()
	{
		if(!flagEditMode){
			thisSprite[currentVariant].sprite = variant[currentVariant].sprite[0];

			buttonOpenGordyn.SetActive(false);
		}
	}

	public void CloseGordyn()
	{
		if(!flagEditMode){
			thisSprite[currentVariant].sprite = variant[currentVariant].sprite[1];

			buttonOpenGordyn.SetActive(true);
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
