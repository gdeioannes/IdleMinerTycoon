using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkPanel : MonoBehaviour {

	public Text loadTxt;
	public int money;
	private int level=1;
	private float bust=1.25f;
	private int digAmount=10;
	private float timeToDig=1f;

	// Use this for initialization
	void Start () {
		loadTxt.text="0";
		StartCoroutine("digMoney");
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator digMoney(){
		while(true){
			yield return new WaitForSeconds(timeToDig);
			money+=Mathf.RoundToInt(digAmount*bust);
			loadTxt.text=""+money;
		}
	}
}
