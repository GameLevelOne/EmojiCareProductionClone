using System.Collections;
using UnityEngine;

public class Sponge : TriggerableFurniture {
	[Header("Sponge Attributes")]
	public SpriteRenderer soapLiquid;

	public void ApplySoapLiquid(Sprite liquidSprite)
	{
		if(soapLiquid.enabled == false) soapLiquid.enabled = true;
		soapLiquid.sprite = liquidSprite;
	}

//	//event triggers
//	public override void BeginDrag()
//	{
//		if(!flagEditMode && !endDrag){
//			thisAnim.SetBool(AnimatorParameters.Bools.HOLD,true);
//			thisSprite.sortingLayerName = SortingLayers.HELD;
//			soapLiquid.sortingLayerName = SortingLayers.HELD;
//		}
//	}
//
//	//event triggers
//	public override void EndDrag()
//	{
//		if(!flagEditMode && !endDrag){
//			endDrag = true;
//
//			thisAnim.SetBool(AnimatorParameters.Bools.HOLD,false);
//			thisSprite.sortingLayerName = SortingLayers.MOVABLE_FURNITURE;
//			soapLiquid.sortingLayerName = SortingLayers.MOVABLE_FURNITURE;
//			soapLiquid.enabled = false;
//			StartCoroutine(BackToFixedPosition());
//		}
//	}


}