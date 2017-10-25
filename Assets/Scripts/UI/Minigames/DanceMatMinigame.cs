using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanceMatMinigame : BaseUI {
	public Image[] danceMatMinigame;
	public Image[] danceMatFurniture;

	int clickCount = 0;
	int maxClick = 4;

	void OnEnable(){
		DanceMatMinigameTile.OnClickDanceMatTile += OnClickDanceMatTile;
	}

	void OnDisable(){
		DanceMatMinigameTile.OnClickDanceMatTile -= OnClickDanceMatTile;
	}

	void OnClickDanceMatTile(int tileIndex,int colorIndex){
		Color currentColor = GetColor(colorIndex);
		danceMatMinigame[tileIndex].color = currentColor;
		danceMatFurniture[tileIndex].color = currentColor;
	}

	Color GetColor(int count){
		Color currentColor;
		if(count == 1){
			return Color.red;
		} else if(count == 2){
			return Color.green;
		} else if(count == 3){
			return Color.blue;
		} else{
			return Color.yellow;
		}
	}
}
