using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DataChangerFromJaG : MonoBehaviour { //csvから読み込んだデータを構造体のデータ等に変換するクラス。

	int massCountColoumnNum = Config.massCountColoumnNum;
	int massKindColoumnNum = Config.massKindColoumnNum;
	int clearcountColoumnNum = Config.clearMovecountColoumnNum;
	int clearnumberColoumnNum = Config.clearnumberColoumnNum;

	public MassStruct[, ] ParseUsableaMapdatas (int[][] jagdata) {
		MassStruct[, ] getdata = new MassStruct[Config.maxGridNum, Config.maxGridNum];
		for (int j = 0; j < Config.maxGridNum * Config.maxGridNum; j++) {
			//for (int j = 0; j < jagdata.Length; j++) {
			//Debug.Log (j);
			getdata[j / Config.maxGridNum, j % Config.maxGridNum].massnumber = jagdata[j][massCountColoumnNum];
			getdata[j / Config.maxGridNum, j % Config.maxGridNum].masskind = jagdata[j][massKindColoumnNum];

		}
		return getdata;
	}

	public ClearConditionStruct[] ParseUsableaClearCondition (int[][] jagdata) {

		ClearConditionStruct[] getdata = new ClearConditionStruct[jagdata.Length];
		for (int j = 0; j < jagdata.Length; j++) {
			try { //debug用trycatch
				getdata[j].clearcount = jagdata[j][clearcountColoumnNum];
				getdata[j].clearnumber = jagdata[j][clearnumberColoumnNum];
			} catch {
				Debug.LogFormat ("j, getdata.Length, jagdata.Length,jagdata[j][clearcountColoumnNum], jagdata[j][clearnumberColoumnNum]はそれぞれ{0},{1},{2},{3},{4}", j, getdata.Length, jagdata.Length, jagdata[j][clearcountColoumnNum], jagdata[j][clearnumberColoumnNum]);
			}
		}
		return getdata;
	}

	private int[, ] parsejagtodoubledata (int[][] jagdata, int ElementNum) { //要素番号を入れる点に注意。
		int[, ] getdata = new int[jagdata.Length, jagdata[0].Length];
		for (int j = 0; j < getdata.GetLength (1); j++) {
			for (int i = 0; i < getdata.GetLength (0); i++) {
				getdata[j / jagdata[0].Length, j % jagdata[0].Length] = jagdata[j][ElementNum];
			}
		}
		return getdata;
	}

}

//Debug.LogFormat("j, getdata.Length, jagdata.Lengthはそれぞれ{0},{1},{2}",  j, getdata.Length, jagdata.Length);

//int xlength = jagdata[0].Length;
//MassStruct[,] getdata = new MassStruct[jagdata.Length / xlength, jagdata[0].Length];
//		for (int j = 0; j<jagdata.Length; j++)
//		{
//			getdata[j / xlength, j % xlength].massnumber = jagdata[j][massCountColoumnNum];
//			getdata[j / xlength, j % xlength].masskind = jagdata[j][massKindColoumnNum];

//		}
//		return getdata;