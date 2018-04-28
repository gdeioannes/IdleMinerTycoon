using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elevator : Company,Action {

	public int id;
	public int level;
	public float timeDown=2;
	public float timeUp=1f;
	private int moneyElevatorMax=300;

	//Set -1 to be ready to start like a flag
	private int workLineIndexCollector=-1;
	public GameObject elevator;
	public GameObject elevatorPosition;
	public Text moneyTxt;
	public Text moneyStorageTxt;


	private int money =0;
	public int moneyStorage=0;

	public bool goBackFlag=false;

	// Use this for initialization
	void Start () {
		addBoss();
	}

	public void callAction(){
		
		if(workLineIndexCollector==-1 && !bossActiveFlag){
			workLineIndexCollector=0;
			StartCoroutine(pickUpGold());
		}
	}

	public IEnumerator bossTakeMoney(){
		bossActiveFlag=true;
		while(true){
			yield return StartCoroutine( pickUpGold());
			workLineIndexCollector=0;
		}
	}

	IEnumerator pickUpGold(){
		
		while(workLineIndexCollector>=0){
			WorkLine workline=GameManager.instance.listWorkLine[workLineIndexCollector]; 
			yield return new WaitForSeconds(timeDown);

			if(workline.working && moneyElevatorMax-(workline.workPanel.money+money)>0){
				money+=workline.workPanel.money;
				workline.workPanel.money=0;
				workline.workPanel.setLoadTxt();
				workLineIndexCollector++;
				Debug.Log("Elevator Moving");
				elevator.GetComponent<ElevatorEntity>().moveToPosition(workline.elevatorPosition.transform);
			}else{
				int saveDiference=moneyElevatorMax-money;
				if(workline.working){
					workline.workPanel.money-=saveDiference;
					workline.workPanel.setLoadTxt();
					money+=saveDiference;
					changeMoneyTxt();

				}
				elevator.GetComponent<ElevatorEntity>().moveToPosition(elevatorPosition.transform);	
				yield return new WaitForSeconds(timeUp*workLineIndexCollector);

				moneyStorage+=money;
				money=0;
				workLineIndexCollector=-1;
				changeMoneyStorageTxt();
			}
			changeMoneyTxt();
		}
	}

	public void changeMoneyTxt(){
		moneyTxt.text="ID "+workLineIndexCollector+" "+money;
	}

	public void changeMoneyStorageTxt(){
		moneyStorageTxt.text=""+moneyStorage;
	}

}
