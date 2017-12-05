using System.Collections;
using UnityEngine;

public class LivingRoom : BaseRoom {
	[Header("Livingroom Attributes")]
	//public HatStand hatStand;
	public GameObject album;
	public bool flagEmojiMakeOver = false;

	public void Init()
	{
		if(PlayerData.Instance.EmojiAlbumData.Count <= 0) album.SetActive(false);
	}

	public void EnterEmojiMakeOver()
	{
		flagEmojiMakeOver = true;
	}

	public void ExitEmojiMakeOver()
	{
		flagEmojiMakeOver = false;
	}



}
