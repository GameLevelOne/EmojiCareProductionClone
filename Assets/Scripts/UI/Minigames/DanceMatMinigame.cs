using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanceMatMinigame : BaseUI {
	public GameObject buttonHotkey;
	public DanceMat danceMatFurnitureRef;
	public Image[] danceMatMinigame;
	public SpriteRenderer[] danceMatFurniture;

	int[] tileColorIndex = new int[9];

	void OnEnable(){
		DanceMatMinigameTile.OnClickDanceMatTile += OnClickDanceMatTile;
		Init ();
	}

	void OnDisable(){
		DanceMatMinigameTile.OnClickDanceMatTile -= OnClickDanceMatTile;
	}

	void Init(){
		buttonHotkey.SetActive (false);
		for(int i=0;i<tileColorIndex.Length;i++){
			tileColorIndex[i]=PlayerPrefs.GetInt (PlayerPrefKeys.Game.DANCE_MAT_TILE_COLOR_DATA + i.ToString(), 0);
			danceMatMinigame [i].color = danceMatFurnitureRef.GetColor (tileColorIndex [i]);
			danceMatFurniture [i].color = danceMatFurnitureRef.GetColor (tileColorIndex [i]);
		}
	}

	void OnClickDanceMatTile(int tileIndex,int colorIndex){
		Color currentColor = danceMatFurnitureRef.GetColor(colorIndex);
		tileColorIndex [tileIndex] = colorIndex;
		danceMatMinigame[tileIndex].color = currentColor;
		danceMatFurniture[tileIndex].color = currentColor;
	}

	void SaveChanges(){
		for(int i=0;i<tileColorIndex.Length;i++){
			PlayerPrefs.SetInt (PlayerPrefKeys.Game.DANCE_MAT_TILE_COLOR_DATA + i.ToString(), tileColorIndex [i]);
		}
	}

	public void OnClickBack(){
		SaveChanges ();
		PlayerData.Instance.PlayerEmoji.playerInput.OnDanceMatMinigameDone ();
		base.CloseUI (this.gameObject);
		buttonHotkey.SetActive (true);
	}


}
