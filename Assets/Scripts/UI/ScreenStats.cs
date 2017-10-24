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

	public Sprite barPos;
	public Sprite barNeg;
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

	void UpdateStatsValue(){
		Emoji currentData = PlayerData.Instance.PlayerEmoji;
		float ratioHunger = currentData.hunger.StatValue / currentData.hunger.MaxStatValue;
		float ratioHygiene = currentData.hygiene.StatValue / currentData.hygiene.MaxStatValue;
		float ratioHappiness = currentData.happiness.StatValue / currentData.happiness.MaxStatValue;
		float ratioStamina = currentData.stamina.StatValue / currentData.stamina.MaxStatValue;
		float ratioHealth = currentData.health.StatValue / currentData.health.MaxStatValue;

//		float ratioHunger = 0.85f;
//		float ratioHygiene = 0.65f;
//		float ratioHappiness = 0.49f;
//		float ratioStamina = 0.2f;
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
		//room mod?
		Emoji currentData = PlayerData.Instance.PlayerEmoji;
		CheckArrows(parentArrowHunger,currentData.hunger.totalModifier);
		CheckArrows(parentArrowHygiene,currentData.hygiene.totalModifier);
		CheckArrows(parentArrowHappiness,currentData.happiness.totalModifier);
		CheckArrows(parentArrowStamina,currentData.stamina.totalModifier);
		CheckArrows(parentArrowHealth,currentData.health.totalModifier);
	}

	void CheckBarSprite(float value,Image barFill){
		if(value>=0.5f){
			barFill.sprite = barPos;
		} else{
			barFill.sprite = barNeg;
		}
	}

	void CheckArrows (Transform parentObj, float totalRoomMod=1f)
	{
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

		if (absRoomMod >= 1f && absRoomMod < 2.5f) {
			arrowCount = 1;
		} else if (absRoomMod >= 2.5f && absRoomMod < 5f) {
			arrowCount = 2;
		} else {
			arrowCount = 3;
		}

		if (!noMod) {
			for (int i = 0; i < arrowCount; i++) {
				GameObject obj = Instantiate (arrowObj, parentObj, false) as GameObject;
				obj.transform.localPosition = new Vector3 (-10 + i * 20, 0, 0);
				if (isPositive) {
					obj.GetComponent<Image> ().sprite = arrowPos;
				} else {
					obj.GetComponent<Image> ().sprite = arrowNeg;
				}
				StartCoroutine (ArrowsFX (obj.GetComponent<Image> ()));
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
