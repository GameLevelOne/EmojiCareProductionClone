using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeysAnimation : MonoBehaviour {
	public GameObject hotkeyPanel;

	Animator hotkeyAnim;
	string triggerOpenHotkey = "ShowHotkeys";
	string triggerCloseHotkey = "CloseHotkeys";

	void Start(){
		hotkeyAnim = hotkeyPanel.GetComponent<Animator>();
	}

	public void ShowHotkeys(){
		AdmobManager.Instance.HideBanner();
		hotkeyPanel.SetActive(true);
		hotkeyAnim.SetTrigger(triggerOpenHotkey);
	}

	public void CloseHotkeys(){
		hotkeyAnim.SetTrigger(triggerCloseHotkey);
		StartCoroutine(WaitForAnim(hotkeyPanel));
	}

	IEnumerator WaitForAnim(GameObject obj){
		yield return new WaitForSeconds(0.31f);
		obj.SetActive(false);
		AdmobManager.Instance.ShowBanner();
	}
}
