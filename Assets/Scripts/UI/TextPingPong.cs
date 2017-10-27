using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPingPong : MonoBehaviour {
	public Text thisObj;
	void Start(){
		StartCoroutine(StartPingpong());
	}

	IEnumerator StartPingpong(){
		Color textColor = Color.black;
		while(true){
			textColor = new Color(0,0,0,Mathf.PingPong(Time.time,1));
			thisObj.color = textColor;
			yield return null;

		}
	}
}
