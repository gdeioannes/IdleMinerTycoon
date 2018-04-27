using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkPanel : Company {

	public Text loadTxt;
	public GameObject worker;
	public GameObject workerPosStart;
	public GameObject workerPosEnd;

	public int money;
	private int level=1;
	private float bust=1;
	public int baseDig;
	private float timeToDig=2f;

	// Use this for initialization
	void Start () {
		addWorker();
		addBoss();
		loadTxt.text="0";
		StartCoroutine(digMoney());
	}

	private void addWorker(){
		worker=Instantiate(worker);
		worker.transform.SetParent(this.transform.parent);
		worker.transform.position=workerPosStart.transform.position;
		worker.transform.localScale=new Vector3(1,1,1);
	}

	IEnumerator digMoney(){
		while(true){
			worker.GetComponent<Entity>().moveToPosition(workerPosStart.transform);
			yield return new WaitForSeconds(timeToDig);
			money+=Mathf.RoundToInt(baseDig*bust);
			loadTxt.text=""+money;
			worker.GetComponent<Entity>().moveToPosition(workerPosEnd.transform);
			yield return new WaitForSeconds(timeToDig);

		}
	}
}
