using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class DataCreateScene : BaseGameScene {


	int usecolomn_of_mapdata = 3;

	void Start () { //メディエイターからの参照の取得と、デバッグボタンクラスにメソッドの譲渡

		csvmanager = meditator.getcsvmanager ();
		mapdatamanager = meditator.getmapdatamanager ();
		itemmakeeditorcreater = meditator.getUIdraghmanager ();
		setbuttonmethod ();

	}
	void setbuttonmethod () { //デバッグボタンマネージャーに自身のイベントを登録
		buttoneventmanager = meditator.getbuttonmanager ();
		buttoneventmanager.setmakeObjectButton (makeObjectFromMapCsvButton);
		buttoneventmanager.setChangeCSVNum (ChangeCSVNum);
		buttoneventmanager.setmakeItemMaker (makeItemMaker);
		buttoneventmanager.setmakeMapCsvButton (makeMapCsv);
	}

	private void makeMapObjectANDupdateLeveldesignDataAndCansetData () {
		int[, ] _leveldesigndata = csvmanager.getMapDataElement (); //生成するための現在ステージのマップデータを読み込み
		makemanager = meditator.getmakemanager ();
		makemanager.instanciateAllMapObject (_leveldesigndata); //オブジェクトの作成命令
		makemanager.gameObject.GetComponent<distinationSetter> ().setditination ();
		makemanager.gameObject.GetComponent<distinationSetter> ().setAidditination ();
		mapdatamanager.updateCansetDatas (_leveldesigndata); //レベルデザインデータを元にアイテムを置けるかの判定用データを更新。
	}

}