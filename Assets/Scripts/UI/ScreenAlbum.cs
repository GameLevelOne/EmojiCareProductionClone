using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenAlbum : BaseUI {

	public GameObject emojiBoxPrefab;
	public RectTransform emojiContentBox;
	public Scrollbar scrollbar;
	public Image emojiBigIcon;

	public Sprite[] emojiIcons = new Sprite[12];
	public Sprite lockedEmoji;

	int totalEmoji = 10;
	int tileWidth = 3;
	int tileHeight = 4;
	float boxSize = 150;
	float contentBoxMarginX = 50;

	public override void InitUI ()
	{
		Debug.Log("album");

		int tempIdx = 0;
		emojiContentBox.sizeDelta = new Vector2 (0, ((float)tileHeight * boxSize) + contentBoxMarginX);

		for (int i = 0; i < tileHeight; i++) {
			for (int j = 0; j < tileWidth; j++) {
				GameObject obj = Instantiate (emojiBoxPrefab, emojiContentBox, false) as GameObject;
				obj.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-140 + j * 140, 230 - i * 150);
				obj.GetComponent<AlbumTile>().exprType = (FaceAnimation)tempIdx;
				if (emojiIcons [tempIdx] != null) {
					Debug.Log("asd");
					obj.transform.GetChild (0).GetComponent<Image> ().sprite = emojiIcons [tempIdx];
				}else{
					obj.transform.GetChild(0).GetComponent<Image>().sprite = lockedEmoji;
					obj.GetComponent<Button>().interactable=false;
				}
				tempIdx++;
			}
		}
		scrollbar.size = 0.1f;
	}

	void OnEnable(){
		AlbumTile.OnSelectExpression += OnSelectExpression;
	}

	void OnDisable(){
		AlbumTile.OnSelectExpression -= OnSelectExpression;
	}

	void OnSelectExpression (FaceAnimation item)
	{
		emojiBigIcon.sprite = emojiIcons[(int)item];
	}
}
