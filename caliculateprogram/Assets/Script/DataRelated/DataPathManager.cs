using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DataPathManager : MonoBehaviour {//ゲームデータの保存パスを管理するクラス。csvmanagerとの連携度高

	private string filename;//0はマップデータ、1はditemdata,2はclearcondinaldata
	private string mapdatapath;
	private string cleardatapath;
	


	void Start() {
		filename = "mapData";
		mapdatapath = Application.dataPath + "/data/" + filename + ".csv";
	}

	public string getmapdatapath() {
		return mapdatapath;
	}

	public string getclearConditionpath() {
		cleardatapath = Application.dataPath + "/data/" + "clearCondition" + ".csv";
		return cleardatapath;
	}

	public void ChangeStagePathNum(int stagevalue) {//保存先かつ呼び出し元のファイルパスを変更する。
		string newfilename = "mapData" + stagevalue.ToString() + ".csv";
		mapdatapath = Application.dataPath + "/data/" + newfilename;
		Debug.Log(String.Format("datapath was changed to {0}", mapdatapath));
	}
	//mapData01.csv,mapData02.csv,mapData03.csvという形で出力される、デフォルト値はtestmapData.csv

}
