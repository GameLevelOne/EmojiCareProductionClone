using System.Collections;
using UnityEngine;

public class AlarmClock : ActionableFurniture {
	[Header("AlarmClock Attributes")]
	public Animator thisAnim;
	public Transform objectTransform;
	bool ringing = false;

	//event triggers
	public override void PointerClick()
	{
		if(!ringing){
			StartCoroutine(Ring());
			thisAnim.SetTrigger(AnimatorParameters.Triggers.ANIMATE);
		}
	}
		
	IEnumerator Ring()
	{
		SoundManager.Instance.PlaySFXOneShot(SFXList.AlarmClock);
		ringing = true;
		float t = 0;
		while(t <= 1.5f){
			t += Time.deltaTime;
			objectTransform.localPosition = new Vector3(Random.Range(-0.03f,0.03f),Random.Range(-0.01f,0.01f),0f);
			yield return null;
		}
		objectTransform.localPosition = Vector3.zero;
		ringing = false;
	}

}
