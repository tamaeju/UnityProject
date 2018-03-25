using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;


public class Scroller : MonoBehaviour {//スクロールするオブジェクトのコンポーネント
	Vector3 variableVector3 = new Vector3();
	RectTransform rectform;
	float heightRange = 1700;//画面のスクロール限界//スクロールが戻る際の挙動が不自然なので修正が必要と思われるが現時点では保留

	public float doubleratio = 1600f;
	public void changeposition(float scrollvalue) {
		rectform = GetComponent<RectTransform>();
		variableVector3 = rectform.position;
		variableVector3.y= scrollvalue * doubleratio;
		rectform.position = variableVector3;
	}

	
	public void DisplayMoveOut() {
		StartCoroutine(moveCoroutine(1200));
	}
	private IEnumerator moveCoroutine(int totalmovedistance) {//画面外にはける動きを作成するために、1Fごとに指定移動距離の1/20を動く。
		for (int i = 0; i < 20; i++) {
			rectform = GetComponent<RectTransform>();
			variableVector3 = rectform.position;
			variableVector3.y = variableVector3.y + totalmovedistance/20;
			rectform.position = variableVector3;
			yield return null;
		}
	}
	public void move(Vector3 moveVector) {//マウスの動きからポジションを移動させる処理。（heightRangeの絶対値以上のスクロールを禁止している）
		rectform = GetComponent<RectTransform>();
		variableVector3 = rectform.position;
		variableVector3.y = rectform.position.y + moveVector.y;
		if (rectform.position.y < heightRange && rectform.position.y > -1 * heightRange)
		{
			rectform.position = variableVector3;
		}
		else
		{
			variableVector3.y = variableVector3.y / Math.Abs(variableVector3.y) * (heightRange - 100);
			rectform.position = variableVector3;
		}
	}
}
