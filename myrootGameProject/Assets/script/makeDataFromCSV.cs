using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class makeDataFromCSV{//CSVからデータを作成するクラス

	int[][] dataElements;//csvから作ったデータ
	int[,] practicalDataElements;//dataElementsからパースして使うデータ

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

		//ジャグ配列の初期化
		dataElements = new int[lines.Length][];
		for (int i = 0; i < lines.Length; i++) {
			dataElements[i] = new int[RowStrings.Length];
		}

		for (int j = 0; j < dataElements.Length; ++j) {
			RowStrings = lines[j].Split(',');
			for (int i = 0; i < dataElements[0].Length; ++i) {
				dataElements[j][i] = Int32.Parse(RowStrings[i]);
			}
		}//lines[]はテキストの情報を一行ずつ入れた文の配列。RowStringsはlines[n]を,で分けた文の配列。dateElements[x,y]は、lines[x+y]のRowStrings[3]を格納した配列
		//DebugJagCSVData();
	}

	void parsePracticalDataElements(int[][] oldData,int usingcolumnNum) {//ジャグ配列から2次元配列への変換メソッド
		practicalDataElements = new int[LevelDesignCreate.maxColumn, LevelDesignCreate.maxColumn];
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

}



