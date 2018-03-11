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
	string filename;
	string csvdatapath;
	[SerializeField]CSVManager csvmanager;
	[SerializeField]DataManager datamanager;
	[SerializeField]MakeManager makemanager;


	void Start() {
		filename = "testData0.csv";
		csvdatapath = Application.dataPath + "/data/" + filename;
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

	public void makeCsvButton()//ボタンプッシュで実行
	{
		datamanager.makeLevelDesignData(UIobjects);
		Debug.Log(datamanager.getLevelDesignData()[0,0]);
		csvmanager.logSave(csvdatapath, datamanager.getLevelDesignData());
	}
	public void makeObjectFromCsvButton()//ボタンプッシュで実行
	{
		int[,]_leveldesigndata = csvmanager.getDataElement(csvdatapath, loadmapColomn - 1);
		Debug.Log("以下のcsvの列番号のデータをチェックします");
		Debug.Log(loadmapColomn);
		makemanager.instanciateAllObject(_leveldesigndata);
		makemanager.makeDraggedObject();
		makemanager.makeleftbutton(canvasposition.transform);
		GameObject goalobject = makemanager.getGoalObject();
		GameObject playerobject = makemanager.getPlayerObject();
		try { makemanager.getPlayerObject().GetComponent<CharactorMove>().setDestination(makemanager.getGoalObject()); }
		catch { Debug.Log(String.Format("ERROR,playerobject is {0}", makemanager.getPlayerObject())); }
		datamanager.updateCansetDatas(_leveldesigndata);
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
		filename = "testData" + dropdown.value.ToString() + ".csv";
		csvdatapath = Application.dataPath + "/data/" + filename;
		Debug.Log(String.Format("{0}file was changed ", filename));
	}
	public void loadCSV() {//指定のcsvからデータを読み込み、UIオブジェクトのstateを変える。
		for (int j = 0; j < DataManager.maxGridNum; ++j)
		{
			for (int i = 0; i < DataManager.maxGridNum; ++i)
			{
				Debug.Log(filename);
				Debug.Log(csvdatapath);
				int objectkind = csvmanager.getDataElement(csvdatapath, loadmapColomn - 1)[i, j];
				UIobjects[j * 10 + i].GetComponent<LevelButton>().changeState(objectkind);
			}
		}
	}
	public string getCsvDatapassandFileName() {
		return csvdatapath;
	}

}
