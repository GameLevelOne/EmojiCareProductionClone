using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumTile : MonoBehaviour {
	public delegate void SelectEmoji(EmojiType item);
	public static event SelectEmoji OnSelectEmoji;

	public Image icon;

	public EmojiType emojiType;

	public void InitTile(Sprite sprite){
		icon.sprite = sprite;
	}

	public void OnClickTile(){
		OnSelectEmoji(emojiType);
	}
}
