using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CurrencyObject : MonoBehaviour {
	public CurrencyType type;

	public UICoin uiCoin;
	public Rigidbody2D thisRigidBody;
	public Collider2D thisCollider;
	public Collider2D touchTrigger;

	public Vector2 coinPanelPos = new Vector2(3f,6f);
	public float upForce = 1f;
	public float sideForce = 1f;
	public float speed = 5f;
	void Start()
	{
		//make jump (add force)
		thisRigidBody.AddForce(new Vector2(UnityEngine.Random.Range(-1*sideForce, sideForce),upForce));
		StartCoroutine(AutoCollect());
	}

	void AddPlayerCurrency()
	{
		touchTrigger.enabled = false;
		switch(type){
		case CurrencyType.Coin: 
			//add 10 coin
			uiCoin.ShowUI (10, true, false, true);
			break;
		case CurrencyType.Gem: 
			//add 1 gem
			uiCoin.ShowUI (1, false, false, true);
			break;
		}
		StartCoroutine(Destroying());
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
		AddPlayerCurrency();
	}

	IEnumerator Destroying()
	{
		
		thisRigidBody.simulated = false;
		thisCollider.enabled = false;

		float t = 0;
		Vector3 startPos = transform.localPosition;
		Vector3 endPos = new Vector3(coinPanelPos.x,coinPanelPos.y,-1f);

		while(t < 1f){
			transform.localPosition = Vector3.Lerp(startPos,endPos,t*speed);
			t+= Time.deltaTime;
			yield return null;

			if(transform.localPosition == endPos){ 
				
				StopAllCoroutines();
				Destroy(gameObject);
			}
		}

		Destroy(gameObject);
	}
}
