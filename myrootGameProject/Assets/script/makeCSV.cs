using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class makeCSV {//CSVデータ作成クラス。x,y,kindの列データを座標の数だけ作成
	StreamWriter m_sw;
	public void logSave(string aDatapath,int[,] writtenData){//アセットフォルダにtest.csvというファイルを作成する。作成するときはこのクラスを呼び出し、データを渡せばいい。
		File.Delete(aDatapath);
		FileInfo fi;
		fi = new FileInfo(aDatapath);
		m_sw = fi.AppendText();
		writeLogData(writtenData);
		m_sw.Flush();
		m_sw.Close();
		Debug.Log("wrote");
	}
	void writeLogData(int[,] writtenData){//実際にログデータを書く部分、流れとしてはオブジェクトのデータを取得し、それを書いていくだけなので、int[,]がもらえればいいだけの話。
		for (int j = 0; j < writtenData.GetLength(1); j++)
		{
			for (int i = 0; i < writtenData.GetLength(0); i++)
			{
				m_sw.WriteLine("{0},{1},{2}", i.ToString(), j.ToString(), writtenData[i,j].ToString());
			}
		}
	}
}

