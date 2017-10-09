using System.Collections;
using UnityEngine;

public class BaseFurniture : MonoBehaviour {
	#region attributes
	[Header("BaseFurniture Attributes")]
	public FurnitureVariant[] variant;
	public SpriteRenderer[] thisSprite;
	public bool flagEditMode = false;

	protected int currentVariant = 0;
	#endregion
//------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public virtual void InitVariant()
	{
		for(int i = 0;i<thisSprite.Length;i++) thisSprite[i].sprite = variant[currentVariant].sprite[i];
	}
	#endregion
//------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public virtual void EnterEditmode(){
		flagEditMode = true;

	}
	public virtual void ExitEditmode(){
		flagEditMode = false;

	}
	#endregion
//------------------------------------------------------------------------------------------------------------------------------------------------
}
