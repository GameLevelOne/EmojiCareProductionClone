using System.Collections;
using UnityEngine;

public class Gordyn : ImmovableFurniture {
	public Sprite gordynOpen, gordynClose;

	SpriteRenderer image;

	void Awake()
	{
		image = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}

	public override void PointerClick()
	{
		if(image.sprite == gordynOpen){
			image.sprite = gordynClose;
			//reduce light
		}else{
			image.sprite = gordynOpen;
			//add light
		}
	}
}
