using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WorkPanel : Company {


	public float timeToDig=3f;
	public int money;

	public Text loadTxt;
	public Text levelTxr;
	public Text minersTxt;
	public Text walkSpeedTxt;
	public Text extractionSpeedTxt;
	public Text workerCapacitiesTxt;
	public Text workLineNumberTxt;

	public int deep;
	public float miners;
	public float walkSpeed;
	public float extractionSpeed;
	public float workerCapacities;


	void Awake(){
		addWorker();
		StartCoroutine( extractMoney());
	}

	public override IEnumerator extractMoney(){
		yield return new WaitForSeconds(Random.Range(0.1f,1f));
		while(true){
			yield return new WaitForSeconds(timeToDig/walkSpeed);
			money+=Mathf.RoundToInt(workerCapacities);
			setLoadTxt();
			worker.GetComponent<Entity>().moveToPosition(workerPosEnd.transform);
			yield return new WaitForSeconds(timeToDig/walkSpeed);
			worker.GetComponent<Entity>().moveToPosition(workerPosStart.transform);
			movingFlag=false;
		}
	}

	public void setLoadTxt(){
		loadTxt.text=""+money;
	}

	public override void setStats(){
		int index=level-1;

		DataModel dataModel=DataController.instance.dataModel;
		WorkLineLevel workLineLevel=dataModel.workLineLevel[index];
		StageDeepLevel stageDepLevel=dataModel.stageDeepLevel[deep-1];

		miners=workLineLevel.miners;
		walkSpeed=workLineLevel.walkSpeed;
		extractionSpeed=workLineLevel.extractionSpeed;
		workerCapacities=Mathf.RoundToInt(workLineLevel.workerCapacities*stageDepLevel.workerCapacitiesBust*workLineLevel.miners);
		levelUpCost=Mathf.RoundToInt(workLineLevel.levelUpCost*stageDepLevel.deepCostBust);
		setStatsText();
	}

	public void setStatsText(){
		minersTxt.text=""+miners;
		walkSpeedTxt.text=""+walkSpeed;
		extractionSpeedTxt.text=""+extractionSpeed+"/s";
		workerCapacitiesTxt.text=""+workerCapacities;
		setLevelText();
	}

	public void setWorkLineNumberTxt(){
		workLineNumberTxt.text=""+deep;
	}

	private void setLevelText(){
		levelTxr.text="Line\nLevel\n"+level+"\nCost "+levelUpCost;
	}

}
