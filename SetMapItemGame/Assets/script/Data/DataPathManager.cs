using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DataPathManager : MonoBehaviour { //ゲームデータの保存パスを管理するクラス。csvmanagerとの連携度高

	private string[] filename; //0はマップデータ、1はditemdata,2はclearcondinaldata
	private string[] csvdatapath;
	private string[] originfilename;
	private int filekindlength = Config.filekindlength;

	void Start () {
		filename = new string[filekindlength];
		csvdatapath = new string[filekindlength];
		originfilename = new string[filekindlength];

		originfilename[0] = "mapData";
		originfilename[1] = "ditemData0";
		originfilename[2] = "clearconditionalData0";

		for (int i = 0; i < filekindlength; i++) {
			filename[i] = originfilename[i];
			csvdatapath[i] = "data/" + filename[i];
		}
	}

	public string getmapdatapath () {
		return csvdatapath[0];
	}
	public string getitemdatapath () {
		return csvdatapath[1];
	}
	public string getconditiondatapath () {
		return csvdatapath[2];
	}

	public void ChangeMapCSVNum (int stagevalue) { //保存先かつ呼び出し元のファイルパスを変更する。
		filename[0] = originfilename[0] + stagevalue.ToString ();
		csvdatapath[0] = "data/" + filename[0];
		Debug.Log (String.Format ("{0}file was changed ", csvdatapath[0]));
	}
	public enum datakind {
		map,
		item,
		clearcondition
	}
}