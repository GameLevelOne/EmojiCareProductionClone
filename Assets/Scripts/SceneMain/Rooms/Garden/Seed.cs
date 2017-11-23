﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour {
	public delegate void SeedPlanted(Seed seed);
	public event SeedPlanted OnSeedPlanted;
	#region attributes
	public Rigidbody2D thisRigidbody;
	public GameObject PlantObject;
	public int growthDuration;

	[Header("Custom Attribute")]
	public IngredientType type;
	public int price;

	[Header("Do Not Modify")]
	public List<GameObject> soilTarget = new List<GameObject>();
	public int seedIndex;

	Vector3 startPos;
	bool flagHold = false;
	#endregion

	#region events
	public delegate void DragSeed(int price);
	public static event DragSeed OnDragSeed;
	#endregion

//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	public void Init(int index)
	{
		startPos = transform.localPosition;
		seedIndex = index;
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	public void OnTriggerEnter2D(Collider2D other)
	{
		if(flagHold){
			if(other.tag == Tags.SOIL){
				soilTarget.Add(other.gameObject);
			}
		}

	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == Tags.SOIL){
			soilTarget.Remove(other.gameObject);
		}
	}

	public void BeginDrag ()
	{
		flagHold = true;
		if (OnDragSeed != null) {
			OnDragSeed (price);
		}
	}

	public void Drag()
	{
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,19f);
		transform.position = Camera.main.ScreenToWorldPoint(tempMousePosition);
	}

	public void EndDrag()
	{
		flagHold = false;
		if(soilTarget.Count > 0){
			if(!soilTarget[0].GetComponent<GardenField>().hasPlant){
				soilTarget[0].GetComponent<GardenField>().PlantSeed(type);
				if(OnSeedPlanted != null) OnSeedPlanted(this);
				Destroy(gameObject);
				return;
			}
		}
		StartCoroutine(Return());	
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
	IEnumerator Return()
	{
		yield return null;
		Vector3 currentPos = transform.localPosition;
		float t = 0;
		while(t < 1){
			transform.localPosition = Vector3.Lerp(currentPos,startPos,t);
			t+= Time.deltaTime*5f;
			yield return null;
		}
		transform.localPosition = startPos;
		soilTarget.Clear();
	}
}
