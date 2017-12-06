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
	string triggerOpenNotif = "OpenNotif";
	string triggerCloseNotif = "CloseNotif";

	public void ShowMeter(EmojiStatsState type,bool sleepOrBath,float currentValue,float targetValue,Sprite barSprite){
		barFill.sprite = barSprite;
		GetComponent<Animator> ().SetTrigger (triggerOpenNotif);
		if(targetValue >= -1){
			StartCoroutine (AnimateMeter (currentValue,targetValue));
		} else{
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
		Debug.Log ("start:" + currentValue);
		Debug.Log ("end:" + targetValue);
		while (barFill.fillAmount < targetValue) {
			barFill.fillAmount = Mathf.Lerp (currentValue, targetValue, time);
			time += Time.deltaTime * 2;
			yield return null;
		}

		StartCoroutine (AutoClose ());
	}

	IEnumerator AutoClose(){
		yield return new WaitForSeconds (0.3f);
		GetComponent<Animator> ().SetTrigger (triggerCloseNotif);
		print("ASKJDKASDJKLASJDKLASJKD");
	}
}
