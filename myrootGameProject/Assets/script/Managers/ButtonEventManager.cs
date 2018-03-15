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

	[SerializeField]
	int usecolomn_of_mapdata = 3;

	public void makeMapCsvButton()//ボタンプッシュで実行
		{
		DataManager datamanager = meditator.getdatamanager();
		DataPathManager datapathmanager = meditator.getdatapathmanager();
		UIManager UImanager = meditator.getUImanager();

		meditator.getdatamanager().makeLevelDesignData();
		Debug.Log(meditator.getdatamanager().getLevelDesignData()[0, 0]);
		meditator.getcsvmanager().MapCsvSave(datapathmanager.getcsvdatapath(0), datamanager.getLevelDesignData());
	}

	public void makeObjectFromMapCsvButton()//ボタンプッシュで実行
	{
		MakeManager makemanager = meditator.getmakemanager();
		CSVManager csvmanager = meditator.getcsvmanager();
		DataManager datamanager = meditator.getdatamanager();
		DataPathManager datapathmanager = meditator.getdatapathmanager();
		UIDragButtonManager UIdraghmanager = meditator.getUIdraghmanager();

		int[,] _leveldesigndata = csvmanager.getDataElement(datapathmanager.getcsvdatapath(0), usecolomn_of_mapdata - 1);
		makemanager.instanciateAllObject(_leveldesigndata);
		makemanager.makeDragedObjectandButton();
		GameObject goalobject = makemanager.getGoalObject();
		GameObject playerobject = makemanager.getPlayerObject();
		try { makemanager.getPlayerObject().GetComponent<CharactorMove>().setDestination(makemanager.getGoalObject()); }//プレイヤーに目的地をセットする処理
		catch { Debug.Log(String.Format("ERROR,playerobject is {0}", makemanager.getPlayerObject())); }
		datamanager.updateCansetDatas(_leveldesigndata);
		UIdraghmanager.deletebutton();

	}
	public void CanvasOFFButton()//ボタンプッシュで実行
	{
		Transform trasnform = meditator.getobjectcontainer().getmapmassuipositionObject().GetComponent<Transform>();
		foreach (Transform item in trasnform) {
			item.gameObject.SetActive(false);
		}
	}
	public void CanvasONButton()//ボタンプッシュで実行
	{
		Transform trasnform = meditator.getobjectcontainer().getmapmassuipositionObject().GetComponent<Transform>();
		foreach (Transform item in trasnform) {
			item.gameObject.SetActive(true);
		}
	}
	public void ChangeCSVNum(Dropdown dropdown) {//保存先と、呼び出し先のcsvを変更するメソッド
		DataPathManager datapathmanager = meditator.getdatapathmanager();
		DataManager datamanager = meditator.getdatamanager();

		datapathmanager.ChangeCSVNum(0, dropdown.value);//0はマップデータ
		datapathmanager.ChangeCSVNum(1, dropdown.value);//0はマップデータ
		datapathmanager.ChangeCSVNum(2, dropdown.value);//0はマップデータ
		datamanager.changeStageNum(dropdown.value);
	}

	public void callloadMapCSV() {//指定のcsvからデータを読み込み、UIオブジェクトのstateを変える。
		UIManager UImanager = meditator.getUImanager();
		UImanager.loadMapCSV();
	}
}
