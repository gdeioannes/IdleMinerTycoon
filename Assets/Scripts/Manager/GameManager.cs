using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GameManager : MonoBehaviour {

	public static GameManager instance;

	//Currency
	private int superMoney=300;
	private int money=300;

	//Stage Object nedded
	public GameObject content;

	//Prefab atached
	public GameObject topWorkLine;
	public GameObject workLine;
	//List of the work lines for general use
	public List<WorkLine> listWorkLine;

	// Use this for initialization
	void Awake () {
		if(instance==null){
			instance=this;
		}
	}

	void Start(){
		UIManager.instance.updateMoney();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void addTopWorkLinePanel (){
		//Add Top panel to the game
		topWorkLine = Instantiate (topWorkLine);
		topWorkLine.transform.SetParent (content.gameObject.transform);
		//Issue with the scale and position, need to fix Gabriel
		topWorkLine.gameObject.transform.localScale = new Vector3 (1, 1, 1);
		topWorkLine.GetComponent<RectTransform> ().localPosition = new Vector3 (1, 1, 1);
	}

	void setTopWorkLineData (SaveData userData)
	{
		topWorkLine.GetComponent<TopWorkLine> ().storage.level = userData.storageData.level;
		topWorkLine.GetComponent<TopWorkLine> ().elevator.level = userData.elevatorData.level;

		topWorkLine.GetComponent<TopWorkLine> ().elevator.moneyStorage = userData.elevatorData.moneyStorage;
		topWorkLine.GetComponent<TopWorkLine> ().elevator.setStats ();
		topWorkLine.GetComponent<TopWorkLine> ().storage.setStats ();

		topWorkLine.GetComponent<TopWorkLine> ().elevator.changeMoneyStorageTxt ();
	}

	void deleteWorkLineObjs (){
		//Empty the contect in case forgot to delete assets
		foreach (Transform child in content.transform) {
			GameObject.Destroy (child.gameObject);
		}
	}
		
	public void stageCreator(){

		deleteWorkLineObjs ();
		addTopWorkLinePanel ();

		for (int i = 0; i < DataController.instance.dataModel.stageDeepLevel.Count; i++) {

			GameObject instanceWorkLine = Instantiate(workLine) ;
			WorkLine workLineObj=instanceWorkLine.GetComponent<WorkLine>();
			DataModel dataModel=DataController.instance.dataModel;

			instanceWorkLine.transform.SetParent(content.gameObject.transform);
			workLineObj.workLineCostMoney=dataModel.stageDeepLevel[i].workLineCostMoney;
			workLineObj.workLineCostSuperMoney=dataModel.stageDeepLevel[i].workLineCostSuperMoney;
			workLineObj.id=dataModel.stageDeepLevel[i].id;
			workLineObj.workPanel.deep=dataModel.stageDeepLevel[i].deept;
			workLineObj.workPanel.setWorkLineNumberTxt();

			//Issue with the scale and position, need to fix Gabriel
			instanceWorkLine.gameObject.transform.localScale=new Vector3(1,1,1);
			instanceWorkLine.GetComponent<RectTransform>().localPosition=new Vector3(1,1,1);

			workLineObj.workPanel.setStats();
			workLineObj.workPanel.setStatsText();
			workLineObj.workPanel.setLoadTxt();
			if(i==0){
				workLineObj.showBuyPanel();
			}else{
				workLineObj.hidePanels();
			}
			listWorkLine.Add(workLineObj);
		}
	}

	public void stageCreatorUserData(){

		deleteWorkLineObjs ();
		addTopWorkLinePanel ();
		SaveData userData=DataController.instance.userData;

		money=userData.money;
		superMoney=userData.superMoney;

		//To only show one buy panel
		bool lineActiveFlag=true;
		for (int i = 0; i < DataController.instance.dataModel.stageDeepLevel.Count; i++) {

			GameObject instanceWorkLine = Instantiate(workLine) ;
			WorkLine workLineObj=instanceWorkLine.GetComponent<WorkLine>();
			DataModel dataModel=DataController.instance.dataModel;
		

			instanceWorkLine.transform.SetParent(content.gameObject.transform);
			workLineObj.workLineCostMoney=dataModel.stageDeepLevel[i].workLineCostMoney;
			workLineObj.workLineCostSuperMoney=dataModel.stageDeepLevel[i].workLineCostSuperMoney;
			workLineObj.id=dataModel.stageDeepLevel[i].id;
			workLineObj.workPanel.deep=dataModel.stageDeepLevel[i].deept;
			workLineObj.workPanel.setWorkLineNumberTxt();

			workLineObj.workPanel.level=userData.workPanelData[i].level;
			workLineObj.workPanel.lineActiveFlag=userData.workPanelData[i].lineActiveFlag;
			workLineObj.workPanel.money=userData.workPanelData[i].money;

			workLineObj.workingFlag=userData.workPanelData[i].lineActiveFlag;

			setTopWorkLineData (userData);

			workLineObj.workPanel.setStats();
			workLineObj.workPanel.setStatsText();
			workLineObj.workPanel.setLoadTxt();

			//Issue with the scale and position, need to fix Gabriel
			instanceWorkLine.gameObject.transform.localScale=new Vector3(1,1,1);
			instanceWorkLine.GetComponent<RectTransform>().localPosition=new Vector3(1,1,1);

			if(userData.workPanelData[i].lineActiveFlag){
				workLineObj.showWorkPanel();
			}else if(lineActiveFlag){
				workLineObj.showBuyPanel();
				lineActiveFlag=false;
			}else{
				workLineObj.hidePanels();
			}

			listWorkLine.Add(workLineObj);
		}
		calculateOffGameMoney();
	}

	void addUserData (int i, WorkLine workLineObj){
		SaveData userData=DataController.instance.userData;
		if(DataController.instance.userData.workPanelData [i]!=null){
			if (DataController.instance.userData.workPanelData [i].level != 1) {
				workLineObj.workPanel.level = userData.workPanelData [i].level;
				workLineObj.workPanel.money = userData.workPanelData [i].money;
			}
		}
	}

	private void calculateOffGameMoney(){

		//Need to calculate in function off all variables
		DateTime now=DateTime.Now;
		DateTime lastLog=DateTime.Parse(DataController.instance.userData.time);
		int diferenceSg =(now-lastLog).Seconds;
		int offLineMoney=0;
		for (int i = 0; i < listWorkLine.Count; i++) {
			WorkPanel workPanel=listWorkLine[i].workPanel;
			if(workPanel.lineActiveFlag){
				offLineMoney+=Mathf.RoundToInt((diferenceSg/(workPanel.timeToDig*2))*workPanel.miners*workPanel.workerCapacities);
			}
		}
		Debug.Log("OFF Money "+offLineMoney);
		money+=offLineMoney;
	}

	public int Money {
		get {
			return money;
		}
		set {
			money = value;
			UIManager.instance.updateMoney();
		}
	}

	public int SuperMoney {
		get {
			return superMoney;
		}
		set {
			superMoney = value;
		}
	}
}
