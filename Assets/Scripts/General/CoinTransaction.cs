using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTransaction : MonoBehaviour {

	public int CurrentCoin{
		get{return PlayerData.Instance.PlayerCoin;}
	}

	public void ModCoin(int mod){
		PlayerData.Instance.PlayerCoin += mod;
	}
}
