using System.Collections;
using UnityEngine;

public class DanceMat : BaseFurniture {
	#region attributes

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public override void InitVariant ()
	{
		base.InitVariant ();
		//load from pref
		for(int i=0;i<base.thisSprite.Length;i++){
			Color currentColor = GetColor (PlayerPrefs.GetInt (PlayerPrefKeys.Game.DANCE_MAT_TILE_COLOR_DATA + i.ToString (), 0));
			base.thisSprite [i].color = currentColor;
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics

	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public Color GetColor(int count){
		Color currentColor;
		if (count == 1) {
			return new Color (0.98f, 0.01f, 0.42f, 1);
		} else if (count == 2) {
			return new Color (0.19f, 0.7f, 0.27f, 1);
		} else if (count == 3) {
			return new Color (0.23f, 0.56f, 0.84f, 1);
		} else if (count == 4) {
			return new Color (0.91f, 0.74f, 0.29f, 1);
		} else if (count == 5) {
			return new Color (0.4f, 0.39f, 0.85f, 1);
		} else
			return Color.white;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
