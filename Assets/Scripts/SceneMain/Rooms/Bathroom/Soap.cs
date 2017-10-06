using System.Collections;
using UnityEngine;

public class Soap : ActionableFurniture {
	[Header("Soap Attributes")]
	public Sponge sponge;
	public Sprite soapLiquid;

	public override void InitVariant ()
	{
		base.InitVariant ();
		soapLiquid = variant[currentVariant].sprite[1];
	}

	public override void PointerClick()
	{
		sponge.ApplySoapLiquid(soapLiquid);
	}
	
}
