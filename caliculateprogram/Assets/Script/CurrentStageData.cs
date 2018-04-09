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
	  
	public int targetMoveCount;
	public int targetCaliculationQuantity;
	public int currentMoveCount;
	public int currentCaliculationQuantity;
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


	public void SetCurrentCaliculationQuantity(int newCount) {
		currentCaliculationQuantity = newCount;
	}
	public int GetCurrentCaliculationQuantity() {
		return currentCaliculationQuantity;
	}


	public void SetTargetCaliculationQuantity(int newCount) {
		targetCaliculationQuantity = newCount;
	}
	public int GettargetCaliculationQuantity() {
		return targetCaliculationQuantity;
	}


	public void SetTargetMoveCount(int newCount) {
		targetMoveCount = newCount;
	}
	public int GetTargetMoveCount() {
		return targetMoveCount;
	}
}
