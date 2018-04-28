using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerEntity : Entity {

	public void callParenAction(){
		gameObject.transform.parent.GetComponent<Action>().callAction();
	}

}
