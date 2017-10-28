using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProloguePopupName : BaseUI {

	public string username;

	public void OnInputName (InputField inputName)
	{
		username = inputName.text;
		PlayerData.Instance.EmojiName = username;
	}
}
