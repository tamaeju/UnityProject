using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {//タイトルのキャンバスを動かすためのコンポーネント、eventsystemを使用。
	Vector3 variableVector3 = new Vector3();
	RectTransform rectform;
	RectTransform outofrectform = new RectTransform();
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
	}
	private IEnumerator moveCoroutine(int movedistance) {//指定した距離を1秒かけて動くメソッド
		for (int i = 0; i < 20; i++) {
			rectform = GetComponent<RectTransform>();
			variableVector3 = rectform.position;
			variableVector3.y = variableVector3.y + movedistance / 20;
			rectform.position = variableVector3;
			yield return null;
		}
	}
}
