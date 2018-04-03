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
using UnityEngine.EventSystems;

public class Canvasbehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {//タイトルのキャンバスを動かすためのコンポーネント、eventsystemを使用
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
	Vector3 variableVector3 = new Vector3();
	RectTransform rectform;
	Action m_act;

	float heightRange = 1700;//画面のスクロール限界//スクロールが戻る際の挙動が不自然なので修正が必要と思われるが現時点では保留


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



	public void OnPointerEnter(PointerEventData eventData) {
		//
	}
	//ポインターがオブジェクトから出た時
	public void OnPointerExit(PointerEventData ped) {
		//
	}
	//クリックされた時
	public void OnPointerDown(PointerEventData _data) {

		StartCoroutine(moveCoroutine(1200));
		m_act();

	}

	public void changebackcolor(Color color) {
		backgroundcolor.color = color;
	}

	public void setMethod(Action canvasmethod) {
		m_act = canvasmethod;
	}


	private IEnumerator moveCoroutine(int movedistance) {//指定した距離を1秒かけて動くメソッド
		RectTransform background = backgroundcolor.gameObject.GetComponent<RectTransform>();
		Debug.Log("Calledtap");
		for (int i = 0; i < 20; i++) {
			variableVector3 = background.position;
			variableVector3.y = variableVector3.y + movedistance / 20;
			background.position = variableVector3;
			yield return null;
		}
	}
}
