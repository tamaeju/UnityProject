using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class InnerData { //内部クラス。セーブ用にデータを代替保持する。
	public int[][][] i_allfieldmapdatas;
	public clearconditiondata[] i_clearConditionData;
	public dragitemdata[][] i_dragitemData;
	//ステージをクリアしたか否か
	public bool[] i_isStageCleared;
	public int[] i_MinClearMoveCount;

	public void UpdataMapandClearconditionData (int[][, ] mapdatas, clearconditiondata[] clearCondition, dragitemdata[][] dragitemData) { //クリアコンディションデータとマップデータを受け取り、インナーデータに格納する

		i_clearConditionData = clearCondition;
		i_allfieldmapdatas = Convert3DimentionAllayElement (mapdatas);
		i_dragitemData = dragitemData;
	}
	public void UpdateClearedData (bool[] stagecleared, int[] minClearMoveCount) {
		i_isStageCleared = stagecleared;
		i_MinClearMoveCount = minClearMoveCount;
	}
	public InnerData () {
		i_allfieldmapdatas = new int[Config.stageCount][][];
		for (int i = 0; i < Config.stageCount; i++) {
			i_allfieldmapdatas[i] = new int[Config.maxGridNum][];
			for (int j = 0; j < Config.maxGridNum; j++) {
				i_allfieldmapdatas[i][j] = new int[Config.maxGridNum];
			}
		}
		Debug.Log ("初期化完了！");
		i_isStageCleared = new bool[Config.stageCount];
		i_MinClearMoveCount = new int[Config.stageCount];
	}

	public int[][][] Convert3DimentionAllayElement (int[][, ] mapdatas) {

		for (int i = 0; i < Config.stageCount; i++) {
			for (int j = 0; j < Config.maxGridNum; j++) {
				for (int k = 0; k < Config.maxGridNum; k++) {
					i_allfieldmapdatas[i][j][k] = mapdatas[i][j, k];
				}
			}
		}
		return i_allfieldmapdatas;
	}

	public int[][, ] Convert1and2DimentionAllayElement (int[][][] mapdatas) {
		int[][, ] get12mapdatas = new int[Config.stageCount][, ];
		for (int i = 0; i < Config.stageCount; i++) {
			get12mapdatas[i] = new int[Config.maxGridNum, Config.maxGridNum];
		}

		for (int i = 0; i < Config.stageCount; i++) {
			for (int j = 0; j < Config.maxGridNum; j++) {
				for (int k = 0; k < Config.maxGridNum; k++) {
					get12mapdatas[i][j, k] = mapdatas[i][j][k];
				}
			}
		}
		return get12mapdatas;
	}

}