using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelProgressManager : MonoBehaviour {

	public Image progressBarFill;
	public Text progressText;
	public Image expressionIcon;
	public Text expressionNameText;
	public Text expressionUnlockConditionText;

	public GameObject expressionBoxPrefab;
	public RectTransform contentBox;

	int expressionTotalCount = 40;
	int tileWidth = 4;
	int tileHeight = 10;
	float expressionBoxWidth = 150f;
	float contentBoxMarginX = 100f;

	void Start(){
		InitContentBox();
	}

	void InitContentBox(){
		contentBox.sizeDelta = new Vector2(0,((float)tileHeight*expressionBoxWidth)+contentBoxMarginX);

		for(int i=0;i<tileHeight;i++){
			for(int j=0;j<tileWidth;j++){
				GameObject obj = Instantiate(expressionBoxPrefab,contentBox,false) as GameObject;
				obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-230+j*150,700-i*150);
			}
		}
	}
}
