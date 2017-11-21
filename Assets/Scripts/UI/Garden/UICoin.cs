using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICoin : MonoBehaviour {
	string triggerOpenNotif = "OpenNotif";
	string triggerCloseNotif = "CloseNotif";

	void ShowUI(){
		GetComponent<Animator> ().SetTrigger (triggerOpenNotif);
	}

	IEnumerator AutoCloseUI(){
		GetComponent<Animator> ().SetTrigger (triggerCloseNotif);
		yield return new WaitForSeconds(0.16f);
	}
}
