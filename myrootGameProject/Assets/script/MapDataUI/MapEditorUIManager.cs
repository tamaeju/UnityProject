using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorUIManager : MonoBehaviour {//マップに何を配置するかを調整するUIであるobjectselectボタンを扱うクラス。

	[SerializeField]
	private GameObject[] MapEditorButtons;//マップエディット用のUIボタンの格納用配列
	[SerializeField]
	private GameObject _levelbutton;//マップエディット用のUIボタン
	[SerializeField]
	private GameObject MapEditoruiButtonpos;//_levelbuttonを表示ONOFFするためのUIの親オブジェクト、レベル選択画面のときはここをnullにしておく。

	[SerializeField]
	Meditator meditator;


	void Start() {//マップエディットUIの生成。
		MapEditorButtons = new GameObject[Config.maxGridNum * Config.maxGridNum];
		instanciateandGetUIObjects();
	}

	Vector3 setUIPos(int x, int y, int z) {
		Vector3 returnPos = new Vector3((x + 11.5f) * 28, (y + 4f) * 28, z);
		return returnPos;
	}


	public void instanciateandGetUIObjects() {//マップエディット用のUIボタン生成と、参照の取得
		var parent = MapEditoruiButtonpos.transform;
		for (int j = 0; j < Config.maxGridNum; ++j) {
			for (int i = 0; i < Config.maxGridNum; ++i) {
				MapEditorButtons[j * Config.maxGridNum + i] = Instantiate(_levelbutton, setUIPos(i, j, 0), Quaternion.identity, parent) as GameObject;
			}
		}
	}
	public GameObject[] getUIobjects() {
		return MapEditorButtons;
	}
	public void loadMapCSV() {//指定のcsvからデータを読み込み、UIオブジェクトのstateを変える。
		CSVManager csvmanager = meditator.getcsvmanager();
		GameObject[] UIobjects = getUIobjects();
		int usecolomn = Config.usecolomn_of_mapdata;

		for (int j = 0; j < Config.maxGridNum; ++j) {
			for (int i = 0; i < Config.maxGridNum; ++i) {
				int objectkind = csvmanager.getMapDataElement()[i, j];
				UIobjects[j * 10 + i].GetComponent<MapEditorbutton>().changeState(objectkind);
			}
		}
	}
	public GameObject MakeGetUIobject(GameObject instanceprefab, Vector2 objectpos) {//UI上にオブジェクトを生成し、vector2の位置にオブジェクトを生成する処理
		PrefabContainer objectcontainer = meditator.getprefabcontainer();
		Transform canvastrans = objectcontainer.getcanvasposition().transform;
		GameObject getobject = Instantiate(instanceprefab, this.transform.position, Quaternion.identity, canvastrans) as GameObject;
		getobject.transform.position = new Vector3(canvastrans.position.x + objectpos.x, canvastrans.position.y + objectpos.y, this.transform.position.z);
		return getobject;
	}
	public void deleteEditorUIbuttons() {
		foreach(var item in MapEditorButtons)
		Destroy(item);
	}
}
