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

	public int currentSum;
	public int currentMoveCount;

	public int targetSum;
	public int targetMoveCount;

	[SerializeField]
	CSVManager csvgetter;
	//データからとってきて、値を更新するメソッド

	void Start() {
		csvgetter.getMapDataElements();
	}

	public void SetMoveCount(int newCount) {
		currentMoveCount = newCount;
	}

	public int GetMoveCount() {
		return currentMoveCount;
	}


	public void SetCurrentSum(int newCount) {
		currentSum = newCount;
	}
	public int GetCurrentSum() {
		return currentSum;
	}


	public void SetTargetSum(int newCount) {
		targetSum = newCount;
	}
	public int GettargetSum() {
		return targetSum;
	}


	public void SetTargetMoveCount(int newCount) {
		targetMoveCount = newCount;
	}
	public int GetTargetMoveCount() {
		return targetMoveCount;
	}
}
