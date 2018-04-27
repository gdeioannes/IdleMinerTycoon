using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataController : MonoBehaviour {

	private string dataFileName="DataModel/DataModel.json";
	public static DataController instance;
	public DataModel dataModel;

	// Use this for initialization
	void Awake () {
		if(instance==null){
			instance=this;
		}

		extractData ();


	}


	void extractData ()
	{
		TextAsset dataModelJson = Resources.Load<TextAsset> ("DataModel");
		dataModel = JsonUtility.FromJson<DataModel> (dataModelJson.text);

	}
}
