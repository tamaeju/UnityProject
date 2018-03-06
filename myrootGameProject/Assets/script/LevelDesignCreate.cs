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
	public GameObject[] dataObjects = new GameObject[maxColumn * maxColumn];//インスペクタで代入するために
	int[,] LevelDesignData;
	public GameObject canvasObject;
	int loadColomn = 3;
	public GameObject levelButton;
	string filename;
	string datapath;


	void InstanciateandgetREFmethod() {//インスペクターで紐づけを行うためのメソッド。インスタンシエイトしたタイミングでapplyして紐づけする。
		var parent = canvasObject.transform;
		for (int j = 0; j < maxColumn; ++j) {
			for (int i = 0; i < maxColumn; ++i) {
				dataObjects[j * 10 + i] = Instantiate(levelButton, settingPosition(i, j, 0), Quaternion.identity, parent) as GameObject;
				if (i == 0 | i == 9 | j == 0 | j == 9) {
					dataObjects[j * 10 + i].GetComponent<LevelButton>().changeState(1);
				}
			}
		}
	}
	Vector3 settingPosition(int x, int y, int z) {//InstanciateandgetREFmethod()と合わせ技のため
		Vector3 returnPos = new Vector3((x+10f)* 28, (y+3f)* 28 ,z);
		return returnPos;
	}

	void Start()//レベルデザインデータのメモリ領域確保
	{
		LevelDesignData = new int[maxColumn, maxColumn];
		InstanciateandgetREFmethod();
		filename = "\\testData.csv";
		datapath = Application.dataPath + "\\data" + filename;
	}

	public void MakeDesignData()//レベルデザインデータを1次元配列から2次元配列へ置換
	{
		for (int j = 0; j < maxColumn; ++j)
		{
			for (int i = 0; i < maxColumn; ++i)
			{
				LevelDesignData[i, j] = dataObjects[j * 10 + i].GetComponent<LevelButton>().returnThisState();
			}
		}
		testShowDebug();
	}

	public void testShowDebug()//テストメソッド
	{
		for (int j = 0; j < maxColumn; ++j)
		{
			for (int i = 0; i < maxColumn; ++i)
			{
				Debug.Log(i + "," + j + "," + LevelDesignData[i, j]);
			}
		}
	}
	public void MakeCsvButton()//ボタンプッシュで実行
	{
		MakeDesignData();
		testShowDebug();
		makeCSV CsvCreater = new makeCSV();
		CsvCreater.logSave(datapath, LevelDesignData);
	}
	public void makeObjectFromCsvButton()//ボタンプッシュで実行
	{
		makeDataFromCSV DataMaker = new makeDataFromCSV();
		LevelDesignData = DataMaker.getDataElement(datapath, loadColomn - 1);
		testShowDebug();
		Debug.Log("csvの列番号{0}のデータをチェックします");
		Debug.Log(loadColomn);
		makeObject ObjectMaker = GetComponent<makeObject>();
		ObjectMaker.instanciateAllObject(LevelDesignData);
	}
	public void CanvasONOFFButton()//ボタンプッシュで実行
	{
		Transform trasnform = canvasObject.GetComponent<Transform>();

		foreach (Transform item in trasnform) {
			item.gameObject.SetActive(false);
		}
	}
	public void ChangeCSVNum(Dropdown dropdown) {
		filename = "\\testData"+ dropdown.value.ToString() + ".csv";
		datapath = Application.dataPath + "\\data" + filename;
		Debug.Log(filename);
		Debug.Log(datapath);
	}
}
