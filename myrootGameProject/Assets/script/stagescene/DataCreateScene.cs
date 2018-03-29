using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DataCreateScene : MonoBehaviour {
	[SerializeField]
	Meditator meditator;
	MakeManager makemanager;
	CSVManager csvmanager;
	ItemDataManager itemdatamanager;
	MapDataManager mapdatamanager;
	DataPathManager datapathmanager;
	ItemmakeEditorManager UIdraghmanager;
	ItemMakerCreater itemmakermanager;
	ButtonEventManager buttoneventmanager;
	int usecolomn_of_mapdata = 3;

	void Start() {
		makemanager = meditator.getmakemanager();
		csvmanager = meditator.getcsvmanager();
		mapdatamanager = meditator.getmapdatamanager();
		itemdatamanager = meditator.getitemdatamanager();
		datapathmanager = meditator.getdatapathmanager();
		UIdraghmanager = meditator.getUIdraghmanager();
		itemmakermanager = meditator.getitemmakermanager();

		setbuttonmethod();

	}
	void setbuttonmethod() {//イベントハンドラのボタンイベントマネージャーに自身のイベントを登録
		buttoneventmanager = meditator.getbuttonmanager();
		buttoneventmanager.setmakeObjectButton(makeObjectFromMapCsvButton);
		buttoneventmanager.setChangeCSVNum(ChangeCSVNum);
		buttoneventmanager.setmakeItemMaker(makeItemMaker);
		buttoneventmanager.setmakeMapCsvButton(makeMapCsv);

	}

	private void makeMapObjectANDupdateLeveldesignDataAndCansetData() {
		int[,] _leveldesigndata = csvmanager.getDataElement(datapathmanager.getmapdatapath(), usecolomn_of_mapdata - 1);//レベルデザインデータをcsvからよみこんできて更新、そのためこの時点で、csvのパスをしっかり変えられて入ればよい。
		makemanager.instanciateAllMapObject(_leveldesigndata);//メイクマネージャーにオブジェクトの作成命令
		makemanager.gameObject.GetComponent<distinationSetter>().setditination();
		mapdatamanager.updateCansetDatas(_leveldesigndata);//レベルデザインデータを元にで置けるか否かのデータを更新。
	}
	public void makeItemMaker(int stageNum) {
		itemdatamanager.LoadALLdragitemdata();//アイテムメイカーのデータを更新
		mapdatamanager.changeStageNum(stageNum);//データマネージャーのステージ番号を変更
		itemmakermanager.makeItemMaker();
	}

	public void ChangeCSVNum(Dropdown dropdown) {//保存先と、呼び出し先のcsvを変更するメソッド、マップデータ以外は１つのcsvファイルに保存（インデックスがステージになっているのでそう設計した）
		datapathmanager.ChangeMapCSVNum(dropdown.value);
		mapdatamanager.changeStageNum(dropdown.value);
	}
	public void makeMapCsv()//UImanagerのデータを取得し、レベルデザインデータへ反映した後、csvmanagerにセーブ要求
{
		mapdatamanager.makeLevelDesignData();
		csvmanager.MapCsvSave(mapdatamanager.getLevelDesignData());
	}
	public void makeObjectFromMapCsvButton() {//csvmanaにデータをとってくるよう要求、そのデータを用いて、makemaneにインスタンス生成を要求、そのデータにて、置けるか置けないかを上書き、プレイヤーのagerntをゴールに設定。
											  //itemデータもその後取得し、datamanagerへそのデータ更新要求を行って、そのデータからアイテムメイカーというオブジェクト作成依頼をかける。
		makeMapObjectANDupdateLeveldesignDataAndCansetData();
		if (UIdraghmanager != null) {
			UIdraghmanager.deletebutton();
		}
		makeItemMaker(mapdatamanager.getStageNum());
	}
}
