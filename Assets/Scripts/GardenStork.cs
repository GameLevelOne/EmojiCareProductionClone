using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenStork : MonoBehaviour {
	Vector3 startPos = new Vector3(-4.68f,5.07f,0);
	Vector3 endPos = new Vector3(4.42f,5.07f,0);
	float timer=0;
	float duration=5;

	public void Init(){
		InvokeRepeating ("MoveStork", 10, 30);
	}

	public void Stop(){
		CancelInvoke ();
	}

	void MoveStork(){
		StartCoroutine (AnimateStork ());
	}

	IEnumerator AnimateStork(){
		while(timer<duration){
			transform.position = Vector3.Lerp (startPos, endPos, timer/duration);
			timer += Time.deltaTime;
			yield return null;
		}
		timer = 0;
	}
}
