using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartboardMinigame : BaseUI {
	public GameObject dartMark;
	bool moveMark = false;
	Animator dartAnim;
	string triggerShoot = "Shoot";
	string triggerReset = "Reset";

	void OnEnable(){
		dartAnim = dartMark.GetComponent<Animator>();
		moveMark=true;
		StartCoroutine(MoveArrow());
	}

	public void OnClickStop(){
		dartMark.SetActive(true);
		Vector3 stopPosition = Vector3.zero;
		moveMark=false;
		if(!moveMark){
			dartAnim.SetTrigger(triggerShoot);
		}
		//StartCoroutine(ClosePanel());
	}

	public void OnClickBack(){
		dartAnim.SetTrigger(triggerReset);
		base.CloseUI(this.gameObject);
	}

	IEnumerator MoveArrow(){
		while (moveMark) {
			dartMark.transform.localPosition = new Vector3 ((Mathf.PingPong (Time.time*500, 400) - 200), -500, 0);
			yield return null;
		}
	}
}
