using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

	public void moveToPosition(Transform trans){
		this.transform.position=trans.position;
	}
}
