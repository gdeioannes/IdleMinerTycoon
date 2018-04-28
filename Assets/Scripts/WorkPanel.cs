using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkPanel : Company,Action {

	public Text loadTxt;

	public int money;
	private int level=1;
	private float bust=1;
	public int baseDig;
	private float timeToDig=2f;

	public Text totalStractionTxt;
	public Text minersTxt;
	public Text walkSpeedTxt;
	public Text extractionSpeedTxt;
	public Text workerCapacitiesTxt;

	public Text levelTxr;

	public float totalStraction=1;
	public float miners=1;
	public float walkSpeed=1;
	public float extractionSpeed=1;
	public float workerCapacities=1;


	// Use this for initialization
	void Start () {
		addWorker();
		addBoss();
		setLoadTxt();
		setStatsText();
		setLevelText();
	}

	public void callAction(){
		if(!movingFlag && !bossActiveFlag){
			movingFlag=true;
			StartCoroutine(digMoney());
		}
	}

	public IEnumerator bossTakeMoney(){
		bossActiveFlag=true;
		while(true){
			yield return StartCoroutine( digMoney());
		}
	}

	IEnumerator digMoney(){
			yield return new WaitForSeconds(timeToDig);
			money+=Mathf.RoundToInt(baseDig*bust);
			setLoadTxt();
			worker.GetComponent<Entity>().moveToPosition(workerPosEnd.transform);
			yield return new WaitForSeconds(timeToDig);
			worker.GetComponent<Entity>().moveToPosition(workerPosStart.transform);
			movingFlag=false;
	}

	public void setLoadTxt(){
		loadTxt.text=""+money;
	}

	public void setStatsText(){
		totalStractionTxt.text=""+totalStraction+"T/s";
		minersTxt.text=""+miners;
		walkSpeedTxt.text=""+walkSpeed;
		extractionSpeedTxt.text=""+extractionSpeed+"/s";
		workerCapacitiesTxt.text=""+workerCapacities;
	}

	public void levelUp(){
		level++;
		setLevelText();
	}

	private void setLevelText(){
		levelTxr.text="Line\nLevel\n"+level;
	}

}
