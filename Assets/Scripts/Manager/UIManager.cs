using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager instance;
	public Text moneyTxt;
	public Text superMoneyTxt;

	// Use this for initialization
	void Awake () {
		if(instance==null){
			instance=this;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void updateMoney(){
		moneyTxt.text=""+GameManager.instance.Money;
		superMoneyTxt.text=""+GameManager.instance.SuperMoney;
	}

}
