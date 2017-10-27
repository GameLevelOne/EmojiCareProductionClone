using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPingPong : MonoBehaviour {
	public Transform cloudLeft;
	public Transform cloudRight;

	void OnEnable () {
		StartCoroutine(StartPingpong());
	}
	
	IEnumerator StartPingpong(){
		while(true){
			cloudLeft.localPosition = new Vector3(Mathf.PingPong(Time.time*100,1440)-720,0,0);
			cloudRight.localPosition = new Vector3(-(Mathf.PingPong(Time.time*100,1400)-720),0,0);
			yield return null;
		}
	}
}
