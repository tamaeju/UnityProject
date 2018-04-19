using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class LevelSelectCanvasManager : MonoBehaviour {//レベルセレクト画面のセレクトボタンを生成するクラス
	[SerializeField]
	GameObject[] objectSetPoint;
	[SerializeField]
	GameObject buttonprefab;
	//ボタンをプレハブ

	public void instanceButtonPrefab(Action<int> buttonmethod) {
		GameObject setbutton;
		for (int i = 0; i < objectSetPoint.Length; i++) {
			setbutton = Instantiate(buttonprefab, objectSetPoint[i].transform.position, Quaternion.identity, objectSetPoint[i].transform) as GameObject;
			setbutton.GetComponent<SelectsceneButton>().changeMystageCount(i);
			setbutton.GetComponent<SelectsceneButton>().changeThisText(i);
			setbutton.GetComponent<SelectsceneButton>().OnClickedStageButton.Subscribe(buttonmethod);
			setbutton.GetComponent<Image>().color = changeButtonColor(i);
		}
		
	}
	private Color changeButtonColor(int buttonElementNum) {//iの値を9で割った時の数が0なら緑、1なら黄色、２なら赤に色修正
		int elementborder = 9;
		if (1 == (buttonElementNum / elementborder)) {
			return Color.yellow;
		}
		else if (2 == (buttonElementNum / elementborder)) {
			return Color.red;
		}
		else { return Color.white; }
	} 

}
