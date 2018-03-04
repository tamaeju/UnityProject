using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelDesignCreate : MonoBehaviour
{//マップエディタのマネージャースクリプト。makeCSVクラスと、makeDataElementクラスを保有

	public static int maxColumn = 10;//他のクラスも参照する最大要素数
	public GameObject[] dataObject = new GameObject[maxColumn * maxColumn];//インスペクタで代入するために
	int[,] LevelDesignData;
	public GameObject canvasObject;

	void Start()
	{//レベルデザインデータのメモリ領域確保
		LevelDesignData = new int[maxColumn, maxColumn];
	}

	public void MakeDesignData()
	{//レベルデザインデータの2次元配列を作成
		for (int j = 0; j < maxColumn; ++j)
		{
			for (int i = 0; i < maxColumn; ++i)
			{
				LevelDesignData[i, j] = dataObject[j * 10 + i].GetComponent<LevelButton>().returnThisState();
			}
		}
		testShowDebug();

	}
	public void testShowDebug()
	{//デバッグメソッド
		for (int j = 0; j < maxColumn; ++j)
		{
			for (int i = 0; i < maxColumn; ++i)
			{
				Debug.Log(i + "," + j + "," + LevelDesignData[i, j]);
			}
		}
	}
	public void MakeCsvButton()
	{
		MakeDesignData();
		makeCSV CsvCreater = new makeCSV();
		string filename = "\\testData.csv";
		string datapath = Application.dataPath + "\\data" + filename;
		CsvCreater.logSave(datapath, dataObject);
	}
	public void makeObjectFromCsvButton()
	{
		int checkColomn = 3;
		string filename = "\\testData.csv";
		string datapath = Application.dataPath + "\\data" + filename;
		makeDataFromCSV DataMaker = new makeDataFromCSV();
		LevelDesignData = DataMaker.getDataElement(datapath, checkColomn - 1);
		testShowDebug();
		Debug.Log("csvの列番号{0}のデータをチェックします");
		Debug.Log(checkColomn);
		makeObject ObjectMaker = GetComponent<makeObject>();
		ObjectMaker.instanciateAllObject(LevelDesignData);
	}
	public void CanvasONOFFButton()
	{
		Transform trasnform = canvasObject.GetComponent<Transform>();

			foreach (Transform item in trasnform)
			{
				item.gameObject.SetActive(false);
			}


	}
}
