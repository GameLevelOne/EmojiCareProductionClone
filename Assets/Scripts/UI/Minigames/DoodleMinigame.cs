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
	}

	public void OnClickBack(){
		//PlayerData.Instance.PlayerEmoji.playerInput.OnDoodleMinigameDone ();
		StartCoroutine(TakeScreenshot());
	}

	IEnumerator TakeScreenshot(){
		yield return new WaitForEndOfFrame();

		Vector3 boardPos = rectBoard.transform.position;
		float startX = boardPos.x - width/2;
		float startY = boardPos.y - height/2;
//		float startX = boardPos.x - width/2;
//		float startY = boardPos.y - height/2;
		Debug.Log("boardPos:"+boardPos);
		Debug.Log("startX:"+startX);
		Debug.Log("startY:"+startY);

//		int newWidth = width-107;
//		int newHeight = height-103;

		int newWidth = width;
		int newHeight = height;

		Texture2D tex = new Texture2D(newWidth,newHeight,TextureFormat.RGB24,false);
		//tex.ReadPixels(new Rect(startX,startY,newWidth,newHeight),0,0);
		tex.ReadPixels (new Rect (100,100,Screen.width,Screen.height), 0, 0);
//		tex.ReadPixels(new Rect(286,508,newWidth,newHeight),0,0);
		tex.Apply();

		furnitureBoard.sprite = Sprite.Create (tex, new Rect (0f, 0f, tex.width, tex.height), new Vector2 (0.5f, 0.5f));
		furnitureBoard.transform.localScale = new Vector3 (0.34f, 0.34f, 1);

		Destroy(particleParent);
	}
	
}
