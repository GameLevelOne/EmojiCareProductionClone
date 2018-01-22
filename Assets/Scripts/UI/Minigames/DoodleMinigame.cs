using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoodleMinigame : BaseUI {
	public Sprite[] randomPictures;
	public GameObject buttonHotkey;
	public GameObject doodleUI;
	public RectTransform rectBoard;
	public SpriteRenderer furnitureBoard;
	GameObject particleParent;
	int width;
	int height;

	void OnEnable(){
		width = System.Convert.ToInt32(rectBoard.rect.width);
		height = System.Convert.ToInt32(rectBoard.rect.height);
		buttonHotkey.SetActive (false);
	}

	public void OnClickBack(){
		PlayerData.Instance.PlayerEmoji.playerInput.OnDoodleMinigameDone ();
		//StartCoroutine(TakeScreenshot());
		buttonHotkey.SetActive (true);
		PlaceRandomPictures ();
	}

	public void PlaceRandomPictures(){
		int temp = Random.Range (0, randomPictures.Length);
		furnitureBoard.sprite = randomPictures [temp];
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

