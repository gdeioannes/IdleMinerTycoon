using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Company {

	public Elevator elevator;
	public GameObject worker;
	public GameObject workerPosStart;
	public GameObject workerPosEnd;

	public float time=2;
	private int maxExtraction=100;

	// Use this for initialization
	void Start () {
		addWorker();
		addBoss();
		StartCoroutine(takeMoney());
	}

	private void addWorker(){
		worker=Instantiate(worker);
		worker.transform.SetParent(this.transform.parent);
		worker.transform.position=workerPosStart.transform.position;
		worker.transform.localScale=new Vector3(1,1,1);
	}
	
	IEnumerator takeMoney(){
		while(true){
			
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
			worker.GetComponent<Entity>().moveToPosition(workerPosStart.transform);
			elevator.changeMoneyStorageTxt();
			GameManager.instance.Money+=diference;
		}

	}
}
