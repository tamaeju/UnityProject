using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;



public class CurrentStageData : MonoBehaviour {

	public long currentSum;//イベント発火のためにパブリックにしている、メソッドをラッパーすることで、アクセッサをprivateにする事も可能なのだがそれはそれでコードが汚くもなるのでいったん保留中
	public long currentMoveCount;//同上

	public long targetSum;
	public long targetMoveCount;

	[SerializeField]
	DataStorage datastorager;
	[SerializeField]
	CSVManager csvmanager;//一旦データストレージクラスが完成するまでは代替させるために存在
	ClearConditionStruct[] clearConditionData;
	//データからとってきて、値を更新するメソッド


	private void Start() {
		currentSum = 2;
	}

	public　void GetClearConditionData() {
		clearConditionData = datastorager.GetClearConditionElements();
		Debug.LogFormat("{0}",datastorager.GetClearConditionElements());
		int stageNum = csvmanager.getStageNum();//一旦データストレージクラスが完成するまでは代替させる。
		Debug.LogFormat("{0}", csvmanager.getStageNum());
		Debug.LogFormat("clearConditionData is {0} ,clearConditionData[stageNum].clearcount) is{1}", clearConditionData, clearConditionData[stageNum].clearcount);
		targetMoveCount = clearConditionData[stageNum].clearcount;
		targetSum = clearConditionData[stageNum].clearnumber;
	}

	public void SetMoveCount(int newCount) {
		currentMoveCount = newCount;
	}

	public long GetMoveCount() {
		return currentMoveCount;
	}


	public void SetCurrentSum(int newCount) {
		currentSum = newCount;
	}
	public long GetCurrentSum() {
		return currentSum;
	}


	public void SetTargetSum(int newCount) {
		targetSum = newCount;
	}
	public long GettargetSum() {
		return targetSum;
	}


	public void SetTargetMoveCount(int newCount) {
		targetMoveCount = newCount;
	}
	public long GetTargetMoveCount() {
		return targetMoveCount;
	}

	public bool canClear() {
		return targetMoveCount > currentMoveCount && targetSum == currentSum;
	}
}
