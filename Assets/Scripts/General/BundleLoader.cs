using System.Collections;
using UnityEngine;

public class BundleLoader : MonoBehaviour {
	#region singleton
	private static BundleLoader instance = null;
	public static BundleLoader Instance {
		get{ return instance;}
	}

	void Awake()
	{
		if(instance != null && instance != this) Destroy(this.gameObject);
		else instance = this;

//		DontDestroyOnLoad(gameObject);
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region attribute
	AssetBundle bundle;
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region public modules
	public void DoLoadBundle(string URL, EmojiType emojiType)
	{
		StartCoroutine((LoadBundle(URL,emojiType)));
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
	#region coroutines
	IEnumerator LoadBundle(string URL, EmojiType emojiType)
	{
		WWW download = WWW.LoadFromCacheOrDownload(URL,0);
		yield return download;

		if(download.error != null){
			throw new System.Exception("Error: "+download.error);
		}else{
			bundle = download.assetBundle;
			if(bundle == null){
				throw new System.Exception("Error: Bundle is null");
			}else{
//				Emoji.Instance.InitEmojiObject((GameObject) bundle.LoadAsset(EmojiObjectName.EMOJI_OBJECTS[(int)emojiType]));
//				PlayerData.Instance.
				PlayerData.Instance.InitPlayerEmoji((GameObject) bundle.LoadAsset(EmojiObjectName.EMOJI_OBJECTS[(int)emojiType]));
			}
		}
	}
	#endregion
//-------------------------------------------------------------------------------------------------------------------------------------------------
}