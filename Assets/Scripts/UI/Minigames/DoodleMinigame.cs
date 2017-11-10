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
	int particleSize = 5;
	GameObject particleParent;
	Vector3 dragPos1;
	Vector3 dragPos2;

	void OnEnable(){
		width = System.Convert.ToInt32(rectBoard.rect.width);
		height = System.Convert.ToInt32(rectBoard.rect.height);
		particleParent = Instantiate(particleParentPrefab,rectBoard.transform,false) as GameObject;
	}

	public void OnClickBack(){
		PlayerData.Instance.PlayerEmoji.playerInput.OnDoodleMinigameDone ();
		StartCoroutine(TakeScreenshot());
	}

	public void OnBeginDrag(){
		Vector3 tempMousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);
		dragPos1 = Camera.main.transform.InverseTransformPoint(tempMousePosition);
		counter=0;
		counter++;
	}

	public void OnDrag ()
	{ 
		Vector3 tempMousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
		tempMousePosition = Camera.main.transform.InverseTransformPoint (tempMousePosition);
		//if (rectBoard.GetComponent<Blackboard> ().insideBoard) {
			GameObject obj = Instantiate (particlePrefab, particleParent.transform, false) as GameObject;
			obj.transform.position = tempMousePosition;
			if (counter == 2) {
				dragPos1 = tempMousePosition;
				FillSprite (dragPos2, dragPos1);
				counter = 1;
			} else if (counter == 1) {
				dragPos2 = tempMousePosition;
				FillSprite (dragPos1, dragPos2);
				counter = 2;
			}
		//}
	}

	public void EndDrag(){
		//rectBoard.GetComponent<Blackboard> ().insideBoard = false;
	}

	public void FillSprite(Vector3 pos1,Vector3 pos2){
		Vector3 tempPos = Vector3.zero;
		float lerpFactor = 0;
		float deltaLerp = (float)particleSize / GetDeltaPos (pos1, pos2);
		while(lerpFactor<1){
			tempPos = GetPoint (pos1, pos2, lerpFactor);
			lerpFactor += deltaLerp;;
			GameObject obj = Instantiate(particlePrefab,particleParent.transform,false) as GameObject;
			obj.transform.position = tempPos;
		}
	}

	Vector3 GetPoint(Vector3 p0,Vector3 p2,float t){
		float oneMinusT = 1f - t;
		Vector3 p1 = Vector3.Lerp (p0, p2, 0.5f);
		return ((oneMinusT * oneMinusT * p0) + (2 * oneMinusT * t * p1) + (t * t * p2));
	}

	float GetDeltaPos(Vector3 pos1,Vector3 pos2){
		float posX = pos2.x - pos1.x;
		float posY = pos2.y - pos1.y;
		return(Mathf.Sqrt(posX * posX + posY * posY));
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
