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
	ClearConditionStruct[] clearConditionData;
	int stageNum;



	public MassStruct[,] GetMapDataElements() {//自身の所有する１フィールドデータを取得する
		return fieldmapdata;
	}
	public ClearConditionStruct[] GetClearConditionElements() {//自身の所有するクリア条件データを取得する
		return clearConditionData;
	}

	public void SaveInnerStorageData() {//自身の内部クラスに実データをセーブする
		InnerData data = new InnerData();
		data.UpdataAllData(allfieldmapdatas, clearConditionData);
		SaveGame.Save("datastrage", data);
	}

	public void LoadAllData() {//自身の内部クラスをロードし、内部クラスの所有するデータで自身のデータを上書きする（csvmanagerが消失してからはこれしか使わない）
		var newstragedata = SaveGame.Load<InnerData>("datastrage");
		Debug.Log("clicked LoadAllData");
		allfieldmapdatas = newstragedata.Convert1and2DimentionAllayElement(newstragedata.allfieldmapdatas);
		clearConditionData = newstragedata.clearConditionData;
	}


	public int getStageNum() {//ステージ番号を返すメソッド
		return stageNum;
	}

	public void ChangeStagePathNum(Dropdown dropdown) {//ステージ番号を変更するメソッド。csvマネージャーは最終的に消したい
		stageNum = dropdown.value;
		//csvmanager.ChangeStagePathNum(dropdown);
	}

	public MassStruct[,] GetStageMapData(int stageCount) {//指定した1ステージのマップデータをゲットするメソッド
		return allfieldmapdatas[stageCount];
	}

	public void LoadAllMapDatasfromCSV() {//csvmanagerからデータを抽出した後はこのメソッドは使用しなくなる。
		allfieldmapdatas = new MassStruct[Config.stageCount][,];
		for (int j = 0; j < Config.stageCount; j++) {
			allfieldmapdatas[j] = csvmanager.getStageMapDataElements(j);
		}
	}

	//public void showDebugWindow(MassStruct[][,] mapdatas) {
	//	for (int i = 0; i < Config.stageCount; i++) {
	//		for (int j = 0; j < Config.maxGridNum; j++) {
	//			for (int k = 0; k < Config.maxGridNum; k++) {
	//				//Debug.LogFormat("i,j,k,mapdatas[i][j, k].masskind.ToString(), mapdatas[i][j, k].massnumber.ToString()は{0},{1},{2},{3},{4}", i,j,k,mapdatas[i][j, k].masskind.ToString(), mapdatas[i][j, k].massnumber.ToString());
	//			}
	//		}
	//	}
	//}
	//public void LoadfromCsvMapDataElements() {//csvmanagerからデータを抽出した後はこのメソッドは使用しなくなる。
	//	fieldmapdata = csvmanager.getMapDataElements();
	//}
	//public void LoadfromCsvClearConditionElements() {//csvmanagerからデータを抽出した後はこのメソッドは使用しなくなる。
	//	clearConditionData = csvmanager.getClearConditionElements();
	//}

	//public void LoadFromCSV() {//csvから今のステージのクリア必要データと、フィールドデータをとってきて自分の値に上書きする。
	//	LoadfromCsvMapDataElements();
	//	LoadfromCsvClearConditionElements();
	//}


	private class InnerData {//内部クラス。セーブ用にデータを代替保持する。
		public MassStruct[][][] allfieldmapdatas;
		public ClearConditionStruct[] clearConditionData;

		public void UpdataAllData( MassStruct[][,] mapdatas, ClearConditionStruct[] clearCondition) {
			clearConditionData = clearCondition;
			allfieldmapdatas = Convert3DimentionAllayElement(mapdatas);
		}
		public InnerData() {
			allfieldmapdatas = new MassStruct[Config.stageCount][][];
			for (int i = 0; i < Config.stageCount; i++) {
				allfieldmapdatas[i] = new MassStruct[Config.maxGridNum][];
				for (int j = 0; j < Config.maxGridNum; j++) {
					allfieldmapdatas[i][j] = new MassStruct[Config.maxGridNum];
						
					}
				}
			Debug.Log("初期化完了！");
		}
		

		public MassStruct[][][] Convert3DimentionAllayElement(MassStruct[][,] mapdatas) {

			for (int i = 0; i < Config.stageCount; i++) {
				for (int j = 0; j < Config.maxGridNum; j++) {
					for (int k = 0; k < Config.maxGridNum; k++) {
							allfieldmapdatas[i][j][k] = mapdatas[i][j, k];
					}
				}
			}
			return allfieldmapdatas;
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