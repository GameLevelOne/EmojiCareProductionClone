using System.Collections;
using UnityEngine;

public class LivingRoom : BaseRoom {
	[Header("Livingroom Attributes")]
	//public HatStand hatStand;
	public bool flagEmojiMakeOver = false;

	public void Init()
	{

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
