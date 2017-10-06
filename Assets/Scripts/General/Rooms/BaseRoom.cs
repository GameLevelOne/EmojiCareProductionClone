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
	[Header("BaseRoom Attributes")]
	public RoomType thisRoom;
	public BaseFurniture[] furnitures;
	public MovableFurniture[] movableFurnitures;
	public float[] roomMod;
	public bool flagEditMode = false;

	Vector3[] furnitureDefaultPosition;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public void InitRoom()
	{
		if(furnitures.Length == 0) return;

		furnitureDefaultPosition = new Vector3[furnitures.Length];
		for(int i = 0;i<furnitures.Length;i++){ 
			furnitures[i].InitVariant();
			furnitureDefaultPosition[i] = furnitures[i].gameObject.transform.localPosition;
		}
			
		MovableFurnituresIgnoreEachOther();
	}

	protected void MovableFurnituresIgnoreEachOther()
	{
		for(int i = 0;i<movableFurnitures.Length;i++){
			for(int j = i;j<movableFurnitures.Length;j++){
				Physics2D.IgnoreCollision(movableFurnitures[i].thisCollider, movableFurnitures[j].thisCollider);
			}
		}
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
			for(int i = 0;i<furnitures.Length;i++){ 
				furnitures[i].transform.localPosition = furnitureDefaultPosition[i];
				furnitures[i].EnterEditmode();
			}

		}else{
			//exit edit mode
			foreach(BaseFurniture f in furnitures) f.ExitEditmode();
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}