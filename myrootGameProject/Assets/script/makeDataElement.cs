using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class makeDataElement {

	int[][] dataElements;//csvから作ったデータ
	int[,] practicalDataElements;//実際に使うデータ
	public int[][] getDataElement(string datapassANDname) {//指定のパス

		string textFile = datapassANDname;
		System.Text.Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
		string[] lines = System.IO.File.ReadAllLines(textFile, enc);
		string[] RowStrings = lines[0].Split(',');

		dataElements = new int[lines.Length][];
		for (int i = 0; i < lines.Length; i++) {
			dataElements[i] = new int[RowStrings.Length];
		}

		for (int j = 0; j < dataElements.GetLength(1); ++j) {
			RowStrings = lines[j].Split(',');
			for (int i = 0; i < dataElements.GetLength(0); ++i) {
				dataElements[j][i] = Int32.Parse(RowStrings[i]);
			}
		}
		return dataElements;
		//lines[]はテキストの情報を一行ずつ入れた文の配列。RowStringsはlines[n]を,で分けた文の配列。dateElements[x,y]は、lines[x+y]のRowStrings[3]を格納した配列
	}

	void parsePracticalDataElements(int[][] oldData,int usingcolumnNum) {
		practicalDataElements = new int[oldData.Length, oldData[0].Length];
		for (int j = 0; j < oldData.Length; j++) {
			for (int i = 0; i < oldData[0].Length; i++) {
				practicalDataElements[i, j] = oldData[practicalDataElements.GetLength(0) * j + i][usingcolumnNum];
			}
		}
	}


}



