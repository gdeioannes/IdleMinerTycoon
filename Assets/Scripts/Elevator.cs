using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elevator : MonoBehaviour {

	public int id;
	public int level;
	public float timeDown=2;
	public float timeUp=1f;
	public int workLineIndexCollector=0;
	public GameObject elevator;
	private int moneyElevatorMax=300;
	public Text moneyTxt;
	private int money =0;
	public Text moneyStorageTxt;
	public int moneyStorage=0;
	public bool goBackFlag=false;

	// Use this for initialization
	void Start () {
		StartCoroutine(pickUpGold());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator pickUpGold(){
		while(true){
			
			yield return new WaitForSeconds(timeDown);
			WorkLine workline=GameManager.instance.listWorkLine[workLineIndexCollector]; 
			Debug.Log(moneyElevatorMax-(workline.workPanel.money+money));
			if(workline.working && moneyElevatorMax-(workline.workPanel.money+money)>0){
				Debug.Log("Go "+workLineIndexCollector);
				money+=workline.workPanel.money;
				workline.workPanel.money=0;
				workLineIndexCollector++;
			}else{
				int saveDiference=moneyElevatorMax-money;
				if(workline.working){
					workline.workPanel.money-=saveDiference;
					money+=saveDiference;
					changeMoneyTxt();
				}
				Debug.Log("Back "+workLineIndexCollector);
				yield return new WaitForSeconds(timeUp*workLineIndexCollector);
				moneyStorage+=money;
				money=0;
				workLineIndexCollector=0;
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
