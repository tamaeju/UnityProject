using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CSVManager : MonoBehaviour{//CSVデータの読み込みと書き込みを行うクラス
	StreamWriter m_sw;//dataElementsからパースして使うデータ
	int[][] stagedata;//何秒以内クリアか、必要捕食数のデータ

	public int[,] getDataElement(string aDatapassANDname,int usingcolumnNum) {
		int[][] dataElements;
		int[,] practicalDataElements;
		Debug.Log(aDatapassANDname);
		dataElements = getJagDataElement(aDatapassANDname);//ジャグデータをもらって、
		practicalDataElements = parsePracticalDataElements(dataElements, usingcolumnNum);//2次元配列にしたcsvのデータを取得するのだが、
		return practicalDataElements;
	}

	public int [][] getJagDataElement(string datapassANDname) {
		int[][] dataElements;
		Debug.Log(datapassANDname);
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
		return dataElements;
	}

	int[,] parsePracticalDataElements(int[][] oldData,int usingcolumnNum) {//ジャグ配列からグリッド座標毎に1要素となるアイテムに対応した2次元配列への変換メソッド
		int [,]practicalDataElements = new int[DataManager.maxGridNum, DataManager.maxGridNum];
		for (int j = 0; j < practicalDataElements.GetLength(1); j++) {
			for (int i = 0; i < practicalDataElements.GetLength(0); i++) {
				practicalDataElements[i, j] = oldData[practicalDataElements.GetLength(0) * j + i][usingcolumnNum];
			}
		}
		return practicalDataElements;
		//DebugCSVData();
	}

	public int[] get1dimentionalData(string aDatapassANDname, int extractcolomn) {
		int[][] dataElements = getJagDataElement(aDatapassANDname);
		int[] getdata = new int[dataElements.Length];
		for (int i = 0; i < 0; i++) {
			getdata[i] = dataElements[i][extractcolomn];
				}
		return getdata;
	}

	//以下データ書き込み部分

	public void MapdataCSVSave(string aDatapath, int[,] writtenData) {//アセットフォルダにtest.csvというファイルを作成する。作成するときはこのクラスを呼び出し、データを渡せばいい。
		File.Delete(aDatapath);
		FileInfo fi;
		fi = new FileInfo(aDatapath);
		m_sw = fi.AppendText();
		writeMapData(writtenData);
		m_sw.Flush();
		m_sw.Close();
		Debug.Log("file was written");
	}
	private void writeMapData(int[,] writtenData) {//実際にログデータを書く部分、流れとしてはオブジェクトのデータを取得し、それを書いていくだけなので、int[,]がもらえればいいだけの話。
		for (int j = 0; j < writtenData.GetLength(1); j++) {
			for (int i = 0; i < writtenData.GetLength(0); i++) {
				m_sw.WriteLine("{0},{1},{2}", i.ToString(), j.ToString(), writtenData[i, j].ToString());
			}
		}
	}

	public void itemdataCSVSave(string aDatapath, dragitemdata[,] writtenData) {//アセットフォルダにtest.csvというファイルを作成する。作成するときはこのクラスを呼び出し、データを渡せばいい。
		Debug.Log("itemdataCSVSave");
		File.Delete(aDatapath);
		FileInfo fi;
		fi = new FileInfo(aDatapath);
		m_sw = fi.AppendText();
		writeitemData(writtenData);
		m_sw.Flush();
		m_sw.Close();
		Debug.Log("file was written");
	}
	private void writeitemData(dragitemdata[,] writtenData) {
		for (int j = 0; j < writtenData.GetLength(1); j++) {
			for (int i = 0; i < writtenData.GetLength(0); i++) {
				m_sw.WriteLine("{0},{1},{2}.{3}", i, j, writtenData[i, j].itemkind, writtenData[i, j].itemcount);
			}
		}
	}

	public void clearconditionaldataCSVSave(string aDatapath,clearconditiondata[] writtenData) {//アセットフォルダにtest.csvというファイルを作成する。作成するときはこのクラスを呼び出し、データを渡せばいい。
		File.Delete(aDatapath);
		FileInfo fi;
		fi = new FileInfo(aDatapath);
		m_sw = fi.AppendText();
		writeclearconditionalData(writtenData);
		m_sw.Flush();
		m_sw.Close();
		Debug.Log("file was written");
	}
	private void writeclearconditionalData(clearconditiondata[] writtenData) {
			for (int i = 0; i < writtenData.Length; i++) {
				m_sw.WriteLine("{0},{1},{2}", i, writtenData[i].timelimit , writtenData[i].RequiredKillCount);
			}
		
	}
}



