using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard : MonoBehaviour {

	public bool insideBoard = false;

	void OnMouseDown(){
		Debug.Log ("as");
		insideBoard = true;
	}
}
