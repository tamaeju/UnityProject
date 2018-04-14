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
	MassStruct[][,] m_fieldMapDatas;
	ClearConditionStruct[] m_clearConditionData;
	bool[] m_isStageCleared;
	int[] m_MinClearMoveCount;
	int m_stageNum;

	public ClearConditionStruct[] GetClearConditionElements() {//自身の所有するクリア条件データを取得する
		return m_clearConditionData;
	}

	public void UpdataStageData(MassStruct[,] savedata) {
		m_fieldMapDatas[m_stageNum] = savedata;
	}

	public void SaveStorageData() {//自身の内部クラスに実データをセーブする
		InnerData data = new InnerData();
		data.UpdataAllData(m_fieldMapDatas, m_clearConditionData, m_isStageCleared, m_MinClearMoveCount);
		SaveGame.Save("datastrage", data);
	}

	public void LoadAllData() {//自身の内部クラスをロードし、内部クラスの所有するデータで自身のデータを上書きする
		var newstragedata = SaveGame.Load<InnerData>("datastrage");
		Debug.Log("clicked LoadAllData");
		m_fieldMapDatas = newstragedata.Convert1and2DimentionAllayElement(newstragedata.i_allfieldmapdatas);
		m_clearConditionData = newstragedata.i_clearConditionData;
		m_isStageCleared = newstragedata.i_isStageCleared;
		m_MinClearMoveCount = newstragedata.i_MinClearMoveCount;
	}


	public int getStageNum() {//ステージ番号を返すメソッド
		return m_stageNum;
	}

	public void ChangeStagePathNum(Dropdown dropdown) {//ステージ番号を変更するメソッド。
		m_stageNum = dropdown.value;
		csvmanager.ChangeStagePathNum(dropdown);//
	}

	public MassStruct[,] GetStageMapData(int stageCount) {//指定した1ステージのマップデータをゲットするメソッド
		return m_fieldMapDatas[stageCount];
	}

	public bool isStageClear() {
		return m_isStageCleared[m_stageNum];
	}
	public int getMaxStageScore() {
		return m_MinClearMoveCount[m_stageNum];
	}

	public void  setStageClear() {
		m_isStageCleared[m_stageNum]  = true;
	}
	public void  setMaxStageScore(int newScore) {
		m_MinClearMoveCount[m_stageNum] = newScore;
	}

	public void initializaClearStatusData() {
		m_isStageCleared = new bool[Config.stageCount];
		m_MinClearMoveCount = new int[Config.stageCount];
	}

	//		public bool[] isStageCleared;
	//public int[] MinClearMoveCount;

	public void LoadfromCsvClearConditionElements() {//csvを作成したらこれでデータを読み込んでから上書きする必要がある。
		m_clearConditionData = csvmanager.getClearConditionElements();
	}

	public void LoadAllMapDatasfromCSV() {//csvmanagerからデータを抽出した後はこのメソッドは使用しなくなる。
		m_fieldMapDatas = new MassStruct[Config.stageCount][,];
		for (int j = 0; j < Config.stageCount; j++) {
			m_fieldMapDatas[j] = csvmanager.getStageMapDataElements(j);
		}
	}

	private void showDebugWindow(MassStruct[][,] mapdatas) {
		for (int i = 0; i < Config.stageCount; i++) {
			for (int j = 0; j < Config.maxGridNum; j++) {
				for (int k = 0; k < Config.maxGridNum; k++) {
					//Debug.LogFormat("i,j,k,mapdatas[i][j, k].masskind.ToString(), mapdatas[i][j, k].massnumber.ToString()は{0},{1},{2},{3},{4}", i,j,k,mapdatas[i][j, k].masskind.ToString(), mapdatas[i][j, k].massnumber.ToString());
				}
			}
		}
	}
	private void LoadfromCsvMapDataElements() {
		m_fieldMapDatas[m_stageNum] = csvmanager.getMapDataElements();
	}


	public void LoadFromCSV() {//csvから今のステージのクリア必要データと、フィールドデータをとってくる
		LoadfromCsvMapDataElements();
		LoadfromCsvClearConditionElements();
	}

	private class InnerData {//内部クラス。セーブ用にデータを代替保持する。
		public MassStruct[][][] i_allfieldmapdatas;
		public ClearConditionStruct[] i_clearConditionData;
		//ステージをクリアしたか否か
		public bool[] i_isStageCleared;
		public int[] i_MinClearMoveCount;


		public void UpdataAllData( MassStruct[][,] mapdatas, ClearConditionStruct[] clearCondition, bool[] isStageCleared, int[] MinClearMoveCount) {
			i_clearConditionData = clearCondition;
			i_allfieldmapdatas = Convert3DimentionAllayElement(mapdatas);
			i_isStageCleared = isStageCleared;
			i_MinClearMoveCount = MinClearMoveCount;

	}
		public InnerData() {
			i_allfieldmapdatas = new MassStruct[Config.stageCount][][];
			for (int i = 0; i < Config.stageCount; i++) {
				i_allfieldmapdatas[i] = new MassStruct[Config.maxGridNum][];
				for (int j = 0; j < Config.maxGridNum; j++) {
					i_allfieldmapdatas[i][j] = new MassStruct[Config.maxGridNum];
					}
				}
			Debug.Log("初期化完了！");
			i_isStageCleared = new bool[Config.stageCount];
			i_MinClearMoveCount = new int[Config.stageCount]; 
		}
		

		public MassStruct[][][] Convert3DimentionAllayElement(MassStruct[][,] mapdatas) {

			for (int i = 0; i < Config.stageCount; i++) {
				for (int j = 0; j < Config.maxGridNum; j++) {
					for (int k = 0; k < Config.maxGridNum; k++) {
							i_allfieldmapdatas[i][j][k] = mapdatas[i][j, k];
					}
				}
			}
			return i_allfieldmapdatas;
		}

		public MassStruct[][,] Convert1and2DimentionAllayElement(MassStruct[][][] mapdatas) {
			MassStruct[][,] get12mapdatas = new MassStruct[Config.stageCount][,];
			for (int i = 0; i < Config.stageCount; i++) {
				get12mapdatas[i] = new MassStruct[Config.maxGridNum, Config.maxGridNum];
			}

			for (int i = 0; i < Config.stageCount; i++) {
				for (int j = 0; j < Config.maxGridNum; j++) {
					for (int k = 0; k < Config.maxGridNum; k++) {
						get12mapdatas[i][j,k] = mapdatas[i][j][ k];
					}
				}
			}
			return get12mapdatas;
		}
		

	}
}

//Debug.LogFormat("allfieldmapdatas, mapdatasは{0},{1}", allfieldmapdatas, mapdatas);
//			Debug.LogFormat("allfieldmapdatas[1][1][1].masskind, mapdatas[1][1,1].masskindは{0},{1}", allfieldmapdatas[1][1][1].masskind, mapdatas[1][1, 1].masskind);
//			Debug.LogFormat(" allfieldmapdatas.Length, allfieldmapdatas[0].Length, allfieldmapdatas[0][0].Length, mapdatas.Length, mapdatas[0].GetLength(0), mapdatas[0].GetLength(1)は{0},{1},{2},{3},{4},{5}", allfieldmapdatas.Length, allfieldmapdatas[0].Length, allfieldmapdatas[0][0].Length, mapdatas.Length, mapdatas[0].GetLength(0), mapdatas[0].GetLength(1));
//			Debug.Log(mapdatas[99][1, 1].masskind.ToString());



