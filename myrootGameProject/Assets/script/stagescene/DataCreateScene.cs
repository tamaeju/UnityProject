using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class DataCreateScene : MonoBehaviour {
	[SerializeField]
	Meditator meditator;
	MakeManager makemanager;
	CSVManager csvmanager;
	ItemDataManager itemdatamanager;
	MapDataManager mapdatamanager;
	ItemmakeEditorCreater itemmakeeditorcreater;
	ItemMakerCreater itemmakermanager;
	ButtonEventManager buttoneventmanager;


	canvasmaker canvasMaker;
	int usecolomn_of_mapdata = 3;

	void Start() {
		makemanager = meditator.getmakemanager();
		csvmanager = meditator.getcsvmanager();
		mapdatamanager = meditator.getmapdatamanager();
		itemmakeeditorcreater = meditator.getUIdraghmanager();


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
		int[,] _leveldesigndata = csvmanager.getMapDataElement();//レベルデザインデータをcsvからよみこんできて更新、そのためこの時点で、csvのパスをしっかり変えられて入ればよい。
		makemanager.instanciateAllMapObject(_leveldesigndata);//メイクマネージャーにオブジェクトの作成命令
		makemanager.gameObject.GetComponent<distinationSetter>().setditination();
		makemanager.gameObject.GetComponent<distinationSetter>().setAidditination();
		mapdatamanager.updateCansetDatas(_leveldesigndata);//レベルデザインデータを元にで置けるか否かのデータを更新。
	}

	public void makeItemMaker(int stageNum) {
		itemmakermanager = meditator.getitemmakermanager();
		itemdatamanager = meditator.getitemdatamanager();

		itemdatamanager.LoadALLdragitemdata();//アイテムメイカーのデータを更新
		mapdatamanager.changeStageNum(stageNum);//データマネージャーのステージ番号を変更
		itemmakermanager.makeItemMaker();
	}

	public void ChangeCSVNum(Dropdown dropdown) {//保存先と、呼び出し先のcsvを変更するメソッド、マップデータ以外は１つのcsvファイルに保存（インデックスがステージになっているのでそう設計した）
		mapdatamanager.changeStageNum(dropdown.value);
	}
	public void makeMapCsv()//UImanagerのデータを取得し、レベルデザインデータへ反映した後、csvmanagerにセーブ要求
{
		mapdatamanager.makeLevelDesignData();
		csvmanager.MapCsvSave(mapdatamanager.getLevelDesignData());
	}
	public void makeObjectFromMapCsvButton() {
		if (itemmakeeditorcreater != null) {
			itemmakeeditorcreater.deletebutton();
		}
		makeItemMaker(mapdatamanager.getStageNum());
		meditator.getclearmanager().clearConditionSet();
	
		makegamestartcanvas();
		
	}
	public void makegamestartcanvas() {//キャンバスメイカーの作成とプレハブの提供、スタートキャンバスの作成依頼

		PrefabContainer prefabcontainar = meditator.getprefabcontainer();
		canvasMaker = gameObject.AddComponent<canvasmaker>();
		canvasMaker.getscenecanvas(prefabcontainar.getinstancecanvas());


		ClearDataManager cleardatamanager = meditator.getcleardatamanager();
		Action canvastapmethod = () => { startStagePlay(); };
		canvasMaker.showstartcanvas(cleardatamanager.getStageClearCondition(), canvastapmethod);//キャンバス作成時に実行するメソッドを渡している。
		meditator.getclearmanager().setcanvasMaker(canvasMaker);//ゲームオーバーイベントで、クリアコンディションマネージャーがキャンバスメイカーを使用するためセット
	}
	public void startStagePlay(){
		ClearConditionManager clearmanager = meditator.getclearmanager();
		StartCoroutine(clearmanager.timedecreasePerSecond());
		MapEditorUIManager mapeditorUImanager = meditator.getUImanager();
		if (mapeditorUImanager != null) {
			mapeditorUImanager.deleteEditorUIbuttons();
		}
		makeMapObjectANDupdateLeveldesignDataAndCansetData(); 
	}

}
