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
	int stageNum;

	void Start () {
		if (datapathmanager == null) {
			datapathmanager = this.gameObject.AddComponent<DataPathManager> ();
		}
	}

	private int[][] getJagDataElement (string datapassANDname) { //ジャグデータをもらってから、それを2次元配列に入れる事が重要。その場合はint[][]からs
		int[][] dataElements;
		string textFile = datapassANDname;
		System.Text.Encoding enc = System.Text.Encoding.GetEncoding ("utf-8");
		TextAsset bindata = Resources.Load (datapassANDname) as TextAsset;
		Debug.LogFormat ("bindata, datapassANDnameは{0}、{1}", bindata, datapassANDname);
		string stringBindata = bindata.text;
		string[] lines = stringBindata.Split ('\n');

		Debug.LogFormat ("lines.Lengthは、{0}", lines.Length);
		//string[] lines = System.IO.File.ReadAllLines (textFile, enc); //システムIOがtextFile（パス）のデータを読み込む。
		string[] RowStrings = lines[0].Split (','); //lines[0]を,で配列にわけて格納する。
		dataElements = new int[lines.Length][];
		for (int i = 0; i < lines.Length; i++) {
			dataElements[i] = new int[RowStrings.Length];
		}
		//for (int j = 0; j < dataElements.Length; ++j) {
		for (int j = 0; j < Config.maxGridNum * Config.maxGridNum; ++j) { //csvがなぜか空行を含んでいたので
			RowStrings = lines[j].Split (',');
			for (int i = 0; i < dataElements[0].Length; ++i) {
				//Debug.LogFormat ("i, jは、{0}、{1}", i, j);
				dataElements[j][i] = Int32.Parse (RowStrings[i]);
			}
		}
		return dataElements;
		//やる事
		//resorcesloadを使って、テキストファイル読み込み
		//テキストファイルを\nで分けて行ごとに分割
		//1行を,で分けて分割
		//1要素ごとにデータを格納する。
	}

	private int[][] getDataElement_needtoprocess (string datapath) {
		return getJagDataElement (datapath);
	}

	public MassStruct[, ] getMapDataElements () //現時点でのステージ番目のマップデータパスを取得してくる
	{
		DataChangerFromJaG datachanger = gameObject.AddComponent<DataChangerFromJaG> ();
		int[][] origindata = getDataElement_needtoprocess (datapathmanager.getmapdatapath ()); //
		return datachanger.ParseUsableaMapdatas (origindata);
	}

	public ClearConditionStruct[] getClearConditionElements () {
		DataChangerFromJaG datachanger = gameObject.AddComponent<DataChangerFromJaG> ();
		int[][] origindata = getDataElement_needtoprocess (datapathmanager.getclearConditionpath ());
		return datachanger.ParseUsableaClearCondition (origindata);
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

	public void MapCsvSave (MassStruct[, ] writtendata) { //CSVSaveのジェネリック使用対応メソッド
		Action<MassStruct[, ]> actaug = writeData;
		CSVSave (datapathmanager.getmapsavedatapath (), writtendata, actaug);
	}

	public void ClearConditionCsvSave (ClearConditionStruct[] writtendata) { //CSVSaveのジェネリック使用対応メソッド
		Action<ClearConditionStruct[]> actaug = writeData;
		CSVSave (datapathmanager.getmapdatapath (), writtendata, actaug);
	}

	private void writeData (ClearConditionStruct[] writtenData) { //オーバーライドメソッド
		for (int i = 0; i < writtenData.GetLength (0); i++) {
			m_sw.WriteLine ("{0},{1},{2}", i.ToString (), writtenData[i].clearcount.ToString (), writtenData[i].clearnumber.ToString ());
		}
		Debug.Log ("ClearData was written");
	}

	private void writeData (MassStruct[, ] writtenData) { //オーバーライドメソッド
		for (int j = 0; j < writtenData.GetLength (1); j++) {
			for (int i = 0; i < writtenData.GetLength (0); i++) {
				m_sw.WriteLine ("{0},{1},{2},{3}", i.ToString (), j.ToString (), writtenData[i, j].masskind.ToString (), writtenData[i, j].massnumber.ToString ());
			}
		}
		Debug.Log ("MapData was written");
	}

	public void ChangeStagePathNum (Dropdown dropdown) {
		ChangeStagePathNum (dropdown.value);
	}

	private void ChangeStagePathNum (int aStageNum) {
		datapathmanager.ChangeStagePathNum (aStageNum);
		stageNum = aStageNum;
	}

	public int getStageNum () {
		return stageNum;
	}

	public MassStruct[, ] getStageMapDataElements (int stageCount) {
		ChangeStagePathNum (stageCount);
		return getMapDataElements ();
	}
	public void DebugsaveAllMapCsvData (MassStruct[, ] samedata) {
		for (int i = 0; i < Config.stageCount; i++) {
			ChangeStagePathNum (i);
			MapCsvSave (samedata);
		}
	}
}

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