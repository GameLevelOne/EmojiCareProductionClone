using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumTile : MonoBehaviour {
	public delegate void SelectEmoji(Sprite sprite,string time,float completionRate);
	public static event SelectEmoji OnSelectEmoji;

	public Image icon;
	string emojiEntryTime;
	float completionRate;

	public EmojiType emojiType;

	public void InitTile(Sprite sprite,string time,float completionRate){
		icon.sprite = sprite;
		emojiEntryTime = time;
		this.completionRate = completionRate;
	}

	public void OnClickTile(){
		OnSelectEmoji(icon.sprite,emojiEntryTime,completionRate);
	}
}
