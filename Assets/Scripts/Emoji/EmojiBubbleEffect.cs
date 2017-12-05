using System.Collections;
using UnityEngine;

public class EmojiBubbleEffect : MonoBehaviour {
	#region attributes
	[Header("EmojiBubbleAttributes")]
	public SpriteRenderer[] bubbleSprites;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region initialization
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region mechanics
	
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void SetBubbleAlpha(float alpha)
	{
		foreach(SpriteRenderer sr in bubbleSprites){
			sr.color = new Color(sr.color.r,sr.color.g,sr.color.b, (alpha*0.8f) );
		}

		if(alpha == 0) {
			foreach(SpriteRenderer s in bubbleSprites){
				if(s.gameObject.activeSelf) s.gameObject.SetActive(false);
			}
		}else{
			foreach(SpriteRenderer s in bubbleSprites){
				if(!s.gameObject.activeSelf) s.gameObject.SetActive(true);
			}
		}
			
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------	
}
