using System;
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
	dragitemdata[,] dragitemdatas;//dragitemdatas構造体の配列
	int stage;
	DataCheck datachecker;
	[SerializeField]
	Meditator meditator;
	MassDealer massdealer;


	void Start() {
		_canSetDatas = new bool[maxGridNum, maxGridNum];
		_leveldesigndata = new int[maxGridNum, maxGridNum];
		needeatcount = new int[Config.stageCount];
		stagelefttimecount = new int[Config.stageCount];
		dragitemdatas = new dragitemdata[Config.stageCount, Config.dragbuttonNum];

		datachecker = meditator.getdatachecker();
		massdealer = meditator.getmassdealer();
	}

	public void changeMapData(Vector3 aSetpos, int objectkind) {//レベルデザインデータを更新するメソッド
		Vector2 setpos = new Vector2();
		setpos = massdealer.parseVector3XYZtoVector2XZ(aSetpos);
		_leveldesigndata[(int)setpos.x, (int)setpos.y] = objectkind;
		Debug.Log(setpos.x.ToString() + "," + setpos.y.ToString() + "ischanged to" + _leveldesigndata[(int)setpos.x, (int)setpos.y].ToString());
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
			cehckvector2 = massdealer.parseVector3XYZtoVector2XZ(item);
			_canSetDatas[(int)cehckvector2.x, (int)cehckvector2.y] = false;
		}
	}
	public void updateCansetDatas(Vector3 existencepoints) {
		Vector2 cehckvector2 = massdealer.parseVector3XYZtoVector2XZ(existencepoints);
		_canSetDatas[(int)cehckvector2.x, (int)cehckvector2.y] = false;
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

	public void LoadALLdragitemdata() {//csvデータを読み込んできてアイテムデータを上書き。
		CSVManager csvmanager = meditator.getcsvmanager();
		DataPathManager datapathmanager = meditator.getdatapathmanager();
		DataChangerFromJaG jagchanger = meditator.getjagchanger();
		int[][] jagitemdata = csvmanager.getJagDataElement(datapathmanager.getcsvdatapath(1));
		dragitemdatas = jagchanger.parsejagtodobledragitemdatadatas(jagitemdata);
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
	public int getStageNum() {
		return stage;
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
	public bool[,] getcanSetDatas() {
		return _canSetDatas;
	}
}
