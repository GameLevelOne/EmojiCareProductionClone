using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpressionIcons : MonoBehaviour {

	public Sprite[] emojiExpressionIcons = new Sprite[40];
	public string[] emojiExpressionUnlockCondition = new string[40];

	public Sprite GetExpressionIcon(EmojiType emojiType, int expression){
		if(emojiType == EmojiType.Emoji){
			return emojiExpressionIcons[expression-1];
		}
		else return emojiExpressionIcons[0]; //temp default
	}

	public string GetExpressionName(EmojiType emojiType, int expression){
		if(emojiType == EmojiType.Emoji){
			return emojiExpressionIcons[expression].name.Substring(3);
		}
		else return emojiExpressionIcons[0].name.Substring(3); //temp default
	}

	public string GetExpressionUnlockCondition(EmojiType emojiType, int expression){
		if(emojiType == EmojiType.Emoji){
			return emojiExpressionUnlockCondition[expression];
		}
		else return emojiExpressionUnlockCondition[0]; //temp default
	}
}
