using System.Collections;
using UnityEngine;

public class Soap : ImmovableFurniture {
	#region attributes
	public Sponge sponge;
	#endregion

	public override void PointerClick()
	{
		sponge.ApplySoapLiquid();
	}
	
}
