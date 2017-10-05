using System.Collections;
using UnityEngine;

public enum RoomType{
	Garden,
	Playroom,
	LivingRoom,
	Kitchen,
	Bedroom,
	Bathroom
}

public class BaseRoom : MonoBehaviour {
	#region attributes
	public BaseFurniture[] furnitures;
	public float[] roomMod;
	public bool flagEditMode = false;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void OnEditMode(bool editMode)
	{
		flagEditMode = editMode;
		if(editMode){
			//enter edit mode
		}else{
			//exit edit mode
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}