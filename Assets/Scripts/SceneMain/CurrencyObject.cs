using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CurrencyObject : MonoBehaviour {
	public CurrencyType type;

	public Rigidbody2D thisRigidBody;
	public Collider2D touchTrigger;

	public float upForce = 1f;
	public float sideForce = 1f;

	void Start()
	{
		//make jump (add force)
		thisRigidBody.AddForce(new Vector2(UnityEngine.Random.Range(-1*sideForce, sideForce),upForce));
	}

	void AddPlayerCurrency()
	{
		switch(type){
		case CurrencyType.Coin: 
			//add 10 coin
			break;
		case CurrencyType.Gem: 
			//add 1 gem
			break;
		}
	}

	public void PointerDown()
	{
		//add player currency according to type
		StopAllCoroutines();
		AddPlayerCurrency();
	}

	IEnumerator AutoCollect()
	{
		yield return new WaitForSeconds(5f);
		touchTrigger.enabled = false;
		AddPlayerCurrency();
	}
}
