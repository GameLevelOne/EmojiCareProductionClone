using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenLocations : BaseUI {
	public Sprite[] bgRoomSprites;
	public GameObject[] doorOpenedSprites;
	public Image[] buttonRooms;
	public Sprite spriteClosedRoom;
	public RoomController roomController;

	int currentRoomIndex=2; //living room

	public override void InitUI ()
	{
		Debug.Log("locations");
	}

	public void OnClickRoom(int roomIndex){
		UpdateDisplay(roomIndex);
		StartCoroutine(WaitToChangeRoom(roomIndex));
	}

	void UpdateDisplay(int currentRoomIndex){
		buttonRooms[this.currentRoomIndex].sprite = spriteClosedRoom;
		doorOpenedSprites[this.currentRoomIndex].SetActive(false);
		this.currentRoomIndex = currentRoomIndex;
		buttonRooms[currentRoomIndex].sprite = bgRoomSprites[currentRoomIndex];
		doorOpenedSprites[currentRoomIndex].SetActive(true);
	}

	IEnumerator WaitToChangeRoom(int roomIndex){
		yield return new WaitForSeconds(1);
		roomController.GoToRoom((RoomType)roomIndex);
		CloseUI(this.gameObject);
	}
}
