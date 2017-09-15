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
	}
	#endregion

	#region attribute
	AssetBundle bundle;
	#endregion

	void OnEnable()
	{
		GameSparkManager.Instance.OnURLResponse += OnURLResponse;
	}

	void OnDisable()
	{
		GameSparkManager.Instance.OnURLResponse -= OnURLResponse;
	}


	public void OnURLResponse(string URL, string emojiType)
	{
		StartCoroutine((LoadBundle(URL,emojiType)));
	}

	IEnumerator LoadBundle(string URL, string emojiType)
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
				Emoji.Instance.InitEmojiObject((GameObject) bundle.LoadAsset(emojiType));
			}
		}
	}
}