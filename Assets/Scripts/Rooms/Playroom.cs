using System.Collections;
using UnityEngine;

public class Playroom : BaseRoom {
	[Header("Playroom attributes")]
	public Collider2D[] walls;
	public RandomPassingToyManager randomPassingToyManager;

	public GameObject miniGameDanceMat;
	public GameObject miniGameBlock;
	public GameObject miniGamePainting;

	public void Init()
	{
		randomPassingToyManager.Cycle();

//		InitMiniGames();
	}

	void InitMiniGames()
	{
		miniGameDanceMat.SetActive(PlayerData.Instance.MiniGameDanceMat == 1 ? true : false);
		miniGameBlock.SetActive(PlayerData.Instance.MiniGameBlocks == 1 ? true : false);
		miniGamePainting.SetActive(PlayerData.Instance.MiniGamePainting == 1 ? true : false);
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
