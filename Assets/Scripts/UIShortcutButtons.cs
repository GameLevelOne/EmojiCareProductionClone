using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShortcutButtons : MonoBehaviour {
	public GameObject PanelUIShortcut;
	public Animator buttonAnim;
	string triggerOpen = "Open";
	string triggerClose = "Close";

	public void OpenMenu(){
		PanelUIShortcut.SetActive(true);
		buttonAnim.SetTrigger(triggerOpen);
	}

	public void CloseMenu(){
		buttonAnim.SetTrigger(triggerClose);
		StartCoroutine(WaitForAnim());
	}

	IEnumerator WaitForAnim(){
		yield return new WaitForSeconds(1);
		PanelUIShortcut.SetActive(false);
	}
}
