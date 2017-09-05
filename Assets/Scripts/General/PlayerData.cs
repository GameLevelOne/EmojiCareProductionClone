using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
	static PlayerData instance;

	bool firstPlay = false;

	public static PlayerData Instance{
		get{return instance;}
	}

	public bool FirstPlay{
		set{firstPlay = value;}
		get{return firstPlay;}
	}

	void Awake(){
		if(instance != null && instance != this){
			Destroy(this.gameObject);
		} else{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
	
}
