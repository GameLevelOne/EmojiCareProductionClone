using System.Collections;
using UnityEngine;

public class BedroomCabinet : BaseFurniture {
	#region attributes

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public override void InitVariant ()
	{
		thisSprite[0].sprite = variant[currentVariant].sprite[0];
		for(int i = 1; i < thisSprite.Length;i++){
			thisSprite[i].sprite = variant[currentVariant].sprite[1];
		}
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
