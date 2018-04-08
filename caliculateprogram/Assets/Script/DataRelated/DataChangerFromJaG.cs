using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class DataChangerFromJaG : MonoBehaviour
{//csvから読み込んだデータを構造体のデータ等に変換するクラス。

	int massCountColoumnNum = 2;
	int massKindColoumnNum = 3;




	public MassStruct[,] ParseUsableaMapdatas(int[][] jagdata)
	{
		int xlength = jagdata[0].Length;
		MassStruct[,] getdata = new MassStruct[jagdata.Length / xlength, jagdata[0].Length];
		for (int j = 0; j < jagdata.Length; j++)
		{
			getdata[j / xlength, j % xlength].massnumber = jagdata[j][massCountColoumnNum];
			getdata[j / xlength, j % xlength].masskind = jagdata[j][massKindColoumnNum];

		}
		return getdata;
	}

	private int[,] parsejagtodoubledata(int[][] jagdata, int ElementNum)
	{//要素番号を入れる点に注意。
		int[,] getdata = new int[jagdata.Length, jagdata[0].Length];
		for (int j = 0; j < getdata.GetLength(1); j++)
		{
			for (int i = 0; i < getdata.GetLength(0); i++)
			{
				getdata[j / jagdata[0].Length, j % jagdata[0].Length] = jagdata[j][ElementNum];
			}
		}
		return getdata;
	}

}
