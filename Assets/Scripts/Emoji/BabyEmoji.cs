using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyEmoji : Emoji {
	[Header("Baby Emoji Attributes")]
	public GameObject emojiAdultObject;
	public SceneMainManager sceneManager;
	public void GrowToJuvenille()
	{
		Vector3 scaleMedium = new Vector3(emojiGrowth.scaleMedium,emojiGrowth.scaleMedium,1f);
		SetBodyCurrentScale(scaleMedium);
	}

	public void GrowToAdult()
	{
		//MODULE HERE
		Destroy(gameObject);
		PlayerData.Instance.InitPlayerEmoji(emojiAdultObject);
		sceneManager.EmojiGrowToAdult();
	}
}
