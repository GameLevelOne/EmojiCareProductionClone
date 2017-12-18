using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksMinigame : BaseUI {
	public List<Toy> toysObj = new List<Toy> ();
	public Transform brickArea;
 	public GameObject brickParentPrefab;
	public GameObject[] bricksPrefab; //rect1,rect2,rect3,circle,triangle,arc
	GameObject brickParent;
	int brickAmount = 8;

	public override void InitUI ()
	{
		PlayerData.Instance.PlayerEmoji.thisRigidbody.simulated = false;
		PlayerData.Instance.PlayerEmoji.body.thisCollider.enabled = false;
		EnableRoomToys (false);
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

		EnableRoomToys (true);
		base.CloseUI (this.gameObject);
	}

	void EnableRoomToys(bool isShowing){
		
		PlayerData.Instance.PlayerEmoji.transform.localPosition = new Vector3 (-1.6f, 0.6f, -2f);
		PlayerData.Instance.PlayerEmoji.thisRigidbody.simulated = true;
		PlayerData.Instance.PlayerEmoji.body.thisCollider.enabled = true;
		foreach(Toy obj in toysObj){
			obj.gameObject.SetActive (isShowing);
		}

		PlayerData.Instance.PlayerEmoji.playerInput.OnBlocksMinigameDone ();
	}
}
