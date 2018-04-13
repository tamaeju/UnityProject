using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;



public class GameScene : MonoBehaviour {
	[SerializeField]
	FieldObjectMaker objectmaker;
	[SerializeField]
	CurrentStageData currentdataholder;
	[SerializeField]
	MassMoveDealer movedealer;
	[SerializeField]
	FieldObjectEditUICreator editUIcreator;
	[SerializeField]
	DataStorage dataholder;
	//[SerializeField]
	//CSVManager csvmanager;

	public void pushStartButton() {
		objectmaker.LoadMapDatas();
		currentdataholder.GetClearConditionData();
		objectmaker.instanciateAllMapObject();
		movedealer.LoadFieldObject();
	}

	public void SaveCurrentMapData() {//現在のエディットボタンのデータを取得し、現在のステージのデータに上書きし、セーブする機能。（注意としてはメンバー変数にデータが乗るだけである事に注意）
		MassStruct[,] savedata = editUIcreator.getCurrentFieldDatas();
		dataholder.UpdataStageData(savedata);

	}

	public void LoadFieldEditorfromStrage() {//ストレージデータの保有するステージデータを元にエディットボタンのステータスを更新する
		editUIcreator.ButtonStatusUpdate(dataholder.GetStageMapData(dataholder.getStageNum()));
	}


	public void StorageLoadAllDatafromEasySave() {//データストレージが保存されているデータをロードする機能
		dataholder.LoadAllData();
	}

	public void ChangeStagePathNum(Dropdown dropdown) {//ステージ選択をする機能
		dataholder.ChangeStagePathNum(dropdown);
	}

	public void deleteDebugUIEditor() {//エディットボタンを消すだけの機能
		editUIcreator.deleteEditorUIbuttons();
	}

	public void SaveStrageALLMapData() {//ストレージのデータをセーブするメソッド。SaveCurrentMapDataの後に実行する必要あり。
		dataholder.SaveStorageData();
	}


	public void initializaClearStatusData() {//クリアした後に保存されるデータの初期化（消去メソッド）実データへの保存を行うためにはこの後saveStrageメソッドを叩く必要がある。単純な初期化処理としても利用可能
		dataholder.initializaClearStatusData();
	}

		//ステージを開始時は
		//StorageLoadAllDatafromEasySave　でデータ読み込み
		//↓
		//ChangeStagePathNum　でステージ選択
		//↓
		//pushStartButton　でステージ開始


		//ステージEdit時は
		//StorageLoadAllDatafromEasySave　でデータ読み込み
		//↓
		//ChangeStagePathNum　でステージ選択
		//↓
		//LoadFieldEditorfromStrage　でデータをGUI化
		//↓
		//GUIいじる
		//↓
		//SaveCurrentMapData　で現在データをストレージのメンバ変数化
		//↓
		//SaveStrageALLMapData　でストレージのメンバ変数を実データ化
		
	}
















//public void DebugSave100MapCsvData() {//デバッグ用。現在のエディットボタンのステータスで100ステージ分上書きする。
//	MassStruct[,] savedata = editUIcreator.getCurrentFieldDatas();
//	csvmanager.DebugsaveAllMapCsvData(savedata);
//}
