using System.Collections;
using UnityEngine;

public class BedroomCabinet : BaseFurniture {
	#region attributes

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public override void InitVariant ()
	{
		variant[0].SetBought(gameObject.name,0);
		prefKeyVariant = PlayerPrefKeys.Game.FURNITURE_VARIANT+gameObject.name;
		print(prefKeyVariant);
		currentVariant = PlayerPrefs.GetInt(prefKeyVariant,0);

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
	public override void SetCurrentVariant ()
	{
		thisSprite[0].sprite = variant[currentVariant].sprite[0];
		for(int i = 1; i < thisSprite.Length;i++){
			thisSprite[i].sprite = variant[currentVariant].sprite[1];
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
