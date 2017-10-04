using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelStatsManager : MonoBehaviour {
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

	bool moveArrows = true;

	void OnEnable(){
//		Emoji.Instance.InitEmojiData();
		CalculateStats();
//		Emoji.Instance.OnEmojiTickStats += CalculateStats;
	}

	void OnDisable(){
//		Emoji.Instance.OnEmojiTickStats -= CalculateStats;
	}

	void CalculateStats(){
//		Emoji currData = Emoji.Instance;
		float tempMaxStats = 100f; //temp

//		float ratioHunger = currData.hunger / tempMaxStats;
//		float ratioHygiene = currData.hygene / tempMaxStats;
//		float ratioHappiness = currData.happiness / tempMaxStats;
//		float ratioStamina = currData.stamina / tempMaxStats;
//		float ratioHealth = currData.health / tempMaxStats;

//		float ratioHunger = 0.85f;
//		float ratioHygiene = 0.65f;
//		float ratioHappiness = 0.49f;
//		float ratioStamina = 0.2f;
//		float ratioHealth = 0.1f;

//		barFillHunger.fillAmount = ratioHunger;
//		barFillHygiene.fillAmount = ratioHygiene;
//		barFillHappiness.fillAmount = ratioHappiness;
//		barFillStamina.fillAmount = ratioStamina;
//		barFillHealth.fillAmount = ratioHealth;

//		InitStats(parentArrowHunger,barFillHunger,ratioHunger);
//		InitStats(parentArrowHygiene,barFillHygiene,ratioHygiene);
//		InitStats(parentArrowHappiness,barFillHappiness,ratioHappiness);
//		InitStats(parentArrowStamina,barFillStamina,ratioStamina);
//		InitStats(parentArrowHealth,barFillHealth,ratioHealth);
	}

	void InitStats(Transform parentObj,Image barFill,float value){
		int arrowCount = 0;
		bool isFlipped = false;
		bool posValue = true;
		if(value>=0f && value<0.15f){
			arrowCount = 3;
			posValue=false;
		} else if(value>=0.15f && value<0.3f){
			arrowCount = 2;
			posValue=false;
		} else if(value>=0.3f && value<0.5f){
			arrowCount = 1;
			posValue=false;
		} else if(value>=0.5f && value<0.7f){
			arrowCount = 1;
		} else if(value>=0.7f && value<0.85f){
			arrowCount = 2;
		} else{
			arrowCount = 3;
		}

		if(posValue){
			barFill.sprite = barPos;
		} else{
			barFill.sprite = barNeg;
		}

		for(int i=0;i<arrowCount;i++){
			GameObject obj = Instantiate(arrowObj,parentObj,false) as GameObject;
			obj.transform.localPosition = new Vector3(-10+i*20,0,0);
			if(posValue){
				obj.GetComponent<Image>().sprite = arrowPos;
			} else{
				obj.GetComponent<Image>().sprite = arrowNeg;
			}
			StartCoroutine(ArrowsFX(obj.GetComponent<Image>()));
		}
	}

	IEnumerator ArrowsFX(Image obj){
		while(moveArrows){
			obj.GetComponent<Image>().color = new Color(1,1,1,Mathf.PingPong(Time.time*0.7f,1));
			yield return null;
		}
	}

}
