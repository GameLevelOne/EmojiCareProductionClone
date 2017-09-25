using System.Collections;
using UnityEngine;

public class Globe : MovableFurniture {
	bool isSpining = false;

	public void PointerClick()
	{
		if(isSpining == false) StartCoroutine(Spinning());
	}

	IEnumerator Spinning()
	{
		isSpining = true;
		thisAnim.SetTrigger(AnimatorParameters.Triggers.SPIN);
		yield return new WaitForSeconds(1f);
		isSpining = false;
	}
}