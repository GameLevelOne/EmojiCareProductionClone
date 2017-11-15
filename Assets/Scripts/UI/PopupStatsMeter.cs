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
	public Transform parentObj;

	Image barFill;
	string triggerOpenNotif = "OpenNotif";
	string triggerCloseNotif = "CloseNotif";

	public void ShowUI(EmojiStatsState type,float currentValue,float targetValue,float maxValue){
		GetComponent<Animator> ().SetTrigger (triggerOpenNotif);
		StartCoroutine (AnimateMeter (currentValue,targetValue,maxValue));
	}

	IEnumerator AnimateMeter(float currentValue,float targetValue,float maxValue){
		yield return new WaitForSeconds (1f);
		float time = 0;
		float startValue = currentValue / maxValue;
		float endValue = targetValue / maxValue;
		while(barFill.fillAmount < endValue){
			barFill.fillAmount = Mathf.Lerp (startValue, endValue, time);
			time += Time.deltaTime*2;
			yield return null;
		}
		StartCoroutine (AutoClose ());
	}

	IEnumerator AutoClose(){
		yield return new WaitForSeconds (0.16f);
		GetComponent<Animator> ().SetTrigger (triggerCloseNotif);
		yield return new WaitForSeconds (0.16f);
		gameObject.SetActive (false);
	}
}
