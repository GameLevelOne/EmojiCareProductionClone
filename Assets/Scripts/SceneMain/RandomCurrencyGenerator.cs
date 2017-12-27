using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomCurrencyGenerator : MonoBehaviour {
	public RoomController roomController;

	public GameObject CoinObject;
	public GameObject GemObject;

	[Header("CooldownTime")]
	public int hour;
	public int min;
	public int sec;

	public void CheckAvailability()
	{
		if(PlayerPrefs.HasKey(PlayerPrefKeys.Player.RANDOM_COINGEM_COOLDOWN)){
			if(DateTime.Now.CompareTo(PlayerData.Instance.RandomCoinAndGemCooldown) >= 0){
				GenerateCurrency();
				Debug.Log("Currency Generated");
			}else{
				Debug.Log("Currency Not Available Yet");
			}
		}else{
			GenerateCurrency();
			Debug.Log("Currency Generated");
		}
	}

	void GenerateCurrency()
	{
		Transform parent = roomController.rooms[(int)roomController.currentRoom].transform;
		GameObject tempObj = (GameObject) Instantiate(UnityEngine.Random.value <= 0.5f ? CoinObject : GemObject,parent);


		TimeSpan tempCooldown = new TimeSpan(hour,min,sec);
		PlayerData.Instance.RandomCoinAndGemCooldown = DateTime.Now.Add(tempCooldown);
	}
}
