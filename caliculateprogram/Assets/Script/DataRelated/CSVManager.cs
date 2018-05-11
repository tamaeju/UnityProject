using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CSVManager : MonoBehaviour { //CSVデータの読み込みと書き込みを行うクラス
	StreamWriter m_sw;
	DataPathManager datapathmanager;

	void Start () {
		if (datapathmanager == null) {
			datapathmanager = this.gameObject.AddComponent<DataPathManager> ();
		}
	}

	private int[][] getJagDataElement (string datapassANDname) { //ジャグデータをもらってから、それを2次元配列に入れる事が重要。その場合はint[][]からs
		int[][] dataElements;
		TextAsset bindata = Resources.Load (datapassANDname) as TextAsset; //指定したパスからテキストアセット型のデータを取得
		Debug.LogFormat ("bindata, datapassANDnameは{0}、{1}", bindata, datapassANDname); //デバッグ用
		string stringBindata = bindata.text; //
		string[] lines = stringBindata.Split ('\n'); //\nで1行毎のデータに変換する。
		Debug.LogFormat ("lines.Lengthは、{0}", lines.Length);
		string[] RowStrings = lines[0].Split (','); //要素数を出すために、lines[0]を,で配列にわけて格納する。(このrowstrings自体は使用しない)
		dataElements = new int[lines.Length][]; //linesの要素数分データエレメントを作成
		for (int i = 0; i < lines.Length; i++) {
			dataElements[i] = new int[RowStrings.Length]; //RowStringsの要素数分データエレメントを作成
		}

		for (int j = 0; j < Config.maxGridNum * Config.maxGridNum; ++j) {
			RowStrings = lines[j].Split (','); //j番目のrowstringsを作成
			for (int i = 0; i < dataElements[i].Length; ++i) {
				dataElements[j][i] = Int32.Parse (RowStrings[i]);
			}
		}
		return dataElements;
	}

	public MassStruct[, ] getMapDataElements (int stageCount) //ステージデータを取得する
	{
		int[][] origindata = getJagDataElement (datapathmanager.getmapdatapath (stageCount));
		DataChangerFromJaG datachanger = gameObject.AddComponent<DataChangerFromJaG> ();
		return datachanger.ParseUsableaMapdatas (origindata);
	}

	public ClearConditionStruct[] getClearConditionElements () { //クリア条件データを取得する
		int[][] origindata = getJagDataElement (datapathmanager.getclearConditionpath ());
		DataChangerFromJaG datachanger = gameObject.AddComponent<DataChangerFromJaG> ();
		return datachanger.ParseUsableaClearCondition (origindata);
	}

	public void MapCsvSave (MassStruct[, ] writtendata, int stageCount) { //CSVSaveのジェネリック使用対応メソッド
		Action<MassStruct[, ]> actaug = writeData;
		CSVSave (datapathmanager.getmapsavedatapath (stageCount), writtendata, actaug);
	}

	public void ClearConditionCsvSave (ClearConditionStruct[] writtendata, int stageCount) { //CSVSaveのジェネリック使用対応メソッド
		Action<ClearConditionStruct[]> actaug = writeData;
		CSVSave (datapathmanager.getclearConditionpath (), writtendata, actaug);
	}
	private void CSVSave<T> (string aDatapath, T writtendata, Action<T> act) { //アセットフォルダにtest.csvというファイルを作成する。
		File.Delete (aDatapath);
		FileInfo fi;
		fi = new FileInfo (aDatapath);
		m_sw = fi.AppendText ();
		act (writtendata);
		m_sw.Flush ();
		m_sw.Close ();
		Debug.Log ("file was written");
	}

	private void writeData (ClearConditionStruct[] writtenData) { //ストリームライターにcsvの書き出しを依頼する
		for (int i = 0; i < writtenData.GetLength (0); i++) {
			m_sw.WriteLine ("{0},{1},{2}", i.ToString (), writtenData[i].clearcount.ToString (), writtenData[i].clearnumber.ToString ());
		}
		Debug.Log ("ClearData was written");
	}

	private void writeData (MassStruct[, ] writtenData) { //ストリームライターにcsvの書き出しを依頼する
		for (int j = 0; j < writtenData.GetLength (1); j++) {
			for (int i = 0; i < writtenData.GetLength (0); i++) {
				m_sw.WriteLine ("{0},{1},{2},{3}", i.ToString (), j.ToString (), writtenData[i, j].masskind.ToString (), writtenData[i, j].massnumber.ToString ());
			}
		}
		Debug.Log ("MapData was written");
	}

	public void DebugsaveAllMapCsvData (MassStruct[, ] samedata) {
		for (int i = 0; i < Config.stageCount; i++) {
			MapCsvSave (samedata, i);
		}
	}

}
//ステージのパスは入力側が入れるように修正する。

//int[][] stagedata;//何秒以内クリアか、必要捕食数のデータのデータ。（ゲームで実際に使用するのはstruct型の2次元配列）
//private int[,] getDataElement(string aDatapassANDname, int usingcolumnNum) {//データパスと使用するカラムを入力して使用する。
//	int[][] dataElements;
//	int[,] practicalDataElements;
//	dataElements = getJagDataElement(aDatapassANDname);
//	practicalDataElements = parsePracticalDataElements(dataElements, usingcolumnNum);
//	return practicalDataElements;
//}

//public int[,] getMapDataElement() {//データパスと使用するカラムを入力して使用する。
//	int usecolomnnum = Config.usecolomn_of_mapdata-1 ;
//	string mapdatapass = datapathmanager.getmapdatapath();
//	return getDataElement(mapdatapass, usecolomnnum);
//}
//private int[,] parsePracticalDataElements(int[][] oldData, int usingcolumnNum) {//ジャグ配列からグリッド座標毎に1要素を使用するものに対応した2次元配列変換メソッド
//	int[,] practicalDataElements = new int[Config.maxGridNum, Config.maxGridNum];
//	for (int j = 0; j < practicalDataElements.GetLength(1); j++) {
//		for (int i = 0; i < practicalDataElements.GetLength(0); i++) {
//			practicalDataElements[i, j] = oldData[practicalDataElements.GetLength(0) * j + i][usingcolumnNum];
//		}
//	}
//	return practicalDataElements;
//}