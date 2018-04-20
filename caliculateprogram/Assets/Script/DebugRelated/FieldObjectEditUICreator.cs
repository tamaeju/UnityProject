using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FieldObjectEditUICreator : MonoBehaviour {//マップに何を配置するかを調整するUIであるobjectselectボタンを扱うクラス。


	private GameObject[,] MapEditorButtons;//マップエディット用のUIボタンの格納用配列
	[SerializeField]
	private GameObject editButtonPrefab;//マップエディット用のUIボタン
	[SerializeField]
	private GameObject MapEditorUIInstancePos;//_levelbuttonを表示ONOFFするためのUIの親オブジェクト、レベル選択画面のときはここをnullにしておく。
	[SerializeField]
	DataStorage dataholder;


	void Start() {//マップエディットUIの生成。
		MapEditorButtons = new GameObject[Config.maxGridNum ,Config.maxGridNum];
		instanciateandGetUIObjects();
	}

	Vector3 setUIPos(int x, int y, int z) {
		Vector3 returnPos = new Vector3((x + 11.5f) * 28, (y + 4f) * 28, z);
		return returnPos;
	}


	public void instanciateandGetUIObjects() {//マップエディット用のUIボタン生成と、参照の取得
		var parent = MapEditorUIInstancePos.transform;
		for (int j = 0; j < Config.maxGridNum; ++j) {
			for (int i = 0; i < Config.maxGridNum; ++i) {
				MapEditorButtons[i,j] = Instantiate(editButtonPrefab, setUIPos(i, j, 0), Quaternion.identity, parent) as GameObject;
			}
		}
	}

	public GameObject[,] getUIobjects() {
		return MapEditorButtons;
	}


	public MassStruct[,] getCurrentFieldDatas() {//save用メソッド、この関数を実行した後csvmanagerのMapCsvSaveを実行する。
		MassStruct[,] fieldData = new MassStruct[Config.maxGridNum, Config.maxGridNum];
		for (int j = 0; j < Config.maxGridNum; ++j) {
			for (int i = 0; i < Config.maxGridNum; ++i) {
				fieldData[i, j].masskind = MapEditorButtons[i, j].GetComponent<FieldObjectEditUI>() .returnThisState();
				fieldData[i, j].massnumber = 2;
			}
		}
		return fieldData;
	}

	public void ButtonStatusUpdate(MassStruct[,] newfieldData) {
		for (int j = 0; j < Config.maxGridNum; ++j) {
			for (int i = 0; i < Config.maxGridNum; ++i) {
				int itemkind = newfieldData[i, j].masskind;
				MapEditorButtons[i, j].GetComponent<FieldObjectEditUI>().changeState(itemkind);
			}
		}
	}

	public void deleteEditorUIbuttons() {
		foreach(var item in MapEditorButtons)
		Destroy(item);
	}

	public void EditorUISetRandamKind() {
		for (int j = 0; j < Config.maxGridNum; ++j) {
			for (int i = 0; i < Config.maxGridNum; ++i) {
				int radomValue = UnityEngine.Random.Range(0, Enum.GetNames(typeof(MathMass.massstate)).Length);//float型を引数にとるときと、int型を引数にとるときで挙動が変わる事に注意
					MapEditorButtons[i, j].GetComponent<FieldObjectEditUI>().changeState(radomValue);
			}
		}
	}
}


//public void loadMapCSV() {//指定のcsvからデータを読み込み、UIオブジェクトのstateを変える。
//	MassStruct[,] fieldData = dataholder.GetStageMapData(dataholder.getStageNum());
//	for (int j = 0; j < Config.maxGridNum; ++j) {
//		for (int i = 0; i < Config.maxGridNum; ++i) {
//			int objectkind = fieldData[i,j].masskind;
//			MapEditorButtons[i,j].GetComponent<FieldObjectEditUI>().changeState(objectkind);
//		}
//	}
//}