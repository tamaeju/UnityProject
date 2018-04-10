﻿using System;
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
	GameObject massprefab;
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
		fieldmapdata = datastorager.GetMapDataElements();
	}


	public void InstanciateObject(int i, int j) {
		if (fieldmapdata[i, j].masskind == 4) {//5はプレイヤー
			moveobject = Instantiate(moveprefab, settingObjectPos(i, j), Quaternion.identity) as GameObject;
			moveobject.GetComponent<MovingMass>().SetMyPos(i, j);
			moveobject.transform.position = settingObjectPos(i, j);
		}

		else {
				massobjects[i, j] = Instantiate(massprefab, settingObjectPos(i, j), Quaternion.identity) as GameObject;
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


//public void debugArrayLogDispklay() {
//	for (int j = 0; j < fieldmapdata.GetLength(1); ++j) {
//		for (int i = 0; i < fieldmapdata.GetLength(0); ++i) {
//			Debug.LogFormat("i, jは{0},{1}", i, j);
//		}
//	}
//}