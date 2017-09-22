using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rooms in the game. Can be modifed for game update.
/// </summary>
public enum RoomName{
	Garden,
	Playroom,
	LivingRoom,
	Kitchen,
	Bedroom,
	Bathroom,
}

public class Room : MonoBehaviour {
	public RoomName roomName;
	public float[] statsFactor = new float[5]{0,0,0,0,0};
	public List<Furniture> movableFurnitures;
	public List<Furniture> immovableFurnitures;

	protected bool editMode = false;

	protected void Awake()
	{
		IgnoreMovableColliders();
	}

	protected void IgnoreMovableColliders()
	{
		for(int i = 0;i < movableFurnitures.Count; i++){
			for(int j = i+1;j< movableFurnitures.Count; j++){
				if( movableFurnitures[i].GetComponent<Collider2D>() != null && movableFurnitures[j].GetComponent<Collider2D>() != null){
					Physics2D.IgnoreCollision(movableFurnitures[i].GetComponent<Collider2D>(),movableFurnitures[j].GetComponent<Collider2D>());
				}
			}
		}
	}

	public void SwitchEditMode()
	{
		if(!editMode) editMode = true;
		else editMode = false;
		foreach(Furniture f in movableFurnitures) f.SwitchEditMode(editMode);
		foreach(Furniture f in immovableFurnitures) f.SwitchEditMode(editMode);
	}

	public void OnRoomChanged(RoomName currentRoom)
	{
		if(roomName == currentRoom){
			//show all furniture
			foreach(Furniture f in movableFurnitures) f.gameObject.SetActive(true);
			foreach(Furniture f in immovableFurnitures) f.gameObject.SetActive(true);
		}else{
			//hide all furniture
			foreach(Furniture f in movableFurnitures) f.gameObject.SetActive(false);
			foreach(Furniture f in immovableFurnitures) f.gameObject.SetActive(false);
		}
	}

}