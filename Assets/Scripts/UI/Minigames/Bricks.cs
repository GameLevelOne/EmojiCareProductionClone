using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour {
	Rigidbody2D thisRigidbody;

	void Start(){
		thisRigidbody = GetComponent<Rigidbody2D>();
		thisRigidbody.simulated=false;
	}

	public void BeginDrag(){
		thisRigidbody.simulated=false;
	}

	public void OnDrag(){
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0f);
		transform.position = Camera.main.transform.InverseTransformPoint(tempMousePosition);
	}

	public void EndDrag(){
		thisRigidbody.simulated=true;
	}
}
