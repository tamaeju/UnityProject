using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class DataChangerFromJaG : MonoBehaviour {//csvから読み込んだデータを構造体のデータ等に変換するクラス。
	int timelimitElementNum = 1;
	int requiredkillcountElementNum = 2;
	int itemkindElementNum = 2;
	int itemcountElementNum = 3;

	public int[,]parsejagtodoubledata(int[][] jagdata,int ElementNum) {//要素番号を入れる点に注意。
		int[,] getdata = new int[jagdata.Length, jagdata[0].Length];
		for (int j = 0; j < getdata.GetLength(1); j++) {
			for (int i = 0; i < getdata.GetLength(0); i++) {
				getdata[j / jagdata[0].Length, j % jagdata[0].Length] = jagdata[j][ElementNum];
			}
		}
		return getdata;
	}

	public clearconditiondata[] parsejagtodobleClearconditiondatas(int[][] jagdata) {//jagデータから構造体に変換する処理

		clearconditiondata[] getdata = new clearconditiondata[jagdata.Length];
		for (int j = 0; j < jagdata.Length; j++) {
				getdata[j].timelimit = jagdata[j][timelimitElementNum];
				getdata[j].RequiredKillCount = jagdata[j][requiredkillcountElementNum];
		}
		return getdata;
	}

	public dragitemdata[,] parsejagtodobledragitemdatadatas(int[][] jagdata) {
		int xlength = jagdata[0].Length;
		dragitemdata[,] getdata = new dragitemdata[jagdata.Length / xlength, jagdata[0].Length];
		for (int j = 0; j < jagdata.Length; j++) {
			getdata[j / xlength, j % xlength].itemkind = jagdata[j][itemkindElementNum];
			getdata[j / xlength, j % xlength].itemcount = jagdata[j][itemcountElementNum];
				
		}
		return getdata;
	}

}
