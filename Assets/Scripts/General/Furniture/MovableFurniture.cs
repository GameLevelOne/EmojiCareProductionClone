using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovableFurniture : Furniture {
	#region attribute

	Animator thisAnim;
	BoxCollider2D thisCollider;
	List<Collider2D> floorColliders = new List<Collider2D>();
	#endregion


	#region initialization
	void Awake()
	{
		Init();
	}

	void Init()
	{
		thisAnim = GetComponent<Animator>();
		thisCollider = GetComponent<BoxCollider2D>();
	}
	#endregion

	#region mechanics
	//triggers and colliders
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.FLOOR){
			floorColliders.Add(other);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == Tags.FLOOR){
			floorColliders.Remove(other);
		}
	}

	//event triggers
	public void BeginDrag()
	{
		
	}
	public void Drag()
	{
		
	}
	public void EndDrag()
	{
		
	}
	#endregion


	#region public methods

	#endregion
}
