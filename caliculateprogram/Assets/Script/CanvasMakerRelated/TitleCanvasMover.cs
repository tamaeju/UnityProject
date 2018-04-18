using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;
using DG.Tweening;



public class TitleCanvasMover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {//タイトルのキャンバスを動かすためのコンポーネント、eventsystemを使用

	private void MoveThisObject() {
		RectTransform rectform;
		rectform = this.gameObject.GetComponentInParent<RectTransform>();
		rectform.DOMove(new Vector3(rectform.position.x, 2000f, rectform.position.z),
	3.0f
	).OnComplete(() => Destroy(this.gameObject));
	}

	public void OnPointerEnter(PointerEventData eventData) {
	}

	public void OnPointerExit(PointerEventData ped) {//ポインターがオブジェクトから出た時
	}
	public void OnPointerDown(PointerEventData _data) {
		MoveThisObject();
	}
}