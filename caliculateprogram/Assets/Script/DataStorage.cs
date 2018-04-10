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


	public void LoadfromCsvMapDataElements() {
		fieldmapdata = csvmanager.getMapDataElements();
	}
	public void LoadfromCsvClearConditionElements() {
		clearConditionData = csvmanager.getClearConditionElements();
	}
	public int getStageNum() {
		return stageNum;
	}

	public void LoadAllMapDatas() {
		allfieldmapdatas = new MassStruct[Config.stageCount][,];
		for (int j = 0; j < Config.stageCount; j++) {
			allfieldmapdatas[j] = csvmanager.getStageMapDataElements(j);
		}
	}

	public MassStruct[,] GetStageMapData(int stageCount) {
		return allfieldmapdatas[stageCount];
	}
	public MassStruct[][,] GetAllStageMapData() {
		return allfieldmapdatas;
	}
}
//