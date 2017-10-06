using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Globe : ActionableFurniture {
	[Header("Globe Attributes")]
	public List<Sprite> globeSpinning = new List<Sprite>();

	bool isSpining = false;

	const float delayPerFrame = 1f/24f;

	public override void InitVariant ()
	{
		base.InitVariant();
		for(int i = 1;i<variant[currentVariant].sprite.Length;i++) globeSpinning.Add(variant[currentVariant].sprite[i]);
	}

	public override void PointerClick()
	{
		if(!isSpining) StartCoroutine(Spinning());
	}

	IEnumerator Spinning()
	{
		isSpining = true;
		int counter = 0;
		while(counter < 8){
			for(int i = 0;i<globeSpinning.Count;i++){
				thisSprite[0].sprite = globeSpinning[i];
				yield return new WaitForSeconds(delayPerFrame);
			}
			counter++;
		}
		isSpining = false;
		thisSprite[0].sprite = variant[currentVariant].sprite[0];
	}
}