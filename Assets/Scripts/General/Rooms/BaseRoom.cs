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
	public RoomType thisRoom;
	public BaseFurniture[] furnitures;
	public float[] roomMod;
	public bool flagEditMode = false;

	Vector3[] furnitureDefaultPosition;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	protected void Awake()
	{
		InitRoom();
		
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	protected void InitRoom()
	{
		for(int i = 0;i<furnitures.Length;i++) furnitureDefaultPosition[i] = furnitures[i].transform.localPosition;
	}

	void ResetFurniturePositions()
	{
		for(int i = 0;i<furnitures.Length;i++) furnitures[i].transform.localPosition = furnitureDefaultPosition[i];
		
	}

	bool HasComponent <T>(GameObject obj) where T : Component{
		return obj.GetComponent<T>() != null;

	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void OnRoomChanged(RoomType currentRoom)
	{
		if(thisRoom == currentRoom){
			//show furnitures
			foreach(BaseFurniture f in furnitures) f.gameObject.SetActive(true);
		}else{
			//hide furnitures
			foreach(BaseFurniture f in furnitures) f.gameObject.SetActive(false);
		}
	}

	public void OnEditMode(bool editMode)
	{
		flagEditMode = editMode;
		if(editMode){
			//enter edit mode
			ResetFurniturePositions();

		}else{
			//exit edit mode
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}