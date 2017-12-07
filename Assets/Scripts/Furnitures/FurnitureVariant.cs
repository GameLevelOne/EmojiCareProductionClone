using UnityEngine;
using System;

[Serializable]
public class FurnitureVariant {
	public Sprite[] sprite;
	public int price;
	public bool bought;

	public bool GetBoughtData(string furnitureName, int currentVariant)
	{
		string prefKey = 
			PlayerPrefKeys.Game.FURNITURE_VARIANT+
			furnitureName+
			PlayerPrefKeys.Game.FURNITURE_VARIAN_STATUS+
			currentVariant.ToString();

		return PlayerPrefs.GetInt(prefKey,0) == 0 ? false : true;
	}

	public void SetBought(string furnitureName, int currentVariant)
	{
		string prefKey = 
			PlayerPrefKeys.Game.FURNITURE_VARIANT+
			furnitureName+
			PlayerPrefKeys.Game.FURNITURE_VARIAN_STATUS+
			currentVariant.ToString();
		
//		Debug.Log(prefKey);

		PlayerPrefs.SetInt(prefKey,1);
		bought = true;
	}
}
