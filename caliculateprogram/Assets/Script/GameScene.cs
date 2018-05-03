using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

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
	[SerializeField]
	canvasmaker canvasmaker;
	[SerializeField]
	PanelView panelview;
	[SerializeField]
	LevelSelectCanvasManager selectButtonCreator;
	[SerializeField]
	GameObject levelSelectCanvas;

	[SerializeField]
	CSVManager csvmanager;

	private void Start () {
		//もし既にセーブデータが存在しないならば、easysaveのストレージからデータを読み込む。
		//存在すればcsvマネージャーがデータを読み込んできて、それをeasysaveに書き込む処理を挟む。
		if (!(dataholder.isExitSavedData ())) {
			dataholder.LoadAllMapDatasfromCSV ();
			StrageSaveALLMapandClearConditionDatatoEasySave ();
		}
		StorageLoadAllDatafromEasySave ();
		if (selectButtonCreator != null) { //clearしたか否かのデータが必要なので、この処理はデータ読み込み後である必要がある。
			selectButtonCreator.instanceButtonPrefab (ShowLevelDisplayWindow, dataholder);
		}
	}

	public void pushStartButton () {
		objectmaker.LoadMapDatas (); //オブジェクトメイカーがマップデータを読み込む
		objectmaker.instanciateAllMapObject (); //データからオブジェクトを生成する

		currentdataholder.GetClearConditionData (); //現在データホルダークラスが全ステージ分のクリア条件を読み込む

		movedealer.LoadFieldObject (); //オブジェクトの移動を扱うクラスがブジェクトメイカーが作成したデータを取得する。

		SetGameEndEvent (); //movedealerにゲーム終了時のイベントハンドラを設定する。

		canvasmaker.showstartcanvas (currentdataholder.GettargetSum (), currentdataholder.GetTargetMoveCount ()); //LevelDisplayCanvasを生成する。

		panelview.registRenewCountEvent (); //パネルビュークラスとカレントデータのイベント紐づけ

		panelview.RenewMovecountText (currentdataholder.GetMoveCount ()); //パネルビューの値を初期化
		panelview.RenewCountText (currentdataholder.GetCurrentSum ()); //パネルビューの値を初期化
	}

	public void SaveCurrentMapData () { //現在のエディットボタンのデータを取得し、現在のステージのデータに上書きし、セーブする機能。（注意としてはメンバー変数にデータが乗るだけである事に注意）
		MassStruct[, ] savedata = editUIcreator.getCurrentFieldDatas ();
		dataholder.UpdataStageData (savedata);
	}

	public void LoadFieldEditorfromStrage () { //ストレージデータの保有するステージデータを元にエディットボタンのステータスを更新する
		editUIcreator.ButtonStatusUpdate (dataholder.GetStageMapData (dataholder.getStageNum ()));
	}

	public void StorageLoadAllDatafromEasySave () { //データストレージが保存されているデータをロードする機能
		dataholder.LoadAllData ();
	}

	public void ChangeStagePathNum (Dropdown dropdown) { //ステージ選択をする機能
		dataholder.ChangeStagePathNum (dropdown);
	}
	private void ChangeStagePathNum (int stageNum) { //ステージ選択をする機能
		dataholder.ChangeStagePathNum (stageNum);
	}

	public void deleteDebugUIEditor () { //エディットボタンを消すだけの機能
		editUIcreator.deleteEditorUIbuttons ();
	}

	public void StrageSaveALLMapandClearConditionDatatoEasySave () { //ストレージのデータをセーブするメソッド。SaveCurrentMapDataの後に実行する必要あり。
		dataholder.StorageSaveEasySave ();
	}

	public void initializaClearStatusData () { //クリアした後に保存されるデータの初期化（消去メソッド）実データへの保存を行うためにはこの後saveStrageメソッドを叩く必要がある。単純な初期化処理としても利用可能、これを実行した後UpdateClearedData、SaveStorageDataを行うとデータがEASYSAVEへ保存される。
		dataholder.initializaClearStatusDataofStrage ();
	}

	public void UpdateClearedData () { //セーブするのはストレージにだけなので、実際にセーブするにはsaveStorageDataを叩く必要がある。
		dataholder.UpdateClearedData ();
	}

	public void SetGameEndEvent () {
		movedealer.OnCleared.Subscribe (_ => canvasmaker.showClearEffect (currentdataholder.GetMoveCount (), currentdataholder.GettargetSum ()));
		movedealer.OnCleared.Subscribe (_ => StrageSaveALLMapandClearConditionDatatoEasySave ());
		movedealer.OnGameOvered.Subscribe (_ => canvasmaker.showGameovercanvas (currentdataholder.GetMoveCount (), currentdataholder.GetTargetMoveCount ()));
	}

	public void EditorUISetRandamKind () {
		editUIcreator.EditorUISetRandamKind ();
	}

	public void DeliteLevelSelectCanvas () {
		levelSelectCanvas.SetActive (false);
	}

	public void startStage (int stageNum) {
		deleteDebugUIEditor ();
		ChangeStagePathNum (stageNum);
		objectmaker.LoadMapDatas (); //オブジェクトメイカーがマップデータを読み込む
		objectmaker.instanciateAllMapObject (); //データからオブジェクトを生成する

		currentdataholder.GetClearConditionData (); //現在データホルダークラスが全ステージ分のクリア条件を読み込む

		movedealer.LoadFieldObject (); //オブジェクトの移動を扱うクラスがブジェクトメイカーが作成したデータを取得する。

		SetGameEndEvent (); //movedealerにゲーム終了時のイベントハンドラを設定する。

		Action tutorialAction = null;
		if (!dataholder.isStageClear (1)) { //ステージ1をクリアしていなかったらチュートリアルを表示する。
			tutorialAction = testTutorialAction;
		}
		canvasmaker.showstartcanvas (currentdataholder.GettargetSum (), currentdataholder.GetTargetMoveCount (), tutorialAction); //LevelDisplayCanvasを生成する。

		panelview.registRenewCountEvent (); //パネルビュークラスとカレントデータのイベント紐づけ

		panelview.RenewMovecountText (currentdataholder.GetMoveCount ()); //パネルビューの値を初期化
		panelview.RenewCountText (currentdataholder.GetCurrentSum ()); //パネルビューの値を初期化
	}

	private void ShowLevelDisplayWindow (int stagenum) {
		Action<int> startAct = startStage;
		Action deletedisplayact = DeliteLevelSelectCanvas;
		dataholder.ChangeStagePathNum (stagenum);
		currentdataholder.GetClearConditionData ();
		canvasmaker.showLevelDisplaycanvas (stagenum, dataholder, currentdataholder, startAct, deletedisplayact);
		//canvasmaker.showLevelDisplaycanvas(dataholder.getStageNum(), currentdataholder.GettargetSum(), currentdataholder.GetTargetMoveCount(), startAct, deletedisplayact);
	}
	public void DebugSave100MapCsvData () { //デバッグ用。現在のエディットボタンのステータスで100ステージ分上書きする。
		MassStruct[, ] savedata = editUIcreator.getCurrentFieldDatas ();
		csvmanager.DebugsaveAllMapCsvData (savedata);
	}
	private void testTutorialAction () {
		Debug.Log ("tutorialAction"); //tutorialウインドウを作成し、gameobjectをonにするような形で多分問題ない。
	}
}

//EditorUISetRandamKind
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