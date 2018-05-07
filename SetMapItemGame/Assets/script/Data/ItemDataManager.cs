using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ItemDataManager : MonoBehaviour {
	dragitemdata[,] dragitemdatas;//dragitemdatas構造体の配列※ステージ数がインデックス

	[SerializeField]
	Meditator meditator;
	DataChangerFromJaG jagchanger;
	DataPathManager datapathmanager;
	CSVManager csvmanager;

	void Start() {//各データの初期化
		dragitemdatas = new dragitemdata[Config.stageCount, Config.dragbuttonNum];
		jagchanger = meditator.getjagchanger();
		csvmanager = meditator.getcsvmanager();
	}
	public void LoadALLdragitemdata() {//csvデータを読み込んできてアイテムデータを上書き。
		int[][] jagitemdata = csvmanager.getitemDataElement_needtoprocess();
		dragitemdatas = jagchanger.parsejagtodobledragitemdatadatas(jagitemdata);
	}
	public void UpdateDragitemData(int UIbuttonNum, int itemkind, int leftcount) {//dragitem更新用処理
		int stage = meditator.getmapdatamanager().getStageNum();
		Debug.Log(String.Format("dragitemdatas, UIbuttonNum, stage   {0},{1},{2}   ", dragitemdatas, UIbuttonNum, stage));
		dragitemdatas[stage, UIbuttonNum].itemkind = itemkind;
		dragitemdatas[stage, UIbuttonNum].itemcount = leftcount;
	}

	public int getDragitemkind(int UIbuttonNum) {
		int stage = meditator.getmapdatamanager().getStageNum();
		return dragitemdatas[stage, UIbuttonNum].itemkind;
	}

	public int getDragitemleft(int UIbuttonNum) {
		int stage = meditator.getmapdatamanager().getStageNum();
		return dragitemdatas[stage, UIbuttonNum].itemcount;
	}
	public dragitemdata[,] getItemData() {
		return dragitemdatas;
	}
}
