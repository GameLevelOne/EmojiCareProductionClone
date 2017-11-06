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

	Animator hotkeyAnim;
	string triggerOpenHotkey = "ShowHotkeys";
	string triggerCloseHotkey = "CloseHotkeys";

	void Start(){
		hotkeyAnim = hotkeyPanel.GetComponent<Animator>();
	}

	public void ShowHotkeys(){
		buttonHotkey.SetActive(false);
		if(AdmobManager.Instance) AdmobManager.Instance.HideBanner();

		int temp = PlayerData.Instance.EmojiAlbumData.Count;
		if(temp > 1){
			buttonAlbum.color = Color.white;
		} else{
			buttonAlbum.color = Color.gray;
		}

		if(roomController.currentRoom == RoomType.Garden){
			buttonEditRoom.color = Color.gray;
			buttonEditRoom.GetComponent<Button> ().interactable = false;
		} else{
			buttonEditRoom.color = Color.white;
			buttonEditRoom.GetComponent<Button> ().interactable = true;
		}

		hotkeyPanel.SetActive(true);
		hotkeyAnim.SetTrigger(triggerOpenHotkey);
	}

	public void CloseHotkeys(){
		hotkeyAnim.SetTrigger(triggerCloseHotkey);
		StartCoroutine(WaitForAnim(hotkeyPanel));
	}

	public void BackToGame(){
		buttonHotkey.SetActive(true);
		CloseHotkeys();
	}

	IEnumerator WaitForAnim(GameObject obj){
		yield return new WaitForSeconds(0.31f);
		if(AdmobManager.Instance) AdmobManager.Instance.ShowBanner();
		obj.SetActive(false);
	}
}
