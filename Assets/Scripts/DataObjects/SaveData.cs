using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {

	public int money;
	public int superMoney;
	public string time;
	public ElevatorData elevatorData=new ElevatorData();
	public StorageData storageData=new StorageData();
	public List<WorkPanelData> workPanelData= new List<WorkPanelData>();

}
