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

	public float[] statsFactor = new float[5]{0,0,0,0,0};
	public List<Furniture> movableFurnitures;
	public List<Furniture> immovableFurnitures;

	protected bool editMode = false;

	public void SwitchEditMode()
	{
		if(!editMode) editMode = true;
		else editMode = false;
		foreach(Furniture f in movableFurnitures) f.SwitchEditMode(editMode);
		foreach(Furniture f in immovableFurnitures) f.SwitchEditMode(editMode);
	}

}