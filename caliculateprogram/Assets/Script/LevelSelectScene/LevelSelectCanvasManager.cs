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
	SelectsceneButton[] setbutton;

	//ボタンをプレハブ

	public void instanceButtonPrefab(Action<int> buttonmethod, DataStorage dataholder) {//ボタンを作成し、ゲームシーンクラスから受け取ったメソッドを渡している。
		setbutton = new SelectsceneButton[objectSetPoint.Length+1];//stageとsetbuttonの0番目のインデックスを使用しないため、objectSetPoint.Length+1個生成する
		for (int i = 1; i < setbutton.Length; i++) {//setbuttonとステージ数を一致させるために1からスタート
		GameObject instanceobj;
			instanceobj = Instantiate(buttonprefab, objectSetPoint[i-1].transform.position, Quaternion.identity, objectSetPoint[i-1].transform) as GameObject;
			setbutton[i] = instanceobj.GetComponent<SelectsceneButton>();
			setbutton[i].changeMystageCount(i);
			setbutton[i].changeThisText(i);
			setbutton[i].OnClickedStageButton.Subscribe(buttonmethod);
			setbutton[i].gameObject.GetComponent<Image>().color = changeButtonColor(i);
			//Debug.LogFormat("setbutton[i],iは{0}{1}",setbutton[i],i);
		}
		setClearedIcons(dataholder);
		setUnplaybleIcons(dataholder);
	}
	private Color changeButtonColor(int buttonElementNum) {//iの値を9で割った時の数が0なら緑、1なら黄色、２なら赤に色修正
		int elementborder = 9;
		if (1 == ((buttonElementNum-1) / elementborder)) {
			return Color.yellow;
		}
		else if (2 == ((buttonElementNum-1)/ elementborder)) {
			return Color.red;
		}
		else { return Color.white; }
	}


	public void setClearedIcons(DataStorage dataholder) {//clear済みであればクリア済みアイコンとunplaybleiconをくっつける。
		for (int i = 1; i < setbutton.Length; i++) {

			if (dataholder.isStageClear(i)) {//2ステージ目をクリアしていたらステージ目のボタンにクリアドアイコンを表示
						// if (setbutton[i] == null) {
						// 	Debug.Log("setbutton is null");
						// 	}
				setClearedIcon(i);
			}

		}
	}
	private void setClearedIcon(int stageNum) {
			setbutton[stageNum].ActiveClearedIcon();
	}

	public void setUnplaybleIcons(DataStorage dataholder) {
		for(int i = 1; i < setbutton.Length-1; i++) {
			// if (setbutton[i] == null) {
			// 	Debug.Log("setbutton is null");
			// }
			if (!(dataholder.isStageClear(i))) {//3ステージ目をクリアしていないかったら、4ステージ目以降のボタンオブジェクトのunplaybleIconをアクティブにする。
				// for (int j = i+1; j < objectSetPoint.Length-1; i++) {
				// 	Debug.LogFormat("setbutton,setbutton[j],j,iはそれぞれ{0},{1},{2},{3}",setbutton,setbutton[j],j,i);
					setUnplayblelacon(i+1);
				// }
			}
		}
	}
	private void setUnplayblelacon(int stageNum) {
		Debug.Log(stageNum);
		setbutton[stageNum].ActiveUnplaybleIcon();
		setbutton[stageNum].RemoveButtonEvent();
	}

}
