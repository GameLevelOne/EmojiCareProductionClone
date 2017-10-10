using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpressionIcons : MonoBehaviour {

	public Sprite[] emojiExpressionIcons = new Sprite[40];

	public Sprite GetExpressionIcon(EmojiType emojiType, int expression){
		if(emojiType == EmojiType.Emoji){
			return emojiExpressionIcons[expression];
		}
		else return emojiExpressionIcons[0]; //temp default
	}
}
