using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour {

	public Elevator elevator;
	public float time=1;
	private int maxExtraction=100;

	// Use this for initialization
	void Start () {
		StartCoroutine(takeMoney());
	}
	
	IEnumerator takeMoney(){
		while(true){
			yield return new WaitForSeconds(time);
			int diference=0;
			if(elevator.moneyStorage-maxExtraction>0){
				elevator.moneyStorage-=maxExtraction;
				diference=maxExtraction;
			}else{
				diference=elevator.moneyStorage;
				elevator.moneyStorage=0;
			}
			elevator.changeMoneyStorageTxt();
			GameManager.instance.Money+=diference;
		}

	}
}
