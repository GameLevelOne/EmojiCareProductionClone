using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Emoji_",menuName = "Cards/Emoji",order = 1)]
public class EmojiSO : ScriptableObject {
	public string emojiName;

	public int maxHealth;
	public int maxHunger;
	public int maxHygiene;
	public int maxHappiness;

//	public int modHealth;
//	public int modHunger;
//	public int modHygiene;
//	public int modHappiness;

	public int statDecreasePerTick;

	public bool isUnlocked;

	public int emojiPrice;

	public Sprite emojiSelectionIcon;
}
