using UnityEngine;

public class Gordyn : ActionableFurniture {
	Sprite open, close;

	public override void InitVariant ()
	{
		base.InitVariant();
		open = variant[currentVariant].sprite[0];
		close = variant[currentVariant].sprite[1];
	}

	public override void SetCurrentVariant ()
	{
		base.SetCurrentVariant();
		open = variant[currentVariant].sprite[0];
		close = variant[currentVariant].sprite[1];
	}

	public override void PointerClick()
	{
		if(thisSprite[0].sprite == open){
			thisSprite[0].sprite = close;
			//reduce light
		}else{
			thisSprite[0].sprite = open;
			//add light
		}
	}
}