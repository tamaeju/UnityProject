using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectCanvasManager : MonoBehaviour { //レベルセレクト画面のセレクトボタンを生成するクラス
	[SerializeField]
	GameObject[] objectSetPoint;
	[SerializeField]
	GameObject buttonprefab;
	SelectsceneButton[] setbutton;

	//ボタンをプレハブ
	public void Start () {
		instanceButtonPrefab ();
	}
	public void instanceButtonPrefab (Action<int> buttonmethod = null) { //ボタンを作成し、ゲームシーンクラスから受け取ったメソッドを渡している。
		setbutton = new SelectsceneButton[objectSetPoint.Length + 1]; //stageとsetbuttonの0番目のインデックスを使用しないため、objectSetPoint.Length+1個生成する
		for (int i = 1; i < setbutton.Length; i++) { //setbuttonとステージ数を一致させるために1からスタート
			//ステージを呼び出したいときは、buttonmethodにステージ呼び出しのメソッドを入れてやればよい。
			GameObject instanceobj;
			instanceobj = Instantiate (buttonprefab, objectSetPoint[i - 1].transform.position, Quaternion.identity, objectSetPoint[i - 1].transform) as GameObject;
			setbutton[i] = instanceobj.GetComponent<SelectsceneButton> ();
			setbutton[i].changeMystageCount (i);
			setbutton[i].changeThisText (i);
			setbutton[i].OnClickedStageButton.Subscribe (buttonmethod);
			setbutton[i].gameObject.GetComponent<Image> ().color = changeButtonColor (i);
		}
		// setClearedIcons(dataholder);
		// setUnplaybleIcons(dataholder);
	}
	private Color changeButtonColor (int buttonElementNum) { //iの値を9で割った時の数が0なら緑、1なら黄色、２なら赤に色修正
		int elementborder = 15;
		if (1 == ((buttonElementNum - 1) / elementborder)) {
			return Color.yellow;
		} else if (2 == ((buttonElementNum - 1) / elementborder)) {
			return Color.red;
		} else { return Color.white; }
	}

	private void setClearedIcon (int stageNum) {
		setbutton[stageNum].ActiveClearedIcon ();
	}

	private void setUnplayblelacon (int stageNum) {
		Debug.Log (stageNum);
		setbutton[stageNum].ActiveUnplaybleIcon ();
		setbutton[stageNum].RemoveButtonEvent ();
	}

	// public void setUnplaybleIcons(DataStorage dataholder) {
	// 	for(int i = 1; i < setbutton.Length-1; i++) {

	// 		if (!(dataholder.isStageClear(i))) {//3ステージ目をクリアしていないかったら、4ステージ目以降のボタンオブジェクトのunplaybleIconをアクティブにする。

	// 				setUnplayblelacon(i+1);

	// 		}
	// 	}
	// }
	// public void setClearedIcons(DataStorage dataholder) {//clear済みであればクリア済みアイコンとunplaybleiconをくっつける。
	// 	for (int i = 1; i < setbutton.Length; i++) {

	// 		if (dataholder.isStageClear(i)) {//2ステージ目をクリアしていたらステージ目のボタンにクリアドアイコンを表示
	// 			setClearedIcon(i);
	// 		}

	// 	}
	// }
}