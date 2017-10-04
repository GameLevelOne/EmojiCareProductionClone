using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenLocations : BaseUI {
	public Sprite[] bgRoomSprites;
	public Image[] buttonRooms;

	public override void InitUI ()
	{
		Debug.Log("locations");

		//get current active room
	}

	public void OnClickRoom(int roomIndex){
		buttonRooms[roomIndex].sprite = bgRoomSprites[roomIndex];
		//StartCoroutine(WaitToChangeRoom());
	}

	IEnumerator WaitToChangeRoom(){
		yield return new WaitForSeconds(2);
		// GoToRoom()

		CloseUI(UIPanels[(int)PanelType.Locations]);
		yield return new WaitForSeconds(0.5f);
		CloseUI(UIPanels[(int)PanelType.Hotkey]);
	}
}
