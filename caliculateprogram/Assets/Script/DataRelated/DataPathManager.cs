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
	private string csvdatapath;
	private int filekindlength = Config.filekindlength;
	private int stageCount = Config.stageCount;


	void Start() {
		filename = "mapData";
		csvdatapath = Application.dataPath + "/data/" + filename + ".csv";
	}

	public string getmapdatapath() {
		return csvdatapath;
	}


	public void ChangeMapCSVNum(int stagevalue) {//保存先かつ呼び出し元のファイルパスを変更する。
		string newfilename = filename + stagevalue.ToString() + ".csv";
		csvdatapath = Application.dataPath + "/data/" + filename;
		Debug.Log(String.Format("{0}file was changed ", csvdatapath));
	}

}
