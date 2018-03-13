using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public GameObject[] UIobjects;
	public GameObject uiposition;
	public GameObject canvasposition;
	public GameObject _levelbutton;
	[SerializeField]
	int loadmapColomn = 3;
	[SerializeField]CSVManager csvmanager;
	[SerializeField]DataManager datamanager;
	[SerializeField]MakeManager makemanager;
	[SerializeField]DataPathManager datapathmanager;
	[SerializeField]UIDragButtonManager UIdraghmanager;


	void Start() {

		UIobjects = new GameObject[DataManager.maxGridNum * DataManager.maxGridNum];
		instanciateandGetUIObjects();
	}

	Vector3 setUIPos(int x, int y, int z) {
		Vector3 returnPos = new Vector3((x + 10f) * 28, (y + 3f) * 28, z);
		return returnPos;
	}


	public void instanciateandGetUIObjects() {//インスペクターで紐づけを行うためのメソッド。インスタンシエイトしたタイミングでapplyして紐づけする。
		var parent = uiposition.transform;
		for (int j = 0; j < DataManager.maxGridNum; ++j) {
			for (int i = 0; i < DataManager.maxGridNum; ++i) {
				UIobjects[j * 10 + i] = Instantiate(_levelbutton, setUIPos(i, j, 0), Quaternion.identity, parent) as GameObject;
			}
		}
	}

	public void makeMapCsvButton()//ボタンプッシュで実行
	{
		datamanager.makeLevelDesignData(UIobjects);
		Debug.Log(datamanager.getLevelDesignData()[0,0]);
		csvmanager.MapdataCSVSave(datapathmanager.getcsvdatapath(0), datamanager.getLevelDesignData());
	}
	public void makeObjectFromMapCsvButton()//ボタンプッシュで実行
	{
		int[,]_leveldesigndata = csvmanager.getDataElement(datapathmanager.getcsvdatapath(0), loadmapColomn - 1);
		makemanager.instanciateAllObject(_leveldesigndata);
		makemanager.makeDraggedObject();
		makemanager.makeleftbutton(canvasposition.transform);
		GameObject goalobject = makemanager.getGoalObject();
		GameObject playerobject = makemanager.getPlayerObject();
		try { makemanager.getPlayerObject().GetComponent<CharactorMove>().setDestination(makemanager.getGoalObject()); }//プレイヤーに目的地をセットする処理
		catch { Debug.Log(String.Format("ERROR,playerobject is {0}", makemanager.getPlayerObject())); }
		datamanager.updateCansetDatas(_leveldesigndata);
		UIdraghmanager.deletebutton();

	}
	public void CanvasOFFButton()//ボタンプッシュで実行
	{
		Transform trasnform = uiposition.GetComponent<Transform>();
		foreach (Transform item in trasnform) {
			item.gameObject.SetActive(false);
		}
	}
	public void CanvasONButton()//ボタンプッシュで実行
	{
		Transform trasnform = uiposition.GetComponent<Transform>();
		foreach (Transform item in trasnform)
		{
			item.gameObject.SetActive(true);
		}
	}
	public void ChangeCSVNum(Dropdown dropdown) {//保存先と、呼び出し先のcsvを変更するメソッド
		datapathmanager.ChangeCSVNum(0,dropdown.value);//0はマップデータ
	}

	public void loadMapCSV() {//指定のcsvからデータを読み込み、UIオブジェクトのstateを変える。
		for (int j = 0; j < DataManager.maxGridNum; ++j)
		{
			for (int i = 0; i < DataManager.maxGridNum; ++i)
			{
				Debug.Log(datapathmanager.getfilename(0));
				Debug.Log(datapathmanager.getcsvdatapath(0));
				int objectkind = csvmanager.getDataElement(datapathmanager.getcsvdatapath(0), loadmapColomn - 1)[i, j];
				UIobjects[j * 10 + i].GetComponent<LevelButton>().changeState(objectkind);
			}
		}
	}


}
