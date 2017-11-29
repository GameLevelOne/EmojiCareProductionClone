using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksMinigame : BaseUI {
	public Transform brickArea;
 	public GameObject brickParentPrefab;
	public GameObject[] bricksPrefab; //rect1,rect2,rect3,circle,triangle,arc
	GameObject brickParent;
	int brickAmount = 8;

	public override void InitUI ()
	{
		brickParent = Instantiate (brickParentPrefab, brickArea, false) as GameObject;
		GameObject obj = null;
		for(int i=0;i<brickAmount;i++){
			if(i>=0 && i<=5){
				obj = Instantiate (bricksPrefab[i], brickParent.transform, false) as GameObject;
			}else{
				obj = Instantiate (bricksPrefab[Random.Range(0,bricksPrefab.Length)], brickParent.transform, false) as GameObject;
			}
			obj.transform.localPosition = new Vector3 (Random.Range (-210, 222), -261, 0);
		}
	}


	public void OnClickBack(){
		Destroy (brickParent);
		PlayerData.Instance.PlayerEmoji.playerInput.OnBlocksMinigameDone ();
		base.CloseUI (this.gameObject);
	}
}
