using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICookbook : MonoBehaviour {
	public GameObject flipPageButton;
	public GameObject flippedPage;

	int totalPage = 3;
	int currentPage = 1;
	int counter=0;

	public void OnClickNext(){
		if(currentPage == (totalPage-1)){
			flipPageButton.SetActive (false);
		} else{
			flipPageButton.SetActive (true);
		}
		counter++;
	}

	public void OnClickPrev(){
		if(currentPage == 2){
			flipPageButton.SetActive (false);
		} else{
			flipPageButton.SetActive (true);
		}
		counter--;
	}
}
