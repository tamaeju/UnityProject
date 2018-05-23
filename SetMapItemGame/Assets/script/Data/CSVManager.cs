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
	int[][] stagedata; //何秒以内クリアか、必要捕食数のデータのデータ。（ゲームで実際に使用するのはstruct型の2次元配列）
	[SerializeField]
	Meditator meditator;
	[SerializeField]
	DataChangerFromJaG datachangerfromJag;

	private int[, ] getDataElement (string aDatapassANDname, int usingcolumnNum) { //データパスと使用するカラムを入力して使用する。
		int[][] dataElements;
		int[, ] practicalDataElements;
		dataElements = getJagDataElement (aDatapassANDname);
		practicalDataElements = parsePracticalDataElements (dataElements, usingcolumnNum);
		return practicalDataElements;
	}

	public int[, ] getMapDataElement () { //データパスと使用するカラムを入力して使用する。
		int usecolomnnum = Config.usecolomn_of_mapdata - 1;
		DataPathManager datapathmanager = meditator.getdatapathmanager ();
		string mapdatapass = datapathmanager.getmapdatapath ();
		return getDataElement (mapdatapass, usecolomnnum);
	}

	private int[][] getJagDataElement (string datapassANDname) { //ジャグデータをもらってから、それを2次元配列に入れる事が重要。その場合はint[][]からs
		int[][] dataElements;
		TextAsset bindata = Resources.Load (datapassANDname) as TextAsset; //指定したパスからテキストアセット型のデータを取得
		Debug.LogFormat ("bindata, datapassANDnameは{0}、{1}", bindata, datapassANDname); //デバッグ用
		string stringBindata = bindata.text; //
		string[] lines = stringBindata.Split ('\n'); //\nで1行毎のデータに変換する。
		Debug.LogFormat ("lines.Lengthは、{0}", lines.Length);
		string[] RowStrings = lines[0].Split (','); //要素数を出すために、lines[0]を,で配列にわけて格納する。(このrowstrings自体は使用しない)
		dataElements = new int[lines.Length - 1][]; //linesの要素数分データエレメントを作成
		for (int i = 0; i < dataElements.Length; i++) {
			dataElements[i] = new int[RowStrings.Length]; //RowStringsの要素数分データエレメントを作成
		}

		for (int j = 0; j < dataElements.Length; ++j) {
			RowStrings = lines[j].Split (','); //j番目のrowstringsを作成
			for (int i = 0; i < dataElements[i].Length; ++i) {
				dataElements[j][i] = Int32.Parse (RowStrings[i]);
			}
		}
		return dataElements;
	}

	public clearconditiondata[] getCCDataElement () {
		DataPathManager datapathmanager = meditator.getdatapathmanager ();
		clearconditiondata[] instancedData = datachangerfromJag.parsejagtodobleClearconditiondatas (getJagDataElement (datapathmanager.getconditiondatapath ()));
		return instancedData;
	}

	public dragitemdata[][] getitemDataElement () {
		DataPathManager datapathmanager = meditator.getdatapathmanager ();
		dragitemdata[][] instancedData = datachangerfromJag.parsejagtodobledragitemdatadatas (getJagDataElement (datapathmanager.getitemdatapath ()));;
		return instancedData;
	}

	private int[, ] parsePracticalDataElements (int[][] oldData, int usingcolumnNum) { //ジャグ配列からグリッド座標毎に1要素となるアイテムに対応した2次元配列への変換メソッド
		int[, ] practicalDataElements = new int[Config.maxGridNum, Config.maxGridNum];
		for (int j = 0; j < practicalDataElements.GetLength (1); j++) {
			for (int i = 0; i < practicalDataElements.GetLength (0); i++) {
				practicalDataElements[i, j] = oldData[practicalDataElements.GetLength (0) * j + i][usingcolumnNum];
			}
		}
		return practicalDataElements;
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

	public void MapCsvSave (int[, ] writtendata) { //CSVSaveのジェネリック使用対応メソッド
		DataPathManager datapathmanager = meditator.getdatapathmanager ();
		Action<int[, ]> actaug = writeData;
		CSVSave (datapathmanager.getmapdatapath (), writtendata, actaug);
	}

	public void itemCsvSave (dragitemdata[][] writtendata) { //CSVSaveのジェネリック使用対応メソッド
		DataPathManager datapathmanager = meditator.getdatapathmanager ();
		Action<dragitemdata[][]> actaug = writeData;
		CSVSave (datapathmanager.getitemdatapath (), writtendata, actaug);
	}

	public void cleardataCsvSave (clearconditiondata[] writtendata) { //CSVSaveのジェネリック使用対応メソッド
		DataPathManager datapathmanager = meditator.getdatapathmanager ();
		Action<clearconditiondata[]> actaug = writeData;
		CSVSave (datapathmanager.getconditiondatapath (), writtendata, actaug);
	}

	private void writeData (int[, ] writtenData) { //オーバーライドメソッド
		for (int j = 0; j < writtenData.GetLength (1); j++) {
			for (int i = 0; i < writtenData.GetLength (0); i++) {
				m_sw.WriteLine ("{0},{1},{2}", i.ToString (), j.ToString (), writtenData[i, j].ToString ());
			}
		}
		Debug.Log ("MapData was written");
	}

	private void writeData (dragitemdata[][] writtenData) { //オーバーライドメソッド
		for (int j = 0; j < writtenData.Length; j++) {
			for (int i = 0; i < writtenData[0].Length; i++) {
				m_sw.WriteLine ("{0},{1},{2},{3}", j, i, writtenData[j][i].itemkind, writtenData[j][i].itemcount);
			}
		}
		Debug.Log ("itemdata was written");
	}

	private void writeData (clearconditiondata[] writtenData) { //オーバーライドメソッド
		for (int i = 0; i < writtenData.Length; i++) {
			m_sw.WriteLine ("{0},{1},{2}", i, writtenData[i].timelimit, writtenData[i].RequiredDeffenceCount);
		}
		Debug.Log ("conditiondata was written");
	}

}