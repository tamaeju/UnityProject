using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEventManager : MonoBehaviour {//ボタンを押した時のクリック処理の大方を扱うクラス。
	[SerializeField]
	Meditator meditator;
	MakeManager makemanager;
	CSVManager csvmanager;
	ItemDataManager itemdatamanager;
	MapDataManager mapdatamanager;
	DataPathManager datapathmanager;
	ItemmakeEditorManager UIdraghmanager;
	ItemMakerCreater itemmakermanager;
	int usecolomn_of_mapdata = 3;

	void Start() {//ボタンタップで各種オブジェクトにアクセスする必要があるため、各種への参照を取得
		makemanager = meditator.getmakemanager();
		csvmanager = meditator.getcsvmanager();
		mapdatamanager = meditator.getmapdatamanager();
		itemdatamanager = meditator.getitemdatamanager();
		datapathmanager = meditator.getdatapathmanager();
		UIdraghmanager = meditator.getUIdraghmanager();
		itemmakermanager = meditator.getitemmakermanager();
	}

	public void makeMapCsvButton()//UImanagerのデータを取得し、レベルデザインデータへ反映した後、csvmanagerにセーブ要求
		{
		mapdatamanager.makeLevelDesignData();
		csvmanager.MapCsvSave(mapdatamanager.getLevelDesignData());
	}

	public void makeObjectFromMapCsvButton(){//csvmanaにデータをとってくるよう要求、そのデータを用いて、makemaneにインスタンス生成を要求、そのデータにて、置けるか置けないかを上書き、プレイヤーのagerntをゴールに設定。
											 //itemデータもその後取得し、datamanagerへそのデータ更新要求を行って、そのデータからアイテムメイカーというオブジェクト作成依頼をかける。
		makeMapObjectANDupdateLeveldesignDataAndCansetData();
		UIdraghmanager.deletebutton();
		makeItemMaker(mapdatamanager.getStageNum());
	}

	public void makeObjectfromSelectScene(int stageNum) {//レベルセレクト画面にてステージレベルを選択した際の遷移。マップエディターで表示しているUIを消す処理以外はmakeObjectFromMapCsvButtonと同様。
		makeMapObjectANDupdateLeveldesignDataAndCansetData();
		makeItemMaker(stageNum);

	}

	public void CanvasOFFButton()//キャンバスの表示オフ
	{
		Transform trasnform = meditator.getprefabcontainer().getmapmassuipositionObject().GetComponent<Transform>();
		foreach (Transform item in trasnform) {
			item.gameObject.SetActive(false);
		}
	}
	public void CanvasONButton()//ボタンプッシュで実行
	{
		Transform trasnform = meditator.getprefabcontainer().getmapmassuipositionObject().GetComponent<Transform>();
		foreach (Transform item in trasnform) {
			item.gameObject.SetActive(true);
		}
	}
	public void ChangeCSVNum(Dropdown dropdown) {//保存先と、呼び出し先のcsvを変更するメソッド、マップデータ以外は１つのcsvファイルに保存（インデックスがステージになっているのでそう設計した）
		datapathmanager.ChangeMapCSVNum(dropdown.value);
		mapdatamanager.changeStageNum(dropdown.value);
	}

	public void callloadMapCSV() {//指定のcsvからデータを読み込み、UIでのボタンのstateを変える。
		UIManager UImanager = meditator.getUImanager();
		UImanager.loadMapCSV();
	}

	public void makeItemMaker(int stageNum) {
		itemdatamanager.LoadALLdragitemdata();//アイテムメイカーのデータを更新
		mapdatamanager.changeStageNum(stageNum);//データマネージャーのステージ番号を変更
		itemmakermanager.makeItemMaker();
	}

	public void makeMapObjectANDupdateLeveldesignDataAndCansetData() {
		int[,] _leveldesigndata = csvmanager.getDataElement(datapathmanager.getmapdatapath(), usecolomn_of_mapdata - 1);//レベルデザインデータをcsvからよみこんできて更新
		makemanager.instanciateAllMapObject(_leveldesigndata);//メイクマネージャーにオブジェクトの作成命令
		try { makemanager.getPlayerObject().GetComponent<CharactorMove>().setDestination(makemanager.getGoalObject()); }//playerキャラクターにターゲットの設定を行わせる
		catch { Debug.Log(String.Format("ERROR,playerobject is {0}", makemanager.getPlayerObject())); }//無理だったらログ出力
		mapdatamanager.updateCansetDatas(_leveldesigndata);//レベルデザインデータで置けるか否かのデータを設定。
	}
}
