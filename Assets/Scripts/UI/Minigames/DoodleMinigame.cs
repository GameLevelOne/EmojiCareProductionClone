using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoodleMinigame : BaseUI {
	public GameObject doodleUI;
	public RectTransform rectBoard;
	public RawImage furnitureBoard;
	GameObject particleParent;
	int width;
	int height;

	void OnEnable(){
		width = System.Convert.ToInt32(rectBoard.rect.width);
		height = System.Convert.ToInt32(rectBoard.rect.height);
	}

	public void OnClickBack(){
		PlayerData.Instance.PlayerEmoji.playerInput.OnDoodleMinigameDone ();
		StartCoroutine(TakeScreenshot());
	}

	IEnumerator TakeScreenshot(){
		yield return new WaitForEndOfFrame();

		Vector3 boardPos = rectBoard.transform.position;
//		float startX = boardPos.x - width/2 - 100;
//		float startY = boardPos.y - height/2 - 100;
		float startX = boardPos.x - width/2;
		float startY = boardPos.y - height/2;
		Debug.Log("boardPos:"+boardPos);
		Debug.Log("startX:"+startX);
		Debug.Log("startY:"+startY);

//		int newWidth = width+200;
//		int newHeight = height+200;
		int newWidth = width;
		int newHeight = height;

		Texture2D tex = new Texture2D(newWidth,newHeight,TextureFormat.RGB24,false);
		tex.ReadPixels(new Rect(startX,startY,newWidth,newHeight),0,0);
		tex.Apply();

		furnitureBoard.texture = tex;

		Destroy(particleParent);
	}
	
}
