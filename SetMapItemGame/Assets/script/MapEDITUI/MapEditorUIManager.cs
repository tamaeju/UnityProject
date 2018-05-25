using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorUIManager : MonoBehaviour { //マップに何を配置するかを調整するUIであるobjectselectボタンを扱うクラス。

	[SerializeField]
	private GameObject[] MapEditorButtons; //マップエディット用のUIボタンの格納用配列
	[SerializeField]
	private GameObject _levelbutton; //マップエディット用のUIボタン
	[SerializeField]
	private GameObject MapEditoruiButtonpos; //_levelbuttonを表示ONOFFするためのUIの親オブジェクト、レベル選択画面のときはここをnullにしておく。

	[SerializeField]
	Meditator meditator;

	void Start () { //マップエディットUIの生成。
		MapEditorButtons = new GameObject[Config.maxGridNum * Config.maxGridNum];
		instanciateandGetUIObjects ();
	}

	Vector3 setUIPos (int x, int y, int z) {
		Vector3 returnPos = new Vector3 ((x + 12f) * 28, (y + 4f) * 28, z);
		return returnPos;
	}

	public void instanciateandGetUIObjects () { //マップエディット用のUIボタン生成と、参照の取得
		var parent = MapEditoruiButtonpos.transform;
		for (int j = 0; j < Config.maxGridNum; ++j) {
			for (int i = 0; i < Config.maxGridNum; ++i) {
				MapEditorButtons[j * Config.maxGridNum + i] = Instantiate (_levelbutton, setUIPos (i, j, 0), Quaternion.identity, parent) as GameObject;
			}
		}
	}
	public GameObject[] getUIobjects () {
		return MapEditorButtons;
	}
	public void ChangeMapEditCSV () { //指定のcsvからデータを読み込み、UIオブジェクトのstateを変える。
		DataStorage dataholder = meditator.getdataholder ();
		GameObject[] UIobjects = getUIobjects ();
		Debug.LogFormat ("dataholder.getM_stage()は{0}です", dataholder.getM_stage ());
		for (int j = 0; j < Config.maxGridNum; ++j) {
			for (int i = 0; i < Config.maxGridNum; ++i) {
				int objectkind = dataholder.GetfieldMapElement () [i, j];
				Debug.LogFormat ("i,j,は{0}{1}、そしてそのときのobjectkindは{2}です", i,j,objectkind);
				UIobjects[j * 10 + i].GetComponent<MapEditorbutton> ().changeState (objectkind);
			}
		}
	}

	public void deleteEditorUIbuttons () {
		foreach (var item in MapEditorButtons)
			Destroy (item);
	}
}