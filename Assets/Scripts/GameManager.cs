using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager _instance;

	// Use this for initialization
	void Start () {
		if(_instance==null)
			_instance=this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
