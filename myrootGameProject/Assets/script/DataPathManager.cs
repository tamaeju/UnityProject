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
	int filekindcount = 3;


	void Start() {
		filename = new string[filekindcount];
		csvdatapath = new string[filekindcount];
		originfilename = new string[filekindcount];

		originfilename[0] = "mapData";
		originfilename[1] = "ditemDat";
		originfilename[2] = "clearconditionalData0";

		for (int i = 0; i < filekindcount; i++) {
			filename[i] = originfilename[i] + "0.csv";
			csvdatapath[i] = Application.dataPath + "/data/" + filename[i];
		}
	}

	public string getfilename(int filekind) {
		return filename[filekind];
	}
	public string getcsvdatapath(int filekind) {
		return csvdatapath[filekind];
	}

	public void ChangeCSVNum(int filekind, int dropdownvalue) {//保存先と、呼び出し先のcsvを変更するメソッド
		filename[filekind] = originfilename[filekind] + dropdownvalue.ToString() + ".csv";
		csvdatapath[filekind] = Application.dataPath + "/data/" + filename[filekind];
		Debug.Log(String.Format("{0}file was changed ", csvdatapath[filekind]));
	}
}
