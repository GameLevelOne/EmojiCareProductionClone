using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICookbook : BaseUI {
	public GameObject flipPageButton;
	public GameObject flippedPage;
	public GameObject[] pages;

	int totalPage = 3;
	int currentPage = 1;
	int counter=1;

	void OnEnable(){
		SetActivePage (1);
	}

	public void OnClickNext(){
		if(currentPage == totalPage){
			currentPage = totalPage;
		} else{
			currentPage++;
		}
		SetActivePage (currentPage);
	}

	public void OnClickPrev(){
		if(currentPage == 1){
			currentPage = 1;
		} else{
			currentPage--;
		}
		SetActivePage (currentPage);
	}

	void SetActivePage(int page){
		for(int i=0;i<pages.Length;i++){
			if(i == (page-1)){
				pages[i].SetActive (true);
			} else{
				pages[i].SetActive (false);
			}
		}
		if(page == totalPage){
			flipPageButton.SetActive (false);
		} else{
			flipPageButton.SetActive (true);
		}
	}
}
