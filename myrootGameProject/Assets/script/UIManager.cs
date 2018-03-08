using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public GameObject[] UIobjects = new GameObject[DataManager.maxGridNum * DataManager.maxGridNum];//インスペクタで代入するために
	int[,] _leveldesigndata;
	public GameObject canvasObject;
	int loadColomn = 3;
	public GameObject _levelbutton;
	string filename;
	string datapath;
	private GameObject goalobject;
	private GameObject playerobject;

	void Start() {
		instanciateandGetUIObjects();
	}

	float blocklength = 0.9f;
	DataManager datamanager;

	Vector3 setUIPos(int x, int y, int z) {//InstanciateandgetREFmethod()と合わせ技のため
		Vector3 returnPos = new Vector3((x + 10f) * 28, (y + 3f) * 28, z);
		return returnPos;
	}


	public void instanciateandGetUIObjects() {//インスペクターで紐づけを行うためのメソッド。インスタンシエイトしたタイミングでapplyして紐づけする。
		var parent = canvasObject.transform;
		for (int j = 0; j < DataManager.maxGridNum; ++j) {
			for (int i = 0; i < DataManager.maxGridNum; ++i) {
				UIobjects[j * 10 + i] = Instantiate(_levelbutton, setUIPos(i, j, 0), Quaternion.identity, parent) as GameObject;
			}
		}
	}

	public void makeCsvButton()//ボタンプッシュで実行
	{
		datamanager.makeLevelDesignData();
		CSVManager CsvCreater = new CSVManager();
		CsvCreater.logSave(datapath, _leveldesigndata);
	}
	public void makeObjectFromCsvButton()//ボタンプッシュで実行
	{
		CSVManager DataMaker = new CSVManager();
		_leveldesigndata = DataMaker.getDataElement(datapath, loadColomn - 1);
		Debug.Log("以下のcsvの列番号のデータをチェックします");
		Debug.Log(loadColomn);
		MakeManager ObjectMaker = GetComponent<MakeManager>();
		ObjectMaker.instanciateAllObject(_leveldesigndata);
		goalobject = ObjectMaker.getGoalObject();
		playerobject = ObjectMaker.getPlayerObject();
		try { playerobject.GetComponent<CharactorMove>().setDestination(goalobject); }
		catch { Debug.Log(String.Format("ERROR,playerobject is {0}", playerobject)); }
		datamanager.updateCansetDatas(_leveldesigndata);
	}
	public void CanvasONOFFButton()//ボタンプッシュで実行
	{
		Transform trasnform = canvasObject.GetComponent<Transform>();

		foreach (Transform item in trasnform) {
			item.gameObject.SetActive(false);
		}
	}
	public void ChangeCSVNum(Dropdown dropdown) {//保存先と、呼び出し先のcsvを変更するメソッド
		filename = "testData" + dropdown.value.ToString() + ".csv";
		datapath = Application.dataPath + "/data/" + filename;
		Debug.Log("changedfileto");
		Debug.Log(filename);
	}
}
