using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrigeratorIngredientTrigger : MonoBehaviour {
	public delegate void EnterBowl();
	public static event EnterBowl OnEnterBowl;

	void OnTriggerEnter2D(Collider2D col){
//		Debug.Log("triggered");
		if(col.tag == Tags.UI.BOWL){
//			Debug.Log("mangkok");
			OnEnterBowl();
		}
	}
}
