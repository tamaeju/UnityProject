﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {
	private static int maxGridNum = Config.maxGridNum;//他のクラスも参照する最大要素数
	int[,] _leveldesigndata;
	bool[,] _canSetDatas;
	int[] needeatcount;
	int needeatcountcolomn = 2;//データの3列目なら3-1で要素番号2で正しい。
	int[] stagelefttimecount;
	int stagelefttimecountcolomn = 1;
	clearconditiondata[] conditionaldatas;
	dragitemdata[,] dragitemdatas;//dragitemdatas構造体の配列
	int stage;

	[SerializeField]
	Meditator meditator;




	void Start() {
		_canSetDatas = new bool[maxGridNum, maxGridNum];
		_leveldesigndata = new int[maxGridNum, maxGridNum];
		needeatcount = new int[Config.stageCount];
		stagelefttimecount = new int[Config.stageCount];
		dragitemdatas = new dragitemdata[Config.stageCount, Config.dragbuttonNum];
		conditionaldatas = new clearconditiondata[Config.stageCount];
	}

	public void changeMapData(Vector3 aSetpos, int objectkind) {//レベルデザインデータを更新するメソッド
		Vector2 setpos = new Vector2();
		setpos = parseVector3toVector2(aSetpos);
		_leveldesigndata[(int)setpos.x, (int)setpos.y] = objectkind;
		Debug.Log(setpos.x.ToString() + "," + setpos.y.ToString() + "ischanged to" + _leveldesigndata[(int)setpos.x, (int)setpos.y].ToString());
	}

	private Vector2 parseVector3toVector2(Vector3 aVector3) {//vector3をvector2に変換するメソッド（x→x,z→y）
		Vector2 indexpos = new Vector2();
		indexpos.x = aVector3.x;
		indexpos.y = aVector3.z;
		return indexpos;
	}

	public Vector2[] getOverRidePoint(Vector3 myposition, float blocklength) {//移動オブジェクトの存在する4点の座標を返すメソッド
		Vector2[] overridepoints = new Vector2[4];
		Vector2 cehckvector2 = parseVector3toVector2(myposition);
		int highx, lowx, highy, lowy;
		highx = (int)Math.Ceiling(cehckvector2.x / blocklength);
		highy = (int)Math.Ceiling(cehckvector2.y / blocklength);
		lowx = (int)Math.Floor(cehckvector2.x / blocklength);
		lowy = (int)Math.Floor(cehckvector2.y / blocklength);
		overridepoints[0].x = lowx; overridepoints[0].y = lowy;
		overridepoints[1].x = lowx; overridepoints[1].y = highy;
		overridepoints[2].x = highx; overridepoints[2].y = lowy;
		overridepoints[3].x = highx; overridepoints[3].y = highy;
		return overridepoints;
	}
	public void updateCansetDatas(int[,] existencepoints) {//ブロックが置いてあるポイントをfalseにして、それ以外をtrueにする
		for (int j = 0; j < existencepoints.GetLength(1); ++j) {
			for (int i = 0; i < existencepoints.GetLength(0); ++i) {
				if (existencepoints[i, j] == 1) {
					_canSetDatas[i, j] = false;
				}
				else { _canSetDatas[i, j] = true; }
			}
		}
	}
	public void updateCansetDatas(Vector3[] existencepoints) {//移動オブジェクトの存在するポイント4点をfalseにして、それ以外をtrueにする
		Vector2 cehckvector2;
		foreach (var item in existencepoints) {
			cehckvector2 = parseVector3toVector2(item);
			_canSetDatas[(int)cehckvector2.x, (int)cehckvector2.y] = false;
		}
	}
	public void updateCansetDatas(Vector3 existencepoints) {
		Vector2 cehckvector2 = parseVector3toVector2(existencepoints);
		_canSetDatas[(int)cehckvector2.x, (int)cehckvector2.y] = false;
	}
	public bool checkCanSet(Vector3 checkvector3) {//そこにおけるかを返すメソッド。
		Vector2 checkvector2 = parseVector3toVector2(checkvector3);
		if (checkinIndex(checkvector2))
			return _canSetDatas[(int)checkvector2.x, (int)checkvector2.y];
		else { return false; }
	}
	private bool checkinIndex(Vector3 checkvector3) {
		Vector2 checkvector2 = parseVector3toVector2(checkvector3);
		return checkvector2.x >= 0 && (int)checkvector2.x < maxGridNum && checkvector2.y >= 0 && (int)checkvector2.y < maxGridNum;
	}

	public void makeLevelDesignData() {
		GameObject[] UIobjects = meditator.getUImanager().getUIobjects();
		for (int j = 0; j < maxGridNum; ++j) {
			for (int i = 0; i < maxGridNum; ++i) {
				_leveldesigndata[i, j] = UIobjects[j * 10 + i].GetComponent<MapEditorbutton>().returnThisState();
			}
		}
	}
	public int[,] getLevelDesignData() {
		return _leveldesigndata;
	}

	public void getEatCountandLefttimeCount() {

		CSVManager csvmanager = meditator.getcsvmanager();
		DataPathManager datapathmanager = meditator.getdatapathmanager();

		needeatcount = csvmanager.get1dimentionalData(datapathmanager.getcsvdatapath(2), needeatcountcolomn);
		stagelefttimecount = csvmanager.get1dimentionalData(datapathmanager.getcsvdatapath(2), stagelefttimecountcolomn);
	}

	public void UpdateALLdragitemdata(dragitemdata[,] adragitemdatas) {
		dragitemdatas = adragitemdatas;
	}

	public void UpdateDragitemData(int UIbuttonNum, int itemkind, int leftcount) {
		CSVManager csvmanager = meditator.getcsvmanager();
		DataPathManager datapathmanager = meditator.getdatapathmanager();

		Debug.Log(String.Format("dragitemdatas, UIbuttonNum, stage   {0},{1},{2}   ", dragitemdatas, UIbuttonNum, stage));
		dragitemdatas[stage, UIbuttonNum].itemkind = itemkind;
		dragitemdatas[stage, UIbuttonNum].itemcount = leftcount;
	}
	public void changeStageNum(int Num) {
		stage = Num;
	}
	public int getDragitemkind(int UIbuttonNum) {
	return dragitemdatas[stage, UIbuttonNum].itemkind;
	}

	public int getDragitemleft(int UIbuttonNum)
	{
		return dragitemdatas[stage, UIbuttonNum].itemcount;
	}
	public dragitemdata[,] getItemData() {
		return dragitemdatas;
	}

}
