using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour {//キャンバスを扱うクラス。オブジェクトがテキストをいじった形で生成できるようにしているので、生成時にメソッドを読んで設定してあげる。unityaction型のイベントハンドラであればボタンをイベントとする事が可能
	[SerializeField]
	Text titletext;
	[SerializeField]
	Text scorelabeltext;
	[SerializeField]
	Text scoretext;
	[SerializeField]
	Text timelabeltext;
	[SerializeField]
	Text timetext;
	[SerializeField]
	Image backgroundcolor;

	[SerializeField]
	Text messagetext;
	[SerializeField]
	Button button;
	Text buttontext;

	Vector3 variableVector3 = new Vector3();
	RectTransform rectform;
	float heightRange = 1700;//画面のスクロール限界//スクロールが戻る際の挙動が不自然なので修正が必要と思われるが現時点では保留

	private void Start() {
		changeTitleText("");
		changeMessagetext("");
		changeScorelabel("");
		changeScoreText(0);
		changeTimelabel("");
		changeTimeText(0);
		setButtonscroll();
		buttontext = button.GetComponentInChildren<Text>();
	}

	public void changeTitleText(string title) {
		titletext.text = title;
	}
	public void changeScoreText(int score) {
		scoretext.text = score.ToString();
	}
	public void changeScorelabel(string label) {
		scorelabeltext.text = label;
	}

	public void changeTimeText(int score) {
		timetext.text = score.ToString();
	}
	public void changeTimelabel(string label) {
		timelabeltext.text = label;
	}


	public void changeMessagetext(string tex) {
		messagetext.text = tex;
	}
	private void setButtonMethod(UnityAction act) {
		button.onClick.AddListener(act);
	}

	public void changebuttontext(string tex) {
		buttontext.text = tex;
	}

	public void setButtonscroll() {
		setButtonMethod(DisplayMoveOut);
	}


	public void changebackcolor(Color color) {
		backgroundcolor.color = color;
	}
	private void DisplayMoveOut() {
		StartCoroutine(moveCoroutine(1200));
	}
	private IEnumerator moveCoroutine(int totalmovedistance) {//画面外にはける動きを作成するために、1Fごとに指定移動距離の1/20を動く。
		for (int i = 0; i < 20; i++) {
			rectform = GetComponent<RectTransform>();
			variableVector3 = rectform.position;
			variableVector3.y = variableVector3.y + totalmovedistance / 20;
			rectform.position = variableVector3;
			yield return null;
		}
	}
}
