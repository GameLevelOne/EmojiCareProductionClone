using System.Collections;
using UnityEngine;

public class Playroom : BaseRoom {
	[Header("Playroom attributes")]
	public Collider2D[] walls;
	public RandomPassingToyManager randomPassingToyManager;
	public void Init()
	{
		randomPassingToyManager.Cycle();
	}

	public override void OnRoomChanged (RoomType currentRoom)
	{
		base.OnRoomChanged (currentRoom);
		if(thisRoom == currentRoom){
			foreach(Collider2D c in walls) c.enabled = true;
		}else{
			foreach(Collider2D c in walls) c.enabled = false;
		}
	}
}
