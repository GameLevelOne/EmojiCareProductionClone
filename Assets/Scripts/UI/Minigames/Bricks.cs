using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bricks : MonoBehaviour {
	Rigidbody2D thisRigidbody;

	void OnEnable(){
		thisRigidbody = GetComponent<Rigidbody2D>();
		StartCoroutine (WaitForUIAnim ());
		RandomColor ();
	}

	void RandomColor(){
		GetComponent<Image> ().color = GetColor (Random.Range (1, 6));
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

	Color GetColor(int count){
		Color currentColor;
		if (count == 1) {
			return new Color (0.98f, 0.01f, 0.42f, 1);
		} else if (count == 2) {
			return new Color (0.19f, 0.7f, 0.27f, 1);
		} else if (count == 3) {
			return new Color (0.98f, 0.37f, 0.1f, 1);
		} else if (count == 4) {
			return new Color (0.91f, 0.74f, 0.29f, 1);
		} else if (count == 5) {
			return new Color (0.4f, 0.39f, 0.85f, 1);
		} else
			return Color.white;
	}

	IEnumerator WaitForUIAnim(){
		thisRigidbody.simulated = false;
		yield return new WaitForSeconds (0.16f);
		thisRigidbody.simulated = true;
	}
}
