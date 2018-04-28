using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Company,Action {

	public Elevator elevator;

	public float time=2;
	private int maxExtraction=100;

	// Use this for initialization
	void Start () {
		addWorker();
		addBoss();
	}

	public void callAction(){
		if(!movingFlag && !bossActiveFlag){
			movingFlag=true;
			StartCoroutine(takeMoney());
		}
	}

	public IEnumerator bossTakeMoney(){
		bossActiveFlag=true;
		while(true){
			yield return StartCoroutine( takeMoney());
		}
	}

	IEnumerator takeMoney(){
			Debug.Log("Take Money Storage");
			yield return new WaitForSeconds(time);
			worker.GetComponent<Entity>().moveToPosition(workerPosEnd.transform);
			int diference=0;
			if(elevator.moneyStorage-maxExtraction>0){
				elevator.moneyStorage-=maxExtraction;
				diference=maxExtraction;
			}else{
				diference=elevator.moneyStorage;
				elevator.moneyStorage=0;
			}
			yield return new WaitForSeconds(time);
			elevator.changeMoneyStorageTxt();
			worker.GetComponent<Entity>().moveToPosition(workerPosStart.transform);
			GameManager.instance.Money+=diference;
			movingFlag=false;

	}
}
