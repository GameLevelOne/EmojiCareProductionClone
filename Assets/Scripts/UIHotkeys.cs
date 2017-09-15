using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHotkeys : MonoBehaviour {
	public Animator panelMainHotkey;
	public Animator panelLocationHotkey;
	string triggerHotkeyOpen = "Open";
	string triggerHotkeyClose = "Close";
	string triggerPopupOpen = "PopupOpen";
	string triggerPopupClose = "PopupClose";

	public void OpenHotkey(GameObject obj){
		obj.SetActive(true);
		obj.GetComponent<Animator>().SetTrigger(triggerHotkeyOpen);
	}

	public void CloseHotkey(GameObject obj){
		obj.GetComponent<Animator>().SetTrigger(triggerHotkeyClose);
		StartCoroutine(WaitForAnim(obj));
	}

	public void OpenMenu(GameObject obj){
		obj.SetActive(true);
		obj.GetComponent<Animator>().SetTrigger(triggerPopupOpen);
	}

	public void CloseMenu(GameObject obj){
		obj.GetComponent<Animator>().SetTrigger(triggerPopupClose);
		StartCoroutine(WaitForAnim(obj));
	}

	IEnumerator WaitForAnim(GameObject obj){
		yield return new WaitForSeconds(0.51f);
		obj.SetActive(false);
	}
}
