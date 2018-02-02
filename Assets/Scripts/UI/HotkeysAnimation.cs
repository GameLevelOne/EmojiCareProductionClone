using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotkeysAnimation : MonoBehaviour {
	public GameObject hotkeyPanel;
	public GameObject buttonHotkey;
	public Image buttonAlbum;
	public Image buttonShop;
	public Image buttonEditRoom;
	public RoomController roomController;

	public Button[] hotkeyButtons;

	Animator hotkeyAnim;
	string boolOpenHotkeys = "ShowHotkeys";
	//string triggerCloseHotkey = "CloseHotkeys";

	void Start(){
		hotkeyAnim = hotkeyPanel.GetComponent<Animator>();
	}

	public void RegisterEmojiEvents()
	{
		PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent += OnEmojiSleepEvent;
	}

	public void UnregisterEmojiEvents()
	{
		PlayerData.Instance.PlayerEmoji.body.OnEmojiSleepEvent -= OnEmojiSleepEvent;
	}

	void OnDestroy(){
		UnregisterEmojiEvents();
	}

	void OnEmojiSleepEvent (bool sleeping)
	{
		buttonHotkey.GetComponent<Button> ().interactable = !sleeping;
	}

	public void ShowHotkeys ()
	{
		buttonHotkey.SetActive(false);
		DisableHotkeyButtons();

		if (SoundManager.Instance) SoundManager.Instance.PlaySFXOneShot (SFXList.OpenThings);
		if (AdmobManager.Instance) AdmobManager.Instance.HideBanner ();

		int temp = PlayerData.Instance.EmojiRecordCount;
		if (temp >= 1) {
			buttonAlbum.color = Color.white;
		} else {
			buttonAlbum.color = Color.gray;
		}

		temp = PlayerData.Instance.Shop;
		if(temp == 1){
			buttonShop.color = Color.white;
			buttonShop.GetComponent<Button> ().interactable = true;
		} else{
			buttonShop.color = Color.grey;
			buttonShop.GetComponent<Button> ().interactable = false;
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
		if(PlayerData.Instance.EditRoom == 0){
			buttonEditRoom.GetComponent<Button>().interactable = false;
			buttonEditRoom.color = Color.gray;
		} else{
			if(roomController.currentRoom == RoomType.Garden){
				buttonEditRoom.GetComponent<Button>().interactable = false;
				buttonEditRoom.color = Color.gray;
			}else{
				buttonEditRoom.GetComponent<Button>().interactable = true;
				buttonEditRoom.color = Color.white;
			}
		}

		print("BUTTON EDIT MODE DI ROOM "+roomController.currentRoom+" is"+buttonEditRoom.GetComponent<Button>().interactable);
	}
}
