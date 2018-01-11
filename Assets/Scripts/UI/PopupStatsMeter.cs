using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EmojiStatsState{
	Hunger,
	Hygiene,
	Happiness,
	Stamina,
	Health
}

public class PopupStatsMeter : MonoBehaviour {
	public Image barFill;
	string boolShowNotif = "ShowNotif";

	public void ShowMeter(EmojiStatsState type,bool sleepOrBath,float currentValue,float targetValue,Sprite barSprite){
		barFill.sprite = barSprite;
		GetComponent<Animator> ().SetBool (boolShowNotif,true);
		if(targetValue > -1000){
			StartCoroutine (AnimateMeter (currentValue,targetValue));
		} else{
//			Debug.Log ("stay");
			barFill.fillAmount = currentValue;
		}	
	}

	public void HideMeter(){
		StartCoroutine (AutoClose ());
	}

	IEnumerator AnimateMeter (float currentValue, float targetValue)
	{
		yield return new WaitForSeconds (1f);
		float time = 0;
//		Debug.Log ("start:" + currentValue);
//		Debug.Log ("end:" + targetValue);
		while (time < 1) {
			barFill.fillAmount = Mathf.Lerp (currentValue, targetValue, time);
			time += Time.deltaTime * 2;
			yield return null;
		}
//		Debug.Log ("calling autoclose");
		StartCoroutine (AutoClose ());
	}

	IEnumerator AutoClose(){
//		Debug.Log ("wait autoclose");
		yield return new WaitForSeconds (0.3f);
//		Debug.Log ("autoclose");
		GetComponent<Animator> ().SetBool (boolShowNotif,false);
	}
}
