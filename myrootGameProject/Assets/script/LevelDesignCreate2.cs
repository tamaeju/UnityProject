using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelDesignCreate2 : MonoBehaviour {

	public static int maxColumn = 10;//他のクラスも参照する最大要素数
	public GameObject buttonPrefab;
	public GameObject[,] dataObject;//インスペクタで代入するために
	int[,] LevelDesignData;

	void Start()//ゲームスタートのタイミングで、maxColumn*maxColumnの数の配列を生成し、画面上にinstanciateし、その参照を格納する。
	{
		LevelDesignData = new int[maxColumn, maxColumn];
		dataObject = new GameObject[maxColumn, maxColumn];
		for (int j = 0; j < maxColumn; ++j)
		{
			for (int i = 0; i < maxColumn; ++i)
			{
				//dataObject[i, j] = Instantiate(buttonPrefab, settingPosition(i, j), Quaternion.identity) as GameObject;
			}
		}
	}

	public void MakeDesignData()
	{//レベルデザインデータの2次元配列を作成
		for (int j = 0; j < maxColumn; ++j)
		{
			for (int i = 0; i < maxColumn; ++i)
			{
				LevelDesignData[i, j] = dataObject[i,j].GetComponent<LevelButton>().returnThisState();
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
				Debug.Log(LevelDesignData[i, j]);
			}
		}
	}
}