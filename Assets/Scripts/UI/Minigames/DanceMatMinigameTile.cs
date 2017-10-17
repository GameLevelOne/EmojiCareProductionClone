using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceMatMinigameTile : MonoBehaviour {
	public delegate void ClickDanceMatTile(int tileIndex,int colorIndex);
	public static event ClickDanceMatTile OnClickDanceMatTile;

	public int tileIndex;
	int clickCount;

	int maxCount = 4;

	public void OnClickTile ()
	{
		if (clickCount < maxCount) {
			clickCount++;
			OnClickDanceMatTile (tileIndex, clickCount);
		}

		if(clickCount == maxCount){
			clickCount=0;
		}
	}
	
}
