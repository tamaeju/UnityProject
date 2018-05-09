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
		//mapdatapath = Application.dataPath + "/Resources/data/" + filename + ".csv";
		mapdatapath = "data/" + filename;
		mapsavedatapath = Application.dataPath + "/Resources/data/" + filename + ".csv";
	}

	public string getmapdatapath () {
		return mapdatapath;
	}

	public string getmapsavedatapath () {
		return mapsavedatapath;
	}

	public string getclearConditionpath () {
		//cleardatapath = Application.dataPath + "/Resources/data/" + "clearCondition" + ".csv";
		cleardatapath = "data/" + "clearCondition";
		return cleardatapath;
	}

	public void ChangeStagePathNum (int stagevalue) { //保存先かつ呼び出し元のファイルパスを変更する。
		string newfilename = "mapData" + stagevalue.ToString ();
		//mapdatapath = Application.dataPath + "/Resources/data/" + newfilename;
		mapdatapath = "data/" + newfilename;
		Debug.Log (String.Format ("datapath was changed to {0}", mapdatapath));

		mapsavedatapath = Application.dataPath + "/Resources/data/" + newfilename;
	}
	//mapData01.csv,mapData02.csv,mapData03.csvという形で出力される、デフォルト値はtestmapData.csv

}