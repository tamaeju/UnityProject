using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class BaseGameScene : MonoBehaviour {
	[SerializeField]
	protected Meditator meditator;
	[SerializeField]
	protected MakeManager makemanager;
	[SerializeField]
	protected CSVManager csvmanager;
	[SerializeField]
	protected MapDataManager mapdatamanager;
	protected ItemmakeEditorCreater itemmakeeditorcreater;
	protected ItemMakerCreater itemmakermanager;
	protected ButtonEventManager buttoneventmanager;
	[SerializeField]
	protected canvasmaker canvasMaker;
	[SerializeField]
	protected DataStorage dataholder;

	[SerializeField]
	GameObject debugCanvas;
	[SerializeField]
	LevelSelectCanvasManager levelselectcanvasmanager;
	[SerializeField]
	MapEditorUIManager EditUImanager;

	int usecolomn_of_mapdata = 3;

	public void startStageButton (int stageNum) { //実際にゲームをスタートするボタン、レベル選択画面のボタンもこれを呼び出す。（１）
		dataholder.ChangeStagePathNum (stageNum);
		Debug.LogFormat ("stage{0}スタート！", stageNum);
		levelselectcanvasmanager.DisplayOffLevelSelectCanvas ();
		makeObjectFromMapCsvButton ();
	}
	public void makeObjectFromMapCsvButton () { //デバッグ画面でのステージ開始処理。（２）
		if (dataholder.isDatasNull () == true) {
			Debug.LogAssertionFormat ("dataholder.isDatasNull ()は{0}", dataholder.isDatasNull ());
			dataholder.getALLDatas ();
			Debug.LogAssertionFormat ("dataholder.isDatasNull ()は{0}", dataholder.isDatasNull ());
			Debug.LogAssertionFormat ("dataholderのデータがなかったのでロードをしました");
		}
		if (itemmakeeditorcreater != null) {
			itemmakeeditorcreater.deletebutton ();
		}
		makeItemMaker (); //itemを作成するクラス（３）
		ClearConditionManager clearmanager = meditator.getclearmanager ();
		clearmanager.clearConditionSet ();
		clearmanager.makeClearConditionDisplay ();
		makegamestartcanvas ();
	}
	public void makeItemMaker () { //アイテムメイカークラスの作成（４）
		itemmakermanager = meditator.getitemmakermanager ();
		dataholder.GetDragItemElements ();
		itemmakermanager.makeItemMaker ();
	}

	public void makegamestartcanvas () { //ゲームスタート時のキャンバスオブジェクトを作成するためのメソッド、ゲーム終了時のメソッド登録もこのタイミングで実行（5）
		PrefabContainer prefabcontainar = meditator.getprefabcontainer (); //作成するキャンバスのプレハブを取得する
		canvasMaker.showstartcanvas (dataholder.GetClearConditionElement (), startStagePlay);
		ClearConditionManager clearmanager = meditator.getclearmanager (); //ゲームオーバー時にクリアコンディションマネージャーがキャンバスメイカーを使用するためセット
		clearmanager.setcanvasMaker (canvasMaker);
	}
	public void startStagePlay () { //ゲームスタートしてからの時間経過処理を行わせるためのメソッド（ゲームスタートウインドウをタップ後に処理開始）。（6）
		ClearConditionManager clearmanager = meditator.getclearmanager ();
		StartCoroutine (clearmanager.timedecreasePerSecond ());
		MapEditorUIManager mapeditorUImanager = meditator.getUImanager ();
		if (mapeditorUImanager != null) {
			mapeditorUImanager.deleteEditorUIbuttons ();
		}
		makeMapObjectANDupdateLeveldesignDataAndCansetData ();
		OffdebugCanvas ();
	}
	private void makeMapObjectANDupdateLeveldesignDataAndCansetData () { //フィールド上のオブジェクトを生成するメソッド。（7）
		int[, ] _leveldesigndata = dataholder.GetfieldMapElement (); //生成するための現在ステージのマップデータを読み込み
		makemanager.instanciateAllMapObject (_leveldesigndata); //オブジェクトの作成命令
		makemanager.gameObject.GetComponent<distinationSetter> ().setditination ();
		makemanager.gameObject.GetComponent<distinationSetter> ().setAidditination ();
		mapdatamanager.updateCansetDatas (_leveldesigndata); //レベルデザインデータを元にアイテムを置けるかの判定用データを更新。
	}

	public void makeMapCsv () //UImanagerのデータを取得し、レベルデザインデータへ反映した後、csvmanagerにセーブ要求
	{
		mapdatamanager.makeLevelDesignData ();
		csvmanager.MapCsvSave (mapdatamanager.getLevelDesignData ());
	}

	public void ChangeCSVNum (Dropdown dropdown) { //保存かつ読み込み元のcsvを変更するメソッド
		dataholder.ChangeStagePathNum (dropdown.value);
	}
	public void makelevelselectButtons () { //レベル選択画面を作るメソッド
		if (levelselectcanvasmanager != null) {
			levelselectcanvasmanager.instanceButtonPrefab (startStageButton);
		} else {
			Debug.Log ("levelselectcanvasmanager==null");
		}
	}
	public void makeNewEasySave () { //csvからデータをとってきて、それをeasysaveに上書きするメソッド
		dataholder.makeNewEasySave ();
	}

	public void OffdebugCanvas () {
		debugCanvas.SetActive (false);
	}
	void Start () {
		makelevelselectButtons ();
	}
	//必要な機能loadcsvデータをとってきて、UIに切り替える処理EditUImanager.loadMapCSV();
	//
	public void loadMapCSV () {
		EditUImanager.ChangeMapEditCSV ();
	}

}