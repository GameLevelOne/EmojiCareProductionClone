using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpressionIcons : MonoBehaviour {

	public Sprite[] emojiExpressionIcons;
	public Sprite[] emojiCamoExpressionIcons;
	public Sprite[] emojiAstronautExpressionIcons;
	public Sprite[] emojiButterflyExpressionIcons;
	public Sprite[] emojiDogExpressionIcons;
	public Sprite[] emojiSlothExpressionIcons;
	public Sprite[] emojiTomatoExpressionIcons;
	public Sprite[] gumijiExpressionIcons;
	public string[] emojiExpressionUnlockCondition;

	public Sprite GetExpressionIcon(EmojiType emojiType, int expression){
//		if(emojiType == EmojiType.Emoji){
//			return emojiExpressionIcons[expression];
//		} else if(emojiType == EmojiType.EmojiCamo){
//			return emojiCamoExpressionIcons[expression];
//		} else if(emojiType == EmojiType.EmojiAstronaut){
//			return emojiAstronautExpressionIcons[expression];
//		} else if(emojiType == EmojiType.EmojiButterfly){
//			return emojiButterflyExpressionIcons[expression];
//		} else if(emojiType == EmojiType.EmojiDog){
//			return emojiDogExpressionIcons[expression];
//		} else if(emojiType == EmojiType.EmojiSloth){
//			return emojiSlothExpressionIcons[expression];
//		} else if(emojiType == EmojiType.EmojiTomato){
//			return emojiTomatoExpressionIcons[expression];
//		} else if(emojiType == EmojiType.Gumiji){
//			return gumijiExpressionIcons[expression];
//		}
//		else return emojiExpressionIcons[0]; //temp default
		return emojiExpressionIcons[expression];
	}

	public string GetExpressionName(EmojiType emojiType, int expression){
		return emojiExpressionIcons[expression].name.Substring(3);
	}

	public string GetExpressionUnlockCondition(EmojiType emojiType, int expression){
		return emojiExpressionUnlockCondition[expression];
	}
}
