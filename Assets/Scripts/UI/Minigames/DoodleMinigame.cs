using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoodleMinigame : BaseUI {
	public GameObject doodleUI;
	public RectTransform rectBoard;
	public SpriteRenderer furnitureBoard;
	GameObject particleParent;
	int width;
	int height;

	void OnEnable(){
		width = System.Convert.ToInt32(rectBoard.rect.width);
		height = System.Convert.ToInt32(rectBoard.rect.height);
		Debug.Log ("width:" + width);
		Debug.Log ("height:" + height);
		Debug.Log ("screenwidth:" + Screen.width);
		Debug.Log ("screenheight:" + Screen.height);
	}

	public void OnClickBack(){
		PlayerData.Instance.PlayerEmoji.playerInput.OnDoodleMinigameDone ();
		StartCoroutine(TakeScreenshot());
	}

	IEnumerator TakeScreenshot(){
		yield return new WaitForEndOfFrame();

		Texture2D tex = new Texture2D(width,height,TextureFormat.RGB24,false);

		float newWidth = Screen.width * width / 720;
		float newHeight = Screen.height * height / 1280 - 10;

		tex.ReadPixels (new Rect (45f, 170f, newWidth, newHeight), 0, 0); //calculate manually,Screen.width/height = 342/608
		tex.Apply();

		furnitureBoard.sprite = Sprite.Create (tex, new Rect (0f, 0f, newWidth, newHeight), new Vector2 (0.5f, 0.5f));
		furnitureBoard.transform.localScale = new Vector3 (0.84f, 0.91f, 1);

		Destroy(particleParent);
	}
	
}

