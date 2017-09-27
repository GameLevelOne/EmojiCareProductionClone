using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class PanelAlbumManager : MonoBehaviour {
	public GameObject emojiBoxPrefab;
	public RectTransform emojiContentBox;

	int totalEmoji = 10;
	int tileWidth = 3;
	int tileHeight = 4;
	float boxSize = 200f;
	float contentBoxMarginX = 100;

	void Start(){
		InitContentBox ();
	}

	void InitContentBox(){
		emojiContentBox.sizeDelta = new Vector2 (0, ((float)tileHeight * boxSize) + contentBoxMarginX);

		for (int i = 0; i < tileHeight; i++) {
			for (int j = 0; j < tileWidth; j++) {
				GameObject obj = Instantiate(emojiBoxPrefab,emojiContentBox,false) as GameObject;
				obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200+j*200,340-i*200);
			}
		}
	}
}
