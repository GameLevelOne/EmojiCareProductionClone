using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bricks : MonoBehaviour {
	Rigidbody2D thisRigidbody;
	public Vector3 startPos;

	void Awake(){
		startPos = transform.localPosition;
		Debug.Log(startPos);
	}

	void OnEnable(){
		thisRigidbody = GetComponent<Rigidbody2D>();
		thisRigidbody.simulated=false;
		transform.localPosition = startPos;
		transform.localRotation = Quaternion.identity;
		RandomColor ();
	}

	void RandomColor(){
		Color[] colors = new Color[]{Color.white,Color.cyan,Color.magenta,Color.yellow,Color.blue };
		GetComponent<Image> ().color = colors [Random.Range (0,colors.Length)];
	}

	public void BeginDrag(){
		thisRigidbody.simulated=false;
	}

	public void OnDrag(){
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0f);
		transform.position = Camera.main.transform.InverseTransformPoint(tempMousePosition);
	}

	public void EndDrag(){
		thisRigidbody.angularVelocity = 0;
		thisRigidbody.velocity = Vector2.zero;
		thisRigidbody.simulated=true;
	}
}
