using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elevator : Company {


	public float timeDown=2;
	public float timeUp=1f;


	public float totalTansport;

	public int id;
	public int load;
	public float speed;
	public float loadSpeed;

	private int workLineIndexCollector=0;
	public GameObject elevator;
	public GameObject elevatorPosition;
	public Text moneyTxt;
	public Text moneyStorageTxt;
	public Text levelTxr;


	public int money =0;
	public int moneyStorage=0;

	public bool goBackFlag=false;

	// Use this for initialization
	void Start () {
		setStats();
		StartCoroutine(extractMoney());
	}

	public override IEnumerator extractMoney(){
		
		while(true){
			WorkLine workline=GameManager.instance.listWorkLine[workLineIndexCollector]; 
			yield return new WaitForSeconds(timeDown);

			if(workline.workingFlag && load-(workline.workPanel.money+money)>0){
				money+=workline.workPanel.money;
				workline.workPanel.money=0;
				workline.workPanel.setLoadTxt();
				workLineIndexCollector++;
				elevator.GetComponent<ElevatorEntity>().moveToPosition(workline.elevatorPosition.transform);
			}else{
				int saveDiference=load-money;
				if(workline.workingFlag){
					workline.workPanel.money-=saveDiference;
					workline.workPanel.setLoadTxt();
					money+=saveDiference;
					changeMoneyTxt();

				}
				elevator.GetComponent<ElevatorEntity>().moveToPosition(elevatorPosition.transform);	
				yield return new WaitForSeconds(timeUp*workLineIndexCollector);

				moneyStorage+=money;
				money=0;
				workLineIndexCollector=0;
				changeMoneyStorageTxt();
			}
			changeMoneyTxt();
		}
	}

	public override void setStats(){
		int index=level-1;
		levelUpCost=DataController.instance.dataModel.elevatorLevel[index].levelUpCost;
		load=DataController.instance.dataModel.elevatorLevel[index].load;
		speed=DataController.instance.dataModel.elevatorLevel[index].speed;
		loadSpeed=DataController.instance.dataModel.elevatorLevel[index].loadSpeed;
		setLevelText();
	}

	public void changeMoneyTxt(){
		moneyTxt.text=""+money;
	}

	public void changeMoneyStorageTxt(){
		moneyStorageTxt.text=""+moneyStorage;
	}
		
	private void setLevelText(){
		levelTxr.text="Elevator\nLevel\n"+level+"\nCost "+levelUpCost;
	}

}
