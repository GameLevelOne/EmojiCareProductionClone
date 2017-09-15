using UnityEditor;

public class ExportAssetBundle {
	[MenuItem("Assets/Asset Bundle/ Build Asset Bundle")]
	static void  BuildAssetBundle()
	{
		BuildPipeline.BuildAssetBundles("AssetBundles",BuildAssetBundleOptions.None,BuildTarget.Android);
	}
}
