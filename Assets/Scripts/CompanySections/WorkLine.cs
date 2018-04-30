using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WorkLine : MonoBehaviour {

	public Text workLineNumTxt;
	public Text superMoneyCostTxt;
	public Text moneyCostTxt;

	public GameObject buyPanel;
	public WorkPanel workPanel;
	public GameObject elevatorPosition;
	public GameObject workLineNext;

	public int id;
	public int workLineNum;
	public int workLineCostMoney;
	public int workLineCostSuperMoney;

	public bool workingFlag=false;

	public void buyWorkLineMoney(){
		if(GameManager.instance.Money>workLineCostMoney){
			GameManager.instance.Money-=workLineCostMoney;
			UIManager.instance.updateMoney();
			showWorkPanel();
			GameManager.instance.listWorkLine[id].GetComponent<WorkLine>().showBuyPanel();
			workingFlag=true;
		}else{
			Debug.Log("Not enougth Money");
		}
	}

	public void buyWorkLineSuperMoney(){
		if(GameManager.instance.SuperMoney>workLineCostSuperMoney){
			GameManager.instance.SuperMoney-=workLineCostSuperMoney;
			UIManager.instance.updateMoney();
			showWorkPanel();
			GameManager.instance.listWorkLine[id].GetComponent<WorkLine>().showBuyPanel();
			workingFlag=true;
		}else{
			Debug.Log("Not enougth Super Money");
		}
	}


	public void showWorkPanel(){
		buyPanel.SetActive(false);
		workPanel.gameObject.SetActive(true);
		StartCoroutine( workPanel.extractMoney());
		workPanel.lineActiveFlag=true;
	}

	public void showBuyPanel(){
		buyPanel.SetActive(true);
		workPanel.gameObject.SetActive(false);
		changeBuyButtonsText ();
	}

	public void hidePanels(){
		buyPanel.SetActive(false);
		workPanel.gameObject.SetActive(false);
	}


	public void setWorkLineNumber(){
		workLineNumTxt.text=""+workLineNum;
	}

	void changeBuyButtonsText ()
	{
		superMoneyCostTxt.text = "Super Money\n" + workLineCostSuperMoney;
		moneyCostTxt.text = "Money\n" + workLineCostMoney;
	}
}
