using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ObjectMaker : MonoBehaviour {//オブジェクト生成を行うクラス。
	
	float blocklength = Config.blocklength;
	[SerializeField]
	GameObject moveprefab;
	[SerializeField]
	GameObject massprefab;

	GameObject moveobject;

	GameObject[,] massobjects;

	private void Start() {
		massobjects = new GameObject[Config.maxGridNum, Config.maxGridNum];
	}


	public void InstanciateObject(int[,] _leveldesigndata, int i, int j) {
		if (_leveldesigndata[i, j] == 0) {//0はプレイヤー
			moveobject = Instantiate(moveprefab, settingObjectPos(i, j, 0), Quaternion.identity) as GameObject;
		}
		else {
			massobjects[i,j] = Instantiate(massprefab, settingObjectPos(i, j, 0), Quaternion.identity) as GameObject;
			//生成後に値を入れるメソッドを実行→MassStruct[,]を使用するmassstruct.massnumberとmassstruct.masskindを使用する。
		}
	}

	public void instanciateAllMapObject(int[,] _leveldesigndata) {//playerやブロックなどのオブジェクトを生成するメソッド。
		for (int j = 0; j < _leveldesigndata.GetLength(1); ++j) {
			for (int i = 0; i < _leveldesigndata.GetLength(0); ++i) {
				if (_leveldesigndata[i, j] != 0) {//0はアイテムなし
					InstanciateObject(_leveldesigndata, i, j);
				}
			} 
		}
	}

	Vector3 settingObjectPos(int x, int y, float z)
		{
		Vector3 returnPos = new Vector3(x * blocklength, z, y * blocklength);
		return returnPos;
	}




}

