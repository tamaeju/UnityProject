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
	DataManager datamanager;
	DataPathManager datapathmanager;
	ItemmakeEditorManager UIdraghmanager;
	ItemMakerCreater itemmakermanager;
	int usecolomn_of_mapdata = 3;

	void Start() {//ボタンタップで各種オブジェクトにアクセスする必要があるため、各種への参照を取得
		makemanager = meditator.getmakemanager();
		csvmanager = meditator.getcsvmanager();
		datamanager = meditator.getdatamanager();
		datapathmanager = meditator.getdatapathmanager();
		UIdraghmanager = meditator.getUIdraghmanager();
		itemmakermanager = meditator.getitemmakermanager();
	}

	public void makeMapCsvButton()//UImanagerのデータを取得し、レベルデザインデータへ反映した後、csvmanagerにセーブ要求
		{
		meditator.getdatamanager().makeLevelDesignData();
		meditator.getcsvmanager().MapCsvSave(datamanager.getLevelDesignData());
	}

	public void makeObjectFromMapCsvButton(){//csvmanaにデータをとってくるよう要求、そのデータを用いて、makemaneにインスタンス生成を要求、そのデータにて、置けるか置けないかを上書き、プレイヤーのagerntをゴールに設定。
		//itemデータもその後取得し、datamanagerへそのデータ更新要求を行って、そのデータからアイテムメイカーというオブジェクト作成依頼をかける。
		int[,] _leveldesigndata = csvmanager.getDataElement(datapathmanager.getmapdatapath(), usecolomn_of_mapdata - 1);
		makemanager.instanciateAllMapObject(_leveldesigndata);

		try { makemanager.getPlayerObject().GetComponent<PlayerMove>().setDestination(makemanager.getGoalObject()); }
		catch { Debug.Log(String.Format("ERROR,playerobject is {0}", makemanager.getPlayerObject())); }

		datamanager.updateCansetDatas(_leveldesigndata);
		UIdraghmanager.deletebutton();

		DataChangerFromJaG jagchanger = meditator.getjagchanger();
		Debug.Log("datapathmanager.getitemdatapath()は→　" + datapathmanager.getitemdatapath());
		int[][] jagitemdata = csvmanager.getJagDataElement(datapathmanager.getitemdatapath());//アイテムデータをcsvからロード
		datamanager.UpdateALLdragitemdata(jagchanger.parsejagtodobledragitemdatadatas(jagitemdata));



		itemmakermanager.makeItemMaker();
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
		datamanager.changeStageNum(dropdown.value);
	}

	public void callloadMapCSV() {//指定のcsvからデータを読み込み、UIでのボタンのstateを変える。
		UIManager UImanager = meditator.getUImanager();
		UImanager.loadMapCSV();
	}


	public void makeObjectfromSelectScene(int stageNum) {//レベルセレクト画面にてステージレベルを選択した際の遷移。マップエディターで表示しているUIを消す処理以外はmakeObjectFromMapCsvButtonと同様。

		int[,] _leveldesigndata = csvmanager.getDataElement(datapathmanager.getmapdatapath(), usecolomn_of_mapdata - 1);
		makemanager.instanciateAllMapObject(_leveldesigndata);

		try { makemanager.getPlayerObject().GetComponent<CharactorMove>().setDestination(makemanager.getGoalObject()); }
		catch { Debug.Log(String.Format("ERROR,playerobject is {0}", makemanager.getPlayerObject())); }

		datamanager.updateCansetDatas(_leveldesigndata);

		DataChangerFromJaG jagchanger = meditator.getjagchanger();
		int[][] jagitemdata = csvmanager.getJagDataElement(datapathmanager.getitemdatapath());
		Debug.Log(datapathmanager.getitemdatapath());
		datamanager.UpdateALLdragitemdata(jagchanger.parsejagtodobledragitemdatadatas(jagitemdata));
		datamanager.changeStageNum(stageNum);

		itemmakermanager.makeItemMaker();
	}
}
