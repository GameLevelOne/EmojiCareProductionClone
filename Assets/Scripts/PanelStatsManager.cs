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

	public GameObject arrowObj;

	public Transform parentArrowHunger;
	public Transform parentArrowHygiene;
	public Transform parentArrowHappiness;
	public Transform parentArrowStamina;
	public Transform parentArrowHealth;

	bool moveArrows = true;

	void Start(){
		InitStats();
	}

	void InitStats(){
		Emoji currData = Emoji.Instance;

		currData.CurrEmojiHunger = 50;
		currData.CurrEmojiHygiene = 80;
		currData.CurrEmojiHappiness = 90;
		currData.CurrEmojiStamina = 49;
		currData.CurrEmojiHealth = 16;

		float ratioHunger = currData.CurrEmojiHunger / currData.MaxEmojiHunger;
		float ratioHygiene = currData.CurrEmojiHygiene / currData.MaxEmojiHygiene;
		float ratioHappiness = currData.CurrEmojiHappiness / currData.MaxEmojiHappiness;
		float ratioStamina = currData.CurrEmojiStamina / currData.MaxEmojiStamina;
		float ratioHealth = currData.CurrEmojiHealth / currData.MaxEmojiHealth;

		barFillHunger.fillAmount = ratioHunger;
		barFillHygiene.fillAmount = ratioHygiene;
		barFillHappiness.fillAmount = ratioHappiness;
		barFillStamina.fillAmount = ratioStamina;
		barFillHealth.fillAmount = ratioHealth;

		InitStatArrows(parentArrowHunger,ratioHunger);
		InitStatArrows(parentArrowHygiene,ratioHygiene);
		InitStatArrows(parentArrowHappiness,ratioHappiness);
		InitStatArrows(parentArrowStamina,ratioStamina);
		InitStatArrows(parentArrowHealth,ratioHealth);
	}

	void InitStatArrows(Transform parentObj,float value){
		int arrowCount = 0;
		bool isFlipped = false;
		if(value>=0f && value<0.15f){
			arrowCount = 3;
			isFlipped=true;
		} else if(value>=0.15f && value<0.3f){
			arrowCount = 2;
			isFlipped = true;
		} else if(value>=0.3f && value<0.5f){
			arrowCount = 1;
			isFlipped=true;
		} else if(value>=0.5f && value<0.7f){
			arrowCount = 1;
			isFlipped=false;
		} else if(value>=0.7f && value<0.85f){
			arrowCount = 2;
			isFlipped = false;
		} else{
			arrowCount = 3;
			isFlipped = false;
		}

		for(int i=0;i<arrowCount;i++){
			GameObject obj = Instantiate(arrowObj,parentObj,false) as GameObject;
			obj.transform.localPosition = new Vector3((-34+i*15),0,0);
			if(isFlipped){
				obj.transform.localScale = new Vector3(-1,1,1);
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
