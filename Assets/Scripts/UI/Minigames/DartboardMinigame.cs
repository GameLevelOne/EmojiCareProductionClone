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
		stopPosition = dartMark.transform.localPosition;
		moveMark=false;
		if(!moveMark){
			dartAnim.SetTrigger(triggerShoot);
		}
		PlayerData.Instance.PlayerEmoji.playerInput.OnDartboardMingameDone (CalculateStatGain (stopPosition.x));
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
