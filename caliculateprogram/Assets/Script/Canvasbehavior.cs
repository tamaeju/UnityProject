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
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class Canvasbehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {//タイトルのキャンバスを動かすためのコンポーネント、eventsystemを使用
	[SerializeField]
	Text titletext;
	[SerializeField]
	Text element1labeltext;
	[SerializeField]
	Text element1text;
	[SerializeField]
	Text element2labeltext;
	[SerializeField]
	Text element2text;
	//[SerializeField]
	//Image backgroundcolor;
	[SerializeField]
	Text messagetext;
	Vector3 variableVector3 = new Vector3();
	RectTransform rectform;
	RectTransform RTElement1;
	RectTransform RTElement2;
	RectTransform RTElementTitle;
	System.Random random;


	private Subject<string> clickedEvent = new Subject<string>();

	public IObservable<string> CanvasTouched {
		get { return clickedEvent; }
	}

	public void changeTitleText(string title) {
		titletext.text = title;
	}
	public void changeElement1Text(int score) {
		element1text.text = score.ToString();
	}
	public void changeElement1label(string label) {
		element1labeltext.text = label;
	}

	public void changeElement2Text(int score) {
		element2text.text = score.ToString();
	}
	public void changeElement2label(string label) {
		element2labeltext.text = label;
	}
	public void changeMessagetext(string text) {
		messagetext.text = text;
	}

	public void OnPointerDown(PointerEventData _data) {
		MoveThisObject();
		
	}

	private void MoveThisObject() {
		rectform = GetComponent<RectTransform>();
		rectform.DOMove(new Vector3(rectform.position.x, 2000f, rectform.position.z),
	3.0f
	).OnComplete(() => Destroy(this.gameObject));
	}


	private IEnumerator moveCoroutine(int movedistance) {//指定した距離を1秒かけて動くメソッド
		RectTransform background = this.gameObject.GetComponent<RectTransform>();
		Debug.Log("Calledtap");
		for (int i = 0; i < 20; i++) {
			variableVector3 = background.position;
			variableVector3.y = variableVector3.y + movedistance / 20;
			this.transform.position = variableVector3;
			yield return null;
		}
	}

	public void OnPointerEnter(PointerEventData eventData) {
	}

	public void OnPointerExit(PointerEventData ped) {//ポインターがオブジェクトから出た時
	}

	private void getRectTransform() {
		Debug.Log("clicked getRectTransform");
		RTElement1 = element1labeltext.GetComponent<RectTransform>();
		RTElement2 = element2labeltext.GetComponent<RectTransform>();
	}

	private void Start() {
	   getRectTransform();
		RTElement1.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 1f).SetLoops(-1, LoopType.Yoyo).Play();
		RTElement2.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 1f).SetLoops(-1, LoopType.Yoyo).Play();
		var messageElem = messagetext.text;
		messagetext.text = "";
		messagetext.DOText(messageElem, messageElem.Length * 0.3f);

		var messageEle1 = element1text.text;
		element1text.text = "";
		element1text.DOText(messageEle1, messageEle1.Length * 0.3f);

		var messageEle2 = element2text.text;
		element2text.text = "";
		element2text.DOText(messageEle2, messageEle2.Length * 0.3f);

	}

	void Update() {
	}

	void scaleChange() {
		random = new System.Random();
	}
}
