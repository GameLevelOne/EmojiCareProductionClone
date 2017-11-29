﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartboardMinigame : BaseUI {
	public GameObject buttonBack;
	public GameObject dartMark;
	bool moveMark = false;
	Animator dartAnim;
	string boolShoot = "Shoot";
	string boolReset = "Reset";
	Vector3 stopPosition;

	void OnEnable(){
		dartAnim = dartMark.GetComponent<Animator>();
		moveMark=true;
		buttonBack.SetActive (true);
		StartCoroutine(MoveArrow());
	}

	public void OnClickStop(){
		buttonBack.SetActive (false);
		StopCoroutine ("MoveArrow");
		stopPosition = Vector3.zero;
		stopPosition = dartMark.transform.localPosition;
		moveMark=false;
		if(!moveMark){
			dartAnim.SetBool (boolShoot, true);
			dartAnim.SetBool (boolReset, false);
		}
		StartCoroutine (ResetDart ());
	}

	int CalculateStatGain(float xPos){
		float absPos = Mathf.Abs (xPos);
		if (absPos >= 0 && absPos < 50) {
			return 4;
		} else if (absPos >= 50 && absPos < 100) {
			return 3;
		} else if (absPos >= 100 && absPos < 150) {
			return 2;
		} else
			return 1;
	}

	public void OnClickBack(){
		PlayerData.Instance.PlayerEmoji.playerInput.OnDartboardMingameDone (CalculateStatGain (stopPosition.x));
		dartAnim.SetBool (boolShoot, false);
		dartAnim.SetBool (boolReset, true);
		moveMark = false;
		base.CloseUI(this.gameObject);
	}

	IEnumerator MoveArrow(){
		while (moveMark) {
			dartMark.transform.localPosition = new Vector3 ((Mathf.PingPong (Time.time*500, 400) - 200), -500, 0);
			yield return null;
		}
	}

	IEnumerator ResetDart(){
		yield return new WaitForSeconds (3f);
		dartAnim.SetBool (boolReset, true);
		dartAnim.SetBool (boolShoot, false);
		moveMark = true;
		buttonBack.SetActive (true);
		StartCoroutine (MoveArrow ());
	}
}
