using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomName{
	LivingRoom,
	Bedroom,
	Bathroom,
	Kitchen,
	Playroom,
	Garden
}

public class Room : MonoBehaviour {
	public List<Furniture> furnitures;

	protected bool editMode = false;

	public void SwitchEditMode()
	{
		if(editMode){
			editMode = false;

		}else{
			editMode = true;

		}
	}
}
