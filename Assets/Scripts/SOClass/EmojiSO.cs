using UnityEngine;

public enum EmojiType{
	Emoji,
	Gumiji,
	Spookiji,
	Chocoji,

	Watermeji,
	Toothiji,
	Nanaji,
	Boneji,
	Moeji,
	Takoji
}

[CreateAssetMenu(fileName = "Emoji_",menuName = "Cards/EmojiSOData",order = 1)]
public class EmojiSO : ScriptableObject {
	public EmojiType emojiType;
	public int emojiPrice;
	public float maxStatsPoint = 100f;
	public float statsTick = 1f;
	public Sprite emojiSelectionIcon;
}