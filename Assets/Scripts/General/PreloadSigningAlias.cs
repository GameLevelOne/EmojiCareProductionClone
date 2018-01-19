#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

[InitializeOnLoad]
public class PreloadSigningAlias
{

	static PreloadSigningAlias ()
	{
		PlayerSettings.Android.keystorePass = "emojigl1";
		PlayerSettings.Android.keyaliasName = "emoji";
		PlayerSettings.Android.keyaliasPass = "emojigl1";
	}

}

#endif