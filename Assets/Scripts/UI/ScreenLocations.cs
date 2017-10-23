using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenLocations : BaseUI {
	public Sprite[] bgRoomSprites;
	public GameObject[] doorOpenedSprites;
	public GameObject[] signInHere;
	public Image[] buttonRooms;
	public Sprite spriteClosedRoom;
	public RoomController roomController;

	int currentRoomIndex=2; //living room

	public override void InitUI ()
	{
		Debug.Log("locations");
		currentRoomIndex = (int)roomController.currentRoom;
		UpdateDisplay(currentRoomIndex);
	}

	public void OnClickRoom(int roomIndex){
		StartCoroutine(WaitToChangeRoom(roomIndex));
	}

	void UpdateDisplay(int currentRoomIndex){
//		buttonRooms[this.currentRoomIndex].sprite = spriteClosedRoom;
//		doorOpenedSprites[this.currentRoomIndex].SetActive(false);
//		this.currentRoomIndex = currentRoomIndex;
//		buttonRooms[currentRoomIndex].sprite = bgRoomSprites[currentRoomIndex];
//		doorOpenedSprites[currentRoomIndex].SetActive(true);
		
		bool isCurrentRoom = false;
		for(int i=0;i<buttonRooms.Length;i++){
			if(i == currentRoomIndex){
				buttonRooms[i].sprite = spriteClosedRoom;
				doorOpenedSprites[i].SetActive(false);
				signInHere[i].SetActive(true);
				buttonRooms[i].GetComponent<Button>().interactable=false;
			}else{
				buttonRooms[i].sprite = bgRoomSprites[i];
				doorOpenedSprites[i].SetActive(true);
				signInHere[i].SetActive(false);
				buttonRooms[i].GetComponent<Button>().interactable=true;
			}
		}
	}

	IEnumerator WaitToChangeRoom(int roomIndex){
		//yield return new WaitForSeconds(1);
		yield return null;
		roomController.GoToRoom((RoomType)roomIndex);
		CloseUI(this.gameObject);
	}
}
