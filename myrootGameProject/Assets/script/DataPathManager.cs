using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DataPathManager : MonoBehaviour {

	string[] filename;//0マップデータ、1ditemdata,2clearcondinaldata
	string[] csvdatapath;
	int stageindex;
	string[] originfilename;
	string mapdata = "mapdata";
	string ditemData = "ditemData";
	string clearconditionalData = "clearconditionalData";

	void Start() {
		filename = new string[3];
		csvdatapath = new string[3];
		originfilename = new string[3];
		filename[0] = "mapData0.csv";
		csvdatapath[0] = Application.dataPath + "/data/" + filename[0];

		filename[1] = "ditemData0.csv";
		csvdatapath[1] = Application.dataPath + "/data/" + filename[1];

		filename[2] = "clearconditionalData0.csv";
		csvdatapath[2] = Application.dataPath + "/data/" + filename[2];


	}

	public string getfilename(int filekind) {
		return filename[filekind];
	}
	public string getcsvdatapath(int filekind) {
		return csvdatapath[filekind];
	}

	public void ChangeCSVNum(int filekind, int dropdownvalue) {//保存先と、呼び出し先のcsvを変更するメソッド
		filename[filekind] = "testData" + dropdownvalue.ToString() + ".csv";
		csvdatapath[filekind] = Application.dataPath + "/data/" + filename[filekind];
		Debug.Log(String.Format("{0}file was changed ", datapathmanager.getfilename()));
	}
}//ファイル名とパスだけを入れるメソッドを作る。
