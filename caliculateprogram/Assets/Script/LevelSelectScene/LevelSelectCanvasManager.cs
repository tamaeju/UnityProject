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

	GameObject[] setbutton;

	//ボタンをプレハブ

	public void instanceButtonPrefab(Action<int> buttonmethod, DataStorage dataholder) {//ボタンを作成し、ゲームシーンクラスから受け取ったメソッドを渡している。
		setbutton = new GameObject[objectSetPoint.Length];
		for (int i = 0; i < objectSetPoint.Length; i++) {//このiのindexから+１したものを各ボタンオブジェクトが保有する。
			setbutton[i] = Instantiate(buttonprefab, objectSetPoint[i].transform.position, Quaternion.identity, objectSetPoint[i].transform) as GameObject;
			setbutton[i].GetComponent<SelectsceneButton>().changeMystageCount(i);
			setbutton[i].GetComponent<SelectsceneButton>().changeThisText(i);
			setbutton[i].GetComponent<SelectsceneButton>().OnClickedStageButton.Subscribe(buttonmethod);
			setbutton[i].GetComponent<Image>().color = changeButtonColor(i);
		}
		setClearedIcons(dataholder);
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
	private void setClearedIcon(int stageNum) {
		//stage[1]はボタンオブジェクト[i-1]に格納されており、stage[0]を遊ぶ術はない。stage[1]に対応したボタンオブジェクトはステージカウント1をもつボタンオブジェクト[0]
		if (stageNum  > 0) {
			setbutton[stageNum - 1].GetComponent<SelectsceneButton>().ActiveClearedIcon();
		}
	}
	private void setUnplayblelacon(int stageNum) {
		if (stageNum  > 0) {
			setbutton[stageNum - 1].GetComponent<SelectsceneButton>().ActiveUplaybleIcon();
		}
	}
	public void setClearedIcons(DataStorage dataholder) {//clear済みであればクリア済みアイコンとunplaybleiconをくっつける。
		for (int i = 0; i < objectSetPoint.Length; i++) {
			if (setbutton[i] = null) {
				Debug.Log("setbutton is null");
				return;
			}
			if (dataholder.isStageClear(i)) {
				setClearedIcon(i-1);
			}
		}
	}

	public void setUnplaybleIcons(DataStorage dataholder) {
		for (int i = 0; i < objectSetPoint.Length; i++) {
			if (setbutton[i] = null) {
				Debug.Log("setbutton is null");
				return;
			}
			if (!dataholder.isStageClear(i)) {
				for (int j = i; j < objectSetPoint.Length-1; i++) {
					setbutton[j+1].GetComponent<SelectsceneButton>().ActiveUplaybleIcon();
				}
			}
		}
	}

}
