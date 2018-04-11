using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using BayatGames.SaveGameFree;


public class DataStorage : MonoBehaviour {//最終的にこのクラスがステージデータを所持している状態になったら、このクラスがステージ番号を変更し、ステージに応じたマップデータを返せるように修正する。現状csvマネージャーから読み込んでいる部分も改修する。
	[SerializeField]
	CSVManager csvmanager;
	MassStruct[,] fieldmapdata;
	MassStruct[][,] allfieldmapdatas;


	public void LoadFromCSV() {
		LoadfromCsvMapDataElements();
		LoadfromCsvClearConditionElements();
	}

	ClearConditionStruct[] clearConditionData;
	int stageNum;

	public MassStruct[,] GetMapDataElements() {
		return fieldmapdata;
	}
	public ClearConditionStruct[] GetClearConditionElements() {
		return clearConditionData;
	}

	public void SaveDataStorageClass() {
		SaveGame.Save("datastrage", this);
	}

	public void LoadAllData() {
		var newstragedata = SaveGame.Load<DataStorage>("datastrage");
		fieldmapdata = newstragedata.GetMapDataElements();
		clearConditionData = newstragedata.GetClearConditionElements();
		allfieldmapdatas = GetAllStageMapData();
	}


	public int getStageNum() {
		return stageNum;
	}

	public void ChangeStagePathNum(Dropdown dropdown) {
		stageNum = dropdown.value;
		csvmanager.ChangeStagePathNum(dropdown);
	}

	public MassStruct[,] GetStageMapData(int stageCount) {
		return allfieldmapdatas[stageCount];
	}
	public MassStruct[][,] GetAllStageMapData() {
		return allfieldmapdatas;
	}

	public void LoadfromCsvMapDataElements() {//csvmanagerからデータを抽出した後はこのメソッドは使用しなくなる。
		fieldmapdata = csvmanager.getMapDataElements();
	}
	public void LoadfromCsvClearConditionElements() {//csvmanagerからデータを抽出した後はこのメソッドは使用しなくなる。
		clearConditionData = csvmanager.getClearConditionElements();
	}

	public void LoadAllMapDatas() {//csvmanagerからデータを抽出した後はこのメソッドは使用しなくなる。
		allfieldmapdatas = new MassStruct[Config.stageCount][,];
		for (int j = 0; j < Config.stageCount; j++) {
			allfieldmapdatas[j] = csvmanager.getStageMapDataElements(j);
		}
	}

}
//