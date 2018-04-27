using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		workLineCreator();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void workLineCreator(){

		//Empty the contect in case forgot to delete assets
		foreach (Transform child in content.transform) {
			GameObject.Destroy(child.gameObject);
		}

		//Add Top panel to the game
		GameObject intTopWorkLine = Instantiate(topWorkLine);
		intTopWorkLine.transform.parent=content.gameObject.transform;

		//Issue with the scale and position, need to fix Gabriel
		intTopWorkLine.gameObject.transform.localScale=new Vector3(1,1,1);
		intTopWorkLine.GetComponent<RectTransform>().localPosition=new Vector3(1,1,1);

		for (int i = 0; i < DataController.instance.dataModel.stageDeepLevel.Count; i++) {
			
			GameObject instanceWorkLine = Instantiate(workLine) ;
			WorkLine workLineObj=instanceWorkLine.GetComponent<WorkLine>();
			DataModel dataModel=DataController.instance.dataModel;

			instanceWorkLine.transform.parent=content.gameObject.transform;
			workLineObj.workLineCostMoney=dataModel.stageDeepLevel[i].workLineCostMoney;
			workLineObj.workLineCostSuperMoney=dataModel.stageDeepLevel[i].workLineCostSuperMoney;
			workLineObj.id=dataModel.stageDeepLevel[i].id;

			//Issue with the scale and position, need to fix Gabriel
			instanceWorkLine.gameObject.transform.localScale=new Vector3(1,1,1);
			instanceWorkLine.GetComponent<RectTransform>().localPosition=new Vector3(1,1,1);
			if(i==0){
				workLineObj.showBuyPanel();
			}else{
				workLineObj.hidePanels();
			}
			listWorkLine.Add(workLineObj);
		}
			
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
