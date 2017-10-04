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

	public float maxStatValue = 100f;
	[Header(" ")]
	public float hungerStart = 50f;
	public float hygeneStart = 50f;
	public float happinessStart = 50f;
	public float staminaStart = 50f;
	public float healthStart = 100f;
	[Header(" ")]
	public float hungerModifier = -1f;
	public float hygeneModifier = -1f;
	public float happinessModifier = -1f;
	public float staminaModifier = -1f;
	public float healthModifier = -1f;
}