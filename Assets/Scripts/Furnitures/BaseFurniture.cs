using System.Collections;
using UnityEngine;

public class BaseFurniture : MonoBehaviour {
	#region attributes
	[Header("BaseFurniture Attributes")]
	public FurnitureVariant[] variant;
	public SpriteRenderer[] thisSprite;
	public bool flagEditMode = false;

	public int currentVariant = 0;
	#endregion
//------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public virtual void InitVariant()
	{
//		print(gameObject.name);
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

	//TEMP
	public void SetEditButton(bool show){
		GameObject obj = transform.GetChild (0).gameObject;
		if(show){
			obj.SetActive (true);
		} else{
			obj.SetActive (false);
		}
	}
	#endregion
//------------------------------------------------------------------------------------------------------------------------------------------------
}
