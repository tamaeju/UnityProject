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
	private int m_stage;

	void Start () {
		filename = new string[filekindlength];
		csvdatapath = new string[filekindlength];
		originfilename = new string[filekindlength];

		originfilename[0] = "mapData";
		originfilename[1] = "ditemData";
		originfilename[2] = "clearconditionalData";

		ChangeMapCSVNum (0);
	}

	public string getmapdatapath () {
		Debug.LogFormat ("今のm_stageは{0}です", m_stage);
		return csvdatapath[0];
	}
	public string getitemdatapath () {
		Debug.LogFormat ("今のm_stageは{0}です", m_stage);
		return csvdatapath[1];
	}
	public string getconditiondatapath () {
		Debug.LogFormat ("今のm_stageは{0}です", m_stage);
		return csvdatapath[2];
	}

	public void ChangeMapCSVNum (int stagevalue) { //保存先かつ呼び出し元のファイルパスを変更する。
		m_stage = stagevalue;
		for (int i = 0; i < filename.Length; i++) {
			filename[i] = originfilename[i] + stagevalue.ToString ();
			csvdatapath[i] = "data/" + filename[i];
			Debug.Log (String.Format ("{0}file was changed ", csvdatapath[i]));
		}
	}
	public enum datakind {
		map,
		item,
		clearcondition
	}
}