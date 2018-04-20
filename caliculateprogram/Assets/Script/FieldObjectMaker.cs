using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FieldObjectMaker : MonoBehaviour {//オブジェクト生成を行うクラス。

	float blocklength = Config.blocklength;
	float blockshiftlength = 0.2f;
	[SerializeField]
	GameObject moveprefab;
	[SerializeField]
	GameObject plusPrefab;
	[SerializeField]
	GameObject substractPrefab;
	[SerializeField]
	GameObject multiplePrefab;
	[SerializeField]
	GameObject dividePrefab;
	[SerializeField]
	GameObject squarePrefab;



	[SerializeField]
	GameObject goalprefab;
	//実生成するオブジェクト

	GameObject moveobject;
	GameObject[,] massobjects;
	GameObject goalobject;
	//生成したオブジェクトの参照


	[SerializeField]
	DataStorage datastorager;
	MassStruct[,] fieldmapdata;
	//生成元データの取得のため

	void Start() {
		massobjects = new GameObject[Config.maxGridNum, Config.maxGridNum];
	}



	public void LoadMapDatas() {
		fieldmapdata = datastorager.GetStageMapData(datastorager.getStageNum());
	}


	public void InstanciateObject(int i, int j) {
		if (fieldmapdata[i, j].masskind == Enum.GetNames(typeof(MathMass.massstate)).Length + (int)FieldObjectEditUI.DebugUIkind.movingobject ) {
			moveobject = Instantiate(moveprefab, settingObjectPos(i, j), Quaternion.identity) as GameObject;
			moveobject.GetComponent<MovingMass>().SetMyPos(i, j);
			moveobject.transform.position = settingObjectPos(i, j);
		}
		
		else if (fieldmapdata[i, j].masskind == Enum.GetNames(typeof(MathMass.massstate)).Length + (int)FieldObjectEditUI.DebugUIkind.goal) {
			massobjects[i, j] = Instantiate(goalprefab, settingObjectPos(i, j), Quaternion.identity) as GameObject;
			massobjects[i, j].GetComponent<MathMass>().ChangeisGoal();
			massobjects[i, j].GetComponent<MathMass>().ChangeMyKind(fieldmapdata[i, j].masskind);
			massobjects[i, j].GetComponent<MathMass>().SetMyPos(i, j);

			
		}
		else{
			int mathmasskind = fieldmapdata[i, j].masskind;
			Debug.LogFormat("i, j,fieldmapdata[i, j],massobjects[i, j]は{0},{1},{2},{3}", i, j, fieldmapdata[i, j], massobjects[i, j]);
			massobjects[i, j] = Instantiate(instanceMathMass(mathmasskind), settingObjectPos(i, j), Quaternion.Euler(0, 0, 180)) as GameObject;
			massobjects[i, j].GetComponent<MathMass>().SetMyPos(i, j);
			massobjects[i, j].GetComponent<MathMass>().ChangeMyKind(fieldmapdata[i, j].masskind);
			massobjects[i, j].GetComponent<MathMass>().ChangeMynumber(fieldmapdata[i, j].massnumber);
		}
	}

	public void instanciateAllMapObject() {//playerやブロックなどのオブジェクトを生成するメソッド。
		for (int j = 0; j < fieldmapdata.GetLength(1); ++j) {
			for (int i = 0; i < fieldmapdata.GetLength(0); ++i) {
				InstanciateObject(i, j);
			}
		}
	}

	public GameObject instanceMathMass(int masskind) {
		switch (masskind) {
			case (int)MathMass.massstate.add:
				return plusPrefab;
			case (int)MathMass.massstate.divide:
				return dividePrefab;
			case (int)MathMass.massstate.multiplicate:
				return multiplePrefab;
			case (int)MathMass.massstate.square:
				return squarePrefab;
			case (int)MathMass.massstate.substract:
				return substractPrefab;
			default:
				Debug.Log("There is no match prefab");
				return null;
		}
		


	}


	public GameObject GetMovingMass() {
		return moveobject;
	}
	public GameObject[,] GetMathMasses() {
		return massobjects;
	}

	public Vector2 settingObjectPos(int x, int y) {
		return Config.settingObjectPos(x, y);
	}
}


//デバッグ用のtrycatch。デバッグ終了後すぐ消す
//Debug.LogFormat("i, jは{0},{1}", i, j);
//Debug.LogFormat("i,j, fieldmapdata[i, j].masskind, fieldmapdata[i, j].massnumberはそれぞれ{0},{1},{2},{3}", i,j, fieldmapdata[i, j].masskind, fieldmapdata[i, j].massnumber);

//public void debugArrayLogDispklay() {
//	for (int j = 0; j < fieldmapdata.GetLength(1); ++j) {
//		for (int i = 0; i < fieldmapdata.GetLength(0); ++i) {
//			Debug.LogFormat("i, jは{0},{1}", i, j);
//		}
//	}
//}