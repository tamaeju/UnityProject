using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

class CSVManager{//CSVデータの読み込みと書き込みを行うクラス
	StreamWriter m_sw;
	int[][] dataElements;//csvから作ったデータ
	int[,] practicalDataElements;//dataElementsからパースして使うデータ
	string filename;
	string datapath;

	public CSVManager() {
		filename = "testData.csv";
		datapath = Application.dataPath + "/data/" + filename;
	}

	public int[,] getDataElement(string aDatapassANDname,int usingcolumnNum) {
		getJagDataElement(aDatapassANDname);
		parsePracticalDataElements(dataElements, usingcolumnNum);
		return practicalDataElements;
	}


	void getJagDataElement(string datapassANDname) {

		string textFile = datapassANDname;
		System.Text.Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
		string[] lines = System.IO.File.ReadAllLines(textFile, enc);
		string[] RowStrings = lines[0].Split(',');


		dataElements = new int[lines.Length][];
		for (int i = 0; i < lines.Length; i++) {
			dataElements[i] = new int[RowStrings.Length];
		}

		for (int j = 0; j < dataElements.Length; ++j) {
			RowStrings = lines[j].Split(',');
			for (int i = 0; i < dataElements[0].Length; ++i) {
				dataElements[j][i] = Int32.Parse(RowStrings[i]);
			}
		}
	}

	void parsePracticalDataElements(int[][] oldData,int usingcolumnNum) {//ジャグ配列から2次元配列への変換メソッド
		practicalDataElements = new int[DataManager.maxGridNum, DataManager.maxGridNum];
		for (int j = 0; j < practicalDataElements.GetLength(1); j++) {
			for (int i = 0; i < practicalDataElements.GetLength(0); i++) {
				practicalDataElements[i, j] = oldData[practicalDataElements.GetLength(0) * j + i][usingcolumnNum];
			}
		}
		//DebugCSVData();
	}

	void DebugJagCSVData()
	{
		int checkcolumn = 2;
		for (int j = 0; j < dataElements.Length; ++j)
		{
				Debug.Log(dataElements[j][0].ToString()+","+ dataElements[j][1] + ","+ dataElements[j][checkcolumn]);
		}
	}
	void DebugCSVData()
	{
		for (int j = 0; j < practicalDataElements.GetLength(1); j++)
		{
			for (int i = 0; i < practicalDataElements.GetLength(0); i++)
			{
				Debug.Log(i + "," + j + "," + practicalDataElements[i,j]);
			}
		}
	}

	public void logSave(string aDatapath, int[,] writtenData) {//アセットフォルダにtest.csvというファイルを作成する。作成するときはこのクラスを呼び出し、データを渡せばいい。
		File.Delete(aDatapath);
		FileInfo fi;
		fi = new FileInfo(aDatapath);
		m_sw = fi.AppendText();
		writeLogData(writtenData);
		m_sw.Flush();
		m_sw.Close();
		Debug.Log("wrote");
	}
	void writeLogData(int[,] writtenData) {//実際にログデータを書く部分、流れとしてはオブジェクトのデータを取得し、それを書いていくだけなので、int[,]がもらえればいいだけの話。
		for (int j = 0; j < writtenData.GetLength(1); j++) {
			for (int i = 0; i < writtenData.GetLength(0); i++) {
				m_sw.WriteLine("{0},{1},{2}", i.ToString(), j.ToString(), writtenData[i, j].ToString());
			}
		}
	}
}



