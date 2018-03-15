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
	public GameObject uiposition;//setbuttonsを入れる。UIを表示ONOFFするため
	public GameObject canvasposition;//leftcountをキャンバスにおくためだけのもの
	public GameObject _levelbutton;
	[SerializeField]
	int usecolomn_of_mapdata = 3;
	[SerializeField]
	Meditator meditator;


	void Start() {

		UIobjects = new GameObject[Config.maxGridNum * Config.maxGridNum];
		instanciateandGetUIObjects();
	}

	Vector3 setUIPos(int x, int y, int z) {
		Vector3 returnPos = new Vector3((x + 12f) * 28, (y + 3f) * 28, z);
		return returnPos;
	}


	public void instanciateandGetUIObjects() {//インスペクターで紐づけを行うためのメソッド。インスタンシエイトしたタイミングでapplyして紐づけする。
		var parent = uiposition.transform;
		for (int j = 0; j < Config.maxGridNum; ++j) {
			for (int i = 0; i < Config.maxGridNum; ++i) {
				UIobjects[j * 10 + i] = Instantiate(_levelbutton, setUIPos(i, j, 0), Quaternion.identity, parent) as GameObject;
			}
		}
	}
	public GameObject[] getUIobjects() {
		return UIobjects;
	}
	public void loadMapCSV() {//指定のcsvからデータを読み込み、UIオブジェクトのstateを変える。
		DataPathManager datapathmanager = meditator.getdatapathmanager();
		CSVManager csvmanager = meditator.getcsvmanager();
		GameObject[] UIobjects = meditator.getUImanager().getUIobjects();

		for (int j = 0; j < Config.maxGridNum; ++j) {
			for (int i = 0; i < Config.maxGridNum; ++i) {
				int objectkind = csvmanager.getDataElement(datapathmanager.getcsvdatapath(0), usecolomn_of_mapdata - 1)[i, j];
				UIobjects[j * 10 + i].GetComponent<MapEditorbutton>().changeState(objectkind);
			}
		}
	}
}
