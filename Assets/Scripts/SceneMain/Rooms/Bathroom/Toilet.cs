using System.Collections;
using UnityEngine;

public class Toilet : ActionableFurniture {
	#region attributes
	[Header("Toilet Attributes")]
	public SpriteRenderer lidOpened;
	public SpriteRenderer lidClosed;
	public SpriteRenderer flush;
	public Sprite flushDown, flushUp;

	bool lidOpen = false, isFlusing;
	#endregion
//------------------------------------------------------------------------------------------------------------------------------------------------
	public override void InitVariant ()
	{
		base.InitVariant ();
		lidOpened.sprite = variant[currentVariant].sprite[1];
		lidClosed.sprite = variant[currentVariant].sprite[2];
		flushUp = variant[currentVariant].sprite[3];
		flushDown = variant[currentVariant].sprite[4];
	}

	//event triggers
	public override void PointerClick()
	{
		lidOpen = !lidOpen;
		lidOpened.enabled = lidOpen;
		lidClosed.enabled = !lidOpen;
	}

	//event triggers
	public void FlushPointerClick()
	{
		if(!isFlusing) StartCoroutine(flushing());
	}

	IEnumerator flushing()
	{
		isFlusing = true;
		flush.sprite = flushUp;
		yield return new WaitForSeconds(2f);
		flush.sprite = flushDown;
		isFlusing = false;
	}
}