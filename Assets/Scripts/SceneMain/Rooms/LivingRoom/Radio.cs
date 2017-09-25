using System.Collections;
using UnityEngine;

public class Radio : MovableFurniture {
	public Animator toneAnimation;
	public SpriteRenderer[] childSprites;

	bool radioOn = false;

	public void PointerDown()
	{
		if(!radioOn) radioOn = true;
		else radioOn = false;
		toneAnimation.SetBool(AnimatorParameters.Bools.RADIO_ON,radioOn);
	}

	protected override void AdjustSortingOrder()
	{
		if(transform.localPosition.y >= 0) thisSprite.sortingOrder = 0;
		else{
			int sortingOrder = 0;
			float abs = Mathf.Abs(transform.localPosition.y);
			float absSisa = abs - Mathf.Floor(abs);

			if(absSisa < 0.5f) sortingOrder = Mathf.FloorToInt(abs) * 2 + 1;
			else sortingOrder = Mathf.CeilToInt(abs) * 2;

			thisSprite.sortingOrder = sortingOrder;
		}
		foreach(SpriteRenderer s in childSprites) s.sortingOrder = thisSprite.sortingOrder;
	}
}