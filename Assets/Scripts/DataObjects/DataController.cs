using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataController : MonoBehaviour {

	SaveData saveDataObj= new SaveData();
	public SaveData userData;
	private string dataFileName="DataModel";
	private string userDataFileName="UserData";
	public static DataController instance;
	public DataModel dataModel;

	// Use this for initialization
	void Awake () {
		if(instance==null){
			instance=this;
		}

		extractData ();
		getUserData();
	}
		
	void extractData ()	{
		if(File.Exists(Application.dataPath+"/Resources/"+dataFileName+".json")){
			TextAsset dataModelJson = Resources.Load<TextAsset> (dataFileName);
			dataModel = JsonUtility.FromJson<DataModel> (dataModelJson.text);
		}else{
			Debug.LogError("No Data File Found");
		}
	}

	public void saveData(){
		GameManager gameManager=GameManager.instance;

		saveDataObj.money=gameManager.Money;
		saveDataObj.superMoney=gameManager.SuperMoney;
		saveDataObj.time=""+System.DateTime.Now;
		saveDataObj.elevatorData.level=gameManager.topWorkLine.GetComponent<TopWorkLine>().elevator.level;
		saveDataObj.elevatorData.moneyStorage=gameManager.topWorkLine.GetComponent<TopWorkLine>().elevator.moneyStorage;
		saveDataObj.storageData.level=gameManager.topWorkLine.GetComponent<TopWorkLine>().storage.level;
		saveDataObj.workPanelData.Clear();
		for (int i = 0; i < GameManager.instance.listWorkLine.Count; i++) {
			WorkPanelData workPanelData=new WorkPanelData();
			workPanelData.level=GameManager.instance.listWorkLine[i].workPanel.level;
			workPanelData.money=GameManager.instance.listWorkLine[i].workPanel.money;
			workPanelData.lineActiveFlag=GameManager.instance.listWorkLine[i].workPanel.lineActiveFlag;
			saveDataObj.workPanelData.Add(workPanelData);
		}
		string saveDataObjJson=JsonUtility.ToJson(saveDataObj);
		File.WriteAllText(Application.persistentDataPath+"/UserData.json",saveDataObjJson);
	}

	public void getUserData(){
		if(File.Exists(Application.persistentDataPath+"/UserData.json")){
			Debug.Log("Create Stage with user Data");
			userData= new SaveData();
			string userDataJson = File.ReadAllText(Application.persistentDataPath+"/UserData.json");
			userData = JsonUtility.FromJson<SaveData> (userDataJson);
			GameManager.instance.stageCreatorUserData();
		}else{
			Debug.Log("No user Data File, created one");
			GameManager.instance.stageCreator();
			saveData();
		}
		StartCoroutine(recordData());
	}

	IEnumerator recordData(){
		while(true){
		yield return new WaitForSeconds(1);
			saveData();
		}
	}
}
