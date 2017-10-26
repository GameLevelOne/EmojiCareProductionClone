using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoodleMinigame : BaseUI {
	public GameObject doodleUI;
	public GameObject particleParentPrefab;
	public GameObject particlePrefab;
	public RectTransform rectBoard;
	public RawImage furnitureBoard;

	int width;
	int height;
	int counter = 0;
	GameObject particleParent;
	Vector3 dragPos1;
	Vector3 dragPos2;

	void OnEnable(){
		width = System.Convert.ToInt32(rectBoard.rect.width);
		height = System.Convert.ToInt32(rectBoard.rect.height);
		particleParent = Instantiate(particleParentPrefab,rectBoard.transform,false) as GameObject;
	}

	public void OnClickBack(){
		StartCoroutine(TakeScreenshot());
	}

	public void OnBeginDrag(){
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);
		dragPos1 = Camera.main.transform.InverseTransformPoint(tempMousePosition);
		counter=0;
		counter++;
	}

	public void OnDrag(){ //TODO: check counter
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);
		GameObject obj = Instantiate(particlePrefab,particleParent.transform,false) as GameObject;
		obj.transform.position = tempMousePosition;
		if(counter == 2){
			dragPos1 = tempMousePosition;
			FillSprite(dragPos2,dragPos1);
			counter = 1;
		} else if(counter == 1){
			dragPos2 = tempMousePosition;
			FillSprite(dragPos1,dragPos2);
			counter = 2;
		}
	}

	public void FillSprite(Vector3 pos1,Vector3 pos2){
		Vector3 tempPos = Vector3.zero;
		float lerpFactor = 0;
		int counter = 0;
		while(lerpFactor<1){
			tempPos = Vector3.Lerp(pos1,pos2,lerpFactor);
			lerpFactor+=0.1f;
			GameObject obj = Instantiate(particlePrefab,particleParent.transform,false) as GameObject;
			obj.transform.position = tempPos;
			counter++;
			Debug.Log("pos:"+tempPos);
		}
	}

	IEnumerator TakeScreenshot(){
		yield return new WaitForEndOfFrame();

		Vector3 boardPos = rectBoard.transform.position;
		float startX = boardPos.x - width/2 - 100;
		float startY = boardPos.y - height/2 - 100;
		Debug.Log("boardPos:"+boardPos);
		Debug.Log("startX:"+startX);
		Debug.Log("startY:"+startY);

		int newWidth = width+200;
		int newHeight = height+200;

		Texture2D tex = new Texture2D(newWidth,newHeight,TextureFormat.RGB24,false);
		tex.ReadPixels(new Rect(startX,startY,newWidth,newHeight),0,0);
		tex.Apply();

		furnitureBoard.texture = tex;

		Destroy(particleParent);
	}
	
}
