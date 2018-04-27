using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Company : MonoBehaviour {
	
	public GameObject boss;
	public GameObject bossSpawnPoint;

	protected void addBoss(){
		boss=Instantiate(boss);
		boss.transform.SetParent(this.transform.parent);
		boss.transform.position=bossSpawnPoint.transform.position;
		boss.transform.localScale=new Vector3(1,1,1);
	}

}
