using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storage : Company {

	public Elevator elevator;
	public Text levelTxr;

	public int id;
	public int carriers;
	public int loadCarrier;
	public float speed;
	public float walkSpeed;
	public float time=2;

	// Use this for initialization
	void Start () {
		addWorker();
		setStats();
		StartCoroutine(extractMoney());
	}

	public override IEnumerator extractMoney(){
		while(true){
			yield return new WaitForSeconds(time);
			worker.GetComponent<Entity>().moveToPosition(workerPosEnd.transform);
			int diference=0;
			if(elevator.moneyStorage-loadCarrier>0){
				elevator.moneyStorage-=loadCarrier;
				diference=loadCarrier;
			}else{
				diference=elevator.moneyStorage;
				elevator.moneyStorage=0;
			}
			elevator.changeMoneyStorageTxt();
			yield return new WaitForSeconds(time);
			worker.GetComponent<Entity>().moveToPosition(workerPosStart.transform);
			GameManager.instance.Money+=diference;
			movingFlag=false;
		}

	}

	public override void setStats(){
		int index=level-1;
		levelUpCost=DataController.instance.dataModel.storageLevel[index].levelUpCost;
		carriers=DataController.instance.dataModel.storageLevel[index].carriers;
		loadCarrier=DataController.instance.dataModel.storageLevel[index].loadCarrier;
		speed=DataController.instance.dataModel.storageLevel[index].speed;
		walkSpeed=DataController.instance.dataModel.storageLevel[index].walkSpeed;
		setLevelText();
	}

	private void setLevelText(){
		levelTxr.text="Elevator\nLevel\n"+level+"\nCost "+levelUpCost;
	}
}
