using System.Collections;
using UnityEngine;

public class BaseFurniture : MonoBehaviour {
	#region attributes
	[Header("BaseFurniture Attributes")]
	public FurnitureVariant[] variant;
	public SpriteRenderer[] thisSprite;
	public bool flagEditMode = false;
	public GameObject editButton;
	public int currentVariant = 0;

	protected string prefKeyVariant;
	#endregion
//------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public virtual void InitVariant()
	{
		variant[0].SetBought(gameObject.name,0);
		prefKeyVariant = PlayerPrefKeys.Game.FURNITURE_VARIANT+gameObject.name;
//		print(prefKeyVariant);
		currentVariant = PlayerPrefs.GetInt(prefKeyVariant,0);

		for(int i = 0;i<thisSprite.Length;i++) {
			thisSprite[i].sprite = variant[currentVariant].sprite[i];
		}
		SetEditButton (false);

	}
	#endregion
//------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public virtual void EnterEditmode(){
		flagEditMode = true;
		SetEditButton (true);
	}
	public virtual void ExitEditmode(){
		flagEditMode = false;
		SetEditButton (false);
	}
	public void OnClickFurniture(){
		Debug.Log ("clicked");
	}

	public void OnVariantBought(int variantIndex)
	{
		variant[variantIndex].SetBought(gameObject.name,variantIndex);
		PlayerPrefs.SetInt(prefKeyVariant,variantIndex);
		print (gameObject.name + " OnVariantBought " + PlayerPrefs.GetInt (prefKeyVariant));
		currentVariant = variantIndex;
		SetCurrentVariant();
	}

	public virtual void SetCurrentVariant()
	{
		for(int i = 0;i<thisSprite.Length;i++) {
			thisSprite[i].sprite = variant[currentVariant].sprite[i];
		}
	}

	//TEMP
	public void SetEditButton(bool show){
		if(editButton!=null){
			editButton.SetActive (show);
		}
	}
	#endregion
//------------------------------------------------------------------------------------------------------------------------------------------------
}
