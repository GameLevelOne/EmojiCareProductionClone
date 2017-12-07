using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotkeysAnimation : MonoBehaviour {
	public GameObject hotkeyPanel;
	public GameObject buttonHotkey;
	public Image buttonAlbum;
	public Image buttonEditRoom;
	public RoomController roomController;

	public Button[] hotkeyButtons;

	Animator hotkeyAnim;
	string boolOpenHotkeys = "ShowHotkeys";
	//string triggerCloseHotkey = "CloseHotkeys";

	void Start(){
		hotkeyAnim = hotkeyPanel.GetComponent<Animator>();
	}

	public void ShowHotkeys ()
	{
		buttonHotkey.SetActive(false);
		DisableHotkeyButtons();

		if (AdmobManager.Instance) AdmobManager.Instance.HideBanner ();

		int temp = PlayerData.Instance.EmojiAlbumData.Count;
		if (temp > 1) {
			buttonAlbum.color = Color.white;
		} else {
			buttonAlbum.color = Color.gray;
		}

//		if (roomController != null) {
//			if (roomController.currentRoom == RoomType.Garden) {
//				buttonEditRoom.color = Color.gray;
//				buttonEditRoom.GetComponent<Button> ().interactable = false;
//			} else {
//				buttonEditRoom.color = Color.white;
//				buttonEditRoom.GetComponent<Button> ().interactable = true;
//			}
//		}
			
		hotkeyAnim.SetBool(boolOpenHotkeys,true);
		StartCoroutine(DelayEnableHotKeys());
	}

	public void CloseHotkeys()
	{
		hotkeyAnim.SetBool(boolOpenHotkeys,false);
	}

	public void BackToGame()
	{
		hotkeyAnim.SetBool(boolOpenHotkeys,false);
		if(AdmobManager.Instance) AdmobManager.Instance.ShowBanner();
		StartCoroutine(DelayShowHotkeyButton());
	}

	IEnumerator DelayEnableHotKeys()
	{
		yield return new WaitForSeconds(8f/24f);
		EnableHotKeyButtons();
		ValidateRoomForButtonEditRoom();
	}

	IEnumerator DelayShowHotkeyButton()
	{
		yield return new WaitForSeconds(8f/24f);
		buttonHotkey.SetActive(true);
	}

	void DisableHotkeyButtons()
	{
		foreach(Button b in hotkeyButtons) b.interactable = false;
	}
	void EnableHotKeyButtons()
	{
		
		foreach(Button b in hotkeyButtons) b.interactable = true;
	}

	void ValidateRoomForButtonEditRoom()
	{
		
		if(roomController.currentRoom == RoomType.Garden){
			buttonEditRoom.GetComponent<Button>().interactable = false;
			buttonEditRoom.color = Color.gray;
		}else{
			buttonEditRoom.GetComponent<Button>().interactable = true;
			buttonEditRoom.color = Color.white;
		}
		print("BUTTON EDIT MODE DI ROOM "+roomController.currentRoom+" is"+buttonEditRoom.GetComponent<Button>().interactable);
	}
}
