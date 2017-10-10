using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiIcons : MonoBehaviour {

	public Sprite[] emojiIcons = new Sprite[10];

	public Sprite GetEmojiIcon(EmojiType emojiType){
		return emojiIcons[(int)emojiType];
	}
}
