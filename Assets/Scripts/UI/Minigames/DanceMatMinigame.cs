using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanceMatMinigame : BaseUI {
	public Image[] danceMatMinigame;
	public SpriteRenderer[] danceMatFurniture;

	int clickCount = 0;
	int maxClick = 6;

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
		if (count == 1) {
			return new Color32 (0xFC, 0x04, 0x6D, 0xFF);
		} else if (count == 2) {
			return new Color32 (0x31, 0xB3, 0x46, 0xFF);
		} else if (count == 3) {
			return new Color32 (0x3D, 0x8F, 0xD8, 0xFF);
		} else if (count == 4) {
			return new Color32 (0xE9, 0xBF, 0x4C, 0xFF);
		} else if (count == 5) {
			return new Color32 (0x68, 0x65, 0xDA, 0xFF);
		} else
			return Color.white;
	}

	public void OnClickBack(){
		PlayerData.Instance.PlayerEmoji.playerInput.OnDanceMatMinigameDone ();
		base.CloseUI (this.gameObject);
	}
}
