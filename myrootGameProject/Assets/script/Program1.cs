using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csvReaderClass {
	class Program {
		static void Main(string[] args) {
		}
	}
	class makeDataElement {

		int[][] dataElement(string datapassANDname) {
			string textFile = datapassANDname;//指定のパスを入れる
			System.Text.Encoding enc = System.Text.Encoding.GetEncoding("utf-8");

			string[] lines = System.IO.File.ReadAllLines(textFile, enc);//行ごとの配列として、テキストファイルの中身をすべて読み込む
			int[][] dataElements;//最終的に要素を格納するstringの2次元配列

			string[] RowStrings = lines[0].Split(',');
			int maxRowCount = RowStrings.Length;

			dataElements = new int[lines.Length][];
			for (int j = 0; j < lines.Length; j++) {
				dataElements[j] = new int[maxRowCount];
			}//データ要素の初期化および要素数決定

			//データ要素を2次元配列としてdataElementsに入れていく。
			for (int j = 0; j < dataElements.GetLength(1); ++j) {
				RowStrings = lines[j].Split(',');
				for (int i = 0; i < dataElements.GetLength(0); ++i) {
					dataElements[j][i] = Int32.Parse(RowStrings[i]);
				}
			}
			return dataElements;

			//lines[]はテキストの情報を一行ずつ入れた文の配列。RowStringsはlines[n]を,で分けた文の配列。dateElements[x,y]は、lines[x+y]のRowStrings[3]を格納した配列
			//これの使い方としては、csvReaderClassを生成し、dataElementメソッドを引数に使いたいパスを入れて実行すればよい。
		}
	}
}



