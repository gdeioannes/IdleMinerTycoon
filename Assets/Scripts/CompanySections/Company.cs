using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Company : MonoBehaviour {

	//This variable is for stage creation purposes
	public bool lineActiveFlag=false;

	public bool movingFlag;
	public GameObject worker;
	public GameObject workerPosStart;
	public GameObject workerPosEnd;
	public int levelUpCost;
	public int level;

	protected void addWorker(){
		worker=Instantiate(worker);
		worker.transform.SetParent(this.transform);
		worker.transform.position=workerPosStart.transform.position;
		worker.transform.localScale=new Vector3(1,1,1);
	}

	public void levelUp(){
		if(GameManager.instance.Money>=levelUpCost){
			level++;
			GameManager.instance.Money-=levelUpCost;
			DataController.instance.saveData();
			setStats();
		}else{
			Debug.Log("Not enougth money");
		}
	}
		

	public virtual IEnumerator extractMoney(){ yield return null;}

	public virtual void setStats(){}

}
