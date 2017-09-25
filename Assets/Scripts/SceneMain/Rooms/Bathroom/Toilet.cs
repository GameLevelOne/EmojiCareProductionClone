using System.Collections;
using UnityEngine;

public class Toilet : ImmovableFurniture {
	#region attributes
	public SpriteRenderer lidOpened, lidClosed, flush;
	public Sprite flushDown, flushUp;
	bool lidOpen, isFlusing;
	#endregion

	void Awake()
	{
		Init();
	}

	void Init()
	{
		lidOpen = isFlusing = false;
		SwitchLid(lidOpen);
	}

	void SwitchLid(bool state)
	{
		if(state == true){
			lidOpened.enabled = true;
			lidClosed.enabled = false;
		}else{
			lidOpened.enabled = false;
			lidClosed.enabled = true;
		}
	}

	public void PointerClick()
	{
		lidOpen = !lidOpen;
		SwitchLid(lidOpen);
	}

	public void FlushPointerClick()
	{
		if(!isFlusing) StartCoroutine(flushing());
	}

	IEnumerator flushing()
	{
		isFlusing = true;
		flush.sprite = flushUp;
		yield return new WaitForSeconds(2f);
		isFlusing = false;
		flush.sprite = flushDown;
	}
	
}
