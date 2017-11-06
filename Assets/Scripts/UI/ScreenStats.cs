using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenStats : BaseUI {
	public Image barFillHunger;
	public Image barFillHygiene;
	public Image barFillHappiness;
	public Image barFillStamina;
	public Image barFillHealth;

	public Sprite barGreen;
	public Sprite barYellow;
	public Sprite barOrange;
	public Sprite barRed;
	public Sprite arrowPos;
	public Sprite arrowNeg;

	public GameObject arrowObj;

	public Transform parentArrowHunger;
	public Transform parentArrowHygiene;
	public Transform parentArrowHappiness;
	public Transform parentArrowStamina;
	public Transform parentArrowHealth;

	public GameObject[] arrowHunger;
	public GameObject[] arrowHygiene;
	public GameObject[] arrowHappiness;
	public GameObject[] arrowStamina;
	public GameObject[] arrowHealth;

	bool moveArrows = true;

	//FOR TESTING
//	public ScreenTutorial tut;

	public override void InitUI(){
		Debug.Log("stats");
	}

	void Update(){
		UpdateUI();
	}

	void UpdateUI(){
		UpdateStatsValue();
		UpdateArrowsDisplay();
	}

	void UpdateStatsValue ()
	{
		Emoji currentData = PlayerData.Instance.PlayerEmoji;
		float ratioHunger = currentData.hunger.StatValue / currentData.hunger.MaxStatValue;
		float ratioHygiene = currentData.hygiene.StatValue / currentData.hygiene.MaxStatValue;
		float ratioHappiness = currentData.happiness.StatValue / currentData.happiness.MaxStatValue;
		float ratioStamina = currentData.stamina.StatValue / currentData.stamina.MaxStatValue;
		float ratioHealth = currentData.health.StatValue / currentData.health.MaxStatValue;

//		float ratioHunger = 0.1f;
//		float ratioHygiene = 0.1f;
//		float ratioHappiness = 0.1f;
//		float ratioStamina = 0.1f;
//		float ratioHealth = 0.1f;

		barFillHunger.fillAmount = ratioHunger;
		barFillHygiene.fillAmount = ratioHygiene;
		barFillHappiness.fillAmount = ratioHappiness;
		barFillStamina.fillAmount = ratioStamina;
		barFillHealth.fillAmount = ratioHealth;

		CheckBarSprite(ratioHunger,barFillHunger);
		CheckBarSprite(ratioHygiene,barFillHygiene);
		CheckBarSprite(ratioHappiness,barFillHappiness);
		CheckBarSprite(ratioStamina,barFillStamina);
		CheckBarSprite(ratioHealth,barFillHealth);
	}

	void UpdateArrowsDisplay(){
		Emoji currentData = PlayerData.Instance.PlayerEmoji;
		CheckArrows(arrowHunger,currentData.hunger.totalModifier);
		CheckArrows(arrowHygiene,currentData.hygiene.totalModifier);
		CheckArrows(arrowHappiness,currentData.happiness.totalModifier);
		CheckArrows(arrowStamina,currentData.stamina.totalModifier);
		CheckArrows(arrowHealth,currentData.health.totalModifier);
	}

	void CheckBarSprite(float value,Image barFill){
		if(value>=0.9f){
			barFill.sprite = barGreen;
		} else if(value>=0.4f && value<0.9f){
			barFill.sprite = barYellow;
		} else if(value>=0.2f && value<0.4f){
			barFill.sprite = barOrange;
		} else{
			barFill.sprite = barRed;
		}
	}

	void CheckArrows (GameObject[] arrowObj, float totalRoomMod = 1f)
	{
//		Debug.Log ("totalMod:" + totalRoomMod);
		bool isPositive = false;
		bool noMod = false;
		int arrowCount = 0;

		if (totalRoomMod > 0) {
			isPositive = true;
		} else if (totalRoomMod < 0) {
			isPositive = false;
		} else {
			noMod = true;
		}

		float absRoomMod = Mathf.Abs (totalRoomMod);

		if (absRoomMod >= 0.0001f && absRoomMod < 0.001f) {
			arrowCount = 1;
		} else if (absRoomMod >= 0.001f && absRoomMod < 0.01f) {
			arrowCount = 2;
		} else {
			arrowCount = 3;
		}


		for (int i = 0; i < 3; i++) {
			if (!noMod) {
				if (i < arrowCount) {
					GameObject obj = arrowObj [i];
					obj.SetActive (true); 
					if (isPositive) {
						obj.GetComponent<Image> ().sprite = arrowPos;
					} else {
						obj.GetComponent<Image> ().sprite = arrowNeg;
					}
					StartCoroutine (ArrowsFX (obj.GetComponent<Image> ()));
				} else {
					arrowObj [i].SetActive (false);
				}
			} else{
				arrowObj[i].SetActive(false);
			}
		}
		
	}

	IEnumerator ArrowsFX(Image obj){
		while(moveArrows){
			obj.GetComponent<Image>().color = new Color(1,1,1,Mathf.PingPong(Time.time*0.7f,1));
			yield return null;
		}
	}
}
