using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Canvasbehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler { //タイトルのキャンバスを動かすためのコンポーネント、eventsystemを使用
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
	Vector3 variableVector3 = new Vector3 ();
	RectTransform rectform;
	Action m_act;

	float heightRange = 1700; //画面のスクロール限界//スクロールが戻る際の挙動が不自然なので修正が必要と思われるが現時点では保留

	private Subject<string> actionsubject = new Subject<string> ();

	public IObservable<string> CanvasScrolled {
		get { return actionsubject; }
	}

	public void changeTitleText (string title) {
		titletext.text = title;
	}
	public void changeScoreText (int score) {
		scoretext.text = score.ToString ();
	}
	public void changeScorelabel (string label) {
		scorelabeltext.text = label;
	}

	public void changeTimeText (int score) {
		timetext.text = score.ToString ();
	}
	public void changeTimelabel (string label) {
		timelabeltext.text = label;
	}
	public void changeMessagetext (string tex) {
		messagetext.text = tex;
	}

	public void OnPointerEnter (PointerEventData eventData) {
		//
	}
	//ポインターがオブジェクトから出た時
	public void OnPointerExit (PointerEventData ped) {
		//
	}
	//クリックされた時
	public void OnPointerDown (PointerEventData _data) {

		MoveThisObject ();
		actionsubject.OnNext ("LevelSelectScene");
		m_act ();

	}

	public void changebackcolor (Color color) {
		backgroundcolor.color = color;
	}

	public void setMethod (Action canvasmethod) {
		m_act = canvasmethod;
	}

	private void MoveThisObject () {
		rectform = GetComponent<RectTransform> ();
		rectform.DOMove (new Vector3 (rectform.position.x, 2000f, rectform.position.z),
			1.5f
		).OnComplete (() => Destroy (this.gameObject));
	}
}