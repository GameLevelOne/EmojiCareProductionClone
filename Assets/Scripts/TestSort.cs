using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSort : MonoBehaviour {

	int[] a = new int[5]{33,161,200,5,43};
	int temp=0;

	void Start(){
		for(int i=1;i<5;i++){
			for(int j=0;j<i;j++){
				if(a[i]<a[j]){
					temp=a[i];
					a[i]=a[j];
					a[j]=temp;
				}
			}
		}

		for(int i=0;i<5;i++){
			Debug.Log(a[i]);
		}
	}
}
