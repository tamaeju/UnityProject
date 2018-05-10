using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DataPathManager : MonoBehaviour { //ゲームデータの保存パスを管理するクラス。csvmanagerとの連携度高

	private string filename; //0はマップデータ、1はditemdata,2はclearcondinaldata
	private string mapdatapath;
	private string mapsavedatapath;
	private string cleardatapath;

	void Start () {
		filename = "mapData0";
		mapdatapath = "data/" + filename;
		mapsavedatapath = Application.dataPath + "/Resources/data/" + filename + ".csv";
	}

	public string getmapdatapath (int stagevalue) {
		ChangeStagePathNum (stagevalue);
		return mapdatapath;
	}

	public string getmapsavedatapath (int stagevalue) {
		ChangeStagePathNum (stagevalue);
		return mapsavedatapath;
	}

	public string getclearConditionpath () {
		cleardatapath = "data/" + "clearCondition";
		return cleardatapath;
	}

	private void ChangeStagePathNum (int stagevalue) { //保存先かつ呼び出し元のファイルパスを変更する。
		string newfilename = "mapData" + stagevalue.ToString ();
		mapdatapath = "data/" + newfilename;
		mapsavedatapath = Application.dataPath + "/Resources/data/" + newfilename + ".csv";
	}
	//mapData01.csv,mapData02.csv,mapData03.csvという形で出力される、デフォルト値はtestmapData.csv

}