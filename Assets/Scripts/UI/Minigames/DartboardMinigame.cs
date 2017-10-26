using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartboardMinigame : BaseUI {
	public Transform arrow;
	public GameObject dartMark;
	bool moveArrow = false;

	void OnEnable(){
		dartMark.SetActive(false);
		moveArrow=true;
		StartCoroutine(MoveArrow());
	}

	public void OnClickStop(){
		dartMark.SetActive(true);
		Vector3 stopPosition = Vector3.zero;
		moveArrow=false;
		if(!moveArrow){
			stopPosition = arrow.localPosition;
			dartMark.transform.localPosition = new Vector3(stopPosition.x,-972,0);
		}
		//StartCoroutine(ClosePanel());
	}

	IEnumerator MoveArrow(){
		while (moveArrow) {
			arrow.localPosition = new Vector3 ((Mathf.PingPong (Time.time*500, 400) - 200), -500, 0);
			yield return null;
		}
	}

	IEnumerator ClosePanel(){
		yield return new WaitForSeconds(1);
		this.gameObject.SetActive(false);
	}
}
