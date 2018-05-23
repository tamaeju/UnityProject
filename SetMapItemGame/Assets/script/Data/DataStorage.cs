using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BayatGames.SaveGameFree;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class DataStorage : MonoBehaviour { //最終的にこのクラスがステージデータを所持している状態になったら、このクラスがステージ番号を変更し、ステージに応じたマップデータを返せるように修正する。現状csvマネージャーから読み込んでいる部分も改修する。
	[SerializeField]
	CSVManager csvmanager;
	[SerializeField]
	DataPathManager datapathmanager;
	int[][, ] m_fieldMapDatas;
	clearconditiondata[] m_clearConditionData;
	dragitemdata[][] i_dragitemData;
	bool[] m_isStageCleared;
	int[] m_MinClearMoveCount;
	int m_stageNum;
	InnerData data;

	public clearconditiondata[] GetClearConditionElements () { //自身の所有するクリア条件データを取得する
		return m_clearConditionData;
	}
	public dragitemdata[][] GetDragItemElements () { //自身の所有するクリア条件データを取得する
		return i_dragitemData;
	}
	public int[][, ] GetfieldMapElements () { //自身の所有するクリア条件データを取得する
		return m_fieldMapDatas;
	}
	public clearconditiondata GetClearConditionElement () { //自身の所有するクリア条件データを取得する
		clearconditiondata StageData = m_clearConditionData[m_stageNum];
		return StageData;
	}
	public dragitemdata[] GetDragItemElement () { //自身の所有するクリア条件データを取得する
		dragitemdata[] StageData = i_dragitemData[m_stageNum];
		return StageData;
	}
	public int[, ] GetfieldMapElement () { //自身の所有するクリア条件データを取得する
		int[, ] StageData = m_fieldMapDatas[m_stageNum];
		return StageData;
	}
	public void UpdateDragitemData (int UIbuttonNum, int itemkind, int leftcount) { //dragitem更新用処理

		Debug.Log (String.Format ("dragitemdatas, UIbuttonNum, stage   {0},{1},{2}   ", i_dragitemData, UIbuttonNum, m_stageNum));
		i_dragitemData[m_stageNum][UIbuttonNum].itemkind = itemkind;
		i_dragitemData[m_stageNum][UIbuttonNum].itemcount = leftcount;
	}

	public void UpdataStageData (int[, ] savedata) {
		DebugnullCheckMapDatas ();
		m_fieldMapDatas[m_stageNum] = savedata;
		Debug.Log ("finished UpdataStageData");
	}

	public void UpdateClearedData () { //ストレージへのセーブだけであり、EasySaveへのセーブはSaveStrageDataメソッドを使用する必要がある。
		if (data == null) {
			Debug.Log ("data is null");
		}
		data.UpdateClearedData (m_isStageCleared, m_MinClearMoveCount);
	}

	public void StorageSaveEasySave () { //自身の内部クラスに実データをセーブする
		if (data == null) {
			data = new InnerData ();
		}
		data.UpdataMapandClearconditionData (m_fieldMapDatas, m_clearConditionData, i_dragitemData);
		data.UpdateClearedData (m_isStageCleared, m_MinClearMoveCount);
		SaveGame.Save ("datastrage", data);
		Debug.Log ("finished StorageSaveEasySave");
	}

	public void LoadAllData () { //自身の内部クラスをロードし、内部クラスの所有するデータで自身のデータを上書きする
		var newstragedata = SaveGame.Load<InnerData> ("datastrage");
		data = newstragedata;

		m_fieldMapDatas = newstragedata.Convert1and2DimentionAllayElement (newstragedata.i_allfieldmapdatas);
		m_clearConditionData = newstragedata.i_clearConditionData;
		m_isStageCleared = newstragedata.i_isStageCleared;
		m_MinClearMoveCount = newstragedata.i_MinClearMoveCount;
		Debug.Log ("finished LoadAllData");
	}

	public int getStageNum () { //ステージ番号を返すメソッド
		return m_stageNum;
	}

	public void ChangeStagePathNum (Dropdown dropdown) { //ステージ番号を変更するメソッド。
		m_stageNum = dropdown.value;
		ChangeStagePathNum (m_stageNum); //
	}

	public void ChangeStagePathNum (int stageNum) { //ステージ番号を変更するメソッド。
		datapathmanager.ChangeMapCSVNum (stageNum);
		m_stageNum = stageNum;
	}

	public int[, ] GetStageMapData (int stageCount) { //指定した1ステージのマップデータをゲットするメソッド
		DebugnullCheckMapDatas ();
		return m_fieldMapDatas[stageCount];
	}

	public bool isStageClear () {
		if (m_isStageCleared == null) { Debug.LogWarning ("found　null!"); }
		return m_isStageCleared[m_stageNum]; //取得するのはステージ変更前なので
	}
	public bool isStageClear (int stageNum) {
		if (m_isStageCleared == null) { Debug.LogWarning ("found　null!"); }
		return m_isStageCleared[stageNum]; //取得するのはステージ変更前なので
	}
	public int getMaxStageScore () {
		return m_MinClearMoveCount[m_stageNum];
	}
	public int getMaxStageScore (int stageNum) {
		return m_MinClearMoveCount[stageNum];
	}

	public void setStageClear () {
		m_isStageCleared[m_stageNum] = true;
	}
	public void setMaxStageScore (int newScore) {
		m_MinClearMoveCount[m_stageNum] = newScore;
	}

	public void initializaClearStatusDataofStrage () {
		m_isStageCleared = new bool[Config.stageCount];
		m_MinClearMoveCount = new int[Config.stageCount];
	}

	public void LoadfromCsvClearConditionElements () { //m_clearConditionDataを初期化し、洗濯中ステージのデータをロードし上書きするメソッド。
		m_clearConditionData = csvmanager.getCCDataElement ();
	}

	public void LoadAllMapDatasfromCSV () { //m_fieldMapDatasを初期化し、選択中ステージのデータをロードし上書きするメソッド。
		m_fieldMapDatas = new int[Config.stageCount][, ];
		for (int j = 0; j < Config.stageCount; j++) {
			datapathmanager.ChangeMapCSVNum (j);
			m_fieldMapDatas[j] = csvmanager.getMapDataElement ();
		}
	}

	public void saveALLMapDatatoCSV () { //全てのマップデータをcsvへセーブするメソッド
		for (int i = 0; i < Config.stageCount; i++) {
			csvmanager.MapCsvSave (GetStageMapData (i));
		}
	}

	public bool isExitSavedData () { //savedataが存在しているか否かをチェックする
		Debug.LogWarningFormat ("SaveGame.Existsは{0}", SaveGame.Exists ("datastrage"));
		return SaveGame.Exists ("datastrage");
	}

	private void DebugnullCheckMapDatas () {
		if (m_fieldMapDatas[m_stageNum] == null) {
			Debug.Log ("m_fieldMapDatas[stageCount]==null");
		}
	}

	private void DebugshowMapdataWindow (int[][, ] mapdatas) { //MassStruct[][, ]の中身をデバッグする
		for (int i = 0; i < Config.stageCount; i++) {
			for (int j = 0; j < Config.maxGridNum; j++) {
				for (int k = 0; k < Config.maxGridNum; k++) {
					//Debug.LogFormat("i,j,k,mapdatas[i][j, k].masskind.ToString(), mapdatas[i][j, k].massnumber.ToString()は{0},{1},{2},{3},{4}", i,j,k,mapdatas[i][j, k].masskind.ToString(), mapdatas[i][j, k].massnumber.ToString());
				}
			}
		}
	}
}