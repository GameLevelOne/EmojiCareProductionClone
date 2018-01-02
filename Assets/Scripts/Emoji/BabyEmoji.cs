using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyEmoji : Emoji {
	[Header("Baby Emoji Attributes")]
	public GameObject emojiAdultObject;
	public SceneMainManager sceneManager;

	public void GrowToJuvenille()
	{
		//sceneManager.celebrationManager.popupEmojiGrowth.SetDisplay (false);
		Vector3 scaleMedium = new Vector3(emojiGrowth.scaleMedium,emojiGrowth.scaleMedium,1f);
		SetBodyCurrentScale(scaleMedium);
		PlayerData.Instance.GardenField1 = 1;
		sceneManager.roomController.soil.gardenFields [1].InitPlayerProgressToGardenField ();
	}

	public void GrowToAdult()
	{
		//MODULE HERE
        //sceneManager.celebrationManager.popupEmojiGrowth.SetDisplay (true);
		sceneManager.EmojiGrowToAdult();
	}
}
