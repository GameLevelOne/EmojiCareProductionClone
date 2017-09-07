using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Emoji_",menuName = "Cards/Emoji",order = 1)]
public class EmojiSO : ScriptableObject {
	public string emojiName;

	public float maxHunger;
	public float maxHygiene;
	public float maxHappiness;
	public float maxStamina;
	public float maxHealth;

//	public int modHealth;
//	public int modHunger;
//	public int modHygiene;
//	public int modHappiness;

	public int statDecreasePerTick = 5;

	public bool isUnlocked;

	public int emojiPrice;

	public Sprite emojiSelectionIcon;
}
