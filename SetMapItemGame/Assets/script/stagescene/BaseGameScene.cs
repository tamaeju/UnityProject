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
	protected MakeManager makemanager;
	protected CSVManager csvmanager;
	protected MapDataManager mapdatamanager;
	protected ItemmakeEditorCreater itemmakeeditorcreater;
	protected ItemMakerCreater itemmakermanager;
	protected ButtonEventManager buttoneventmanager;
	[SerializeField]
	protected canvasmaker canvasMaker;

	[SerializeField]
	GameObject debugCanvas;
	int usecolomn_of_mapdata = 3;

	void Start () { //メディエイターからの参照の取得と、デバッグボタンクラスにメソッドの譲渡
		csvmanager = meditator.getcsvmanager ();
		mapdatamanager = meditator.getmapdatamanager ();

	}

	private void makeMapObjectANDupdateLeveldesignDataAndCansetData () {
		int[, ] _leveldesigndata = csvmanager.getMapDataElement (); //生成するための現在ステージのマップデータを読み込み
		makemanager = meditator.getmakemanager ();
		makemanager.instanciateAllMapObject (_leveldesigndata); //オブジェクトの作成命令
		makemanager.gameObject.GetComponent<distinationSetter> ().setditination ();
		makemanager.gameObject.GetComponent<distinationSetter> ().setAidditination ();
		mapdatamanager.updateCansetDatas (_leveldesigndata); //レベルデザインデータを元にアイテムを置けるかの判定用データを更新。
	}

	public void makeItemMaker (int stageNum) { //アイテムメイカークラスの作成
		ItemDataManager itemdatamanager;
		itemmakermanager = meditator.getitemmakermanager ();
		itemdatamanager = meditator.getitemdatamanager ();

		itemdatamanager.LoadALLdragitemdata ();
		mapdatamanager.changeStageNum (stageNum);
		itemmakermanager.makeItemMaker ();
	}

	public void ChangeCSVNum (Dropdown dropdown) { //保存かつ読み込み元のcsvを変更するメソッド
		mapdatamanager.changeStageNum (dropdown.value);
	}
	public void makeMapCsv () //UImanagerのデータを取得し、レベルデザインデータへ反映した後、csvmanagerにセーブ要求
	{
		mapdatamanager.makeLevelDesignData ();
		csvmanager.MapCsvSave (mapdatamanager.getLevelDesignData ());
	}
	public void makeObjectFromMapCsvButton () { //アイテムメイカーエディターがあれば削除し、クリア条件を設定する。
		if (itemmakeeditorcreater != null) {
			itemmakeeditorcreater.deletebutton ();
		}
		makeItemMaker (mapdatamanager.getStageNum ());
		ClearConditionManager clearmanager = meditator.getclearmanager ();
		clearmanager.clearConditionSet ();
		clearmanager.makeClearConditionDisplay ();

		makegamestartcanvas ();

	}
	public void makegamestartcanvas () {
		//プレハブコンテナから作成するキャンバスのプレハブの取得、
		//その後スタート開始時のキャンバスの作成、
		//その後クリアコンディションマネージャーにゲームオーバー時のキャンバスの登録

		PrefabContainer prefabcontainar = meditator.getprefabcontainer (); //作成するキャンバスのプレハブを取得する

		ClearDataManager cleardatamanager = meditator.getcleardatamanager (); //ステージ開始時のキャンバス作成と、キャンバスタップ時の実行メソッドを渡している。
		canvasMaker.showstartcanvas (cleardatamanager.getStageClearCondition (), startStagePlay);

		ClearConditionManager clearmanager = meditator.getclearmanager (); //ゲームオーバー時にクリアコンディションマネージャーがキャンバスメイカーを使用するためセット
		clearmanager.setcanvasMaker (canvasMaker);
	}
	public void startStagePlay () { //clearconditionmanagerに時間を減少させるコルーチンを実行させた後にメソッド実行。
		ClearConditionManager clearmanager = meditator.getclearmanager ();
		StartCoroutine (clearmanager.timedecreasePerSecond ());
		MapEditorUIManager mapeditorUImanager = meditator.getUImanager ();
		if (mapeditorUImanager != null) {
			mapeditorUImanager.deleteEditorUIbuttons ();
		}
		makeMapObjectANDupdateLeveldesignDataAndCansetData ();
		OffdebugCanvas ();
	}

	public void OffdebugCanvas () {
		debugCanvas.SetActive (false);

	}

}