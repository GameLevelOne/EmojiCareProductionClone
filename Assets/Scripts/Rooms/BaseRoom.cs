using UnityEngine;
using System.Collections.Generic;

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
	public List<Collider2D> collidersToIgnoreWhenChangingRoom;

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
//		for(int i = 0;i<movableFurnitures.Length;i++){
//			for(int j = i;j<movableFurnitures.Length;j++){
//				if(movableFurnitures[i].gameObject.activeSelf){
//					Physics2D.IgnoreCollision(movableFurnitures[i].thisCollider, movableFurnitures[j].thisCollider);
//				}
//			}
//
//			if(movableFurnitures[i].gameObject.activeSelf && movableFurnitures[i].GetComponent<Toy>() == null) {
//				print("ignoring emoji and "+ movableFurnitures[i].name);
//				Physics2D.IgnoreCollision(movableFurnitures[i].thisCollider, PlayerData.Instance.PlayerEmoji.body.thisCollider);
//			}
//		}
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
	public virtual void OnRoomChanged(RoomType currentRoom)
	{
		if(thisRoom == currentRoom){
			//show furnitures
			foreach(MovableFurniture f in movableFurnitures) f.gameObject.SetActive(true);
			SetEmojiRoomModifier();
			PlayerData.Instance.PlayerEmoji.triggerFall.AddAndIgnoreColliders(collidersToIgnoreWhenChangingRoom);
		}else{
			//hide furnitures
			foreach(MovableFurniture f in movableFurnitures) f.gameObject.SetActive(false);
			PlayerData.Instance.PlayerEmoji.triggerFall.ResetIgnoringColliders(collidersToIgnoreWhenChangingRoom);
		}
		MovableFurnituresIgnoreEachOther();
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

			foreach(MovableFurniture f in movableFurnitures) f.flagEditMode = true;

		}else{
			//exit edit mode
			foreach(BaseFurniture f in furnitures) f.ExitEditmode();
			foreach(MovableFurniture f in movableFurnitures) f.flagEditMode = false;
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}