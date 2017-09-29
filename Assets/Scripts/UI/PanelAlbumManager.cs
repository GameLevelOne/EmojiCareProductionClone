using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelAlbumManager : MonoBehaviour {
	public GameObject emojiBoxPrefab;
	public RectTransform emojiContentBox;
	public Scrollbar scrollbar;

	public Sprite[] emojiIcons = new Sprite[12];
	public Sprite lockedEmoji;

	int totalEmoji = 10;
	int tileWidth = 3;
	int tileHeight = 4;
	float boxSize = 150;
	float contentBoxMarginX = 50;

	void Start(){
		InitContentBox ();
	}

	void InitContentBox ()
	{
		int tempIdx = 0;
		emojiContentBox.sizeDelta = new Vector2 (0, ((float)tileHeight * boxSize) + contentBoxMarginX);

		for (int i = 0; i < tileHeight; i++) {
			for (int j = 0; j < tileWidth; j++) {
				GameObject obj = Instantiate (emojiBoxPrefab, emojiContentBox, false) as GameObject;
				obj.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-140 + j * 140, 230 - i * 150);
				if (emojiIcons [tempIdx] != null) {
					Debug.Log("asd");
					obj.transform.GetChild (0).GetComponent<Image> ().sprite = emojiIcons [tempIdx];
				}else{
					obj.transform.GetChild(0).GetComponent<Image>().sprite = lockedEmoji;
				}
				tempIdx++;
			}
		}
		scrollbar.size = 0.1f;
	}
}
