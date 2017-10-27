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
	public float[] roomMod;
	public BaseFurniture[] furnitures;
	public MovableFurniture[] movableFurnitures;
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
			print("ignoring emoji and "+ movableFurnitures[i].name);
			Physics2D.IgnoreCollision(movableFurnitures[i].thisCollider, PlayerData.Instance.PlayerEmoji.body.thisCollider);
		}
	}

	void SetEmojiRoomModifier()
	{
//		PlayerData.Instance.PlayerEmoji.hunger.RoomModifier = roomMod[0];
//		PlayerData.Instance.PlayerEmoji.hygiene.RoomModifier = roomMod[1];
//		PlayerData.Instance.PlayerEmoji.happiness.RoomModifier = roomMod[2];
//		PlayerData.Instance.PlayerEmoji.stamina.RoomModifier = roomMod[3];
//		PlayerData.Instance.PlayerEmoji.health.RoomModifier = roomMod[4];
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void OnRoomChanged(RoomType currentRoom)
	{
		if(thisRoom == currentRoom){
			//show furnitures
			foreach(BaseFurniture f in furnitures) f.gameObject.SetActive(true);
			SetEmojiRoomModifier();
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