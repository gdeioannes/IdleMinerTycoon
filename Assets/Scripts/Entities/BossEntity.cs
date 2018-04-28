using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEntity : MonoBehaviour {

	public Sprite activeImage;
	public GameObject bustBtn;

	public void callParenAction(){
		StartCoroutine( gameObject.transform.parent.GetComponent<Action>().bossTakeMoney());
		bustBtn.SetActive(true);
		this.gameObject.GetComponent<Image>().sprite=activeImage;
	}


}
