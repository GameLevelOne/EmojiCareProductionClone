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
	public Sprite spriteLockedRoom;
	public RoomController roomController;

	int currentRoomIndex=2; //living room

	bool[] availableRooms = new bool[6]{false, false, true, false, false, false};

	public override void InitUI ()
	{
//		Debug.Log("locations");
		currentRoomIndex = (int)roomController.currentRoom;
		UpdateDisplay(currentRoomIndex);
	}

	public void OnClickRoom(int roomIndex){
		StartCoroutine(WaitToChangeRoom(roomIndex));
	}

	void UpdateDisplay (int currentRoomIndex)
	{
		availableRooms [0] = (PlayerData.Instance.LocationGarden == 1 ? true : false);
		availableRooms [1] = (PlayerData.Instance.LocationPlayroom == 1 ? true : false);
		availableRooms [2] = (PlayerData.Instance.LocationLivingroom == 1 ? true : false);
		availableRooms [3] = (PlayerData.Instance.LocationKitchen == 1 ? true : false);
		availableRooms [4] = (PlayerData.Instance.LocationBedroom == 1 ? true : false);
		availableRooms [5] = (PlayerData.Instance.LocationBathroom == 1 ? true : false);
		
		bool isCurrentRoom = false;
		for (int i = 0; i < buttonRooms.Length; i++) {
			if (availableRooms [i]) {
				buttonRooms [i].sprite = bgRoomSprites [i];
				doorOpenedSprites [i].SetActive (true);
				if (i == currentRoomIndex) {
					//buttonRooms [i].sprite = spriteClosedRoom;
					//doorOpenedSprites [i].SetActive (false);
					signInHere [i].SetActive (true);
					buttonRooms [i].GetComponent<Button> ().interactable = false;
				} else {
					//buttonRooms [i].sprite = bgRoomSprites [i];
					//doorOpenedSprites [i].SetActive (true);
					signInHere [i].SetActive (false);
					buttonRooms [i].GetComponent<Button> ().interactable = true;
				}
			} else{
				//room locked 
				buttonRooms [i].sprite = spriteLockedRoom;
				doorOpenedSprites [i].SetActive (false);
				signInHere [i].SetActive (false);
				buttonRooms [i].GetComponent<Button> ().interactable = false;
			}
		}
	}

	IEnumerator WaitToChangeRoom(int roomIndex){
		//yield return new WaitForSeconds(1);
		yield return null;
		roomController.GoToRoom((RoomType)roomIndex);
		hotkeyAnim.buttonHotkey.SetActive(true);
		CloseUI(this.gameObject);
	}
}
