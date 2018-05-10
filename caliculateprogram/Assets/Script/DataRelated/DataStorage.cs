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
	MassStruct[][, ] m_fieldMapDatas;
	ClearConditionStruct[] m_clearConditionData;
	bool[] m_isStageCleared;
	int[] m_MinClearMoveCount;
	int m_stageNum;
	InnerData data;

	public ClearConditionStruct[] GetClearConditionElements () { //自身の所有するクリア条件データを取得する
		return m_clearConditionData;
	}

	public void UpdataStageData (MassStruct[, ] savedata) {
		nullCheckMapDatas ();
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
		data.UpdataMapandClearconditionData (m_fieldMapDatas, m_clearConditionData);
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
		m_stageNum = stageNum;
	}

	public MassStruct[, ] GetStageMapData (int stageCount) { //指定した1ステージのマップデータをゲットするメソッド
		nullCheckMapDatas ();
		return m_fieldMapDatas[stageCount];
	}

	public bool isStageClear () {
		nullCheckMapDatas ();
		return m_isStageCleared[m_stageNum]; //取得するのはステージ変更前なので
	}
	public bool isStageClear (int stageNum) {
		nullCheckMapDatas ();
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

	private void nullCheckMapDatas () {
		if (m_fieldMapDatas[m_stageNum] == null) {
			Debug.Log ("m_fieldMapDatas[stageCount]==null");
		}
	}

	//		public bool[] isStageCleared;
	//public int[] MinClearMoveCount;

	public void LoadfromCsvClearConditionElements () { //m_clearConditionDataを初期化し、洗濯中ステージのデータをロードし上書きするメソッド。
		m_clearConditionData = csvmanager.getClearConditionElements ();
	}

	public void LoadAllMapDatasfromCSV () { //m_fieldMapDatasを初期化し、選択中ステージのデータをロードし上書きするメソッド。
		m_fieldMapDatas = new MassStruct[Config.stageCount][, ];
		for (int j = 0; j < Config.stageCount; j++) {
			m_fieldMapDatas[j] = csvmanager.getMapDataElements (j);
		}
	}

	private void showDebugWindow (MassStruct[][, ] mapdatas) {
		for (int i = 0; i < Config.stageCount; i++) {
			for (int j = 0; j < Config.maxGridNum; j++) {
				for (int k = 0; k < Config.maxGridNum; k++) {
					//Debug.LogFormat("i,j,k,mapdatas[i][j, k].masskind.ToString(), mapdatas[i][j, k].massnumber.ToString()は{0},{1},{2},{3},{4}", i,j,k,mapdatas[i][j, k].masskind.ToString(), mapdatas[i][j, k].massnumber.ToString());
				}
			}
		}
	}
	private void LoadfromCsvMapDataElements () {
		nullCheckMapDatas ();
		m_fieldMapDatas[m_stageNum] = csvmanager.getMapDataElements (m_stageNum);
	}

	public void saveALLMapDatatoCSV () { //全てのマップデータをcsvにセーブするメソッド
		for (int i = 0; i < Config.stageCount; i++) {
			csvmanager.MapCsvSave (GetStageMapData (i), i);
		}
	}

	public bool isExitSavedData () {
		Debug.LogWarningFormat ("SaveGame.Existsは{0}", SaveGame.Exists ("datastrage"));
		return SaveGame.Exists ("datastrage");
	}

	public void LoadFromCSV () { //csvから今のステージのクリア必要データと、フィールドデータをとってくる、データの初期化ができてない場合はこのメソッドを先に呼んではいけない。
		LoadfromCsvMapDataElements ();
		LoadfromCsvClearConditionElements ();
	}

}

//Debug.LogFormat("allfieldmapdatas, mapdatasは{0},{1}", allfieldmapdatas, mapdatas);
//			Debug.LogFormat("allfieldmapdatas[1][1][1].masskind, mapdatas[1][1,1].masskindは{0},{1}", allfieldmapdatas[1][1][1].masskind, mapdatas[1][1, 1].masskind);
//			Debug.LogFormat(" allfieldmapdatas.Length, allfieldmapdatas[0].Length, allfieldmapdatas[0][0].Length, mapdatas.Length, mapdatas[0].GetLength(0), mapdatas[0].GetLength(1)は{0},{1},{2},{3},{4},{5}", allfieldmapdatas.Length, allfieldmapdatas[0].Length, allfieldmapdatas[0][0].Length, mapdatas.Length, mapdatas[0].GetLength(0), mapdatas[0].GetLength(1));
//			Debug.Log(mapdatas[99][1, 1].masskind.ToString());

//もしクリアコンディションデータもしくはマップデータが消失したときは
//初期化→csvからの代入→ストレージへの保存→EASYSAVEへの保存という流れが必要となる。
//そのため下記手順での実行が必要となる。
//ChangeStagePathNum
//↓
//LoadAllMapDatasfromCSV
//↓
//LoadfromCsvClearConditionElements
//↓
//LoadFieldEditorfromStrage//本当にストレージにデータができているかの確認用
//↓
//SaveCurrentMapData
//↓
//SaveStrageALLMapData
//の順での実行が必要
//最後にdebug start buttonを実行してみて試す。

//ロードの時に、インナークラスを作ってそれから全データをとってくる→OK
//セーブの時に、インナークラスを作ってそれでデータを保存する。