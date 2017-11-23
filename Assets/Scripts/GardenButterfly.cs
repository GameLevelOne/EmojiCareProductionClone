using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenButterfly : MonoBehaviour {

	public Vector3[] stayPositions;

	Vector3 startPos;
	Vector3 endPos;

	float flyingSpeed = 10;
	float timer=0;
	float flyDuration=1;
	float breakTime = 180; //in seconds
	int startPosIdx=-1;
	int endPosIdx=0;

	void Start(){
		InvokeRepeating ("ShowButterfly", 0, breakTime);
	}

	void ShowButterfly ()
	{
		if (startPosIdx == -1) {
			startPosIdx = Random.Range (0, stayPositions.Length);
		} 
		endPosIdx = Random.Range (0, stayPositions.Length);
		transform.position = stayPositions [startPosIdx];
		StartCoroutine (AnimateButterfly(stayPositions[startPosIdx],stayPositions[endPosIdx]));
	}

	IEnumerator AnimateButterfly(Vector3 pos1,Vector3 pos2){
		while(timer<flyDuration){
			transform.position = Vector3.Lerp (pos1, pos2, timer);
			timer += Time.deltaTime;
			yield return null;
		}
		timer = 0;
		startPosIdx = endPosIdx;
	}
}
