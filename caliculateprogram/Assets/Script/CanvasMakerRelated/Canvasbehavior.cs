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
	[SerializeField]
	Text m_messagetext;
	[SerializeField]
	Button m_backbutton;

	Vector3 variableVector3 = new Vector3();
	RectTransform rectform;
	RectTransform RTElementTitle;


	private Subject<int> clickedEvent = new Subject<int>();

	public IObservable<int> CanvasTouched {
		get { return clickedEvent; }
	}

	public void changeTitleText(string title) {
		titletext.text = title;
	}
	public void changeElement1Text(long score) {
		element1text.text = score.ToString();
	}
	public void changeElement1label(string label) {
		element1labeltext.text = label;
	}

	public void changeElement2Text(long score) {
		element2text.text = score.ToString();
	}
	public void changeElement2label(string label) {
		element2labeltext.text = label;
	}
	public void changeMessagetext(string text) {
		m_messagetext.text = text;
	}

	public void OnPointerDown(PointerEventData _data) {
		clickedEvent.OnNext(1);
		MoveThisObject();
		
	}

	private void MoveThisObject() {
		rectform = GetComponent<RectTransform>();
		rectform.DOMove(new Vector3(rectform.position.x, 2000f, rectform.position.z),
	3.0f
	).OnComplete(() => Destroy(this.gameObject));
	}




	public void OnPointerEnter(PointerEventData eventData) {
	}

	public void OnPointerExit(PointerEventData ped) {//ポインターがオブジェクトから出た時
	}

	private void getRectTransform() {
		Debug.Log("clicked getRectTransform");
		RTElementTitle = titletext.GetComponent<RectTransform>();
	}

	private void Start() {
	   getRectTransform();
		RTElementTitle.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 2f).SetLoops(-1, LoopType.Yoyo).Play();
		var messageElem = m_messagetext.text;
		m_messagetext.text = "";
		m_messagetext.DOText(messageElem, messageElem.Length * 0.2f);


		m_backbutton.onClick.AddListener(()=>Destroy(this.gameObject));
	}
	public void buttonActiveOn() {
		m_backbutton.gameObject.SetActive(true);
	} 

}
//private IEnumerator moveCoroutine(int movedistance) {//指定した距離を1秒かけて動くメソッド
//	RectTransform background = this.gameObject.GetComponent<RectTransform>();
//	Debug.Log("Calledtap");
//	for (int i = 0; i < 20; i++) {
//		variableVector3 = background.position;
//		variableVector3.y = variableVector3.y + movedistance / 20;
//		this.transform.position = variableVector3;
//		yield return null;
//	}
//}

