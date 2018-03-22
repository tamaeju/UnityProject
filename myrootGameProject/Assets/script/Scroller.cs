using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;


public class Scroller : MonoBehaviour {//自身のレクトトランスフォームを引数に合わせて変更する（これの場合はスクロールのバリューが1で1600動くよう設定）
	Vector3 variableVector3 = new Vector3();
	RectTransform rectform;
	public float doubleratio = 1600f;
	public void changeposition(float scrollvalue) {
		rectform = GetComponent<RectTransform>();
		variableVector3 = rectform.position;
		variableVector3.y= scrollvalue * doubleratio;
		rectform.position = variableVector3;
	}

	//0.1秒ごとに指定移動距離の1/10を動き、画面外にはけるようなメソッド。
	public void DisplayMoveOut() {
		StartCoroutine(moveCoroutine(1200));
	}
	private IEnumerator moveCoroutine(int movedistance) {//指定した距離を1秒かけて動くメソッド
		for (int i = 0; i < 20; i++) {
			rectform = GetComponent<RectTransform>();
			variableVector3 = rectform.position;
			variableVector3.y = variableVector3.y + movedistance/20;
			rectform.position = variableVector3;
			yield return null;
		}
	}
	public void move(Vector3 moveVector) {//指定した距離を1秒かけて動くメソッド
		rectform = GetComponent<RectTransform>();
		variableVector3 = rectform.position + moveVector;
		rectform.position = variableVector3;
	}
}
