using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEventManager : MonoBehaviour {
	[SerializeField]
	Meditator meditator;

	MakeManager makemanager;

	CSVManager csvmanager;

	DataManager datamanager;

	DataPathManager datapathmanager;

	ItemmakeEditorManager UIdraghmanager;

	ItemMakerCreater itemmakermanager;
	int usecolomn_of_mapdata = 3;

	void Start() {
		makemanager = meditator.getmakemanager();
		csvmanager = meditator.getcsvmanager();
		datamanager = meditator.getdatamanager();
		datapathmanager = meditator.getdatapathmanager();
		UIdraghmanager = meditator.getUIdraghmanager();
		itemmakermanager = meditator.getitemmakermanager();
	}

	public void makeMapCsvButton()//ボタンプッシュで実行
		{
		meditator.getdatamanager().makeLevelDesignData();
		Debug.Log(meditator.getdatamanager().getLevelDesignData()[0, 0]);
		meditator.getcsvmanager().MapCsvSave(datamanager.getLevelDesignData());
	}

	public void makeObjectFromMapCsvButton(){
		int[,] _leveldesigndata = csvmanager.getDataElement(datapathmanager.getcsvdatapath(0), usecolomn_of_mapdata - 1);
		makemanager.instanciateAllObject(_leveldesigndata);//マップオブジェクト作成

		try { makemanager.getPlayerObject().GetComponent<PlayerMove>().setDestination(makemanager.getGoalObject()); }//プレイヤーに目的地をセットする処理
		catch { Debug.Log(String.Format("ERROR,playerobject is {0}", makemanager.getPlayerObject())); }

		datamanager.updateCansetDatas(_leveldesigndata);
		UIdraghmanager.deletebutton();

		DataChangerFromJaG jagchanger = meditator.getjagchanger();
		Debug.Log("datapathmanager.getcsvdatapath(1)は→　"+datapathmanager.getcsvdatapath(1));
		int[][] jagitemdata = csvmanager.getJagDataElement(datapathmanager.getcsvdatapath(1));//アイテムデータをcsvからロード
		datamanager.UpdateALLdragitemdata(jagchanger.parsejagtodobledragitemdatadatas(jagitemdata));



		itemmakermanager.makeItemMaker();//アイテムメイカー作成
	}
	public void CanvasOFFButton()//ボタンプッシュで実行
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
	public void ChangeCSVNum(Dropdown dropdown) {//保存先と、呼び出し先のcsvを変更するメソッド
		datapathmanager.ChangeMapCSVNum(dropdown.value);//0はマップデータ
		datamanager.changeStageNum(dropdown.value);
	}

	public void callloadMapCSV() {//指定のcsvからデータを読み込み、UIオブジェクトのstateを変える。
		UIManager UImanager = meditator.getUImanager();
		UImanager.loadMapCSV();
	}


	public void makeObjectfromSelectScene(int stageNum){//グラウンドを作成する必要がある。

		int[,] _leveldesigndata = csvmanager.getDataElement(datapathmanager.getcsvdatapath(0), usecolomn_of_mapdata - 1);
		makemanager.instanciateAllObject(_leveldesigndata);//マップオブジェクト作成

		try { makemanager.getPlayerObject().GetComponent<CharactorMove>().setDestination(makemanager.getGoalObject()); }//プレイヤーに目的地をセットする処理
		catch { Debug.Log(String.Format("ERROR,playerobject is {0}", makemanager.getPlayerObject())); }

		datamanager.updateCansetDatas(_leveldesigndata);

		DataChangerFromJaG jagchanger = meditator.getjagchanger();
		int[][] jagitemdata = csvmanager.getJagDataElement(datapathmanager.getcsvdatapath(1));
		Debug.Log(datapathmanager.getcsvdatapath(1));
		datamanager.UpdateALLdragitemdata(jagchanger.parsejagtodobledragitemdatadatas(jagitemdata));
		datamanager.changeStageNum(stageNum);

		itemmakermanager.makeItemMaker();//アイテムメイカー作成
	}
}
