using System.Collections;
using UnityEngine;

public class PlayerInventory {
	#region constructor

	#endregion

	#region Ingredient
	/// <summary>
	/// <para>positive value = add</para>
	/// <para>negative value = subtract</para>
	/// </summary>
	public void SetIngredientValue(IngredientType type, int value)
	{
		if(value < 0) return;

		string tempPrefKey = PlayerPrefKeys.Player.Inventory.INGREDIENT + type.ToString();

		PlayerPrefs.SetInt(tempPrefKey,value);
	}


	public void ModIngredientValue(IngredientType type, int value)
	{
		if(value == 0) return;

		string tempPrefKey = PlayerPrefKeys.Player.Inventory.INGREDIENT + type.ToString();

		int item = PlayerPrefs.GetInt(tempPrefKey,0);
		item += value;

		if(item < 0) item = 0;

		PlayerPrefs.SetInt(tempPrefKey,item);
	}

	public int GetIngredientValue(IngredientType type)
	{
		string tempPrefKey = PlayerPrefKeys.Player.Inventory.INGREDIENT + type.ToString();
		return PlayerPrefs.GetInt(tempPrefKey,0);
	}
	#endregion
}
