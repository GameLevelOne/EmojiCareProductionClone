using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour {
	public delegate void FadeInFinished();
	public delegate void FadeOutFinished();
	public static event FadeInFinished OnFadeInFinished;
	public static event FadeOutFinished OnFadeOutFinished;

	Image panelFader;

	void Awake(){
		panelFader = GetComponent<Image>();
		panelFader.enabled = true;
	}

	void Start(){
		FadeIn();
	}

	public void FadeIn(){
		panelFader.gameObject.SetActive(true);
		StartCoroutine(StartFade(true));
	}

	public void FadeOut(){
		panelFader.gameObject.SetActive(true);
		StartCoroutine(StartFade(false));
	}

	IEnumerator StartFade(bool fadeIn){
		float elapsedTime = 0f;

		while(elapsedTime < 1f){
			if(fadeIn) panelFader.color = Color.Lerp(Color.black,Color.clear,elapsedTime);
			else  panelFader.color = Color.Lerp(Color.clear,Color.black,elapsedTime);
			
			elapsedTime += Time.deltaTime;

			yield return null;
		}

		if(fadeIn){
			if(OnFadeInFinished !=null){
				OnFadeInFinished();
			}
			gameObject.SetActive(false);
		} else{
			if(OnFadeOutFinished != null){
				OnFadeOutFinished();
			}
		}
	}
}