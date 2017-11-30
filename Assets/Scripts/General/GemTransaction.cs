using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemTransaction : MonoBehaviour {

	public int CurrentGem{
		get{return PlayerData.Instance.PlayerGem;}
	}

	public void ModGem(int mod){
		PlayerData.Instance.PlayerGem += mod;
	}
}
