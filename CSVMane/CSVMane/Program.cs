using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;


namespace CSVMane {
	class Program {


		static void Main(string[] args) {
			Moldsentence moldclass = new Moldsentence();

		}
	}
	public class Moldsentence {
		string loaddirectori = @"C:\Users\appirits_1020520\ownCloud\TIM\01_ドキュメント\_玉衛\string";
		string savedirectori = @"C:\Users\appirits_1020520\ownCloud\TIM\01_ドキュメント\_玉衛\string";
		int limitcount = 44;
		string[][] dataElements;
		StreamWriter m_sw;


		void makedataelement (){
			string textFile = loaddirectori;
			System.Text.Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
			string[] lines = System.IO.File.ReadAllLines(textFile, enc);
			string[] RowStrings = lines[0].Split('\n');

			dataElements = new string[lines.Length][];
			for (int i = 0; i < lines.Length; i++) {
				RowStrings = lines[i].Split('\n');
				dataElements[i] = new string[RowStrings.Length];
			}

			for (int j = 0; j < dataElements.Length; ++j) {
				RowStrings = lines[j].Split('\n');
				for (int i = 0; i < RowStrings.Length; ++i) {
					dataElements[j][i] = RowStrings[i];
				}
			}
		}
		void makelistfromArray(int[][] jagdata) {
			List <string> = new List<string>;
			for (int j = 0; j < jagdata.Length; ++j) {
				for (int i = 0; i < jagdata[j].Length; ++i) {
				}
			}
		}

		void makecorrectdata(string aDatapath, string[][] writtenData) {//アセットフォルダにtest.csvというファイルを作成する。作成するときはこのクラスを呼び出し、データを渡せばいい。
			File.Delete(aDatapath);
			FileInfo fi;
			fi = new FileInfo(aDatapath);
			m_sw = fi.AppendText();
			writeLogData(writtenData);
			m_sw.Flush();
			m_sw.Close();
		}

		public void writeLogData(string[][] writtenData) {//実際にログデータを書く部分、流れとしてはオブジェクトのデータを取得し、それを書いていくだけなので、int[,]がもらえればいいだけの話。
			for (int j = 0; j < writtenData.GetLength(1); j++) {
				foreach (var item in writtenData[j]) {
					m_sw.WriteLine(item);
				}
			}
		}
		
	}
}
