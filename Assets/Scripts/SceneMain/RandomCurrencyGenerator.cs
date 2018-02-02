using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class RandomCurrencyGenerator : MonoBehaviour {
	public RoomController roomController;
	public UICoin uiCoin;

	public GameObject CoinObject;
	public GameObject GemObject;

	[Header("CooldownTime")]
	public int hour;
	public int min;
	public int sec;

	public void CheckAvailability(Transform selectedObject)
	{
		if(PlayerPrefs.HasKey(PlayerPrefKeys.Player.RANDOM_COINGEM_COOLDOWN)){
			if(DateTime.Now.CompareTo(PlayerData.Instance.RandomCoinAndGemCooldown) >= 0){
				
				GenerateCurrency(selectedObject);
//				Debug.Log("Currency Generated");
			}//else{
//				Debug.Log("Currency Not Available Yet");
//			}
		}else{
			GenerateCurrency(selectedObject);
//			Debug.Log("Currency Generated");
		}
	}

	void GenerateCurrency(Transform selectedObject)
	{
		Transform parent = roomController.rooms[(int)roomController.currentRoom].transform;
		GameObject tempObj = (GameObject) Instantiate(UnityEngine.Random.value <= 0.5f ? CoinObject : GemObject,parent);
		tempObj.GetComponent<CurrencyObject>().uiCoin = uiCoin;
		tempObj.transform.position = new Vector3(selectedObject.position.x,selectedObject.position.y+1.5f,-1f);

		TimeSpan tempCooldown = new TimeSpan(hour,min,sec);
		PlayerData.Instance.RandomCoinAndGemCooldown = DateTime.Now.Add(tempCooldown);
	}
}
