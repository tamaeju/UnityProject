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
	private string[] csvLoadPath;
	private string[] csvSavePath;

	private string[] originfilename;
	private int filekindlength = Config.filekindlength;
	private int m_stage;

	void Start () {
		filename = new string[filekindlength];
		csvLoadPath = new string[filekindlength];
		csvSavePath = new string[filekindlength];
		originfilename = new string[filekindlength];

		originfilename[0]　 = "mapData";
		originfilename[1]　 = "ditemData";
		originfilename[2]　 = "clearconditionalData";
		ChangeMapCSVNum (0);
	}

	public string getmapdatapath () {
		if (csvLoadPath[0] == null) { Debug.LogWarning ("csvLoadPathがnullです"); }
		Debug.LogFormat ("パスを返します。csvLoadPath[0]は{0}です", csvLoadPath[0]);
		return csvLoadPath[0];
	}
	public string getitemdatapath () {
		if (csvLoadPath[0] == null) { Debug.LogWarning ("csvLoadPathがnullです"); }
		Debug.LogFormat ("パスを返します。csvLoadPath[1]は{0}です", csvLoadPath[1]);
		filename[1] = originfilename[1] + "0";
		csvLoadPath[1] = "data/" + filename[1];
		return csvLoadPath[1];
	}
	public string getconditiondatapath () {
		if (csvLoadPath[2] == null) { Debug.LogWarning ("csvLoadPathがnullです"); }
		Debug.LogFormat ("csvLoadPath[2]は{0}です", csvLoadPath[2]);
		//return csvLoadPath[2];
		return csvLoadPath[2];
	}
	public string getmapdatasavepath () {
		if (csvSavePath[0] == null) { Debug.LogWarning ("csvSavePathがnullです"); }
		Debug.LogFormat ("パスを返します。csvSavePath[0]は{0}です", csvSavePath[0]);
		return csvSavePath[0];
	}
	public string getitemdatasavepath () {
		if (csvSavePath[1] == null) { Debug.LogWarning ("csvSavePathがnullです"); }
		Debug.LogFormat ("パスを返します。csvSavePath[1]は{1}です", csvSavePath[1]);
		return csvSavePath[1];
	}
	public string getconditiondatasavepath () {
		if (csvSavePath[2] == null) { Debug.LogWarning ("csvSavePathがnullです"); }
		Debug.LogFormat ("パスを返します。csvSavePath[2]は{2}です", csvSavePath[2]);

		return csvSavePath[2];
	}

	public void ChangeMapCSVNum (int stagevalue) {
		m_stage = stagevalue; //デバッグログ用
		for (int i = 0; i < filename.Length; i++) {
			if (i == 0) { //実際変更するのはフィールドデータのみとなる。それ以外はひとつのcsvに全データが存在するので処理の必要なし。
				filename[i] = originfilename[i] + stagevalue.ToString ();
			} else {
				filename[i] = originfilename[i] + "0";
			}
			csvLoadPath[i] = "data/" + filename[i];
			csvSavePath[i] = Application.dataPath + "/Resources/data/" + filename[i] + ".csv";
			Debug.Log (String.Format ("csvLoadPathは{0}、csvSavePath[i]は{1}に変更されました", csvLoadPath[i], csvSavePath[i]));
		}
	}

}