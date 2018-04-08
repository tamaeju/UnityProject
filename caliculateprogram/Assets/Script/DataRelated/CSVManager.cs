﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CSVManager : MonoBehaviour {//CSVデータの読み込みと書き込みを行うクラス
	StreamWriter m_sw;
	int[][] stagedata;//何秒以内クリアか、必要捕食数のデータのデータ。（ゲームで実際に使用するのはstruct型の2次元配列）
	DataPathManager datapathmanager;

	void Start() {

		datapathmanager = this.gameObject.AddComponent<DataPathManager>();
	}


	private int[,] getDataElement(string aDatapassANDname, int usingcolumnNum) {//データパスと使用するカラムを入力して使用する。
		int[][] dataElements;
		int[,] practicalDataElements;
		dataElements = getJagDataElement(aDatapassANDname);
		practicalDataElements = parsePracticalDataElements(dataElements, usingcolumnNum);
		return practicalDataElements;
	}

	public int[,] getMapDataElement() {//データパスと使用するカラムを入力して使用する。
		int usecolomnnum = Config.usecolomn_of_mapdata-1 ;
		string mapdatapass = datapathmanager.getmapdatapath();
		return getDataElement(mapdatapass, usecolomnnum);
	}



	private int[][] getJagDataElement(string datapassANDname) {//ジャグデータをもらってから、それを2次元配列に入れる事が重要。その場合はint[][]からs
		int[][] dataElements;
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


	private int[][] getMapaDataElement_needtoprocess() {
		return getJagDataElement(datapathmanager.getmapdatapath() );
	}

	public MassStruct[,] getMapDataElements()
	{
		DataChangerFromJaG datachanger = gameObject.AddComponent<DataChangerFromJaG>();
		int[][]origindata  = getMapaDataElement_needtoprocess();
		return datachanger.ParseUsableaMapdatas (origindata);
	}

	private int[,] parsePracticalDataElements(int[][] oldData, int usingcolumnNum) {//ジャグ配列からグリッド座標毎に1要素を使用するものに対応した2次元配列変換メソッド
		int[,] practicalDataElements = new int[Config.maxGridNum, Config.maxGridNum];
		for (int j = 0; j < practicalDataElements.GetLength(1); j++) {
			for (int i = 0; i < practicalDataElements.GetLength(0); i++) {
				practicalDataElements[i, j] = oldData[practicalDataElements.GetLength(0) * j + i][usingcolumnNum];
			}
		}
		return practicalDataElements;
	}


	private void CSVSave<T>(string aDatapath, T writtendata, Action<T> act) {//アセットフォルダにtest.csvというファイルを作成する。
		File.Delete(aDatapath);
		FileInfo fi;
		fi = new FileInfo(aDatapath);
		m_sw = fi.AppendText();
		act(writtendata);
		m_sw.Flush();
		m_sw.Close();
		Debug.Log("file was written");
	}

	public void MapCsvSave(int[,] writtendata) {//CSVSaveのジェネリック使用対応メソッド
		Action<int[,]> actaug = writeData;
		CSVSave(datapathmanager.getmapdatapath(), writtendata, actaug);
	}


	private void writeData(int[,] writtenData) {//オーバーライドメソッド
		for (int j = 0; j < writtenData.GetLength(1); j++) {
			for (int i = 0; i < writtenData.GetLength(0); i++) {
				m_sw.WriteLine("{0},{1},{2}", i.ToString(), j.ToString(), writtenData[i, j].ToString());
			}
		}
		Debug.Log("MapData was written");
	}


}



