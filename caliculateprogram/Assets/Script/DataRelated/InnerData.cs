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
	public MassStruct[][][] i_allfieldmapdatas;
	public ClearConditionStruct[] i_clearConditionData;
	//ステージをクリアしたか否か
	public bool[] i_isStageCleared;
	public int[] i_MinClearMoveCount;

	public void UpdataMapandClearconditionData (MassStruct[][, ] mapdatas, ClearConditionStruct[] clearCondition) {
		//m_isStageCleared, m_MinClearMoveCount
		i_clearConditionData = clearCondition;
		i_allfieldmapdatas = Convert3DimentionAllayElement (mapdatas);
	}
	public void UpdateClearedData (bool[] stagecleared, int[] minClearMoveCount) {
		i_isStageCleared = stagecleared;
		i_MinClearMoveCount = minClearMoveCount;
	}
	public InnerData () {
		i_allfieldmapdatas = new MassStruct[Config.stageCount][][];
		for (int i = 0; i < Config.stageCount; i++) {
			i_allfieldmapdatas[i] = new MassStruct[Config.maxGridNum][];
			for (int j = 0; j < Config.maxGridNum; j++) {
				i_allfieldmapdatas[i][j] = new MassStruct[Config.maxGridNum];
			}
		}
		Debug.Log ("初期化完了！");
		i_isStageCleared = new bool[Config.stageCount];
		i_MinClearMoveCount = new int[Config.stageCount];
	}

	public MassStruct[][][] Convert3DimentionAllayElement (MassStruct[][, ] mapdatas) {

		for (int i = 0; i < Config.stageCount; i++) {
			for (int j = 0; j < Config.maxGridNum; j++) {
				for (int k = 0; k < Config.maxGridNum; k++) {
					i_allfieldmapdatas[i][j][k] = mapdatas[i][j, k];
				}
			}
		}
		return i_allfieldmapdatas;
	}

	public MassStruct[][, ] Convert1and2DimentionAllayElement (MassStruct[][][] mapdatas) {
		MassStruct[][, ] get12mapdatas = new MassStruct[Config.stageCount][, ];
		for (int i = 0; i < Config.stageCount; i++) {
			get12mapdatas[i] = new MassStruct[Config.maxGridNum, Config.maxGridNum];
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