using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenAlbum : BaseUI {

	public GameObject emojiBoxPrefab;
	public RectTransform emojiContentBox;
	public Scrollbar scrollbar;
	public Image emojiBigIcon;

	public Sprite[] emojiIcons = new Sprite[10];
	public Sprite lockedEmoji;

	List<EmojiType> emojiData = new List<EmojiType>();

	int tileCount = 6; //initial display
	int tileWidth = 3;
	int tileHeight = 2;
	int currentRecordCount = 1;

	float boxSize = 150;
	float contentBoxMarginX = 50;

	void OnEnable(){
		//PlayerData.Instance.PlayerEmoji.OnEmojiDead += OnEmojiDead;
		AlbumTile.OnSelectExpression += OnSelectExpression;
	}

	void OnDisable(){
		//PlayerData.Instance.PlayerEmoji.OnEmojiDead -= OnEmojiDead;
		AlbumTile.OnSelectExpression -= OnSelectExpression;
	}

	void OnEmojiDead ()
	{
		currentRecordCount++;
		if(currentRecordCount > tileCount){
			tileCount = currentRecordCount;
		}
		emojiData.Add(PlayerData.Instance.PlayerEmoji.emojiBaseData.emojiType);
		PlayerPrefs.SetInt(PlayerPrefKeys.Player.EMOJI_RECORD_COUNT,currentRecordCount);
	}

	void OnSelectExpression (FaceAnimation item)
	{
		emojiBigIcon.sprite = emojiIcons[(int)item];
	}

	public override void InitUI ()
	{
		Debug.Log ("album");

		tileHeight = Mathf.CeilToInt (tileCount / tileWidth);

		int tempIdx = 0;
		emojiContentBox.sizeDelta = new Vector2 (0, ((float)tileHeight * boxSize) + contentBoxMarginX);

		emojiData.Add(EmojiType.Emoji);

		for (int i = 0; i < tileHeight; i++) {
			for (int j = 0; j < tileWidth; j++) {
				GameObject obj = Instantiate (emojiBoxPrefab, emojiContentBox, false) as GameObject;
				obj.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (90 + j * 140, -85 - i * 150);

				if (emojiData.Count != 0 && (currentRecordCount-1) >=tempIdx) {
					if (emojiData [tempIdx] != null) {
						Debug.Log ("asd");
						obj.transform.GetChild (0).GetComponent<Image> ().sprite = emojiIcons [(int)emojiData [tempIdx]];
					} else {
						obj.GetComponent<Button> ().interactable = false;
					}
					tempIdx++;
				}
			}
		}
		scrollbar.size = 0.1f;
	}


}
