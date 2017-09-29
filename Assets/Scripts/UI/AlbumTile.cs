using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumTile : MonoBehaviour {
	public delegate void SelectExpression(FaceAnimation item);
	public static event SelectExpression OnSelectExpression;

	public FaceAnimation exprType;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickTile(){
		OnSelectExpression(exprType);
	}
}
