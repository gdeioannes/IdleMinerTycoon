using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Company : MonoBehaviour {

	public bool bossActiveFlag;
	public bool movingFlag;
	public GameObject boss;
	public GameObject bossSpawnPoint;
	public GameObject worker;
	public GameObject workerPosStart;
	public GameObject workerPosEnd;


	protected void addBoss(){
		boss=Instantiate(boss);
		boss.transform.SetParent(this.transform);
		boss.transform.position=bossSpawnPoint.transform.position;
		boss.transform.localScale=new Vector3(1,1,1);
	}

	protected void addWorker(){
		worker=Instantiate(worker);
		worker.transform.SetParent(this.transform);
		worker.transform.position=workerPosStart.transform.position;
		worker.transform.localScale=new Vector3(1,1,1);
	}

}
