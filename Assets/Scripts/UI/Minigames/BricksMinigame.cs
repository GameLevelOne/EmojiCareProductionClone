using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksMinigame : BaseUI {

	public Transform[] objects;
	Vector3[] objStartPos = new Vector3[6];

	void Start(){
//		for(int i=0;i<objects.Length;i++){
//			objStartPos[i] = objects[i].localPosition;
//		}
	}

	public override void InitUI ()
	{
//		for(int i=0;i<objects.Length;i++){
//			objects[i].localPosition = objStartPos[i];
//		}
		//StartCoroutine(WaitForAnim());
	}

	IEnumerator WaitForAnim(){
		SimulatePhysics(false);
		yield return new WaitForSeconds(0.16f);
		SimulatePhysics(true);
	}

	void SimulatePhysics(bool doSimulate){
		for(int i=0;i<objects.Length;i++){
			if(doSimulate)
				objects[i].GetComponent<Rigidbody2D>().simulated=true;
			else
				objects[i].GetComponent<Rigidbody2D>().simulated=false;
		}
	}
}
