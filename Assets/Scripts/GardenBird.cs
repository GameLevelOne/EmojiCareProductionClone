using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBird : MonoBehaviour {
	Vector3 startPos = new Vector3(-0.31f,4.54f,0);
	Vector3 startScale = new Vector3(1.8f,1.8f,1.8f);
	Vector3 endPos = new Vector3(2.45f,4.54f,0);
	Vector3 endScale = new Vector3(0.1f,0.1f,0.1f);
	float timer=0;
	float duration=5;

	public void Init(){
		InvokeRepeating ("MoveBird", 0, 10);
	}

	public void Stop(){
		CancelInvoke ();
	}

	void MoveBird(){
		StartCoroutine (AnimateBird ());
	}

	IEnumerator AnimateBird(){
		while(timer<duration){
			transform.position = Vector3.Lerp (startPos, endPos, timer/duration);
			transform.localScale = Vector3.Lerp (startScale, endScale, timer/duration);
			timer += Time.deltaTime;
			yield return null;
		}
		timer = 0;
	}
}
